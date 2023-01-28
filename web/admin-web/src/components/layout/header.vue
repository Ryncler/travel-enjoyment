<template>
    <el-row :span="24" class="headerCol">
        <el-menu :default-active="router.currentRoute.value.path" class="elmenu" mode="horizontal" :ellipsis="false"
            router>
            <el-menu-item index="/dashboard" class="logoMenu" style="border-bottom: 0px;">
                <el-image class="logoImg" :src="logoUrl" fit="scale-down" />
            </el-menu-item>

            <div class="flex-grow">
                <el-breadcrumb :separator-icon="ArrowRight">
                    <el-breadcrumb-item v-for="item, index in store.getters['breadcrumb/routerInfos']" :key="index"
                        :to="{ path: item.path }">{{ item.title }}
                    </el-breadcrumb-item>
                </el-breadcrumb>
            </div>
            <el-sub-menu index="/dashboard" class="subMenu">
                <template #title>
                    <el-avatar :size="55" :src="imageHandle(avatarUrl)" />
                </template>
                <el-menu-item index="/">Home</el-menu-item>
                <el-menu-item index="/userinfo">编辑个人信息</el-menu-item>
                <!-- <el-divider /> -->
                <el-menu-item index="/login" @click="goLogout()" divided>退出</el-menu-item>
            </el-sub-menu>
        </el-menu>
    </el-row>
</template>

<script setup>
import { ArrowRight } from '@element-plus/icons-vue'
import { ref } from '@vue/reactivity'
import store from '@/store'
import { imageHandle } from '@/utils/common/index'
import { logout } from '@/api/identity/identity'
import router from '@/router/index'

const avatarUrl = ref(store.getters['identity/userInfo'].avatar)
const logoUrl = require('@/assets/logo.png')

const goLogout = () => {
    store.commit('identity/removeAny')
    logout()
    router.go(0)
}
// eslint-disable-next-line no-undef
defineExpose({
    logoUrl,
})
</script>

<style lang="scss">
.el-row:last-child {
    margin-bottom: 0;
}

.el-row {
    max-height: 100%;
}

.flex-grow {
    float: left;
    position: relative;
    flex-grow: 1;
}

.headerCol {
    height: 60px;
}

.logoImg {
    height: 100%;
    width: 100%;
    max-height: 100%;
    max-width: 100%;
    background-color: white;
}

.logoImg:hover {
    background-color: white;
}

.svg-container {
    color: #66CCCC;
    vertical-align: middle;
    width: 30px;
    display: inline-block;
}

.elmenu {
    height: 100%;
    width: 100%;
    max-height: 100%;
    max-width: 100%;
}

.logoMenu {
    padding: 0%;
    width: 200px;
    float: left;
}

.rightMenu,
.subMenu {
    float: right;
}

.el-dropdown {
    height: 60px;
}

.centreCol {
    height: 100%;
    width: 100%;
    max-height: 100%;
    max-width: 100%;
    box-shadow: 0 1px 4px rgb(0 21 41 / 8%);
}

.el-breadcrumb {
    text-align: center;
    margin: 0 auto;
    position: absolute;
    top: 35%;
    left: 2%;
}
</style>