<template>
  <div id="registerWindow">
    <div id="register-style">
      <div id="register-form">
        <el-form
          :model="formRegister"
          :rules="rules"
          label-position="left"
          label-width="90px"
          ref="formRegister"
        >
          <el-form-item label="用户名:" prop="username" :error="errors">
            <el-input
              v-model="formRegister.username"
              placeholder="请输入用户名"
              prefix-icon="el-icon-user-solid"
            />
          </el-form-item>
          <el-form-item prop="password" label="密码:">
            <el-input
              v-model="formRegister.password"
              placeholder="请输入密码"
              prefix-icon="el-icon-lock"
              show-password
            />
          </el-form-item>
          <el-form-item label="确认密码:" prop="checkPass">
            <el-input
              v-model="formRegister.checkPass"
              placeholder="请确认密码"
              prefix-icon="el-icon-lock"
              show-password
            />
          </el-form-item>
          <el-form-item prop="name">
            <el-button type="primary" v-on:click="submitForm" style="width:100%">下一步</el-button>
          </el-form-item>
        </el-form>
      </div>
    </div>
  </div>
</template>
<style>
</style>
<script>
export default {
  data() {
    var userRe = /^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{2,16}$/;
    var checkUsename = (rule, value, callback) => {
      if (!value) {
        return callback(new Error("用户名不能为空！"));
      } else if (value.length < 2 || value.length > 16) {
        callback(new Error("用户名应在2-16位之间！"));
      } else if (!userRe.test(value)) {
        callback(new Error("用户名必须为数字和字母组合"));
      } else {
        callback();
      }
    };
    var validatePass = (rule, value, callback) => {
      if (value === "") {
        callback(new Error("请输入密码"));
      } else if (value.length < 6 || value.length > 32) {
        callback(new Error("密码长度应在6-32位之间"));
      } else {
        if (this.formRegister.password !== "") {
          this.$refs.formRegister.validateField("checkPass");
        }
        callback();
      }
    };
    var validatePass2 = (rule, value, callback) => {
      const reg = /^\s+$/;
      if (reg.test(value)) {
        callback(new Error("密码中不能含有空格！"));
      } else {
        callback();
      }
    };
    var validateCheckPass = (rule, value, callback) => {
      if (value === "") {
        callback(new Error("请再次输入密码"));
      } else if (value !== this.formRegister.password) {
        callback(new Error("两次输入密码不一致!"));
      } else {
        callback();
      }
    };

    return {
      formRegister: {
        username: "",
        password: "",
        checkPass: ""
      },
      errors: "",
      rules: {
        username: [{ validator: checkUsename, trigger: "blur" }],
        password: [
          { validator: validatePass, trigger: "blur" },
          { validator: validatePass2, trigger: "change" }
        ],
        checkPass: [{ validator: validateCheckPass, trigger: "blur" }]
      }
    };
  },
  methods: {
    RegisterUser() {
      this.axios({
        method: "post",
        url: "auth/signup",

        data: JSON.stringify({
          username: this.formRegister.username,
          password: this.formRegister.password
        })
      })
        .then(response => {
          console.log("response.status");
          console.log(response.status);
          this.login();
        })
        .catch(error => {
          console.log(error.response);
          if (error.response.data.error === "Username already exists.")
            this.errors = "用户名重复";
        });
    },
    login() {
      this.axios({
        method: "post",
        url: "auth/signin",
        data: JSON.stringify({
          username: this.formRegister.username,
          password: this.formRegister.password
        })
      })
        .then(response => {
          localStorage.setItem("accessToken", response.data.accessToken);
          localStorage.setItem("refreshToken", response.data.refreshToken);
          localStorage.setItem("username", this.formRegister.username);
          this.$store.commit('login',{accessToken:response.data.accessToken,username:this.formRegister.username});
          this.$store.dispatch("userLoad", this.formRegister.username);
          this.$router.go(0);
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
    submitForm() {
      this.$refs.formRegister.validate(valid => {
        if (valid) {
          this.RegisterUser();
        }
      });
    }
  }
};
</script>