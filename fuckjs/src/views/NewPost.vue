<template>
  <q-page class="flex flex-center">
    <q-input
        @input="selectFiles"
        multiple
        filled
        type="file"
        hint="Native file (multiple)"
      />
    New post
  </q-page>
</template>

<script>
// @ is an alias to /src

export default {
  name: 'new-post',
  components: {
  },
  data () {
    return {
      files: null
    }
  },
  methods: {
    selectFiles(val) {
      val.forEach(file => {
        console.log(file);
      });
    }
  }
}
</script>
