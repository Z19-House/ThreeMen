<template>
  <div id="Head">
    <el-row>
      <el-col :span="14">
        <el-col :span="6">
          <el-image :src="url" style="width: 130px; height: 130px;  float:right;" />
        </el-col>
        <el-col :span="8">
          <h2 />
          <h1 style="font-size:40px; float:left;">FishBuy</h1>
        </el-col>
      </el-col>
    </el-row>
  </div>
</template>

<style>
</style>

<script>
export default {
  data() {
    return {
      url: require("../../assets/logo.png")
    };
  }
};
</script>