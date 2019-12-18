<template>
  <div class="register">
    <registerHead />
    <el-divider style="width:80%">
      <span style="font-size:40px">注册</span>
    </el-divider>
    <router-view />
  </div>
</template>

<style>
</style>

<script>
import registerHead from "@/components/Register/Head.vue";

export default {
  components: {
    registerHead: registerHead
  }
};
</script>