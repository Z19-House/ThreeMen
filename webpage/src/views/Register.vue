<template>
  <div class="register">
    <registerHead />
    <el-divider style="width:80%;">
      <span style="font-size:40px;">注册</span>
    </el-divider>
    <div class="body">
    <registerWindow v-if="!username"/>
    <userInformation v-else :isDisplay="2" :username="username" />
    </div>
  </div>
</template>

<style>
.register .body{
    margin: 100px auto 0;
  width: 520px;

}
</style>

<script>
import registerHead from "@/components/Register/Head.vue";
import registerWindow from '@/components/Register/RegisterWindow.vue'
import userInformation from '@/components/global/UserInformation.vue'

export default {
  components: {
    registerHead,
    registerWindow,
    userInformation
  },
  computed: {
    username(){
      return this.$store.state.username;
    }
  }
};
</script>