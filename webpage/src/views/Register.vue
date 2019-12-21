<template>
  <div class="register">
    <registerHead />
    <el-divider style="width:80%;">
      <span style="font-size:40px;">注册</span>
    </el-divider>
    <div class="body">
    <router-view />
    </div>
  </div>
</template>

<style>
.register .body{
    margin: 100px auto 0;
  width: 520px;

}
</style>

<script>
import registerHead from "@/components/Register/Head.vue";

export default {
  components: {
    registerHead: registerHead
  }
};
</script>