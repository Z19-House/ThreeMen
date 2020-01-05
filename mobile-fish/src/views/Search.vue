<template>
  <div>
    <div style="position:sticky;top:0px;z-index:3000;background-color:white;">
      <q-input
        dense
        standout
        outlined
        debounce="1000"
        v-model="keyword"
        v-on:keyup.enter="search"
        class="q-ml-md"
        style="margin:0rem;padding:1rem"
      >
        <template v-slot:append>
          <q-icon v-if="keyword === ''" name="search" />
          <q-icon v-else name="clear" class="cursor-pointer" @click="keyword = ''" />
        </template>
      </q-input>

      <q-tabs
        v-model="tab"
        dense
        class="text-grey"
        active-color="primary"
        indicator-color="primary"
        align="justify"
      >
        <q-tab name="posts" label="商品" />
        <q-tab name="tags" label="标签" />
        <q-tab name="user" label="用户" />
      </q-tabs>
      <q-separator />
    </div>

    <InfinitePostsComponent v-if="tab=='posts' || tab=='tags'" :url="getUrl()"></InfinitePostsComponent>
    <InfiniteUsersComponent v-else :url="getUrl()"></InfiniteUsersComponent>
  </div>
</template>

<script>
// @ is an alias to /src
import InfinitePostsComponent from "@/components/InfinitePostsComponent.vue";
import InfiniteUsersComponent from "@/components/InfiniteUsersComponent.vue";

export default {
  name: "search",
  components: {
    InfinitePostsComponent,
    InfiniteUsersComponent
  },
  data() {
    return {
      tab: this.$route.query.type,
      keyword: this.$route.query.keyword
    };
  },
  computed: {},
  watch: {
    tab(newVal) {
      if (newVal != this.$route.query.type) {
        this.$router.push({
          name: "search",
          query: { type: this.tab, keyword: this.keyword }
        });
      }
    },
    $route(to) {
      if (to.name == "search") {
        this.tab = to.query.type;
        this.keyword = to.query.keyword;
      }
    }
  },
  methods: {
    getUrl() {
      return "/search/" + this.tab + "/" + this.keyword;
    },
    search() {
      this.$router.push({
        name: "search",
        query: { type: this.tab, keyword: this.keyword }
      });
    }
  }
};
</script>
