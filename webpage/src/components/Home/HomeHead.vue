<template>
  <div id="homeHead">
    <el-row>
      <el-col :span="4">
        <el-image :src="url" style="width: 110px; height: 110px;  float:right;" />
      </el-col>
      <el-col :span="3">
        <h2 />
        <h1 style="font-size:30px; float:left;">FishBuy</h1>
      </el-col>
      <el-col :span="14">
        <div id="searchBarDiv">
          <el-input placeholder="请输入内容" v-model="seachVaule" style="margin-top:4%;width:60%;">
            <el-button type="primary" slot="append" icon="el-icon-search" />
          </el-input>
        </div>
      </el-col>
    </el-row>
  </div>
</template>


<script>
export default {
  data() {
    return {
      seachVaule: "",
      url: require("../../assets/logo.png")
    };
  }
};
</script>