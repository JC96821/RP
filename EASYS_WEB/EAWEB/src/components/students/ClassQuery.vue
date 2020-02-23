<template>
    <div>
      <el-container>
        <el-header class="container-header">
          <el-form :inline="true" :model="form" label-width="100px">
            <el-form-item label="课程编号">
              <el-input v-model="form.curriculum_cd" clearable></el-input>
            </el-form-item>
            <el-form-item label="课程名">
              <el-input v-model="form.curriculum_nm" clearable></el-input>
            </el-form-item>
            <el-form-item label="学期">
              <el-select v-model="form.term" placeholder="请选择学期" clearable>
                <el-option key="0" value="0" label="第一学期上"></el-option>
                <el-option key="1" value="1" label="第一学期下"></el-option>
                <el-option key="2" value="2" label="第二学期上"></el-option>
                <el-option key="3" value="3" label="第二学期下"></el-option>
              </el-select>
            </el-form-item>
            <el-button type="primary" @click="search()">查询</el-button>
          </el-form>
        </el-header>
        <el-main class="container-main">
          <el-table :data="tableData" v-loading="loading" height="750px">
            <el-table-column type="index" width="50"></el-table-column>
            <el-table-column prop="curriculum_cd" label="课程编号"></el-table-column>
            <el-table-column prop="curriculum_nm" label="课程名"></el-table-column>
            <el-table-column prop="term" label="学期">
              <template slot-scope="scope">
                {{getTerm(scope.row.term)}}
              </template>
            </el-table-column>
            <el-table-column prop="class_time" label="上课时间">
              <template slot-scope="scope">
                <el-popover
                  placement="top-start"
                  width="200"
                  trigger="hover"
                  :content="getDays(scope.row, 1)">
                  <span slot="reference">{{getDays(scope.row, 0)}}</span>
                </el-popover>
              </template>
            </el-table-column>
          </el-table>
        </el-main>
      </el-container>
    </div>
</template>

<script>
    export default {
        name: "ClassQuery",
        data(){
          return{
            //检索表单数据
            form:{curriculum_cd:'', curriculum_nm:'', term:'', user_cd:this.$Storage.get('user_cd')},
            //表格数据
            tableData:[],
            //表格数据加载标记
            loading:false
          }
        },
      //画面渲染时加载
        mounted(){
          this.loading = true
          this.$Api.post('ClassQuery/search', this.form).then((resp)=>{
            this.tableData = resp
            this.loading = false
          }).catch(()=>{
            this.$message.error('网络异常,请检查后再试')
          })
        },
        methods:{
          //查询
          search(){
            this.getData()
          },
          //检索实行
          getData(){
            this.loading = true
            this.$Api.post('ClassQuery/search', this.form).then((resp)=>{
              this.tableData = resp
              this.loading = false
              if(!resp || resp.length == 0){
                this.$message.warning('没有检索到数据')
              }
            }).catch(()=>{
              this.$message.error('网络异常,请检查后再试')
            })
          },
          //获取上课时间
          getDays(row, showflg){
            if(!row.monday && !row.tuesday && !row.wednesday && !row.thursday && !row.friday && !row.saturday && !row.sunday){
              return '暂无安排'
            }
            let str = '每 '
            if(row.monday)str += '周一' + row.monday + ' '
            if(row.tuesday)str += '周二' + row.tuesday + ' '
            if(row.wednesday)str += '周三' + row.wednesday + ' '
            if(row.thursday)str += '周四' + row.thursday + ' '
            if(row.friday)str += '周五' + row.friday + ' '
            if(row.saturday)str += '周六' + row.saturday + ' '
            if(row.sunday)str += '周日' + row.sunday + ' '
            str += '上课'
            if(str.length > 15 && !showflg)return str.substring(0, 15) + '...'
            return str
          },
          //获取学期
          getTerm(term){
            if(term == '0'){
              return '第一学期上'
            }else if (term == '1'){
              return '第一学期下'
            }else if (term == '2'){
              return '第二学期上'
            }else if(term == '3'){
              return '第二学期下'
            }
          }
        }
    }
</script>
