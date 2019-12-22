<template>
  <div class="row justify-center">
    <div class="col-xs-12 col-sm-8 col-md-6 col-lg-4 q-pa-md q-gutter-md column items-stretch">
      <q-input outlined v-model="username" placeholder="用户名" />
      <q-input v-model="password" outlined placeholder="密码" :type="isPwd ? 'password' : 'text'">
        <template v-slot:append>
          <q-icon
            :name="isPwd ? 'visibility_off' : 'visibility'"
            class="cursor-pointer"
            @click="isPwd = !isPwd"
          />
        </template>
      </q-input>
      <q-btn color="primary" label="登录" @click="signIn" />
      <q-btn color="white" text-color="black" label="还没有账号？注册" to="sign-up" />
    </div>
  </div>
</template>


<script>
// @ is an alias to /src
import api from "@/api";

export default {
  name: "sign-in",
  components: {},
  data() {
    return {
      username: "",
      password: "",
      isPwd: true
    };
  },
  methods: {
    async signIn() {
      try {
        await api.signIn(this.username, this.password);
        this.$store.commit("setUsername", this.username);
        this.$router.push("/");
      } catch (error) {
        console.log(error.response.data);
      }
    }
  }
};
</script>
