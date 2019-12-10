<template>
  <div class="home">
    <homeHead/>
    <div id="homeBody">
      <homeUser/>
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import homeHead from '@/components/Home/HomeHead.vue'
import homeUser from '@/components/Home/HomeUser.vue'

export default {

  name: 'home',
  components:{
    homeHead:homeHead,
    homeUser:homeUser
  },
  data(){
    return {

    }
  }
}
</script>
<style>

#homeBody{
  height: 1000px;
  margin: 10px 10% 5px;
  background-color: aqua;
}
</style>