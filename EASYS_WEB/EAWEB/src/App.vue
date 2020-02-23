<template>
  <div id="app">
    <el-container style="height: 944px;">
      <el-aside>
        <navMenu></navMenu>
      </el-aside>
      <el-main>
        <router-view/>
      </el-main>
    </el-container>
  </div>
</template>

<script>
  import navMenu from './components/Menu'
  export default {
    name: 'App',
    components:{
      navMenu
    }
  }
</script>
