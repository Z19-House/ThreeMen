<template>
  <postEditerComponent :post="post" @savePost="editPost" />
</template>

<script>
// @ is an alias to /src
import api from "@/api";
import postEditerComponent from "@/components/PostEditerComponent.vue";

export default {
  name: "edit-post",
  components: {
    postEditerComponent
  },
  props: {
    id: Number
  },
  data() {
    return {
      post: {}
    };
  },
  methods: {
    async editPost(postData) {
      try {
        console.log(postData);
        await api.editPost(
          this.id,
          postData.title,
          postData.content,
          postData.tags.join(","),
          "sale",
          parseFloat(postData.price),
          postData.address,
          postData.medias
        );
        this.$router.replace(`/post/${this.id}`);
      } catch (error) {
        console.log(error.response.data);
      }
    },
    async getPostDetail() {
      try {
        let response = await api.getPostDetail(this.id);
        this.post = response.data;
        this.post.tags = this.post.tags ? this.post.tags.split(",") : [];
      } catch (error) {
        console.log(error.response.data);
      }
    }
  },
  created() {
    if (!this.$store.state.username) {
      this.$router.replace("sign-in");
    }
    this.getPostDetail();
  }
};
</script>
