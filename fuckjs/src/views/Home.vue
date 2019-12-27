<template>
  <div>
    <InfinitePostsComponent :url="url"></InfinitePostsComponent>
    <q-page-sticky position="bottom-right" :offset="[18, 18]">
      <q-btn fab icon="add" color="accent" to="/new-post" />
    </q-page-sticky>
    <!-- place QPageScroller at end of page -->
    <q-page-scroller position="bottom-right" :scroll-offset="150" :offset="[18, 90]">
      <q-btn fab icon="keyboard_arrow_up" color="accent" />
    </q-page-scroller>
  </div>
</template>


<script>
// @ is an alias to /src
import InfinitePostsComponent from "@/components/InfinitePostsComponent.vue";

export default {
  name: "home",
  components: {
    InfinitePostsComponent
  },
  props: {
    reloadDate: Date
  },
  watch: {
    reloadDate() {
      this.url = `/post#${this.reloadDate}`;
    }
  },
  data() {
    return {
      url: "/post"
    };
  },
  methods: {}
};
</script>
