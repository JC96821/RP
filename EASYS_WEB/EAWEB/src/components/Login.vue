<template>
    <div>
      <el-form :model="loginForm" :rules="rules" ref="loginForm" label-width="100px" class="login-form">
        <el-form-item>
          <h2>请输入用户名密码</h2>
        </el-form-item>
        <el-form-item label="用户名" prop="user_cd">
          <el-input v-model="loginForm.user_cd" class="input-txt"></el-input>
        </el-form-item>
        <el-form-item label="密码" prop="password">
          <el-input type="password" v-model="loginForm.password" class="input-txt"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="submitForm('loginForm')">登录</el-button>
          <el-button @click="resetForm('loginForm')">重置</el-button>
          <el-button type="primary" @click="registration">注册</el-button>
        </el-form-item>
      </el-form>
    </div>
</template>

<script>
  export default {
      name: "Login",
      data(){
          return{
            loginForm:{user_cd:"", password:""},
            rules: {
              user_cd: [
                { required: true, message: '请输入用户名', trigger: 'blur' }
              ],
              password: [
                { required: true, message: '请输入密码', trigger: 'blur' }
              ]
            }
          }
      },
      methods:{
          //数据提交
          submitForm(formName) {
            this.$refs[formName].validate((valid) => {
              if (valid) {
                this.$Api.post('Login/LoginVerificaty', this.loginForm).then((resp)=>{
                  if(resp == '0' || resp == '1' || resp=='9'){
                    this.$emit('loginSubmit', resp);
                    this.$Storage.set('user_cd', this.loginForm.user_cd)
                    this.$Storage.set('distinction', resp)
                  }else if(resp == '-1'){
                    this.$message.warning('该用户不存在')
                  }else if(resp == '-2'){
                    this.$message.warning('密码错误哦')
                  }
                }).catch(()=>{
                  this.$message.error('网络异常,请检查后再试')
                })
              }
            });
          },
          //表单数据重置
          resetForm(formName) {
            if(this.$refs[formName])this.$refs[formName].resetFields();
          },
          //注册
          registration(){
            this.$emit('registrate');
          }
      }
    }
</script>
