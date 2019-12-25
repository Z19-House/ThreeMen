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
          <el-radio-group v-model="isDisplay">
            <el-radio-button label="1">用户信息</el-radio-button>
            <el-radio-button label="0">编辑</el-radio-button>
          </el-radio-group>
          <userInformation :username="username" :isDisplay="isDisplay" style="margin-top:10px" />
        </el-tab-pane>
        <el-tab-pane>
          <span slot="label" class="tabStyle">
            <i class="el-icon-star-on"></i> 收藏
          </span>
          <userProduct :title="'我的收藏'" :username="username" :pagerCount="10" :Category="'collection'" />
        </el-tab-pane>
        <el-tab-pane>
          <span slot="label" class="tabStyle">
            <i class="el-icon-circle-plus"></i> 发布
          </span>
          <el-tabs tab-position="left" type="border-card" value="second">
            <el-tab-pane label="发布" name="first">
              <releaseProduct />
            </el-tab-pane>
            <el-tab-pane label="我的发布" name="second">
              <userProduct :username="username" :pagerCount="10" />
            </el-tab-pane>
          </el-tabs>
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

</style>

<script>
import userInformation from "@/components/global/UserInformation.vue";
import releaseProduct from "@/components/user/ReleaseProduct.vue";
import userProduct from "@/components/user/userProduct.vue";

export default {
  components: {
    userInformation,
    releaseProduct,
    userProduct
  },
  props: {
    isCurrentUser: {
      type: Boolean,
      default: () => false
    }
  },
  data() {
    return {
      url: require("../assets/logo.png"),
      isDisplay: 1,
      username: this.$route.params.username,
    };
  },
  methods: {
    goBack() {
      this.$router.go(-1);
    }
  }
};
</script>