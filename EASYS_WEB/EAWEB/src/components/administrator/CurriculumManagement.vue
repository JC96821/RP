<template>
    <div>
      <el-container>
        <el-header class="container-header-pro">
          <el-form :inline="true" :model="form" label-width="100px">
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
                <el-select v-model="form.user_cd" placeholder="请选择教师" class="input-txt" clearable>
                  <el-option
                    v-for="item in user_list"
                    :key="item.user_cd"
                    :label="item.user_nm"
                    :value="item.user_cd">
                  </el-option>
                </el-select>
              </el-form-item>
              <el-form-item>
                <el-button type="primary" @click="search()">查询</el-button>
                <el-button type="success" @click="add()">新增课程</el-button>
              </el-form-item>
            </div>
          </el-form>
        </el-header>
        <el-main class="container-main">
          <el-table :data="tableData" v-loading="loading" height="700px">
            <el-table-column type="index" width="50"></el-table-column>
            <el-table-column prop="curriculum_cd" label="课程编号"></el-table-column>
            <el-table-column prop="curriculum_nm" label="课程名"></el-table-column>
            <el-table-column prop="user_nm" label="教师姓名"></el-table-column>
            <el-table-column prop="term" label="学期">
              <template slot-scope="scope">
                {{getTerm(scope.row.term)}}
              </template>
            </el-table-column>
            <el-table-column label="上课时间">
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
            <el-table-column fixed="right" label="操作" width="180">
              <template slot-scope="scope">
                <el-button type="text" @click="edit(scope.row)" style="margin-right: 10px">修改课程</el-button>
                <el-popconfirm title="确定删除吗？" @onConfirm="del(scope.row)">
                  <el-button type="danger" size="mini" slot="reference">删除</el-button>
                </el-popconfirm>
              </template>
            </el-table-column>
          </el-table>
        </el-main>
      </el-container>
      <el-dialog :title="digTitle" :visible.sync="digVisible" style="text-align: left" width="680px">
        <el-form :inline="true" :model="digForm" ref="form" :rules="rules" label-width="85px">
          <div>
            <el-form-item label="课程编号:" prop="curriculum_cd">
              <el-input v-model="digForm.curriculum_cd" :disabled="digDisabled" clearable></el-input>
            </el-form-item>
            <el-form-item label="课程名:" prop="curriculum_nm">
              <el-input v-model="digForm.curriculum_nm" :disabled="digDisabled" clearable></el-input>
            </el-form-item>
          </div>
          <div>
            <el-form-item label="教师:" prop="user_cd">
              <el-select v-model="digForm.user_cd" placeholder="请选择教师" style="width: 212px" clearable>
                <el-option
                  v-for="item in user_list"
                  :key="item.user_cd"
                  :label="item.user_nm"
                  :value="item.user_cd">
                </el-option>
              </el-select>
            </el-form-item>
            <el-form-item label="学期:" prop="term">
              <el-select v-model="digForm.term" placeholder="请选择学期" style="width: 212px" :disabled="digDisabled" clearable>
                <el-option key="0" value="0" label="第一学期上"></el-option>
                <el-option key="1" value="1" label="第一学期下"></el-option>
                <el-option key="2" value="2" label="第二学期上"></el-option>
                <el-option key="3" value="3" label="第二学期下"></el-option>
              </el-select>
            </el-form-item>
          </div>
        </el-form>
        <el-table :data="digData" height="500px">
          <el-table-column prop="label" label="曜日" width="200"></el-table-column>
          <el-table-column prop="value" label="上课时间">
            <template slot-scope="scope">
              <el-time-select
                v-model="scope.row.value"
                :picker-options="{start: '08:30',step: '00:30',end: '20:30'}"
                class="input-txt"
                placeholder="选择时间">
              </el-time-select>
            </template>
          </el-table-column>
        </el-table>
        <div slot="footer" class="dialog-footer">
          <el-button @click="digVisible = false">取 消</el-button>
          <el-button type="success" @click="save()">保存</el-button>
        </div>
      </el-dialog>
    </div>
</template>

<script>
    export default {
      name: "CurriculumManagement",
      data(){
        return{
            //检索表单数据
            form:{curriculum_cd:'', curriculum_nms:'', term:'', user_cd:''},
            //教师下拉数据
            user_list:[],
            //表格数据加载标记
            loading:false,
            //表格数据
            tableData:[],
            //子画面显示标记
            digVisible:false,
            //子画面标题
            digTitle:'',
            //子画面'课程编号'、'课程名'、'学期'活性化标记
            digDisabled:false,
            //子画面头部表单数据
            digForm:{curriculum_cd:'', curriculum_nm:'', user_cd:'', term:''},
            //子画面明细部表格数据
            digData:[
              {label:'周一', value:''},
              {label:'周二', value:''},
              {label:'周三', value:''},
              {label:'周四', value:''},
              {label:'周五', value:''},
              {label:'周六', value:''},
              {label:'周日', value:''}
            ],
            //子画面表单验证规则
            rules: {
              curriculum_cd: [
                { required: true, message: '请输入课程编号', trigger: 'blur' }
              ],
              curriculum_nm: [
                { required: true, message: '请输入课程名', trigger: 'blur' }
              ],
              user_cd: [
                { required: true, message: '请选择任课教师', trigger: 'blur' }
              ],
              term: [
                { required: true, message: '请选择开课学期', trigger: 'blur' }
              ]
            },
            //子画面处理标记 0:新规 1:更新
            exec_flg:''
          }
      },
      //画面渲染时加载
      mounted(){
        this.loading = true
        let p1 = this.$Api.post('Curriculum/getTeacherList')
        let p2 = this.$Api.post('Curriculum/search', this.form)
        Promise.all([p1, p2]).then((resp)=>{
          if(resp[0])this.user_list = resp[0]
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
        //新增
        add(){
          this.digVisible = true
          this.digTitle = '新增课程'
          this.digDisabled = false
          this.exec_flg = "0"
          this.digForm = {curriculum_cd:'', curriculum_nm:'', user_cd:'', term:''}
          if(this.$refs['form'])this.$refs['form'].resetFields()
          this.digData.map((item, index)=>{
            item.value = ''
          })
        },
        //编辑
        edit(row){
          this.digVisible = true
          this.digTitle = '修改课程'
          this.digDisabled = true
          this.exec_flg = "1"
          this.digForm = {curriculum_cd:row.curriculum_cd, curriculum_nm:row.curriculum_nm, user_cd:row.user_cd, term:row.term}
          this.digData = [
            {label:'周一', value:row.monday},
            {label:'周二', value:row.tuesday},
            {label:'周三', value:row.wednesday},
            {label:'周四', value:row.thursday},
            {label:'周五', value:row.friday},
            {label:'周六', value:row.saturday},
            {label:'周日', value:row.sunday}
          ]
        },
        //保存
        save(){
          this.$refs['form'].validate((valid) => {
            if (valid) {
              this.doSave()
            }
          })
        },
        //删除
        del(row){
          this.$Api.post('Curriculum/delete', row).then((resp)=>{
            if(resp == '0'){
              this.$message.success('删除成功')
              let index = this.tableData.indexOf(row)
              this.tableData.splice(index, 1)
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
          this.$Api.post('Curriculum/search', this.form).then((resp)=>{
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
          let user_nm = ''
          let item = this.user_list.filter((param)=>{return param.user_cd == this.digForm.user_cd })
          if(item && item.length >0){
            user_nm = item[0].user_nm
          }
          let data = {
            curriculum_cd:this.digForm.curriculum_cd,
            curriculum_nm:this.digForm.curriculum_nm,
            user_cd:this.digForm.user_cd,
            user_nm: user_nm,
            term:this.digForm.term,
            monday:this.digData[0].value,
            tuesday:this.digData[1].value,
            wednesday:this.digData[2].value,
            thursday:this.digData[3].value,
            friday:this.digData[4].value,
            saturday:this.digData[5].value,
            sunday:this.digData[6].value,
            login_user: this.$Storage.get('user_cd'),
            upd_user: this.$Storage.get('user_cd'),
            exec_flg:this.exec_flg
          }
          this.$Api.post('Curriculum/save', data).then((resp)=>{
            if(resp == '0'){
              this.$message.success('保存成功')
              this.getData()
              this.digVisible = false
            }else if(resp == '-1'){
              this.$message.warning('保存失败')
            }else if(resp=='-2'){
              this.$message.warning('该课表已存在，请重新输入课程编号')
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
