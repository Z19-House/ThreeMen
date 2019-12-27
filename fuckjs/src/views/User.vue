<template>
  <div v-if="user">
    <div style="position:sticky;top:0px;z-index:3000;background-color:white;">
      <q-item>
        <q-item-section avatar>
          <q-avatar size="68px">
            <q-img :src="user.imageUrl" />
          </q-avatar>
        </q-item-section>

        <q-item-section>
          <q-item-label class="text-h6">
            {{ user.nickname }}
            <span class="text-subtitle2" style="color:gray">@{{ user.username }}</span>
          </q-item-label>
          <q-item-label class="text-subtitle2">手机：{{ user.phone }}</q-item-label>
          <q-item-label class="text-subtitle2">生日：{{ user.birthDate }}</q-item-label>
        </q-item-section>
        <q-item-section>
          <q-item-label style="height:26px"></q-item-label>
          <q-item-label class="text-subtitle2">性别：{{ user.sex }}</q-item-label>
          <q-item-label class="text-subtitle2">地址：{{ user.address }}</q-item-label>
        </q-item-section>
      </q-item>

      <q-tabs
        v-model="tab"
        dense
        class="text-grey"
        active-color="primary"
        indicator-color="primary"
        align="justify"
      >
        <q-tab name="posts" label="商品" />
        <q-tab name="collection" label="收藏" />
      </q-tabs>

      <q-separator />
    </div>

    <InfinitePostsComponent :url="getUrl()"></InfinitePostsComponent>
  </div>
</template>

<script>
// @ is an alias to /src
import InfinitePostsComponent from "@/components/InfinitePostsComponent.vue";
import api from "@/api";

//import PostComponent from "@/components/PostComponent.vue";

export default {
  name: "user",
  props: {
    id: [String, Number]
  },
  components: {
    InfinitePostsComponent
  },
  data() {
    return {
      tab: "posts",
      user: {}
    };
  },
  watch: {
    id() {
      this.getUserInfo();
    }
  },
  methods: {
    getUrl() {
      return "/user/" + this.id + "/" + this.tab;
    },
    async getUserInfo() {
      try {
        let response = await api.getUserInfo(this.id);
        this.user = response.data;
      } catch (error) {
        console.log(error.response.data);
      }
    }
  },
  created() {
    this.getUserInfo();
  }
};
</script>
