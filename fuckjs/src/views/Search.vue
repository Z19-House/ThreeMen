<template>
  <q-page class="flex flex-center">Search {{ type }} {{ keyword }}</q-page>
</template>

<script>
// @ is an alias to /src

export default {
  name: "user",
  data() {
    return {
      type: this.$route.query.type,
      keyword: this.$route.query.keyword
    };
  },
  components: {}
};
</script>
