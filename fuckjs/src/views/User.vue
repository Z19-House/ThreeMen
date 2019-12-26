<template>
  <q-card v-if="user">
    <q-item>
      <q-item-section avatar>
        <q-avatar size="8rem">
          <q-img :src="user.imageUrl" />
        </q-avatar>
      </q-item-section>

      <q-item-section>
        <q-item-label class="text-h6">{{ user.nickname }}</q-item-label>
        <q-item-label caption>@{{ user.username }}</q-item-label>
        <q-item-label class="text-subtitle2">手机：{{ user.phone }}</q-item-label>
        <q-item-label class="text-subtitle2">生日：{{ user.birthDate }}</q-item-label>
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
      narrow-indicator
    >
      <q-tab name="posts" label="商品" />
      <q-tab name="collection" label="收藏" />
    </q-tabs>

    <q-separator />

    <q-tab-panels v-model="tab" ref="tabPanels" animated :style="contentHeight">
      <q-tab-panel name="posts">
        <InfinitePostsComponent :url="getUrl()"></InfinitePostsComponent>
      </q-tab-panel>
      <q-tab-panel name="collection">
        <InfinitePostsComponent :url="getUrl()"></InfinitePostsComponent>
      </q-tab-panel>
    </q-tab-panels>
  </q-card>
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
      user: {},
      contentHeight: { height: "" }
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
    setScrollHeight() {
      if (this.$refs.tabPanels) {
        setTimeout(() => {
          this.contentHeight.height =
            document.documentElement.clientHeight -
            this.$refs.tabPanels.$el.offsetTop -
            50 +
            "px";
        }, 60);
      }
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
    window.addEventListener("resize", this.setScrollHeight);
  },
  mounted() {
    this.setScrollHeight();
  },
  destroyed() {
    window.removeEventListener("resize", this.setScrollHeight);
  }
};
</script>
