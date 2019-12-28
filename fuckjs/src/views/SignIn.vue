<template>
  <div class="row justify-center">
    <form
      @submit.prevent.stop="signIn"
      class="col-xs-12 col-sm-8 col-md-6 col-lg-4 q-pa-md q-gutter-md column items-stretch"
    >
      <q-input
        outlined
        ref="username"
        v-model="username"
        label="用户名 *"
        mask="NNNNNNNNNNNNNNNN"
        lazy-rules
        :rules="[val => val && val.length >= 2 && val.length <= 16 || '用户名在2-16位之间']"
      />
      <q-input
        v-model="password"
        outlined
        ref="password"
        label="密码 *"
        :type="isPwd ? 'password' : 'text'"
        lazy-rules
        :rules="[val => val && val.length >= 6 && val.length <= 32 || '密码在6-32位之间']"
      >
        <template v-slot:append>
          <q-icon
            :name="isPwd ? 'visibility_off' : 'visibility'"
            class="cursor-pointer"
            @click="isPwd = !isPwd"
          />
        </template>
      </q-input>
      <q-btn color="primary" label="登录" type="submit" />
      <q-btn color="white" text-color="black" label="还没有账号？注册" to="sign-up" />
    </form>
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
      this.$refs.username.validate();
      this.$refs.password.validate();

      if (this.$refs.username.hasError || this.$refs.password.hasError) {
        this.formHasError = true;
      } else {
        try {
          await api.signIn(this.username, this.password);
          let user = (await api.getUserInfo(this.username)).data;
          this.$store.commit("setUser", {
            userId: user.userId,
            username: user.username,
            userImage: user.imageUrl,
            userGroup: user.userGroup
          });
          this.$q.notify({
            icon: "done",
            color: "positive",
            message: "登录成功！"
          });
          this.$router.replace("/");
        } catch (error) {
          console.log(error.response.data);
          this.$q.notify({
            color: "negative",
            message: "登录失败，用户名或密码错误！"
          });
        }
      }
    }
  }
};
</script>
