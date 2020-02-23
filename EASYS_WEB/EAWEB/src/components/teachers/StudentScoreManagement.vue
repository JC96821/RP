<template>
    <div>
      <el-container>
        <el-header class="container-header">
          <el-form :inline="true" :model="form" label-width="80px">
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
              <el-form-item>
                <el-button type="primary" @click="search()">查询</el-button>
              </el-form-item>
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
            <el-table-column fixed="right" label="操作" width="120">
              <template slot-scope="scope">
                <el-button type="text" slot="reference" @click="edit(scope.row)">学生分数评价</el-button>
              </template>
            </el-table-column>
          </el-table>
        </el-main>
      </el-container>

      <el-dialog title="学生分数评价" :visible.sync="digVisible"  style="text-align: left">
        <el-container>
          <el-header>
            <el-form :inline="true" label-width="80px">
              <el-form-item label="课程编号">{{digForm.curriculum_cd}}</el-form-item>
              <el-form-item label="课程名">{{digForm.curriculum_nm}}</el-form-item>
              <el-form-item label="学期">{{getTerm(digForm.term)}}</el-form-item>
            </el-form>
          </el-header>
          <el-main>
            <el-table :data="digData" v-loading="digLoading" height="600px">
              <el-table-column type="index" width="50"></el-table-column>
              <el-table-column prop="user_cd" label="学生编号"></el-table-column>
              <el-table-column prop="user_nm" label="学生姓名"></el-table-column>
              <el-table-column prop="score" label="分数">
                <template slot-scope="scope">
                  <el-input-number v-model="scope.row.score" :min="0" :max="100"></el-input-number>
                </template>
              </el-table-column>
              <el-table-column fixed="right" label="操作" width="90">
                <template slot-scope="scope">
                  <el-popconfirm title="确定保存吗？" @onConfirm="save(scope.row)">
                    <el-button type="text" size="mini" slot="reference">确定</el-button>
                  </el-popconfirm>
                </template>
              </el-table-column>
            </el-table>
          </el-main>
        </el-container>
      </el-dialog>
    </div>
</template>

<script>
    export default {
        name: "StudentScoreManagement",
      data(){
          return{
            //检索表单数据
            form:{curriculum_cd:'', curriculum_nm:'', term:'', user_cd:this.$Storage.get('user_cd')},
            //表格数据
            tableData:[],
            //表格数据加载标记
            loading:false,
            //子画面弹出标记
            digVisible:false,
            //子画面数据
            digForm:{curriculum_cd:'', curriculum_nm:'', term:''},
            //子画面表格数据
            digData:[],
            //子画面表格加载数据
            digLoading:false
          }
      },
      //画面渲染时加载
      mounted(){
        this.loading = true
        this.$Api.post('StudentScore/search', this.form).then((resp)=>{
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
          //编辑
          edit(row){
            this.digVisible = true
            this.digForm = {curriculum_cd:row.curriculum_cd, curriculum_nm:row.curriculum_nm, term:row.term}
            this.digLoading = true
            this.$Api.post('StudentScore/getCurriculumData', this.digForm).then((resp)=>{
              this.digData = resp
              this.digLoading = false
              if(!resp || resp.length == 0){
                this.$message.warning('该课程未登记过学生')
              }
            }).catch(()=>{
              this.$message.error('网络异常,请检查后再试')
            })
          },
          //保存
          save(row){
            let data = {curriculum_cd:this.digForm.curriculum_cd, user_cd:row.user_cd, score:row.score}
            this.$Api.post('StudentScore/save', data).then((resp)=>{
              if(resp == '0'){
                this.$message.success('保存成功')
              }else if(resp == '-1'){
                this.$message.warning('保存失败')
              }
            }).catch(()=>{
              this.$message.error('网络异常,请检查后再试')
            })
          },
          //检索实行
          getData(){
            this.loading = true
            this.$Api.post('StudentScore/search', this.form).then((resp)=>{
              this.tableData = resp
              this.loading = false
              if(!resp || resp.length == 0){
                this.$message.warning('没有检索到数据')
              }
            }).catch(()=>{
              this.$message.error('网络异常,请检查后再试')
            })
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

