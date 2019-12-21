import Vue from 'vue'
import App from './App.vue'
import axios from './api/api'
import router from './router'
import './plugins/element.js'

Vue.config.productionTip = false


new Vue({
  router,
  axios,
  render: h => h(App)
}).$mount('#app')

