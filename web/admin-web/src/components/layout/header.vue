<template>
    <el-row :span="24" class="headerCol">
        <el-menu :default-active="activeIndex" class="elmenu" mode="horizontal" :ellipsis="false" @select="selectItem">
            <el-menu-item index="0" class="logoMenu" style="border-bottom: 0px;">
                <el-image class="logoImg" :src="logoUrl" :fit="fit" />
            </el-menu-item>

            <div class="flex-grow">
                <el-breadcrumb :separator-icon="ArrowRight">
                    <el-breadcrumb-item v-for="item, index in store.getters['breadcrumb/routerInfos']" :key="index"
                        :to="{ path: item.path }">{{
                            item.title
                        }}</el-breadcrumb-item>
                </el-breadcrumb>
            </div>
            <el-sub-menu index="1" class="subMenu">
                <template #title>
                    <el-avatar :size="55" :src="imageHandle(avatarUrl)" />
                </template>
                <el-menu-item index="1-1">Home</el-menu-item>
                <el-menu-item index="1-2">编辑个人信息</el-menu-item>
                <!-- <el-divider /> -->
                <el-menu-item index="1-3" divided>退出</el-menu-item>
            </el-sub-menu>
        </el-menu>
    </el-row>
</template>

<script setup>
import { ArrowRight } from '@element-plus/icons-vue'
import menuManage from '@/utils/menuManage'
import { ref } from '@vue/reactivity'
import store from '@/store'
import { imageHandle } from '@/utils/common/index'

const avatarUrl = ref(store.getters['identity/userInfo'].avatar)
const logoUrl = ref('https://img-prod-cms-rt-microsoft-com.akamaized.net/cms/api/am/imageFileData/RE4wHgx?ver=5481')

const selectItem = (key, keyPath) => {
    menuManage.linkRouterByPath('header', keyPath)
}

// eslint-disable-next-line no-undef
defineExpose({
    logoUrl, selectItem,
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
    object-fit: cover;
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