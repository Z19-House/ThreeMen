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
          <el-radio-group v-model="isDisplay" v-if="isCurrentUser">
            <el-radio-button label="1">用户信息</el-radio-button>
            <el-radio-button label="0">编辑</el-radio-button>
          </el-radio-group>
          <userInformation :username="username" :isDisplay="isDisplay" style="margin-top:10px" />
        </el-tab-pane>
        <el-tab-pane>
          <span slot="label" class="tabStyle">
            <i class="el-icon-star-on"></i> 收藏
          </span>
          <userProduct
            :title="'我的收藏'"
            :username="username"
            :pagerCount="10"
            :pageSize="12"
            :Category="'collection'"
          />
        </el-tab-pane>
        <el-tab-pane>
          <span slot="label" class="tabStyle">
            <i class="el-icon-circle-plus"></i> 发布
          </span>
          <el-tabs tab-position="left" type="border-card" v-model="value" v-if="isCurrentUser">
            <el-tab-pane label="发布" name="post">
              <releaseProduct :postId="postId" @isModify="isModify" @isPost="isPost" />
            </el-tab-pane>
            <el-tab-pane label="我的发布" name="myPost">
              <userProduct
                :username="username"
                ref="userProduct"
                :pagerCount="10"
                :pageSize="12"
                @getPostId="getModifyPostId"
              />
            </el-tab-pane>
          </el-tabs>
          <userProduct :username="username" :pagerCount="10" :pageSize="12" v-else />
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

  data() {
    return {
      url: require("../assets/logo.png"),
      isDisplay: 1,
      isCurrentUser: 0,
      postId: "",
      value: "myPost"
    };
  },
  computed: {
    username() {
      return this.$route.params.username;
    }
  },
  watch: {
    username: function() {
      console.log("我进来了");
      this.judgeCurrentUser();
    }
  },
  mounted() {
    this.judgeCurrentUser();
  },
  methods: {
    goBack() {
      this.$router.go(-1);
    },
    judgeCurrentUser() {
      if (this.username == localStorage.getItem("username")) {
        this.isCurrentUser = 1;
      } else {
        this.isCurrentUser = 0;
      }
    },
    getModifyPostId(postId) {
      console.log("拿到postId", postId);
      this.postId = postId;
      this.value = "post";
    },
    isModify(status) {
      if (status == "success") {
        this.value = "myPost";
        this.$refs["userProduct"].page = [];
        this.$refs["userProduct"].LoadPage1();
      }
    },
    isPost(status) {
      if (status == "success") {
        this.value="myPost"
        this.$refs["userProduct"].page = [];
        this.$refs["userProduct"].LoadPage1();
      }
    }
  }
};
</script>