import Vue from 'vue'
import axios from 'axios'
import VueAxios from 'vue-axios'

Vue.use(VueAxios, axios)

axios.defaults.baseURL = 'http://118.25.64.161/api/';
axios.defaults.headers.post['Content-Type'] ="application/json";
axios.defaults.headers.put['Content-Type'] ="application/json";

export default axios