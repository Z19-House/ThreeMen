import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import Register from '../views/Register.vue'
// import registerWindow from '../components/Register/RegisterWindow.vue'
// import UserInformation from '../components/global/UserInformation.vue'
import homeCommodity from "../views/HomeCommodity.vue";
import productBrowsing from "../views/ProductBrowsing.vue";
import user from "../views/User.vue"
import store from '@/store/index';
import homeSearch from '@/views/HomeSearch.vue'

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
          console.log("1111",to.params.postId)
          store.commit("setPostId",to.params.postId);
          next(true);
        },
        
      },
      {
        path: '/homeSearch',
        name: 'homeSearch',
        component: homeSearch 
      }
    ]
  },
  {
    path: '/register',
    name: 'register',
    component: Register,
    // children: [
    //   {
    //     path: '/',
    //     name: 'registerWindow',
    //     component: registerWindow,
    //   },
    //   {
    //     path: '/userInformation',
    //     name: 'userInformation',
    //     component: UserInformation,
    //   }
    // ]
  },
  {
    path: '/user/:username',
    name: 'user',
    component: user,
    modal: route => ({
      username: route.params.username,
      isDisplay: route.params.isDisplay
    }),
    beforeEnter: (to, from, next) => {
      if (localStorage.getItem("accessToken")) {     //如果localStorage里存了token，则表示已登录
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
