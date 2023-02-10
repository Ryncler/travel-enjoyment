<template>
    <el-image style="width: 130px; height: 130px" :src="logoUrl" :fit="scale - down" />
    <div>
        <el-menu default-active="/" class="elmenu" mode="horizontal" :ellipsis="false" router
            active-text-color="#66CCCC">
            <el-menu-item v-for="item in store.getters['menu/menuInfo']" :key="item.name" :index="item.path"
                class="itemMenu" style="border-bottom: 0px;">
                {{ item.title }}
            </el-menu-item>
            <el-menu-item index="/search" class="itemMenu">
                <icon data="@/icons/search.svg" class="svg-container search" />
            </el-menu-item>
        </el-menu>
    </div>
    <div class="btnd">
        <el-button round type="primary" class=" btn">
            <icon data="@/icons/register.svg" class="svg-container icon" />
            注册
        </el-button>
        <el-button round type="primary" class="rigthbtn btn">
            <icon data="@/icons/login.svg" class="svg-container icon" />
            登录
        </el-button>
    </div>
</template>

<script setup>
import { ref } from 'vue';
import store from '@/store'
import { onBeforeMount } from '@vue/runtime-core';

const logoUrl = require('@/assets/logo.png')
</script>

<style scoped>
.elmenu {
    width: 920px;
    height: 120px;
    box-shadow: 0 1px 6px 0;
    border-radius: 5px;
    Border-bottom: 0
}

.itemMenu {
    font-size: 28px;
    width: 184px;
    height: 120px;
}

.el-menu--horizontal .el-menu-item:not(.is-disabled):hover {
    color: #66CCCC;
    background-color: white;
}

.search {
    width: 100px;
    height: 45px;
}

.btnd {
    margin: 0 0 0 20px;
}

.btn {
    width: 100px;
    height: 40px;
    font-size: 15px;
    font-weight: bold;
    background-color: #66CCFF;
    border: 1px solid #66CCFF;
}

.btn:hover {
    transition: all 0.5s;
    background-color: #66CCCC;
    border: 1px solid #66CCCC;
}

.btn:focus {
    transition: all 0.5s;
    background-color: #66CCCC;
    border: 1px solid #66CCCC;
}

.rigthbtn {
    margin-left: 30px;
}

.icon {
    color: white;
}
</style>