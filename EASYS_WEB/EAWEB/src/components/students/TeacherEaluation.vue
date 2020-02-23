<template>
    <div>
      <el-container>
        <el-header class="container-header-pro">
          <el-form :inline="true" :model="form" label-width="80px">
            <div>
              <el-form-item label="课程编号">
                <el-input v-model="form.curriculum_cd" class="input-txt" clearable></el-input>
              </el-form-item>
              <el-form-item label="课程名">
                <el-input v-model="form.curriculum_nm" class="input-txt" clearable></el-input>
              </el-form-item>
            </div>
            <div>
              <el-form-item label="学期">
                <el-select v-model="form.term" placeholder="请选择学期" class="input-txt" clearable>
                  <el-option key="0" value="0" label="第一学期上"></el-option>
                  <el-option key="1" value="1" label="第一学期下"></el-option>
                  <el-option key="2" value="2" label="第二学期上"></el-option>
                  <el-option key="3" value="3" label="第二学期下"></el-option>
                </el-select>
              </el-form-item>
              <el-form-item label="教师">
                <el-select v-model="form.teacher_cd" placeholder="请选择教师" class="input-txt" clearable>
                  <el-option
                    v-for="item in teacher_list"
                    :key="item.user_cd"
                    :label="item.user_nm"
                    :value="item.user_cd">
                  </el-option>
                </el-select>
              </el-form-item>
              <el-button type="primary" @click="search()">查询</el-button>
            </div>
          </el-form>
        </el-header>
        <el-main class="container-main">
          <el-table :data="tableData" v-loading="loading" height="740px">
            <el-table-column type="index" width="50"></el-table-column>
            <el-table-column prop="curriculum_cd" label="课程编号"></el-table-column>
            <el-table-column prop="curriculum_nm" label="课程名"></el-table-column>
            <el-table-column prop="term" label="学期">
              <template slot-scope="scope">
                {{getTerm(scope.row.term)}}
              </template>
            </el-table-column>
            <el-table-column prop="user_nm" label="教师"></el-table-column>
            <el-table-column prop="evaluation_teacher" label="评价">
              <template slot-scope="scope">
                <el-rate v-model="scope.row.evaluation_teacher" show-score text-color="#ff9900"></el-rate>
              </template>
            </el-table-column>
            <el-table-column fixed="right" label="操作" width="100">
              <template slot-scope="scope">
                <el-popconfirm title="确定提交吗？" @onConfirm="save(scope.row)">
                  <el-button type="text" slot="reference">评价</el-button>
                </el-popconfirm>
              </template>
            </el-table-column>
          </el-table>
        </el-main>
      </el-container>
    </div>
</template>

<script>
    export default {
      name: "TeacherEaluation",
      data(){
        return{
          //表格数据
          tableData:[],
          //表格数据加载标记
          loading:false,
          //检索表单数据
          form:{curriculum_cd:'',curriculum_nm:'',term:'', teacher_cd:'', student_cd:this.$Storage.get('user_cd')},
          //教师下拉数据
          teacher_list:[]
        }
      },
      mounted(){
        this.loading = true
        let p1 =  this.$Api.post('TeacherEaluation/getTeacherList', {student_cd:this.$Storage.get('user_cd')})
        let p2 = this.$Api.post('TeacherEaluation/search', this.form)
        Promise.all([p1,p2]).then((resp)=>{
          if(resp[0])this.teacher_list = resp[0]
          if(resp[1])this.tableData = resp[1]
          this.loading = false
        }).catch(()=>{
          this.$message.error('网络异常,请检查后再试')
        })
      },
      methods:{
        //检索
        search(){
          this.getData()
        },
        //保存
        save(row){
          let data = {curriculum_cd:row.curriculum_cd, student_cd:this.$Storage.get('user_cd'), evaluation_teacher:row.evaluation_teacher}
          this.$Api.post('TeacherEaluation/save', data).then((resp)=>{
            if(resp == '0'){
              this.getData()
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
          this.$Api.post('TeacherEaluation/search', this.form).then((resp)=>{
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
