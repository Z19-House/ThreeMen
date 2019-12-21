<template>
  <div id="homeNavigation">
    <el-menu class="el-menu-demo" mode="horizontal">
      <el-submenu style="float:right" index="1">
        <template slot="title">我的收藏</template>

        <el-menu-item index="1-1">选项1</el-menu-item>
        <el-menu-item index="1-2">选项2</el-menu-item>
        <el-menu-item index="1-3">选项3</el-menu-item>
      </el-submenu>
      <el-submenu style="float:right" index="2">
        <template slot="title">我的发布</template>
        <el-menu-item index="2-1">选项1</el-menu-item>
        <el-menu-item index="2-2">选项2</el-menu-item>
        <el-menu-item index="2-3">选项3</el-menu-item>
      </el-submenu>
      <el-menu-item style="float:right" index="3" @click.native="userClick">
        <template slot="title">
          <el-avatar :size="40" :src="userImage" @error="errorHandler" fit="cover">
            <img src="../../errorImage/errorImage.png" />
          </el-avatar>
          <span>{{username}}</span>
        </template>
      </el-menu-item>
    </el-menu>
  </div>
</template>

<style>
</style>

<script>
export default {
  props: {
    username: String,
    userImage: String
  },
  methods: {
    errorHandler() {
      return true;
    },
    userClick() {
      console.log(this.username);
      this.$router.push({ name: "user", params: { username: this.username } });
    }
  }
};
</script>