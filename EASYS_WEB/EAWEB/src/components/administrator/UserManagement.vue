<template>
    <div>
      <el-container>
        <el-header class="container-header-pro">
          <el-form :inline="true" :model="form" label-width="100px">
            <div>
              <el-form-item label="用户编号">
                <el-input v-model="form.user_cd" class="input-txt" clearable></el-input>
              </el-form-item>
              <el-form-item label="用户姓名">
                <el-input v-model="form.user_nm" class="input-txt" clearable></el-input>
              </el-form-item>
            </div>
            <div>
              <el-form-item label="用户区分">
                <el-select v-model="form.distinction" placeholder="请选择" class="input-txt" clearable>
                  <el-option key="0" value="0" label="学生"></el-option>
                  <el-option key="1" value="1" label="教师"></el-option>
                  <el-option key="9" value="9" label="管理员"></el-option>
                </el-select>
              </el-form-item>
              <el-form-item label="在校/退校">
                <el-select v-model="form.del_flg" placeholder="请选择" class="input-txt" clearable>
                  <el-option key="0" value="0" label="在校"></el-option>
                  <el-option key="1" value="1" label="退校"></el-option>
                </el-select>
              </el-form-item>
              <el-form-item>
                <el-button type="primary" @click="search()">查询</el-button>
              </el-form-item>
            </div>
          </el-form>
        </el-header>
        <el-main class="container-main">
          <el-table :data="tableData" v-loading="loading" height="700px">
            <el-table-column type="index" width="50"></el-table-column>
            <el-table-column prop="user_cd" label="用户编号"></el-table-column>
            <el-table-column prop="user_nm" label="用户姓名"></el-table-column>
            <el-table-column prop="distinction" label="用户区分"></el-table-column>
            <el-table-column prop="class_nm" label="所属班级"></el-table-column>
            <el-table-column prop="del_flg" label="在校/退校"></el-table-column>
            <el-table-column fixed="right" label="操作" width="210">
              <template slot-scope="scope">
                <el-button
                  type="text"
                  slot="reference"
                  @click="view(scope.row)"
                  style="margin-right: 10px">
                  查看
                </el-button>
                <el-button
                  type="text"
                  slot="reference"
                  @click="edit(scope.row)"
                  style="margin-right: 10px"
                  :disabled="scope.row.distinction == '管理员' || scope.row.del_flg ==  '退校'?true:false">
                  班级变更
                </el-button>
                <el-popconfirm title="确定删除该用户吗？" @onConfirm="del(scope.row)" :disabled="scope.row.distinction == '管理员' || scope.row.del_flg ==  '退校'?true:false">
                  <el-button
                    :type="scope.row.distinction == '管理员' || scope.row.del_flg ==  '退校'?'text':'danger'"
                    :disabled="scope.row.distinction == '管理员' || scope.row.del_flg ==  '退校'?true:false"
                    size="mini"
                    slot="reference">
                    退校
                  </el-button>
                </el-popconfirm>
              </template>
            </el-table-column>
          </el-table>
        </el-main>
      </el-container>
      <!--明细查看子画面-->
      <el-dialog :title="viewTitle" :visible.sync="viewVisible" style="text-align: left" width="400px">
        <el-form label-width="120px">
          <el-form-item label="用户编号：">{{viewForm.user_cd}}</el-form-item>
          <el-form-item label="用户姓名：">{{viewForm.user_nm}}</el-form-item>
          <el-form-item label="用户区分：">{{viewForm.distinction}}</el-form-item>
          <el-form-item label="所属班级：">{{viewForm.class_nm}}</el-form-item>
          <el-form-item label="联系电话：">{{viewForm.tell_phone}}</el-form-item>
          <el-form-item label="电子邮箱：">{{viewForm.email}}</el-form-item>
          <el-form-item label="在校/退校：">{{viewForm.del_flg}}</el-form-item>
        </el-form>
        <div slot="footer" class="dialog-footer">
          <el-button @click="viewVisible = false">取 消</el-button>
        </div>
      </el-dialog>
      <!--班级变更子画面-->
      <el-dialog title="班级变更" :visible.sync="digVisible" style="text-align: left" width="400px">
        <el-form :model="digForm" ref="form" :rules="rules" label-width="80px" style="padding: 10px 40px 0 40px">
          <div>
            <el-form-item label="用户编号:">{{digForm.user_cd}}</el-form-item>
            <el-form-item label="用户姓名:">{{digForm.user_nm}}</el-form-item>
            <el-form-item label="用户区分:">{{digForm.distinction}}</el-form-item>
          </div>
          <div v-if="dtl_show">
            <el-form-item label="班级" prop="class_cd">
              <el-select v-model="digForm.class_cd" placeholder="请选择班级" clearable>
                <el-option
                  v-for="item in class_list"
                  :key="item.class_cd"
                  :label="item.class_nm"
                  :value="item.class_cd">
                </el-option>
              </el-select>
            </el-form-item>
          </div>
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
      name: "UserManagement",
      data(){
          return{
            //检索表单数据
            form:{user_cd:'', user_nm:'', distinction:'', del_flg:''},
            //表格加载标记
            loading: false,
            //表格数据
            tableData:[],
            //子画面弹出标记
            digVisible:false,
            //班级下拉列表数据
            class_list:[],
            //子画面表单数据
            digForm:{user_cd:'', user_nm:'', distinction:'', class_cd:'', profession_cd:''},
            //子画面表单验证规则
            rules: {
              class_cd: [
                { required: true, message: '请选择班级', trigger: 'blur' }
              ]
            },
            //子画面明细项目是否加载
            dtl_show: true,
            //查看明细子画面显示标记
            viewVisible:false,
            //查看明细子画面标题
            viewTitle:'',
            //查看明细子画面表单数据
            viewForm:{user_cd:'', user_nm:'', distinction:'', class_nm:'', tell_phone:'', email:'', del_flg:''}
          }
      },
      //画面渲染时加载
      mounted(){
        this.loading = true
        let p1 = this.$Api.post('User/getClassList')
        let p2 = this.$Api.post('User/search', this.form)
        Promise.all([p1, p2]).then((resp)=>{
          if(resp[0])this.class_list = resp[0]
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
        //查看
        view(row){
          this.viewTitle = row.user_nm
          this.viewForm = {user_cd:row.user_cd, user_nm:row.user_nm, distinction:row.distinction, class_nm:row.class_nm, tell_phone:row.tell_phone, email:row.email, del_flg:row.del_flg}
          this.viewVisible=true
        },
        //修改
        edit(row){
          this.digVisible = true
          this.digForm = {user_cd:row.user_cd, user_nm:row.user_nm, distinction:row.distinction, class_cd:row.class_cd}
          if(this.$refs['form'])this.$refs['form'].resetFields();
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
          this.$Api.post('User/delete', row).then((resp)=>{
            if(resp == '0'){
              this.$message.success('删除成功')
              let index = this.tableData.indexOf(row)
              this.tableData[index].del_flg = '退校'
              this.digVisible = false
            }else if(resp == '-2'){
              this.$message.warning('该用户已删除，请不要重复操作')
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
          this.$Api.post('User/search', this.form).then((resp)=>{
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
          this.$Api.post('User/save', this.digForm).then((resp)=>{
            if(resp == '0'){
              this.$message.success('保存成功')
              this.getData()
              this.digVisible = false
            }else if(resp == '-3'){
              this.$message.warning('该用户已登记到该班级，请不要重复登录')
            }else if(resp == '-2'){
              this.$message.warning('该用户不存在，请刷新后再试')
            }else if(resp == '-1'){
              this.$message.warning('保存失败')
            }
          }).catch(()=>{
            this.$message.error('网络异常,请检查后再试')
          })
        }
      }
    }
</script>

