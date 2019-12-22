import Vue from 'vue'
import App from './App.vue'
import axios from './api/api'
import router from './router'
import './plugins/element.js'
import store from './store'

Vue.config.productionTip = false


new Vue({
  router,
  axios,
  store,
  render: h => h(App)
}).$mount('#app')

