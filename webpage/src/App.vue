<template>
  <div id="app">
    <keep-alive  >
      <router-view />
    </keep-alive>
  </div>
</template>

<script>
export default {
  name: "app"
};
</script>

<style>
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
  text-align: center;
}
body {
  overflow:auto !important;
  margin: 0 0 0;
      background-color: #f4f4f4;

}
</style>
