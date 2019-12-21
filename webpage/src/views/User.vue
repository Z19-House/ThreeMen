<template>
  <div class="user">
    <div class="head">
      <el-page-header @back="goBack">
        <span slot="title" class="pageHeadleft">返回</span>
        <span slot="content" class="pageHeadRight">
          <el-image :src="url" style="width: 100px; height: 100px;" />
        </span>
      </el-page-header>
    </div>
    <div class="body">
      <el-tabs class="tab">
        <el-tab-pane>
          <span slot="label" class="tabStyle">
            <i class="el-icon-s-home"></i> 用户信息
          </span>
          <userInformation :username="username" />
        </el-tab-pane>
        <el-tab-pane>
          <span slot="label" class="tabStyle">
            <i class="el-icon-star-on"></i> 收藏
          </span>用户管理
        </el-tab-pane>
        <el-tab-pane>
          <span slot="label" class="tabStyle">
            <i class="el-icon-circle-plus"></i> 发布
          </span>用户管理
        </el-tab-pane>
      </el-tabs>
    </div>
  </div>
</template>

<style >
.user {
  margin: 0 18%;
}
.head {
  height: 120px;
}
.body .tab {
  height: 100px;
}
.tabStyle {
  font-size: 18px;
}
.head .el-page-header .el-page-header__title {
  align-self: center;
}
.head .el-page-header__content .pageHeadRight {
}
</style>

<script>
import userInformation from "@/components/global/UserInformation.vue";
export default {
  components: {
    userInformation: userInformation
  },
  data() {
    return {
      url: require("../assets/logo.png"),
      disable: true,
      username: this.$route.params.username
    };
  },
  methods: {
    goBack() {
      this.$router.go(-1);
    },
  
  },
  
};
</script>