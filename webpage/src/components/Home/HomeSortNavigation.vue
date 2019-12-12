<template>
    <div id="homeSortNavigation">
        <el-menu
            class="el-menu-demo "
            mode="horizontal"
            background-color="#545c64"
            text-color="#fff"
            active-text-color="#ffd04b">
            <el-menu-item index="1-1">选项1</el-menu-item>
            <el-menu-item index="1-2">选项2</el-menu-item>
            <el-menu-item index="1-3">选项3</el-menu-item>
            <el-menu-item index="2-1">选项1</el-menu-item>
            <el-menu-item index="2-2">选项2</el-menu-item>
            <el-menu-item index="2-3">选项3</el-menu-item>
            <el-menu-item index="3-1">选项1</el-menu-item>
            <el-menu-item index="3-2">选项2</el-menu-item>
            <el-menu-item index="3-3">选项3</el-menu-item>
        </el-menu>
    </div>
</template>

<style>
.el-menu--horizontal .el-menu{
    float: none;
    height: 23px;
    line-height: 23px;
    width: 100%;
} 
 .el-menu-item {
     width:auto;
 }
</style>