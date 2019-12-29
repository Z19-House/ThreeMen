<template>
  <div id="loginWindow">
    <el-tabs type="border-card">
      <el-tab-pane label="密码登录">
        <div class="inputForm">
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
      </el-tab-pane>
      <el-tab-pane label="二维码登录">
        <div class="qr-login">
          <div class="img">
            <el-image style="width:100%; height: 100%;" :src="url" fit="covor"></el-image>
            <span>请扫描二维码登录</span>
          </div>
        </div>
      </el-tab-pane>
    </el-tabs>
  </div>
</template>

<style  scoped>
/* #login-style {
  width: 300px;
  height: 320px;
  background-color: white;
} */
/* #login-head {
  margin-bottom: 30px;
  border: 1px solid rgb(198, 201, 198);
  border-radius: 0px;
} */
div {
  display: block;
}
.inputForm {
  margin-top: 30px;
}
.qr-login {
  width: 268px;
  height: 204px;
  font-size: 14px;
  position: relative;
  text-align: center;
}
.img {
  margin: 20px auto 20px;
  width: 130px;
  height: 130px;
  position: absolute;
  left: 0;
  right: 0;
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
        data: JSON.stringify(formName)
      })
        .then(response => {
          localStorage.setItem("accessToken", response.data.accessToken);
          localStorage.setItem("refreshToken", response.data.refreshToken);
          localStorage.setItem("username", formName.username);
          localStorage.setItem("username", formName.username);
          this.$store.commit("login", {
            accessToken: response.data.accessToken,
            username: formName.username
          });
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