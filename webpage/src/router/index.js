import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import Register from '../views/Register.vue'
import registerWindow from '../components/Register/RegisterWindow.vue'
import UserInformation from '../components/Register/UserInformation.vue'


Vue.use(VueRouter)

const routes = [
  {
    path : '/',
    name : 'login',
    component : Login
  },
  {
    path : '/home',
    name : 'home',
    component : Home,
  },
  {
    path : '/register',
    component : Register,
    children:[
      {
        path : '/',
        name : 'registerWindow',
        component : registerWindow,
      },
      {
        path : '/userInformation',
        name : 'userInformation',
        component : UserInformation,
      }
    ]
  }
    
]

const router = new VueRouter({
  routes
})

export default router
