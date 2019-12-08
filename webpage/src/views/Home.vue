<template>
  <div class="home">
    <div id='homeHead'>
      <el-row>
            <el-col :span="4">
              <el-image :src="url" style="width: 110px; height: 110px;  float:right;"/>
            </el-col>
            <el-col :span="3">
              <h2/>
              <h1 style="font-size:30px; float:left;" >FishBuy</h1>
            </el-col>
            <el-col :span="14">
              <div id="searchBarDiv">
              </div>
            </el-col>
      </el-row>
    </div>
    <div id="homeBody">

    </div>
  </div>
</template>

<script>
// @ is an alias to /src

export default {
  name: 'home',
  data(){
    return {
      fit:'fill',
      url:require( '../assets/logo.png')
    }
  }
}
</script>
<style>
#homeHead{
}
#searchBarDiv{
  height: 110px;
  background-color: antiquewhite;
}
#homeBody{
  height: 1000px;
  margin: 10px 10% 5px;
  background-color: aqua;
}
</style>