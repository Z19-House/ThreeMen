<template>
  <q-card>
    <q-card-section>
      <div class="row no-wrap">
        <router-link class="my-router-link" :to="{name: 'user', params: {id: user.userId}}">
          <q-avatar size="80px" style="margin-right:16px">
            <q-img
              :src="user.imageUrl"
              :placeholder-src="require('../assets/placeholder.png')"
            />
          </q-avatar>
        </router-link>
        <div class="column col-10 q-col-gutter-md">
          <div>
            <router-link class="my-router-link" :to="{name: 'user', params: {id: user.userId}}">
              <div class="text-h6">{{ user.nickname }}</div>
            </router-link>
            <div class="text-subtitle2">商品：{{ user.postsCount }}</div>
          </div>

          <div
            v-if="$q.platform.is.desktop"
            class="row q-col-gutter-md no-wrap"
            style="margin-top:-8px;"
          >
            <router-link
              class="my-router-link items-start no-wrap row q-gutter-sm"
              v-for="(post, index) in user.posts"
              :key="index"
              :to="{name: 'post', params: {id: post.postId}}"
            >
              <q-img
                :src="post.imageUrl"
                :placeholder-src="require('../assets/placeholder.png')"
                style="height:80px;width:120px"
              />
              <div class="my-text" style="height:80px;width:80px;">{{ post.title }}</div>
            </router-link>
          </div>
        </div>
      </div>
    </q-card-section>
  </q-card>
</template>

<script>
// @ is an alias to /src

export default {
  name: "UserComponent",
  components: {},
  props: {
    user: Object
  },
  data() {
    return {};
  },
  methods: {
    onCardClick() {
      this.$router.push({ name: "user", params: { id: this.user.userId } });
    }
  }
};
</script>

<style scoped>
.my-router-link {
  text-decoration: none;
}
.my-text {
  text-overflow: ellipsis;
  white-space: nowrap;
  overflow: hidden;
}
</style>