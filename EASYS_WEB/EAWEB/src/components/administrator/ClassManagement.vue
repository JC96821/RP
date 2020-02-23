<template>
    <div>
      <el-container>
        <el-header class="container-header-pro">
          <el-form :inline="true" :model="form" label-width="100px">
            <div>
              <el-form-item label="班级编号">
                <el-input v-model="form.class_cd" class="input-txt" clearable></el-input>
              </el-form-item>
              <el-form-item label="班级名">
                <el-input v-model="form.class_nm" class="input-txt" clearable></el-input>
              </el-form-item>
              <el-form-item label="班级导师">
                <el-select v-model="form.user_cd" placeholder="请选择" class="input-txt" clearable>
                  <el-option
                    v-for="item in user_list"
                    :key="item.user_cd"
                    :label="item.user_nm"
                    :value="item.user_cd">
                  </el-option>
                </el-select>
              </el-form-item>
            </div>
            <div>
              <el-form-item label="专业">
                <el-select v-model="form.profession_cd" placeholder="请选择" class="input-txt" clearable>
                  <el-option
                    v-for="item in profession_list"
                    :key="item.profession_cd"
                    :label="item.profession_nm"
                    :value="item.profession_cd">
                  </el-option>
                </el-select>
              </el-form-item>
              <el-form-item label="学院">
                <el-select v-model="form.college_cd" placeholder="请选择学院" class="input-txt" clearable>
                  <el-option
                    v-for="item in college_list"
                    :key="item.college_cd"
                    :label="item.college_nm"
                    :value="item.college_cd">
                  </el-option>
                </el-select>
              </el-form-item>
              <el-form-item label=" ">
                <el-button type="primary" @click="search()">查询</el-button>
                <el-button type="success" @click="add()">新设班级</el-button>
              </el-form-item>
            </div>
          </el-form>
        </el-header>
        <el-main class="container-main">
          <el-table :data="tableData" v-loading="loading" height="700px">
            <el-table-column type="index" width="50"></el-table-column>
            <el-table-column prop="class_cd" label="班级编号"></el-table-column>
            <el-table-column prop="class_nm" label="班级名"></el-table-column>
            <el-table-column prop="profession_nm" label="专业"></el-table-column>
            <el-table-column prop="college_nm" label="学院"></el-table-column>
            <el-table-column prop="user_nm" label="班级导师"></el-table-column>
            <el-table-column fixed="right" label="操作" width="178">
              <template slot-scope="scope">
                <el-button type="text" style="margin-right: 10px" @click="edit(scope.row)">变更班级导师</el-button>
                <el-popconfirm title="确定删除吗？" @onConfirm="del(scope.row)">
                  <el-button type="danger" size="mini" slot="reference">删除</el-button>
                </el-popconfirm>
              </template>
            </el-table-column>
          </el-table>
        </el-main>
      </el-container>

      <el-dialog :title="digTitle" :visible.sync="digVisible" style="text-align: left" width="480px">
        <el-form :model="digForm" ref="form" :rules="rules"  label-width="80px" style="padding: 0 20px 0 20px">
          <el-form-item label="班级编号" prop="class_cd">
            <el-input v-model="digForm.class_cd" class="input-txt" clearable :disabled="digDisabled"></el-input>
          </el-form-item>
          <el-form-item label="班级名" prop="class_nm">
            <el-input v-model="digForm.class_nm" class="input-txt" clearable :disabled="digDisabled"></el-input>
          </el-form-item>
          <el-form-item label="专业" prop="profession_cd">
            <el-select v-model="digForm.profession_cd" placeholder="请选择专业" class="input-txt" :disabled="digDisabled" clearable>
              <el-option
                v-for="item in profession_list"
                :key="item.profession_cd"
                :label="item.profession_nm"
                :value="item.profession_cd">
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="班级导师" prop="user_cd">
            <el-select v-model="digForm.user_cd" placeholder="请选择班级导师" class="input-txt" clearable>
              <el-option
                v-for="item in user_list"
                :key="item.user_cd"
                :label="item.user_nm"
                :value="item.user_cd">
              </el-option>
            </el-select>
          </el-form-item>
        </el-form>
        <div slot="footer" class="dialog-footer">
          <el-button @click="digVisible = false">取 消</el-button>
          <el-button type="success" @click="save()">保存</el-button>
        </div>
      </el-dialog>

    </div>
</template>

<script>
    export default {
      name: "ClassManagement",
      data(){
          return{
            //检索表单数据
            form:{class_cd:'', class_nm:'', profession_cd:'', college_cd:'', user_cd:''},
            //学院下拉数据
            college_list:[],
            //专业下拉数据
            profession_list:[],
            //班级导师下拉数据
            user_list:[],
            //表格数据加载标记
            loading:false,
            //表格数据
            tableData:[],
            //子画面显示标记
            digVisible:false,
            //子画面标题
            digTitle:'',
            //字画卖弄'班级编号'、'班级名'、'专业'活性化标记
            digDisabled:false,
            //子画面表单数据
            digForm:{class_cd:'', class_nm:'', profession_cd:'', user_cd:'', exec_flg:''},
            //子画面表单验证规则
            rules: {
              class_cd: [
                { required: true, message: '请输入班级编号', trigger: 'blur' }
              ],
              class_nm: [
                { required: true, message: '请输入班级名', trigger: 'blur' }
              ],
              profession_cd: [
                { required: true, message: '请选择专业', trigger: 'blur' }
              ],
              user_cd: [
                { required: true, message: '请选择班级导师', trigger: 'blur' }
              ]
            }
          }
      },
      //画面渲染时加载
      mounted(){
        this.loading = true
        let p1 = this.$Api.post('Class/getTeacherList')
        let p2 = this.$Api.post('Class/getProfessionList')
        let p3 = this.$Api.post('Class/getCollegeList')
        let p4 = this.$Api.post('Class/search', this.form)
        Promise.all([p1, p2, p3, p4]).then((resp)=>{
          if(resp[0])this.user_list = resp[0]
          if(resp[1])this.profession_list = resp[1]
          if(resp[2])this.college_list = resp[2]
          if(resp[3])this.tableData = resp[3]
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
        //新增
        add(){
          this.digVisible = true
          this.digTitle = '新设班级'
          this.digDisabled = false
          this.digForm.exec_flg = '0'
          this.digForm = {class_cd:'', class_nm:'', profession_cd:'', user_cd:'', exec_flg:''}
          if(this.$refs['form'])this.$refs['form'].resetFields()
        },
        //编辑
        edit(row){
          this.digVisible = true
          this.digTitle = '变更班级导师'
          this.digDisabled = true
          this.digForm.exec_flg = '1'
          this.digForm = {class_cd:row.class_cd, class_nm:row.class_nm, profession_cd:row.profession_cd, user_cd:row.user_cd}
          if(this.$refs['form'])this.$refs['form'].resetFields()
        },
        //保存
        save(){
          this.$refs['form'].validate((valid) => {
            if (valid) {
              this.doSave()
            }
          });
        },
        //删除
        del(row){
          this.$Api.post('Class/delete', row).then((resp)=>{
            if(resp == '0'){
              let index = this.tableData.indexOf(row)
              this.tableData.splice(index, 1)
              this.$message.success('删除成功')
            }else if(resp == '-1'){
              this.$message.warning('删除失败')
            }
          }).catch(()=>{
            this.$message.error('网络异常,请检查后再试')
          })
        },
        //检索实行
        getData(){
          this.loading = true
          this.$Api.post('Class/search', this.form).then((resp)=>{
            this.tableData = resp
            this.loading = false
            if(!resp || resp.length == 0){
              this.$message.warning('没有检索到数据')
            }
          }).catch(()=>{
            this.$message.error('网络异常,请检查后再试')
          })
        },
        //保存实行
        doSave(){
          this.$Api.post('Class/save', this.digForm).then((resp)=>{
            if(resp == '0'){
              this.$message.success('保存成功')
              this.getData()
              this.digVisible = false
            }else if(resp == '-1'){
              this.$message.warning('保存失败')
            }else if(resp == '-2'){
              this.$message.warning('该班级已存在，请重新输入班级编号')
            }
          }).catch(()=>{
            this.$message.error('网络异常,请检查后再试')
          })
        }
      }
    }
</script>
