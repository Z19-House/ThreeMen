<template>
  <div class="row justify-center">
    <div class="col-xs-12 col-sm-8 col-md-6 col-lg-4 q-pa-md q-gutter-md column items-stretch">
      <q-input outlined :value="username" readonly placeholder="用户名" />
      <q-input
        ref="password"
        v-model="password"
        outlined
        label="原密码"
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
        ref="newPassword"
        v-model="newPassword"
        outlined
        label="新密码"
        :type="isPwd2 ? 'password' : 'text'"
        lazy-rules
        :rules="[val => !!val && val.length >= 6 && val.length <= 32 || '密码在6-32位之间']"
      >
        <template v-slot:append>
          <q-icon
            :name="isPwd2 ? 'visibility_off' : 'visibility'"
            class="cursor-pointer"
            @click="isPwd2 = !isPwd2"
          />
        </template>
      </q-input>
      <q-input
        ref="reNewPassword"
        v-model="reNewPassword"
        outlined
        label="再次输入新密码"
        :type="isPwd3 ? 'password' : 'text'"
        lazy-rules
        :rules="[val => !!val && val.length >= 6 && val.length <= 32 || '密码在6-32位之间',
                 val => val == newPassword || '两次输入的密码不同']"
      >
        <template v-slot:append>
          <q-icon
            :name="isPwd3 ? 'visibility_off' : 'visibility'"
            class="cursor-pointer"
            @click="isPwd3 = !isPwd3"
          />
        </template>
      </q-input>
      <q-btn color="primary" label="修改密码" @click="changePassword" />
    </div>
  </div>
</template>


<script>
// @ is an alias to /src
import api from "@/api";

export default {
  name: "change-password",
  components: {},
  computed: {
    username() {
      return this.$store.state.username;
    }
  },
  data() {
    return {
      password: "",
      newPassword: "",
      reNewPassword: "",
      isPwd: true,
      isPwd2: true,
      isPwd3: true
    };
  },
  methods: {
    async changePassword() {
      this.$refs.password.validate();
      this.$refs.newPassword.validate();
      this.$refs.reNewPassword.validate();
      if (
        this.$refs.password.hasError ||
        this.$refs.newPassword.hasError ||
        this.$refs.reNewPassword.hasError
      ) {
        this.formHasError = true;
      } else {
        try {
          await api.changePassword(
            this.username,
            this.password,
            this.newPassword
          );
          api.signOut();
          this.$store.commit("removeUser");
          this.$q.notify({
            icon: "done",
            color: "positive",
            message: "密码修改成功，请重新登录！"
          });
          this.$router.replace("sign-in");
        } catch (error) {
          console.log(error.response.data);
          this.$q.notify({
            color: "negative",
            message: "原密码错误！"
          });
        }
      }
    }
  }
};
</script>
