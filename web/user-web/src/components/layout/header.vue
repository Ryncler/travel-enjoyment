<template>
    <el-menu :default-active="getParent()" class="elmenu" mode="horizontal" :ellipsis="false" router
        active-text-color="#66CCCC">
        <el-menu-item v-for="item in store.getters['menu/menuInfo']" :key="item.name" :index="item.path" class="itemMenu"
            style="border-bottom: 0px;">
            {{ item.title }}
        </el-menu-item>
        <el-menu-item index="/search" :class="!isLogin() ? 'itemMenu searchItem' : 'itemMenu searchItemf'">
            <icon data="@/icons/search.svg" class="svg-container search" />
        </el-menu-item>
        <div class="menu" v-if="isLogin()">
            <el-dropdown :hide-on-click="false" class="dropMenu">
                <span class="el-dropdown-link" @click="goUserInfo()">
                    <el-avatar :size="100" :src="logoUrl" />
                    <el-icon class="el-icon--right">
                        <arrow-down />
                    </el-icon>
                </span>
                <template #dropdown>
                    <el-dropdown-menu>
                        <el-dropdown-item>
                            <icon data="@/icons/edit-user.svg" class="svg-container iconf" />
                            <p class="menuItem">编辑个人信息</p>
                        </el-dropdown-item>
                        <el-dropdown-item>
                            <icon data="@/icons/edit-user.svg" class="svg-container iconf" />
                            <p class="menuItem">我的游记</p>
                        </el-dropdown-item>
                        <el-dropdown-item>
                            <icon data="@/icons/edit-user.svg" class="svg-container iconf" />
                            <p class="menuItem">我的收藏</p>
                        </el-dropdown-item>
                        <el-dropdown-item>
                            <icon data="@/icons/edit-user.svg" class="svg-container iconf" />
                            <p class="menuItem">账户设置</p>
                        </el-dropdown-item>
                        <el-dropdown-item divided>
                            <icon data="@/icons/edit-user.svg" class="svg-container iconf" />
                            <p class="menuItem">退出</p>
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
    </el-menu>
</template>

<script setup>
import { ref } from 'vue';
import { onBeforeMount } from '@vue/runtime-core';
import store from '@/store'
import router from '@/router/index'
import { ArrowDown } from '@element-plus/icons-vue'
import { isLogin } from '@/utils/common'

const logoUrl = require('@/assets/logo.png')
const getParent = () => {
    var routerInfo = router.currentRoute.value
    var result = routerInfo.fullPath.slice(0, routerInfo.fullPath.lastIndexOf('/'))
    return result
}

const goUserInfo = () => {
    router.push({ name: 'User' })
}
</script>

<style scoped>
.elmenu {
    width: 100%;
    height: 100px;
    box-shadow: 0 1px 2px 0;
    border-radius: 5px;
    Border-bottom: 0
}

.el-menu--horizontal .el-menu-item:not(.is-disabled):hover {
    color: #66CCCC;
    background-color: white;
}

.el-menu--horizontal .el-menu-item:not(.is-disabled):focus {
    background-color: white;
}

.itemMenu {
    font-size: 28px;
    width: 300px;
    height: 100px;
}

.menuItem {
    margin-top: 8px;
}

.search {
    width: 100px;
    height: 45px;
}

.searchItem {
    margin-left: 10%;
    width: 200px;
}

.searchItemf {
    margin-left: 15%;
    width: 200px;
}

.btnd {
    margin-top: 30px;
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
    width: 20px;
    height: 20px;
    margin-right: 5px;
}

.iconf {
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



/deep/ .el-dropdown-menu__item:not(.is-disabled):focus {
    color: #66CCCC;
    background-color: white;
    font-weight: bold;
}
</style>