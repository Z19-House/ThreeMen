<template>
  <div class="row justify-center">
    <form
      @submit.prevent.stop="signUp"
      class="col-xs-12 col-sm-8 col-md-6 col-lg-4 q-pa-md q-gutter-md column items-stretch"
    >
      <q-input
        outlined
        v-model="username"
        ref="username"
        label="用户名 *"
        mask="NNNNNNNNNNNNNNNN"
        lazy-rules
        :rules="[val => val && val.length >= 2 && val.length <= 16 || '用户名在2-16位之间',
                 val => isNaN(val) || '用户名不能是纯数字']"
      />
      <q-input
        v-model="password"
        outlined
        ref="password"
        label="密码 *"
        :type="isPwd ? 'password' : 'text'"
        lazy-rules
        :rules="[val => !!val && val.length >= 6 && val.length <= 32 || '密码在6-32位之间']"
      >
        <template v-slot:append>
          <q-icon
            :name="isPwd ? 'visibility_off' : 'visibility'"
            class="cursor-pointer"
            @click="isPwd = !isPwd"
          />
        </template>
      </q-input>
      <q-input
        v-model="rePassword"
        outlined
        ref="rePassword"
        label="确认密码 *"
        :type="isPwd ? 'password' : 'text'"
        lazy-rules
        :rules="[val => !!val && val.length >= 6 && val.length <= 32 || '密码在6-32位之间',
                 val => val == password || '两次输入的密码不同']"
      >
        <template v-slot:append>
          <q-icon
            :name="isPwd ? 'visibility_off' : 'visibility'"
            class="cursor-pointer"
            @click="isPwd = !isPwd"
          />
        </template>
      </q-input>

      <q-btn color="primary" label="注册" @click="signUp" />
      <q-btn color="white" text-color="black" label="已有账号？登录" to="sign-in" />
    </form>
  </div>
</template>


<script>
// @ is an alias to /src
import api from "@/api";

export default {
  name: "sign-up",
  components: {},
  data() {
    return {
      username: "",
      password: "",
      rePassword: "",
      isPwd: true
    };
  },
  methods: {
    async signUp() {
      this.$refs.username.validate();
      this.$refs.password.validate();
      this.$refs.rePassword.validate();
      if (
        this.$refs.username.hasError ||
        this.$refs.password.hasError ||
        this.$refs.rePassword.hasError
      ) {
        this.formHasError = true;
      } else {
        try {
          await api.signUp(this.username, this.password);
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
            message: "注册成功，请完善用户信息！"
          });
          this.$router.replace({
            name: "edit-user",
            query: { firstSign: true }
          });
        } catch (error) {
          console.log(error.response.data);
          this.$q.notify({
            color: "negative",
            message: "注册失败！"
          });
        }
      }
    }
  }
};
</script>
