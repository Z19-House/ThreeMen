<template>
  <div class="row justify-center">
    <div class="col-xs-12 col-sm-8 col-md-6 col-lg-4 q-pa-md q-gutter-md column items-stretch">
      <q-input outlined :value="username" readonly placeholder="用户名" />
      <q-input v-model="password" outlined label="原密码" :type="isPwd ? 'password' : 'text'">
        <template v-slot:append>
          <q-icon
            :name="isPwd ? 'visibility_off' : 'visibility'"
            class="cursor-pointer"
            @click="isPwd = !isPwd"
          />
        </template>
      </q-input>
      <q-input v-model="newPassword" outlined label="新密码" :type="isPwd2 ? 'password' : 'text'">
        <template v-slot:append>
          <q-icon
            :name="isPwd2 ? 'visibility_off' : 'visibility'"
            class="cursor-pointer"
            @click="isPwd2 = !isPwd2"
          />
        </template>
      </q-input>
      <q-input
        v-model="reNewPassword"
        outlined
        label="再次输入新密码"
        :type="isPwd3 ? 'password' : 'text'"
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
      try {
        await api.changePassword(
          this.username,
          this.password,
          this.newPassword
        );
        api.signOut();
        this.$store.commit("removeUser");
        this.$router.replace("sign-in");
      } catch (error) {
        console.log(error.response.data);
      }
    }
  }
};
</script>
