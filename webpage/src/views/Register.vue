<template>
    <div class="register">
        <registerHead/>
        <el-divider style="width:80%"><span style="font-size:40px">注册</span></el-divider>
            <registerWindow/>
    </div>
</template>

<style>

</style>

<script>
import registerHead from '@/components/Register/Head.vue'
import registerWindow from '@/components/Register/RegisterWindow.vue'


export default {
    components:{
        registerHead:registerHead,
        registerWindow:registerWindow
    }
}
</script>