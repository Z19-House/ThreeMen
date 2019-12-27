<template>
  <q-layout view="hHh lpR fff">
    <q-header reveal elevated class="bg-primary text-white">
      <q-toolbar>
        <q-toolbar-title style="min-width:100px">
          <router-link to="/">
            <q-avatar>
              <q-img src="https://cdn.quasar.dev/logo/svg/quasar-logo.svg" />
            </q-avatar>
          </router-link> 摸鱼
        </q-toolbar-title>

        <q-input
          v-if="$route.name!=='search'"
          dark
          dense
          standout
          v-model="text"
          v-on:keyup.enter="search"
          input-class="text-right"
          class="q-ml-md"
          style="margin-right:1rem"
        >
          <template v-slot:append>
            <q-icon v-if="text === ''" name="search" />
            <q-icon v-else name="clear" class="cursor-pointer" @click="text = ''" />
          </template>
        </q-input>

        <q-btn round>
          <q-avatar size="42px">
            <q-img v-if="userImage" :src="userImage" />
            <q-img v-else src="http://118.25.64.161/images/efd0fc7d20532fdaf769a25683617711.png" />
          </q-avatar>
          <q-menu auto-close>
            <q-list v-if="!userId" style="min-width: 100px">
              <q-item clickable to="/sign-in">
                <q-item-section>登录</q-item-section>
              </q-item>
              <q-item clickable to="/sign-up">
                <q-item-section>注册</q-item-section>
              </q-item>
              <q-separator />
            </q-list>
            <q-list v-else style="min-width: 100px">
              <q-item dense style="color:gray;font-size:13px">
                <q-item-section>@{{ username }}</q-item-section>
              </q-item>
              <q-item clickable @click="myPage">
                <q-item-section>个人主页</q-item-section>
              </q-item>
              <q-item clickable to="/edit-user">
                <q-item-section>信息编辑</q-item-section>
              </q-item>
              <q-item clickable to="/change-password">
                <q-item-section>修改密码</q-item-section>
              </q-item>
              <q-separator />
              <q-item clickable @click="signOut">
                <q-item-section>注销</q-item-section>
              </q-item>
            </q-list>
          </q-menu>
        </q-btn>
      </q-toolbar>
    </q-header>

    <q-page-container>
      <keep-alive include="home,user">
        <router-view />
      </keep-alive>
    </q-page-container>
  </q-layout>
</template>

<script>
import api from "@/api";

export default {
  data() {
    return {
      text: ""
    };
  },
  computed: {
    userId() {
      return this.$store.state.userId;
    },
    username() {
      return this.$store.state.username;
    },
    userImage() {
      return this.$store.state.userImage;
    }
  },
  methods: {
    myPage() {
      this.$router.push({ name: "user", params: { id: this.userId } });
    },
    signOut() {
      api.signOut();
      this.$store.commit("removeUser");
      this.$router.replace("sign-out");
      this.$router.replace("/");
    },
    search() {
      this.$router.push({
        name: "search",
        query: { type: "title", keyword: this.text }
      });
    }
  },
  async created() {
    let user = (await api.getUserInfo(this.username)).data;
    this.$store.commit("setUser", {
      userId: user.userId,
      username: user.username,
      userImage: user.imageUrl,
      userGroup: user.userGroup
    });
  }
};
</script>