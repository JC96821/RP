<template>
    <div>
      <el-menu
        default-active="2"
        class="el-menu-vertical-demo"
        style="height: 944px;"
        background-color="#545c64"
        text-color="#fff"
        active-text-color="#66b1ff"
        :default-active="activeName"
        router>
        <el-header height="300px">
            <!--登陆后头部图片-->
            <el-image :src="imageUrl" @click="showMpi()" class="menu-image" v-show="imageUrl != ''"></el-image>
            <!--主页Menu头部图片-->
            <el-image src="https://i.loli.net/2020/02/01/mXBy8oJe2RDpP4c.jpg" class="menu-image" v-show="imageUrl == ''"></el-image>
        </el-header>
        <el-menu-item index="/" v-if="home_load">
          <i class="el-icon-menu"></i>
          <span slot="title">主页</span>
        </el-menu-item>
        <div class="el-menu-item menu-logout" v-if="home_load" @click="showLogin()">
          <i class="el-icon-setting"></i>
          <span slot="title">登录</span>
        </div>
        <!-- 学生-->
        <el-menu-item index="/ClassQuery" v-if="students_load" style="padding-left: 75px;text-align: left">
          <i class="el-icon-search"></i>
          <span slot="title">课表查询</span>
        </el-menu-item>
        <el-menu-item index="/ChooseCoursesAndTeachers" v-if="students_load" style="padding-left: 75px;text-align: left">
          <i class="el-icon-tickets"></i>
          <span slot="title">选择培训课程及教师</span>
        </el-menu-item>
        <el-menu-item index="/TeacherEaluation" v-if="students_load" style="padding-left: 75px;text-align: left">
          <i class="el-icon-edit-outline"></i>
          <span slot="title">教师质量评价</span>
        </el-menu-item>
        <el-menu-item index="/ScoreInquiry" v-if="students_load" style="padding-left: 75px;text-align: left">
          <i class="el-icon-search"></i>
          <span slot="title">成绩查询</span>
        </el-menu-item>
        <div class="el-menu-item menu-logout" v-if="students_load" @click="logout" style="padding-left: 75px;text-align: left">
          <i class="el-icon-house"></i>
          <span slot="title">注销</span>
        </div>
        <!-- 教师-->
        <el-menu-item index="/StudentManagement" v-if="teacher_load" style="padding-left: 75px;text-align: left">
          <i class="el-icon-user"></i>
          <span slot="title">学生管理</span>
        </el-menu-item>
        <el-menu-item index="/StudentScoreManagement" v-if="teacher_load" style="padding-left: 75px;text-align: left">
          <i class="el-icon-notebook-1"></i>
          <span slot="title">学生成绩管理</span>
        </el-menu-item>
        <el-menu-item index="/StudentEvaluation" v-if="teacher_load" style="padding-left: 75px;text-align: left">
          <i class="el-icon-edit-outline"></i>
          <span slot="title">学生评价</span>
        </el-menu-item>
        <div class="el-menu-item menu-logout" v-if="teacher_load" @click="logout" style="padding-left: 75px;text-align: left">
          <i class="el-icon-house"></i>
          <span slot="title">注销</span>
        </div>
        <!-- 管理员-->
        <el-menu-item index="/UserManagement" heigh="200"  v-if="admin_load" style="padding-left: 75px;text-align: left">
          <i class="el-icon-user"></i>
          <span slot="title">用户管理</span>
        </el-menu-item>
        <el-menu-item index="/CurriculumManagement" v-if="admin_load" style="padding-left: 75px;text-align: left">
          <i class="el-icon-tickets"></i>
          <span slot="title">课表管理</span>
        </el-menu-item>
        <el-menu-item index="/ClassManagement" v-if="admin_load" style="padding-left: 75px;text-align: left">
          <i class="el-icon-document-copy"></i>
          <span slot="title">班级管理</span>
        </el-menu-item>
        <div class="el-menu-item menu-logout" v-if="admin_load" @click="logout" style="padding-left: 75px;text-align: left">
          <i class="el-icon-house"></i>
          <span slot="title">注销</span>
        </div>
      </el-menu>
      <!--登录子画面-->
      <el-dialog
        title="登录"
        :visible.sync="dialogVisible"
        class="login-main"
        width="480px">
        <login ref="login" @loginSubmit="loginSubmit" @registrate="showReg"></login>
      </el-dialog>
      <!--注册子画面-->
      <el-drawer
        :visible.sync="regVisible"
        direction="ltr"
        :with-header="false">
        <registration ref="reg" @regSubmit="regVisible = false"></registration>
      </el-drawer>
      <!--修改个人信息子画面-->
      <el-drawer
        :visible.sync="mpiVisivle"
        direction="ltr"
        :with-header="false">
        <mpi ref="mpi" :data="mpiData" @mpiSubmit="mpiVisivle=false"></mpi>
      </el-drawer>
    </div>
</template>
<script>
  import login from './Login'
  import registration from './Registration'
  import mpi from './ModifyPersonalInformation'
    export default {
      name: "Menu",
      components:{
           login,         //登录
           registration,  //注册
           mpi            //修改个人信息
      },
      data(){
         return{
           //未登录主页 导航栏显示flg
           home_load:true,
           //学生导航栏显示flg
           students_load:false,
           //教师导航栏显示flg
           teacher_load:false,
           //管理员导航栏显示flg
           admin_load:false,
           //登录子画面是否显示
           dialogVisible:false,
           //注册子画面是否显示
           regVisible:false,
           //修改个人信息子画面是否显示
           mpiVisivle:false,
           //默认选择项目
           activeName:'',
           //用户数据修改表单数据
           mpiData:{},
           //头部图片的url
           imageUrl:''
         }
      },
      //画面渲染时加载 刷新
      mounted(){
        let param = this.$Storage.get('distinction')
        this.home_load = false
        this.students_load = false
        this.teacher_load = false
        this.admin_load = false
        //学生
        if(param === 0){
          this.imageUrl = 'https://i.loli.net/2020/02/01/Cqj1r7WJMicnyY8.jpg'
          this.students_load = true
        }
        //教师
        else if(param === 1){
          this.imageUrl = 'https://i.loli.net/2020/02/01/Aurxlp6Ts7OVFJL.jpg'
          this.teacher_load = true
        }
        //管理员
        else if(param === 9){
          this.imageUrl= 'https://i.loli.net/2020/02/01/hT9OfDiRs6vZJxB.jpg'
          this.admin_load = true
        }
        else{
          this.home_load = true
        }
        console.log(this.imageUrl)
      },
      methods: {
          //打开登录字画卖弄
          showLogin(){
            this.dialogVisible = true
            if(this.$refs &&
              this.$refs.login &&
              this.$refs.login.$refs &&
              this.$refs.login.$refs['loginForm']){
              this.$refs.login.$refs['loginForm'].resetFields()
            }
          },
          //打开注册子画面
          showReg(){
            this.regVisible = true
            if(this.$refs &&
              this.$refs.reg &&
              this.$refs.reg.$refs &&
              this.$refs.reg.$refs['form']){
              this.$refs.reg.$refs['form'].resetFields()
            }
          },
          //打开修改个人信息字话main
          showMpi(){
            this.$Api.post('Mpi/getUserInfo', {user_cd:this.$Storage.get('user_cd')}).then((resp)=>{
              if(resp != null && resp.length != 0){
                let data = resp[0]
                this.mpiData =
                  {
                    user_cd:data.user_cd,
                    user_nm:data.user_nm,
                    password:data.password,
                    checkPass:'',
                    tell_phone:data.tell_phone,
                    email:data.email
                  }
              }else{
                this.mpiData =
                  {
                    user_cd:'',
                    user_nm:'',
                    password:'',
                    checkPass:'',
                    tell_phone:'',
                    email:''
                  }
              }
            }).catch(()=>{
              this.$message.error('网络异常,请检查后再试')
            })
            this.mpiVisivle = true
          },
          //登录
          loginSubmit(param){
            this.home_load = false
            this.students_load = false
            this.teacher_load = false
            this.admin_load = false
            //学生
            if(param == 0){
              this.imageUrl = 'https://i.loli.net/2020/02/01/Cqj1r7WJMicnyY8.jpg'
              this.students_load = true
              this.jumpRouter('/ClassQuery')
            }
            //教师
            else if(param == 1){
              this.imageUrl = 'https://i.loli.net/2020/02/01/Aurxlp6Ts7OVFJL.jpg'
              this.teacher_load = true
              this.jumpRouter('/StudentManagement')
            }
            //管理员
            else if(param == 9){
              this.imageUrl= 'https://i.loli.net/2020/02/01/hT9OfDiRs6vZJxB.jpg'
              this.admin_load = true
              this.jumpRouter('/UserManagement')
            }
            this.dialogVisible = false
          },
          //注销
           logout(){
             this.$confirm('确认注销吗？')
                .then(() => {
                  this.students_load = false
                  this.teacher_load = false
                  this.admin_load = false
                  this.home_load = true
                  this.imageUrl = ''
                  this.$Storage.set('user_cd', '')
                  this.$Storage.set('distinction', '')
                  this.jumpRouter('/')
               })
                .catch(_ => {});
           },
           //路由跳转
           jumpRouter(path) {
              this.$router.push(path)
              this.activeName = path
           }
      }
    }
</script>
