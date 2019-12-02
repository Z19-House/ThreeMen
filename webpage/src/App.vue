<template>
  <div id="app">
    <el-menu router>
          <el-submenu index="1">
            <template slot="title"><i class="el-icon-document">资产概况</i> </template>
            <el-menu-item-group>
              <el-menu-item index="/home">资产列表</el-menu-item>
              <el-menu-item index="/about">配件列表</el-menu-item>
            </el-menu-item-group>
          </el-submenu>
        </el-menu>
    <router-view/>
  </div>
</template>

<script>


export default {
  name: 'app'
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 0px;
}
</style>
