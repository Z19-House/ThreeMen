import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import Register from '../views/Register.vue'
import registerWindow from '../components/Register/RegisterWindow.vue'
import UserInformation from '../components/global/UserInformation.vue'
import homeCommodity from "../views/HomeCommodity.vue";
import productBrowsing from "../views/ProductBrowsing.vue";
import user from "../views/User.vue"


Vue.use(VueRouter)

const routes = [
  {
    path: '/login',
    name: 'login',
    component: Login
  },
  {
    path: '/',
    name: 'home',
    component: Home,
    children: [
      {
        path: '/',
        name: 'homeCommodity',
        component: homeCommodity,
      },
      {
        path: '/productBrowsing/:postId',
        name: 'productBrowsing',
        component: productBrowsing,

      }
    ]
  },
  {
    path: '/register',
    name:'register',
    component: Register,
    children: [
      {
        path: '/',
        name: 'registerWindow',
        component: registerWindow,
      },
      {
        path: '/userInformation',
        name: 'userInformation',
        component: UserInformation,
      }
    ]
  },
  {
    path: '/user/:username',
    name: 'user',
    component: user,
    beforeEnter: (to, from, next) => {
      if (localStorage.getItem("accessToken")) {     //如果localStorage里存了用户名，则表示已登录
        next(true);
      } else {
        next('/login');
          
        }
      }
    
  }

]

const router = new VueRouter({
  routes
})

export default router
