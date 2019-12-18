import Vue from 'vue'
import App from './App.vue'
import axios from './axios/axios'
import router from './router'
import './plugins/element.js'

Vue.config.productionTip = false

new Vue({
  router,
  axios,
  render: h => h(App)
}).$mount('#app')

