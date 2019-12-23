<template>
  <q-card>
    <q-img
      v-if="post.imageUrl"
      @click="onCardClick"
      :src="post.imageUrl"
      :ratio="4/3"
      spinner-color="red"
      placeholder-src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAJYAAACWBAMAAADOL2zRAAAAG1BMVEXMzMyWlpaqqqq3t7fFxcW+vr6xsbGjo6OcnJyLKnDGAAAACXBIWXMAAA7EAAAOxAGVKw4bAAABAElEQVRoge3SMW+DMBiE4YsxJqMJtHOTITPeOsLQnaodGImEUMZEkZhRUqn92f0MaTubtfeMh/QGHANEREREREREREREtIJJ0xbH299kp8l8FaGtLdTQ19HjofxZlJ0m1+eBKZcikd9PWtXC5DoDotRO04B9YOvFIXmXLy2jEbiqE6Df7DTleA5socLqvEFVxtJyrpZFWz/pHM2CVte0lS8g2eDe6prOyqPglhzROL+Xye4tmT4WvRcQ2/m81p+/rdguOi8Hc5L/8Qk4vhZzy08DduGt9eVQyP2qoTM1zi0/uf4hvBWf5c77e69Gf798y08L7j0RERERERERERH9P99ZpSVRivB/rgAAAABJRU5ErkJggg=="
    >
      <template v-slot:error>
        <div class="absolute-full flex flex-center bg-negative text-white">Cannot load image</div>
      </template>
      <div class="absolute-bottom text-h6">{{ post.title }}</div>
    </q-img>
    <q-card-section v-else @click="onCardClick">
      <div class="text-h6">{{ post.title }}</div>
    </q-card-section>

    <q-card-section>
      <div @click="onCardClick" class="text-subtitle2 text-red">￥{{ post.price }}</div>
      <div @click="onCardClick" class="text-subtitle2">{{ post.content }}</div>
      <div v-if="post.tags.length != 0">
        <q-chip
          v-for="(tag, index) in tags"
          :key="index"
          clickable
          @click="onTagClick(tag)"
          dense
        >#{{ tag }}</q-chip>
      </div>
    </q-card-section>

    <q-separator />

    <q-item :to="{name: 'user', params: {id: post.userId}}">
      <q-item-section avatar>
        <q-avatar size="2.2rem" >
          <q-img :src="post.userImageUrl" />
        </q-avatar>
      </q-item-section>

      <q-item-section>
        <q-item-label>{{ post.userNickname }}</q-item-label>
        <q-item-label caption>{{ post.address }}</q-item-label>
      </q-item-section>
    </q-item>
  </q-card>
</template>

<script>
// @ is an alias to /src

export default {
  name: "PostComponent",
  components: {},
  props: {
    post: Object
  },
  data() {
    return {
      tags: this.post.tags.split(",")
    };
  },
  methods: {
    onTagClick(tag) {
      console.log(this.tags);
      this.$router.push({
        name: "search",
        query: { type: "tag", keyword: tag }
      });
    },
    onCardClick() {
      this.$router.push({ name: "post", params: { id: this.post.postId } });
    }
  }
};
</script>
