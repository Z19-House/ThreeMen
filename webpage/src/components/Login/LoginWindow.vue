<template>
  <div id="loginWindow">
    <div id="login-style">
      <div id="login-head">
        <p style="font-size:20px;">用户登录</p>
      </div>
      <div id="login-form">
        <el-form :model="formlogin">
          <el-form-item>
            <el-input
              v-model="formlogin.username"
              placeholder="用户名"
              prefix-icon="el-icon-user-solid"
            />
          </el-form-item>
          <el-form-item>
            <el-input
              v-model="formlogin.password"
              placeholder="密码"
              prefix-icon="el-icon-lock"
              show-password
            />
          </el-form-item>
          <el-form-item prop="name">
            <el-button type="primary" @click="onSubmit(formlogin)">登录</el-button>
            <el-button v-on:click="register">注册</el-button>
          </el-form-item>
        </el-form>
      </div>
    </div>
  </div>
</template>

<style>
#login-style {
  width: 300px;
  height: 320px;
  background-color: white;
}
#login-head {
  height: 60px;
  margin-bottom: 30px;
  border: 1px solid rgb(198, 201, 198);
  border-radius: 0px;
}
#login-form {
  width: 80%;
  margin: 0 auto;
}
</style>

<script>
export default {
  data() {
    return {
      formlogin: {
        username: "",
        password: ""
      }
    };
  },
  methods: {
    onSubmit(formName) {
      // let _this=this;
      this.axios({
        method: "post",
        url: "auth/signin",
        data: JSON.stringify(formName),

      })
        .then(response => {
          localStorage.setItem("accessToken", response.data.accessToken);
          localStorage.setItem("refreshToken", response.data.refreshToken);
          localStorage.setItem("username", formName.username);
          localStorage.setItem("username", formName.username);
          this.$store.commit('login',{accessToken:response.data.accessToken,username:formName.username});
          this.$store.dispatch("userLoad", formName.username);
          this.$router.replace({ path: "/" });
        })
        .catch(error => {
          console.log(error);
          this.$message.error("用户名或密码错误");
          if (localStorage.getItem("username") != null) {
            localStorage.removeItem("accessToken");
            localStorage.removeItem("refreshToken");
            localStorage.removeItem("username");
          }
        });
    },
    register() {
      this.$router.replace({ path: "/register" });
    }
  }
};
</script>