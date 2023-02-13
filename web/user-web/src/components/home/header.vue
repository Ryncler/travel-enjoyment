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
    <div class="btnd" v-if="isLogin()">
        <el-dropdown :hide-on-click="false" class="dropMenu">
            <span class="el-dropdown-link">
                <el-avatar :size="100" :src="logoUrl" />
                <el-icon class="el-icon--right">
                    <arrow-down />
                </el-icon>
            </span>
            <template #dropdown>
                <el-dropdown-menu>
                    <el-dropdown-item>
                        <icon data="@/icons/edit-user.svg" class="svg-container icon" />
                        <p>编辑个人信息</p>
                    </el-dropdown-item>
                    <el-dropdown-item>
                        <icon data="@/icons/edit-user.svg" class="svg-container icon" />
                        <p>我的游记</p>
                    </el-dropdown-item>
                    <el-dropdown-item>
                        <icon data="@/icons/edit-user.svg" class="svg-container icon" />
                        <p>我的收藏</p>
                    </el-dropdown-item>
                    <el-dropdown-item >
                        <icon data="@/icons/edit-user.svg" class="svg-container icon" />
                        <p>账户设置</p>
                    </el-dropdown-item>
                    <el-dropdown-item divided>
                        <icon data="@/icons/edit-user.svg" class="svg-container icon" />
                        <p>退出</p>
                    </el-dropdown-item>
                </el-dropdown-menu>
            </template>
        </el-dropdown>
    </div>

    <div class="btnd" v-show="!isLogin()">
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
import { ArrowDown } from '@element-plus/icons-vue'
import { onBeforeMount } from '@vue/runtime-core';
import { isLogin } from '@/utils/common'

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
    color: #66CCCC;
    width: 20px;
    height: 20px;
    margin-right: 5px;
}

.example-showcase .el-dropdown-link {
    cursor: pointer;
    color: var(--el-color-primary);
    display: flex;
    align-items: center;
    width: 200px;
    background-color: #66CCCC;
}

.el-dropdown-link {
    display: flex;
    align-items: center;
}

.el-icon--right {
    width: 40px;
    height: 40px;
    margin: 0;
}

.el-icon svg {
    width: 25px;
    height: 25px;
    color: #66CCCC;
}

.dropMenu{
    margin-left: 90px;
}

/deep/ .el-dropdown-menu__item:not(.is-disabled):focus {
    color: #66CCCC;
    background-color: white;
    font-weight: bold;
}
</style>