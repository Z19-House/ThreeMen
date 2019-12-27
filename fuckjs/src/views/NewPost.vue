<template>
  <postEditerComponent :post="post" @savePost="newPost" />
</template>

<script>
// @ is an alias to /src
import api from "@/api";
import postEditerComponent from "@/components/PostEditerComponent.vue";

export default {
  name: "new-post",
  components: {
    postEditerComponent
  },
  data() {
    return {
      post: {
        title: "",
        content: "",
        tags: [],
        price: 0,
        address: "",
        medias: []
      }
    };
  },
  methods: {
    async newPost(postData) {
      try {
        console.log(postData);
        await api.newPost(
          postData.title,
          postData.content,
          postData.tags.join(","),
          "sale",
          parseFloat(postData.price),
          postData.address,
          postData.medias
        );
        this.$router.replace({name: "home", params: {reloadDate: new Date()}});
      } catch (error) {
        console.log(error.response.data);
      }
    }
  },
  created() {
    if (!this.$store.state.username) {
      this.$router.replace("sign-in");
    }
  }
};
</script>
