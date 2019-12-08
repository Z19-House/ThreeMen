<template>
  <div id='Login'>
    <div id='head'>
      <el-row >
        <el-col :span="14">
            <el-col :span="6">
              <el-image :src="url" style="width: 130px; height: 130px;  float:right;"/>
            </el-col>
            <el-col :span="8">
              <h2/>
              <h1 style="font-size:40px; float:left;" >FishBuy<span style="font-size:20px"> 欢迎登录</span></h1>
            </el-col>
        </el-col>
      </el-row>
    </div>
    <div id='body'>
      <el-row>
        <el-col :span="15"><div style="height:550px;"></div></el-col>
        <el-col :span="5">
          <div id="login-style">
              <div id="login-head">
                <p style="font-size:20px;">用户登录</p>
              </div>
              <div id="login-form">
                <el-form :model="formlogin">
                  <el-form-item >
                    <el-input v-model="formlogin.username" placeholder="用户名"  prefix-icon="el-icon-user-solid"></el-input>
                  </el-form-item>
                  <el-form-item >
                      <el-input  v-model="formlogin.password" placeholder="密码"  prefix-icon="el-icon-lock" show-password></el-input>
                  </el-form-item>
                  <el-form-item prop="name">
                     <el-button type="primary" @click="onSubmit(formlogin)">登录</el-button>
                    <el-button>注册</el-button>
                  </el-form-item>
                </el-form>
              </div>
          </div>
        </el-col>
      </el-row>
    </div>
    <div id='bottom'>
      <router-view/>
  </div>
  </div>
</template>

<script>
import axios from 'axios';

axios.defaults.withCredentials=true;
export default {
  data(){
    return {
      formlogin:{
        username: '',
        password: ''
      },
      fit:'fill',
      url:require( '../assets/logo.png')
    }
  },
  methods: {
    onSubmit(formName) {
     // let _this=this;
      console.log(formName.password);
      axios({
        method: 'post',
        url: 'http://118.25.64.161:5000/api/auth/signin',
        
        data: JSON.stringify(
          formName
        ),
        headers:
        {
          'Content-Type': 'application/json'
        }
      })
      .then((response)=> {
          localStorage.setItem("accessToken", response.data.accessToken);
          localStorage.setItem("refreshToken", response.data.refreshToken);
          this.$router.push({ path : '/home'});
      })
      .catch((error)=> {
        console.log(error);
         this.$message.error('用户名或密码错误');

      });
    }
  }
}
</script>

<style>
#login{
  width:100%;
  height:100%;
}
#head{
  width: 100%;
  height: 130px;
}
#body{
  width: 100%;
  height: 450px;
  background-image: url("../assets/login_page.jpg");
  background-position:center;
  background-size:100% 100%;
}
#bottom{
  width: 100%;
  height: 100px;
}
#login-style{
  width:300px;
  height:320px;
  background-color: white;
  margin: 15%;

}
#login-head{
  height: 60px; 
  margin-bottom: 30px;
  border:1px solid rgb(198, 201, 198); 
  border-radius: 0px
}
#login-form{
  width: 80%;
  margin: 0 auto;
}
</style>