import Vue from 'vue'
import axios from 'axios'
import VueAxios from 'vue-axios'
import store from '@/store/index';

Vue.use(VueAxios, axios)

axios.defaults.baseURL = 'https://api.z19.site/';
axios.defaults.headers.post['Content-Type'] ="application/json";
axios.defaults.headers.put['Content-Type'] ="application/json";

// 请求拦截器
axios.interceptors.request.use(    
    config => {        
        // 每次发送请求之前判断vuex中是否存在token        
        // 如果存在，则统一在http请求的header都加上token，这样后台根据token判断你的登录情况
        // 即使本地存在token，也有可能token是过期的，所以在响应拦截器中要对返回状态进行判断 
        const token = store.state.token;        
        token && (config.headers.Authorization = token);        
        return config;    
    },    
    error => {        
        return Promise.error(error);    
})

export default axios