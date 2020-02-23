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
            <el-table-column fixed="right" label="操作" width="150">
              <template slot-scope="scope">
                <el-button type="text" slot="reference" @click="edit(scope.row)">学生管理</el-button>
              </template>
            </el-table-column>
          </el-table>
        </el-main>
      </el-container>
      <el-dialog title="学生管理" :visible.sync="digVisible" style="text-align: left">
        <el-form :inline="true" :model="digForm" label-width="80px">
          <el-form-item label="课程编号:">{{digForm.curriculum_cd}}</el-form-item>
          <el-form-item label="课程名:">{{digForm.curriculum_nm}}</el-form-item>
          <el-form-item label="学期:">{{digForm.term}}</el-form-item>
        </el-form>
        <el-form :inline="true" :model="digHdrForm" ref="form" :rules="rules" label-width="80px">
          <el-form-item label="学生" prop="user_cd">
            <el-select v-model="digHdrForm.user_cd" placeholder="请选择学生" class="input-txt" clearable>
              <el-option
                v-for="item in user_list"
                :key="item.user_cd"
                :label="item.user_nm"
                :value="item.user_cd"
                :disabled="item.disabled">
              </el-option>
            </el-select>
          </el-form-item>
          <el-button type="success" @click="add()">新增</el-button>
        </el-form>
        <el-table :data="digData" v-loading="digLoading" height="450px">
          <el-table-column type="index" width="50"></el-table-column>
          <el-table-column prop="user_cd" label="学生编号"></el-table-column>
          <el-table-column prop="user_nm" label="学生名"></el-table-column>
          <el-table-column fixed="right" label="操作" width="100">
            <template slot-scope="scope">
              <el-popconfirm title="确定删除吗？" @onConfirm="del(scope.row)">
                <el-button type="danger" size="mini" slot="reference">删除</el-button>
              </el-popconfirm>
            </template>
          </el-table-column>
        </el-table>
        <div slot="footer" class="dialog-footer">
          <el-button @click="digVisible = false">取 消</el-button>
        </div>
      </el-dialog>
    </div>
</template>

<script>
    export default {
        name: "StudentManagement",
      data(){
          return{
            //学生下拉数据
            user_list:[],
            //检索表单数据
            form:{curriculum_cd:'', curriculum_nm:'', term:'', user_cd:this.$Storage.get('user_cd')},
            //表格数据
            tableData:[],
            //表格数据加载标记
            loading:false,
            //子画面显示标记
            digVisible:false,
            //子画面头部表单数据
            digForm:{curriculum_cd:'', curriculum_nm:'', term:''},
            //子画面头部学生选择表单数据
            digHdrForm:{user_cd:''},
            //子画面头部学生选择表单验证规则
            rules: {
              user_cd: [
                { required: true, message: '请选择学生', trigger: 'blur' }
              ]
            },
            //子画面表格数据
            digData:[],
            //子画面数据加载标记
            digLoading:false
          }
      },
      //画面渲染时加载
      mounted(){
        this.loading = true
        this.$Api.post('StudentMangement/search', this.form).then((resp)=>{
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
          this.digLoading = true
          this.digForm = {curriculum_cd:row.curriculum_cd, curriculum_nm:row.curriculum_nm, term:this.getTerm(row.term)}
          let p1 = this.$Api.post('StudentMangement/getUserList', this.digForm)
          let p2 = this.$Api.post('StudentMangement/getCurriculumData', this.digForm)
          Promise.all([p1, p2]).then((resp)=>{
            this.user_list = resp[0]
            this.digData = resp[1]
            this.digHdrForm.user_cd=''
            if(this.$refs['form'])this.$refs['form'].resetFields();
            this.digLoading=false
          }).catch(()=>{
            this.$message.error('网络异常,请检查后再试')
          })
          this.digVisible = true
        },
        //新增
        add(){
          this.$refs['form'].validate((valid) => {
            if (valid) {
              //获取指定学生编号的下拉数据
              let data = {}
              this.user_list.map((item, index)=>{
                if(item.user_cd == this.digHdrForm.user_cd){
                  data = {curriculum_cd: this.digForm.curriculum_cd, user_cd:item.user_cd,user_nm:item.user_nm, index:index}
                }
              })
              this.$Api.post('StudentMangement/save',data).then((resp)=>{
                if(resp == '0'){
                  //下拉列表对应数据去除,防止重复追加
                  this.user_list.splice(data.index, 1)
                  this.digHdrForm.user_cd = ''
                  //表格数据追加
                  this.digData.push(data)
                  this.$message.success('保存成功')
                }else if(resp == '-1'){
                  this.$message.warning('保存失败')
                }
              })
            }
          })
        },
        //删除
        del(row){
          let data = {curriculum_cd: this.digForm.curriculum_cd, user_cd:row.user_cd}
          this.$Api.post('StudentMangement/delete',data).then((resp)=>{
            if(resp == '0'){
              //手动刷新表格数据
              let index = this.digData.indexOf(row)
              this.digData.splice(index, 1)
              this.user_list.push(row)
              this.$message.success('删除成功')
            }else if(resp=='-2'){
              //手动刷新表格数据
              let index = this.digData.indexOf(row)
              this.digData.splice(index, 1)
              this.user_list.push(row)
              this.$message.warning('该课程中未找到该学生')
            }else if(resp == '-1'){
              this.$message.warning('删除失败')
            }
          })
        },
        //检索实行
        getData(){
          this.loading = true
          this.$Api.post('StudentMangement/search', this.form).then((resp)=>{
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

