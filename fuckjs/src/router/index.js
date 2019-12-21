import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'home',
    component: Home
  },
  {
    path: '/about',
    name: 'about',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  },
  {
    path: '/new-post',
    name: 'new-post',
    component: () => import('../views/NewPost.vue')
  },
  {
    path: '/post/:id',
    name: 'post',
    props: true,
    component: () => import('../views/Post.vue')
  },
  {
    path: '/user/:id',
    name: 'user',
    props: true,
    component: () => import('../views/User.vue')
  },
  {
    path: '/edit-user',
    name: 'edit-user',
    component: () => import('../views/EditUser.vue')
  },
  {
    path: '/search',
    name: 'search',
    component: () => import('../views/Search.vue')
  },
  {
    path: '/sign-in',
    name: 'sign-in',
    component: () => import('../views/SignIn.vue')
  },
  {
    path: '/sign-up',
    name: 'sign-up',
    component: () => import('../views/SignUp.vue')
  },
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.NODE_ENV === 'production' ? '/vue/' : '/',
  routes
})

export default router
