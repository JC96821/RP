<template>
    <div class="el-scrollbar__wrap" style="overflow: hidden">
      <el-container>
        <el-header height="100px" style="padding: 20px;text-align: left;">
          <div style="display: inline-block;vertical-align: top;">
            <img src="http://www.dlust.edu.cn/images/dlkjxy/logo-blue-test.png" alt="..." style="width: 250px;height: 50px;">
          </div>
          <div style="display: inline-block;vertical-align: top;padding-left:280px;font-size:40px;font-weight: bold;">
            毕业设计
          </div>
        </el-header>
      </el-container>
      <el-main>
        <div class="home-card-left">
          <el-timeline :reverse="reverse" style="padding: 40px;">
            <el-timeline-item
              v-for="(activity, index) in activities"
              :key="index"
              :timestamp="activity.timestamp">
              {{activity.content}}
            </el-timeline-item>
          </el-timeline>
        </div>
        <div class="home-card-right">
          <div style="padding: 20px;">
            <h3>项目简介</h3>
            <div class="text-subline offset-top"></div>
            <p>
              本项目采用了前后端完全分离的方式进行开发，前台提供UI与客户交互，
              后台提供api给前台进行数据的交换处理。开发简单方便，老少皆宜。
            </p>
            <h3>技术栈</h3>
            <div class="text-subline offset-top"></div>
            <h4>前台VUE技术简介:</h4>
            <p>
              是一套用于构建用户界面的渐进式框架。
              与其它大型框架不同的是，Vue 被设计为可以自底向上逐层应用。
              Vue 的核心库只关注视图层，不仅易于上手，还便于与第三方库或既有项目整合。
              另一方面，当与现代化的工具链以及各种支持类库结合使用时，Vue 也完全能够为复杂的单页应用提供驱动。
            </p>
            <h4>后台.Net Core技术简介:</h4>
            <p>
              .NET Core是适用于 windows、linux 和 macos 操作系统的免费、开源托管的计算机软件框架，
              是微软开发的第一个官方版本，具有跨平台 (Windows、Mac OSX、Linux) 能力的应用程序开发框架 (Application Framework)，
              未来也将会支持 FreeBSD 与 Alpine 平台，也是微软在一开始发展时就开源的软件平台  ，
              它经常也会拿来和现有的开源 .NET 平台 Mono 比较。
            </p>
            <p>
              由于 .NET Core 的开发目标是跨平台的 .NET 平台，因此 .NET Core 会包含 .NET Framework 的类库，
              但与 .NET Framework 不同的是 .NET Core 采用包化 (Packages) 的管理方式，应用程序只需要获取需要的组件即可，
              与 .NET Framework 打包式安装的作法截然不同，同时各包亦有独立的版本线 (Version line)，不再硬性要求应用程序跟随主线版本。
            </p>
          </div>
        </div>
      </el-main>
      <el-footer style="text-align: left;">
        <div>作者: 任萍</div>
        <div class="text-subline offset-top"></div>
        <div style="padding-top:5px;padding-bottom: 5px;">
          学号: 10001&nbsp;&nbsp;&nbsp;&nbsp;
          班级: 四年级一班&nbsp;&nbsp;&nbsp;&nbsp;
          专业:软件工程&nbsp;&nbsp;&nbsp;&nbsp;
          学院:信息工程学院
        </div>
      </el-footer>
    </div>
</template>

<script>
    export default {
        name: "Home",
        data(){
            return{
              activeIndex:'1',
              reverse:false,
              activities: [
                {content: '工程创建',timestamp: '2020-01-24'},
                {content: '前端环境搭建',timestamp: '2020-01-25'},
                {content: '前端编码开始',timestamp: '2020-01-26'},
                {content: '前端编码结束',timestamp: '2020-02-28'},
                {content: '后端编码开始',timestamp: '2020-03-05'},
                {content: '后端编码结束',timestamp: '2020-03-31'},
                {content: '前后端耦合测试开始',timestamp: '2020-04-01'},
                {content: '前后端耦合测试结束',timestamp: '2020-04-25'},
                {content: '结合测试开始',timestamp: '2020-04-28'},
                {content: '结合测试结束',timestamp: '2020-04-30'},
              ]
            }
        }
    }
</script>
