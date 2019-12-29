<template>
  <div id="Login">
    <div id="head">
      <loginHead></loginHead>
    </div>
    <div id="body">
      <div class="login-form">
        <loginWindow />
      </div>
    </div>
    <div id="bottom">
      <router-view />
    </div>
  </div>
</template>

<script>
import loginHead from "@/components/Login/LoginHeader.vue";
import loginWindow from "@/components/Login/LoginWindow.vue";

export default {
  components: {
    loginHead,
    loginWindow
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
.login-form {
  height: 300px;
  width: 300px;
  float: right;
  margin: 5% 13%;
}
</style>