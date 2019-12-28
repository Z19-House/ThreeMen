<template>
  <div class="searchView">
    <el-tabs v-model="value" style="background-color:#fff; box-shadow: 0 0 0 1px #eee;">
      <el-tab-pane name="product">
        <span slot="label" class="tabStyle">
          <i class="el-icon-s-goods"></i> 商品
        </span>
        <homeSearchResult :Category="'posts'" />
      </el-tab-pane>
      <el-tab-pane name="tag">
        <span slot="label" class="tabStyle">
          <i class="el-icon-user"></i> 用户
        </span>
        <SearchUser  />
      </el-tab-pane>
      <el-tab-pane name="user">
        <span slot="label" class="tabStyle">
          <i class="el-icon-s-grid"></i> 标签
        </span>
        <homeSearchResult  :Category="'tags'" />
      </el-tab-pane>
    </el-tabs>
  </div>
</template>



<style  scoped>


.searchView {
  width: 980px;
  margin: 0 auto;
}
.tabStyle {
  height: 80px;
  line-height: 80px;
  font-size: 16px;
  margin: 0 25px;
}
</style>

<script>
import homeSearchResult from "@/components/Home/HomeSearchResult.vue";
import SearchUser from "@/components/Home/HomeSearchUser.vue";

export default {
  components: {
    homeSearchResult,
    SearchUser
  },
  data() {
    return {
      value: "product"
    };
  }
};
</script>