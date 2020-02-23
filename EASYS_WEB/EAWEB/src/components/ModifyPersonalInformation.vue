<template>
    <div  class="mpi-main">
      <el-form :model="data" :rules="rules" ref="form" label-width="100px">
        <el-form-item label="用户编号" class="reg-item">
          {{data.user_cd}}
        </el-form-item>
        <el-form-item label="用户名" class="reg-item">
          {{data.user_nm}}
        </el-form-item>
        <el-form-item label="密码" prop="password" class="reg-item">
          <el-input type="password" v-model="data.password" maxlength="20" class="input-txt"></el-input>
        </el-form-item>
        <el-form-item label="确认密码" prop="checkPass" class="reg-item">
          <el-input type="password" v-model="data.checkPass" maxlength="20" class="input-txt"></el-input>
        </el-form-item>
        <el-form-item label="手机号" prop="tell_phone" class="reg-item">
          <el-input type="tell_phone" v-model="data.tell_phone" maxlength="11" class="input-txt" show-word-limit clearable></el-input>
        </el-form-item>
        <el-form-item label="电子邮箱" prop="email" class="reg-item">
          <el-input type="email" v-model="data.email" maxlength="19" class="input-txt" show-word-limit clearable></el-input>
        </el-form-item>
        <el-form-item class="reg-item reg-bottom">
          <el-button @click="resetForm('form')">重置</el-button>
          <el-button type="success" @click="submitForm('form')">保存</el-button>
        </el-form-item>
      </el-form>
    </div>
</template>

<script>
  export default {
    name: "ModifyPersonalInformation",
    //父组件传送数据
    props:{
      data:Object
    },
    data(){
      var validatePass = (rule, value, callback) => {
        if (value !== this.data.password) {
          callback(new Error('两次输入密码不一致!'));
        } else {
          callback();
        }
      };
      return{
        rules: {
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
        }
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
        //表单数据上传
        this.$Api.post('Mpi/save', this.data).then((resp)=>{
          if(resp == '0'){
            this.$emit('mpiSubmit')
            this.$message.success('保存成功')
          }else if(resp == '-1'){
            this.$message.warning('保存失败')
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

