import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import Register from '../views/Register.vue'


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
    name : 'register',
    component : Register
  }
    
]

const router = new VueRouter({
  routes
})

export default router
