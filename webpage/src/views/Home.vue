<template>
  <div class="home">
    <homeNavigation :username="username" :userImage="userImage" />
    <div class="homehead">
      <homeHead />
    </div>
    <div class="homeBody">
      <keep-alive include="homeCommodity">
        <router-view :userImage="userImage" />
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
    homeHead: homeHead,
    homeNavigation: homeNavigation
  },
  data() {
    return {
      username: "未登录",
      userImage: ""
    };
  },
  mounted: function() {
    this.LoadUserInformation();
  },

  methods: {
    errorHandler() {
      return true;
    },
    LoadUserInformation: function() {
      console.log("我发送了消息");
      this.axios({
        method: "get",
        url: "user/" + localStorage.getItem("username")
      })
        .then(response => {
          this.username = response.data.username;
          this.userImage = response.data.imageUrl;
        })
        .catch(error => {
          console.log(error);
        });
    }
  }
};
</script>
<style>
.homeBody {
  background-color: #f4f4f4;
}
.homehead {
  height: 130px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.12), 0 0 6px rgba(0, 0, 0, 0.04);
}
</style>