<template>
  <div class="home">
    <homeNavigation/>
    <div class="homehead">
      <homeHead />
    </div>
    <div class="homeBody">
      <keep-alive exclude="productBrowsing">
        <router-view />
      </keep-alive>
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import homeHead from "@/components/Home/HomeHead.vue";
import homeNavigation from "@/components/Home/HomeNavigation.vue";

export default {
  name: "home",
  components: {
    homeHead,
    homeNavigation
  },
  data() {
    return {};
  },
  
  methods: {
    errorHandler() {
      return true;
    }
  }
};
</script>
<style  >

.homeBody {
  background-color: #f4f4f4;
}
.homehead {
  height: 130px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.12), 0 0 6px rgba(0, 0, 0, 0.04);
}
</style>