<template>
  <div id="homeNavigation">
    <el-menu class="el-menu-demo" mode="horizontal" active-text-color="#2c3e50">
      <el-menu-item style="float:right" index="1">
        我的收藏
        <el-badge :value="collectionValue" class="item" type="primary"></el-badge>
      </el-menu-item>
      <el-menu-item style="float:right" index="2">
        我的发布
        <el-badge :value="postValue" class="item" type="primary"></el-badge>
      </el-menu-item>

      <el-submenu style="float:right" index="3" @click.native="userClick">
        <template slot="title">
          <el-avatar :size="40" :src="userImage" @error="errorHandler" fit="contain">
            <img src="../../errorImage/errorImage.png" />
          </el-avatar>
          <span v-if="username">{{username}}</span>
          <span v-else>未登录</span>
        </template>
        <el-menu-item @click.native="userLogout">注销</el-menu-item>
      </el-submenu>
    </el-menu>
  </div>
</template>

<style>
.item {
  margin-left: 2px;
  height: 66px;
}
</style>

<script>
export default {
  data() {
    return {
      collectionValue: 0,
      postValue: 0
    };
  },
  computed: {
    username() {
      return this.$store.state.username;
    },
    userImage() {
      return this.$store.state.userImage;
    }
  },
  watch: {
    username: function() {
      this.getPostAndCollectionNum();
    }
  },
  mounted() {
    this.getPostAndCollectionNum();
  },
  methods: {
    errorHandler() {
      return true;
    },
    userClick() {
      this.$router.push({ name: "user", params: { username: this.username } });
    },
    userLogout() {
      console.log("用户登出");
      localStorage.removeItem("accessToken");
      localStorage.removeItem("refreshToken");
      localStorage.removeItem("username");
      localStorage.removeItem("userImage");
      this.$store.commit("logout");
    },
    getPostAndCollectionNum() {
      this.axios
        .get("user/" + this.username + "/posts", {
          params: {
            skip: 0,
            take: 0
          }
        })
        .then(response => {
          this.postValue = response.data.count;
          console.log(response.data);
        })
        .catch(error => {
          console.log(error);
        });
        this.axios
        .get("user/" + this.username + "/collection", {
          params: {
            skip: 0,
            take: 0
          }
        })
        .then(response => {
          this.collectionValue = response.data.count;
          console.log(response.data);
        })
        .catch(error => {
          console.log(error);
        });
    }
  }
};
</script>