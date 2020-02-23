<template>
    <div class="reg-main">
      <el-form :model="form" :rules="rules" ref="form" label-width="100px">
        <el-form-item label="用户编号" prop="user_cd" class="reg-item">
          <el-input v-model="form.user_cd" maxlength="20" class="input-txt" show-word-limit clearable></el-input>
        </el-form-item>
        <el-form-item label="用户名" prop="user_nm" class="reg-item">
          <el-input v-model="form.user_nm" maxlength="50" class="input-txt" show-word-limit clearable></el-input>
        </el-form-item>
        <el-form-item label="密码" prop="password" class="reg-item">
          <el-input type="password" v-model="form.password" maxlength="20" class="input-txt" clearable></el-input>
        </el-form-item>
        <el-form-item label="确认密码" prop="checkPass" class="reg-item">
          <el-input type="password" v-model="form.checkPass" maxlength="20" class="input-txt" clearable></el-input>
        </el-form-item>
        <el-form-item label="用户区分" class="reg-item">
          <el-radio-group v-model="form.distinction">
            <el-radio :label="0">学生</el-radio>
            <el-radio :label="1">教师</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="手机号" prop="tell_phone" class="reg-item">
          <el-input v-model="form.tell_phone" maxlength="11" class="input-txt" show-word-limit clearable></el-input>
        </el-form-item>
        <el-form-item label="电子邮箱" prop="email" class="reg-item">
          <el-input v-model="form.email" maxlength="19" class="input-txt" show-word-limit clearable></el-input>
        </el-form-item>
        <el-form-item class="reg-item reg-bottom">
          <el-button @click="resetForm('form')">重置</el-button>
          <el-button type="success" @click="submitForm('form')">注册</el-button>
        </el-form-item>
      </el-form>
    </div>
</template>

<script>
    export default {
      name: "Registration",
      data(){
        var validatePass = (rule, value, callback) => {
          if (value !== this.form.password) {
            callback(new Error('两次输入密码不一致!'));
          } else {
            callback();
          }
        };
          return{
            form:{user_cd:'', user_nm:'', password:'', checkPass:'', distinction:0, tell_phone:'', email:''},
            rules: {
              user_cd: [
                { required: true, message: '请输入用户名', trigger: 'blur' }
              ],
              user_nm: [
                { required: true, message: '请输入用户名', trigger: 'blur' }
              ],
              password: [
                { required: true, message: '请输入密码', trigger: 'blur' }
              ],
              checkPass: [
                { required: true, message: '请再输入密码', trigger: 'blur' },
                { validator: validatePass, trigger: 'blur' }
              ],
              tell_phone: [
                { required: true, message: '请输入手机号', trigger: 'blur' }
              ],
              email: [
                { required: true, message: '请输入电子邮箱', trigger: 'blur' }
              ]
            },
            imageUrl: ''
          }
      },
      methods:{
        //数据提交
        submitForm(formName) {
          this.$refs[formName].validate((valid) => {
            if (valid) {
              this.doSave()
            } else {
              return false;
            }
          });
        },
        //保存实行
        doSave(){
          this.$Api.post('Registration/save', this.form).then((resp)=>{
            if(resp == '0'){
              this.$emit('regSubmit')
              this.$Storage.set('user_cd', this.form.user_cd)
              this.$message.success('注册成功')
            }else if(resp == '-1'){
              this.$message.warning('啊哦,出现了未知的问题,请联系管理员')
            }else if(resp == '-2'){
              this.$message.warning('该用户已存在，请重新输入用户编号')
            }
          }).catch(()=>{
            this.$message.error('网络异常,请检查后再试')
          })
        },
        //表单数据重置
        resetForm(formName) {
          this.$refs[formName].resetFields();
        }
      }
    }
</script>
