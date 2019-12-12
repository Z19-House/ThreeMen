<template>
  <div class="home">
    <homeNavigation
      :username="username"
      :userImage="userImage"
    />
    <homeHead/>
    <div id="homeBody">
      <homeSortNavigation/>
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import homeHead from '@/components/Home/HomeHead.vue'
import homeNavigation from '@/components/Home/HomeNavigation.vue'
import homeSortNavigation from '@/components/Home/HomeSortNavigation.vue'
import axios from 'axios'

export default {

  name: 'home',
  components:{
    homeHead:homeHead,
    homeNavigation:homeNavigation,
    homeSortNavigation:homeSortNavigation
  },
   data(){
        return{
            username:"未登录",
            userImage:'',
        }
    },
    mounted:function(){
        this.LoadUserInformation();
    },
    methods: {
        errorHandler() {
            return true
        },
        LoadUserInformation:function(){
            console.log("我发送了消息");
            axios({
                method: 'get',
                url: 'http://118.25.64.161/api/user/'+ localStorage.getItem("username"),
                headers:
                {
                    'Content-Type': 'application/json'
                }
            })
            .then((response)=> {
                this.username=response.data.username;
                this.userImage=response.data.imageUrl;
                console.log(this.username);
                console.log(this.userImage);
            })
            .catch((error)=> {
                console.log(error);
            });
        }
    }
}
</script>
<style>

#homeBody{
  height: 1000px;
  margin: 15px 13% 5px;
  background-color: aqua;
}
</style>