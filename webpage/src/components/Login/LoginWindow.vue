<template>
  <div id="loginWindow">
    <el-tabs type="border-card" @tab-click="loginModeSwitching" v-model="loginMode">
      <el-tab-pane label="密码登录" name="formLogin">
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
      <el-tab-pane label="二维码登录" name="QRLogin">
        <div class="qr-login">
          <div class="img">
            <!-- <div ref="qrcode"></div> -->
            <div v-if="disable">
              <vueQr :text="token" :size="130" />
            </div>
            <div class="refresh" v-if="!disable">
              <span style="margin:auto;font-size:60px;line-height:130px">
                <el-button class="buttonStyle" icon="el-icon-refresh" @click="refreshQRCode"></el-button>
                <!-- <i class="el-icon-refresh" /> -->
              </span>
            </div>
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
.buttonStyle {
  height: 100%;
  width: 100%;
  font-size: 60px;
  background-color: rgba(0, 0, 0, 0.2);
  color: #2c3e50;
}
.refresh {
  width: 130px;
  height: 130px;
  background-image: url("../../assets/22222.jpg");
  display: inline-block;
}
</style>

<script>
import vueQr from "vue-qr";
export default {
  components: {
    vueQr
  },
  data() {
    return {
      formlogin: {
        username: "",
        password: ""
      },
      loginMode: "formLogin",
      token: "",
      disable: true
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
          this.$store.commit("login", response.data.accessToken);
          this.$store.dispatch("userLoad", response.data.userId);
          this.$router.replace({ path: "/" });
        })
        .catch(error => {
          console.log(error);
          this.$message.error("用户名或密码错误");
          if (localStorage.getItem("username") != null) {
            localStorage.removeItem("accessToken");
            localStorage.removeItem("refreshToken");
            localStorage.removeItem("username");
            localStorage.removeItem("userImage");
          }
        });
    },
    register() {
      this.$router.replace({ path: "/register" });
    },
    loginModeSwitching(mode) {
      if (mode.name == "QRLogin") {
        this.getQRCode();
      }
    },
    getQRCode() {
      this.axios
        .get("qrcode/code")
        .then(res => {
          this.token = res.data.token;
          this.getLoginStatus(res.data.id);
        })
        .catch(error => {
          console.log(error);
        });
    },
    getLoginStatus(id) {
      this.axios
        .post("qrcode/status", JSON.stringify({ id: id }))
        .then(res => {
          console.log(res);
          localStorage.setItem("accessToken", res.data.accessToken);
          localStorage.setItem("refreshToken", res.data.refreshToken);
          this.$store.commit("login", res.data.accessToken);
          this.$store.dispatch("userLoad", res.data.userId);
          this.$router.replace({ path: "/" });
        })
        .catch(error => {
          console.log(error);
          this.$message({
            showClose: true,
            message: "二维码已过期",
            type: "error"
          });
          this.disable = false;
        });
    },
    refreshQRCode(){
      this.disable=true;
      this.getQRCode();

    }
  }
};
</script>