<template>
  <q-card>
    <q-card-section>
      <div class="row no-wrap">
        <router-link class="my-router-link" :to="{name: 'user', params: {id: user.userId}}">
          <q-avatar size="80px" style="margin-right:16px">
            <q-img
              :src="user.imageUrl"
              placeholder-src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAJYAAACWBAMAAADOL2zRAAAAG1BMVEXMzMyWlpaqqqq3t7fFxcW+vr6xsbGjo6OcnJyLKnDGAAAACXBIWXMAAA7EAAAOxAGVKw4bAAABAElEQVRoge3SMW+DMBiE4YsxJqMJtHOTITPeOsLQnaodGImEUMZEkZhRUqn92f0MaTubtfeMh/QGHANEREREREREREREtIJJ0xbH299kp8l8FaGtLdTQ19HjofxZlJ0m1+eBKZcikd9PWtXC5DoDotRO04B9YOvFIXmXLy2jEbiqE6Df7DTleA5socLqvEFVxtJyrpZFWz/pHM2CVte0lS8g2eDe6prOyqPglhzROL+Xye4tmT4WvRcQ2/m81p+/rdguOi8Hc5L/8Qk4vhZzy08DduGt9eVQyP2qoTM1zi0/uf4hvBWf5c77e69Gf798y08L7j0RERERERERERH9P99ZpSVRivB/rgAAAABJRU5ErkJggg=="
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
                placeholder-src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAJYAAACWBAMAAADOL2zRAAAAG1BMVEXMzMyWlpaqqqq3t7fFxcW+vr6xsbGjo6OcnJyLKnDGAAAACXBIWXMAAA7EAAAOxAGVKw4bAAABAElEQVRoge3SMW+DMBiE4YsxJqMJtHOTITPeOsLQnaodGImEUMZEkZhRUqn92f0MaTubtfeMh/QGHANEREREREREREREtIJJ0xbH299kp8l8FaGtLdTQ19HjofxZlJ0m1+eBKZcikd9PWtXC5DoDotRO04B9YOvFIXmXLy2jEbiqE6Df7DTleA5socLqvEFVxtJyrpZFWz/pHM2CVte0lS8g2eDe6prOyqPglhzROL+Xye4tmT4WvRcQ2/m81p+/rdguOi8Hc5L/8Qk4vhZzy08DduGt9eVQyP2qoTM1zi0/uf4hvBWf5c77e69Gf798y08L7j0RERERERERERH9P99ZpSVRivB/rgAAAABJRU5ErkJggg=="
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