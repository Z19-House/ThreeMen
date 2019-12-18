<template>
  <div id="Login">
    <div id="head">
      <loginHead></loginHead>
    </div>
    <div id="body">
      <div style="float:right;margin-top:3%;margin-right:13%; ">
        <loginWindow />
      </div>
    </div>
    <div id="bottom">
      <router-view />
    </div>
  </div>
</template>

<script>
import axios from "axios";
import loginHead from "@/components/Login/LoginHeader.vue";
import loginWindow from "@/components/Login/LoginWindow.vue";

axios.defaults.withCredentials = true;
export default {
  components: {
    loginHead: loginHead,
    loginWindow: loginWindow
  }
};
</script>

<style>
#body {
  height: 450px;
  background-image: url("../assets/login_page.jpg");
  background-position: center;
  background-size: cover;
}
</style>