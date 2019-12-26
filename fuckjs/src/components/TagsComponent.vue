<template>
  <div v-if="tags.length != 0">
    <q-chip
      v-for="(tag, index) in splitedTags"
      :key="index"
      clickable
      @click="onTagClick(tag)"
      dense
    >#{{ tag }}</q-chip>
  </div>
</template>

<script>
export default {
  name: "TagsComponent",
  props: {
    tags: String
  },
  data() {
    return {
      splitedTags: this.tags.split(",")
    };
  },
  methods: {
    onTagClick(tag) {
      this.$router.push({
        name: "search",
        query: { type: "tag", keyword: tag }
      });
    }
  }
};
</script>