<template>
    <div id="homeUser">
        <div class="demo-basic--circle">
            <div class="block">
                <el-avatar :size="80" :src="userImge" @error="errorHandler">
                    <img src="../../errorImage/errorImage.png"/>
                </el-avatar>
            </div>
        </div>
    </div>
</template>

<style>
#homeUser{
    height: 90px;
}
</style>

<script>
export default {
    data(){
        return{
            userImage:require( '../../assets/logo.png'),

        }
    },
    methods: {
      errorHandler() {
        return true
      }
    }
}
</script>