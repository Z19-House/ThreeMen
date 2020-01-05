<template>
  <q-pull-to-refresh @refresh="refresh">
    <q-infinite-scroll ref="infiniteScroll" @load="onLoad" :offset="250">
      <div class="q-pa-md row items-stretch q-col-gutter-md">
        <div
          v-for="(item, index) in items"
          :key="index"
          class="col-12"
        >
          <UserComponent :user="item" />
        </div>
      </div>
      <template v-slot:loading>
        <div class="row justify-center q-my-md">
          <q-spinner color="primary" name="dots" size="40px" />
        </div>
      </template>
    </q-infinite-scroll>
  </q-pull-to-refresh>
</template>

<script>
import api from "@/api";
import UserComponent from "@/components/UserComponent.vue";

export default {
  name: "infinite-posts-component",
  components: {
    UserComponent
  },
  props: {
    url: String
  },
  data() {
    return {
      items: [],
      skip: 0,
      take: 20,
      datetime: new Date().toUTCString()
    };
  },
  watch: {
    async url() {
      this.resetLimit();
      await this.loadMoreData();
    }
  },
  methods: {
    async onLoad(index, done) {
      done(await this.loadMoreData());
    },
    async refresh(done) {
      this.resetLimit();
      await this.loadMoreData();
      this.$refs.infiniteScroll.resume();
      this.$refs.infiniteScroll.poll();
      done();
    },
    resetLimit() {
      this.datetime = new Date().toUTCString();
      this.skip = 0;
      this.items = [];
    },
    async loadMoreData() {
      try {
        let response = await api.getPosts(
          this.url,
          this.datetime,
          this.skip,
          this.take
        );
        Array.from(response.data.data).forEach(it => this.items.push(it));
        this.skip += this.take;
        if (response.data.count <= this.skip) {
          // Load complete.
          return true;
        }
        return false;
      } catch (error) {
        console.log(error);
        setTimeout(() => {
          return false;
        }, 3000);
      }
    }
  }
};
</script>