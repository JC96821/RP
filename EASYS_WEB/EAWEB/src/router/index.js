import Vue from 'vue'
import Router from 'vue-router'
import Storage from "../storage";
Vue.use(Router)

const router = new Router({
  routes: [
    //主页
    {
      path: '/',
      name: 'Home',
      component: () => import('../components/Home')
    },
    //登录
    {
      path: '/Login',
      name: 'Login',
      component: () => import('../components/Home')
    },
    //注册
    {
      path: '/Registration',
      name: 'Registration',
      component: () => import('../components/Registration')
    },
    //修改个人信息
    {
      path: '/ModifyPersonalInformation',
      name: 'ModifyPersonalInformation',
      component: () => import('../components/ModifyPersonalInformation'),
      meta: {requireAuth: true}
    },
    //选择课程和指导教师
    {
      path: '/ChooseCoursesAndTeachers',
      name: 'ChooseCoursesAndTeachers',
      component: ()=>import('../components/students/ChooseCoursesAndTeachers'),
      meta: {requireAuth: true}
    },
    //课表查询
    {
      path: '/ClassQuery',
      name: 'ClassQuery',
      component: ()=>import('../components/students/ClassQuery'),
      meta: {requireAuth: true}
    },
    //成绩查询
    {
      path: '/ScoreInquiry',
      name: 'ScoreInquiry',
      component: ()=>import('../components/students/ScoreInquiry'),
      meta: {requireAuth: true}
    },
    //教师质量评价
    {
      path: '/TeacherEaluation',
      name: 'TeacherEaluation',
      component: () => import('../components/students/TeacherEaluation'),
      meta: {requireAuth: true}
    },
    //学生成绩管理
    {
      path: '/StudentScoreManagement',
      name: 'StudentScoreManagement',
      component: () => import('../components/teachers/StudentScoreManagement'),
      meta: {requireAuth: true}
    },
    //学生管理
    {
      path: '/StudentManagement',
      name: 'StudentManagement',
      component: () => import('../components/teachers/StudentManagement'),
      meta: {requireAuth: true}
    },
    //学生评价
    {
      path: '/StudentEvaluation',
      name: 'StudentEvaluation',
      component: () => import('../components/teachers/StudentEvaluation'),
      meta: {requireAuth: true}
    },
    //用户管理
    {
      path: '/UserManagement',
      name: 'UserManagement',
      component: () => import('../components/administrator/UserManagement'),
      meta: {requireAuth: true}
    },
    //课表管理
    {
      path: '/CurriculumManagement',
      name: 'CurriculumManagement',
      component: () => import('../components/administrator/CurriculumManagement'),
      meta: {requireAuth: true}
    },
    //班级管理
    {
      path: '/ClassManagement',
      name: 'ClassManagement',
      component: () => import('../components/administrator/ClassManagement'),
      meta: {requireAuth: true}
    },
  ]
})

export default router
