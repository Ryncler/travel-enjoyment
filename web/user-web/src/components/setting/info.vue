<template>
    <el-row :gutter="0">
        <el-col :span="4">
            <el-avatar class="avatar" :size="150" src="https://empty" @error="errorHandler">
                <img :src="userData.avatar" />
            </el-avatar>
            <h4 class="username">{{ userData.userName }}</h4>
            <el-divider />
            <el-tabs v-model="tabs" tab-position="left" style="height: 200px" @tab-click="changeTabs">
                <el-tab-pane name="editInfo">
                    <template #label>
                        <div class="custom-tabs-label">
                            <icon data="@/icons/profile.svg"
                                :class="editInfoActive !== true ? 'svg-container icon' : 'svg-container is-activeIcon'" />
                            <h5>编辑个人资料</h5>
                        </div>
                    </template>
                </el-tab-pane>
                <el-tab-pane name="editPassword">
                    <template #label>
                        <div class="custom-tabs-label">
                            <icon data="@/icons/password.svg"
                                :class="editPasswordActive !== true ? 'svg-container icon' : 'svg-container is-activeIcon'" />
                            <h5>修改密码</h5>
                        </div>
                    </template>
                </el-tab-pane>
                <el-tab-pane name="other">
                    <template #label>
                        <div class="custom-tabs-label">
                            <icon data="@/icons/systemsetting.svg"
                                :class="otherActive !== true ? 'svg-container icon' : 'svg-container is-activeIcon'" />
                            <h5>其他设置</h5>
                        </div>
                    </template>
                </el-tab-pane>
            </el-tabs>
        </el-col>
        <el-col :span="19" :offset="1">
            <editInfoVue v-if="editInfoActive"></editInfoVue>
            <editPasswordVue v-if="editPasswordActive"></editPasswordVue>
            <editSetiingVue v-if="otherActive"></editSetiingVue>
        </el-col>
    </el-row>
</template>

<script setup>
import { ref } from 'vue';
import { onBeforeMount } from '@vue/runtime-core';
import editInfoVue from './editInfo'
import editPasswordVue from './editPassword'
import editSetiingVue from './editSetiing'

const tabs = ref('editInfo')
const editInfoActive = ref(true)
const editPasswordActive = ref(false)
const otherActive = ref(false)
const userData = ref({
    avatar: 'https://www.otsuka.co.jp/img/index_im01_01.jpg.webp',
    userName: 'Ryncler',
    profile: '一个逗比',
    travelNum: 24,
    starNum: 10
})

const changeTabs = (tab, event) => {
    if (tab.paneName === 'editInfo') {
        editInfoActive.value = true
        editPasswordActive.value = false
        otherActive.value = false
    }
    if (tab.paneName === 'editPassword') {
        editPasswordActive.value = true
        editInfoActive.value = false
        otherActive.value = false
    }
    if (tab.paneName === 'other') {
        otherActive.value = true
        editInfoActive.value = false
        editPasswordActive.value = false
    }
}
</script>

<style scoped>
.avatar {
    margin-left: 15%;
}

h5 {
    font-weight: bold;
}

.username {
    margin-top: 20px;
    text-align: center;
    font-weight: bold;
}

.custom-tabs-label {
    display: flex;
    margin-left: 20%;
    text-align: center;
}

.icon {
    color: black;
    width: 18px;
    height: 18px;
    margin-top: 8px;
    margin-right: 5px;
}

.is-activeIcon {
    color: #66CCCC;
    width: 18px;
    height: 18px;
    margin-top: 8px;
    margin-right: 5px;
}
</style>


<style>
.el-tabs__item {
    border-radius: 20px;
}

.el-tabs__item:hover {
    color: #66CCCC;
}

.el-tabs__item.is-active {
    color: #66CCCC;
}

.el-tabs__active-bar {
    background-color: #66CCCC;
}

.el-tabs__header {
    width: 100%;
}
</style>