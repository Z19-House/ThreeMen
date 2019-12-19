import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import Register from '../views/Register.vue'
import registerWindow from '../components/Register/RegisterWindow.vue'
import UserInformation from '../components/Register/UserInformation.vue'
import homeCommodity from "@/components/Home/HomeCommodity.vue";
import productBrowsing from "@/components/Home/ProductBrowsing.vue";



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
        beforeEnter: (to, from, next) => {
          console.log('即将进入：', to);
          console.log('即将离开：', from);
          next();
        }
      }
    ]
  },
  {
    path: '/register',
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
  }

]

const router = new VueRouter({
  routes
})

export default router
