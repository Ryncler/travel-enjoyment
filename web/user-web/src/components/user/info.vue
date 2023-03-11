<template>
    <el-row :gutter="0">
        <el-col :span="4">
            <div class="info">
                <el-avatar class="avatar" :size="150" src="https://empty" @error="errorHandler">
                    <img :src="imageHandle(userData.avatar)" />
                </el-avatar>
                <h4 class="username">{{ userData.userName }}</h4>
                <h5 class="center">{{ userData.profile }}</h5>
                <el-button round type="primary" class=" btn" @click="showCommentDrawer()">编辑资料</el-button>
                <el-divider />
                <h5 class="center">成就</h5>
                <div class="otherInfo">
                    <div class="otherItem">
                        <icon data="@/icons/comment.svg" class="svg-container otherIcon" />
                        <p class="author">游记：{{ userTravelNum }} </p>
                    </div>
                    <div class="otherItem">
                        <icon data="@/icons/star.svg" class="svg-container otherIcon" />
                        <p class="author">收藏：{{ userStarNum }}</p>
                    </div>
                </div>
            </div>
        </el-col>
        <el-col class="contenCol" :span="19" :offset="1">
            <el-tabs v-model="tabs" class="sightsTabs" @tab-click="changeTabs">
                <el-tab-pane name="overview">
                    <template #label>
                        <div class="custom-tabs-label">
                            <icon data="@/icons/profile.svg"
                                :class="overviewActive !== true ? 'svg-container icon' : 'svg-container is-activeIcon'" />
                            <h3>概览</h3>
                        </div>
                    </template>
                </el-tab-pane>
                <el-tab-pane name="myTravel">
                    <template #label>
                        <div class="custom-tabs-label">
                            <icon data="@/icons/travel.svg"
                                :class="myTravelActive !== true ? 'svg-container icon' : 'svg-container is-activeIcon'" />
                            <h3>我的游记</h3>
                        </div>
                    </template>
                </el-tab-pane>
                <el-tab-pane name="myStar">
                    <template #label>
                        <div class="custom-tabs-label">
                            <icon data="@/icons/star.svg"
                                :class="myStarActive !== true ? 'svg-container icon' : 'svg-container is-activeIcon'" />
                            <h3>我的收藏</h3>
                        </div>
                    </template>
                </el-tab-pane>
            </el-tabs>

            <overviewVue v-if="overviewActive"></overviewVue>
            <myTravelVue v-if="myTravelActive"></myTravelVue>
            <myStarVue v-if="myStarActive"></myStarVue>
        </el-col>
    </el-row>
</template>

<script setup>
import { ref } from 'vue';
import store from '@/store'
import { useRouter } from 'vue-router';
import { onBeforeMount } from '@vue/runtime-core';
import overviewVue from './overview'
import myTravelVue from './myTravel'
import myStarVue from './myStar'
import { imageHandle } from '@/utils/common/index'
import { getStarCountByUserId, getTravelCountByUserId } from '@/api/identity/user'

const overviewActive = ref(true)
const myTravelActive = ref(false)
const myStarActive = ref(false)
const tabs = ref('overview')

const router = useRouter()
const userData = ref(store.getters['identity/userInfo'])
const userStarNum = ref(0)
const userTravelNum = ref(0)

const changeTabs = (tab, event) => {
    if (tab.paneName === 'overview') {
        overviewActive.value = true
        myTravelActive.value = false
        myStarActive.value = false
    }
    if (tab.paneName === 'myTravel') {
        myTravelActive.value = true
        overviewActive.value = false
        myStarActive.value = false
    }
    if (tab.paneName === 'myStar') {
        myStarActive.value = true
        overviewActive.value = false
        myTravelActive.value = false
    }
}
const changeTabsByRouter = () => {
    var isTravel = router.currentRoute.value.query.travel
    var isStar = router.currentRoute.value.query.star
    if (isTravel !== undefined && isTravel !== '' && isTravel !== null) {
        tabs.value = 'myTravel'
        myTravelActive.value = true
        overviewActive.value = false
        myStarActive.value = false
        return
    } if (isStar !== undefined && isStar !== '' && isStar !== null) {
        tabs.value = 'myStar'
        myStarActive.value = true
        overviewActive.value = false
        myTravelActive.value = false
        return
    }
}

const getStarAndTravelNum = () => {
    getStarCountByUserId(userData.value.id).then(res => {
        if (res.status === 200) {
            userStarNum.value = res.data
        }
    })
    getTravelCountByUserId(userData.value.id).then(res => {
        if (res.status === 200) {
            userTravelNum.value = res.data
        }
    })
}
onBeforeMount(() => {
    changeTabsByRouter()
    getStarAndTravelNum()
})
</script>

<style scoped>
.avatar {
    margin-left: 15%;
}

.username {
    margin-top: 20px;
    text-align: center;
    font-weight: bold;
}

.center {
    margin-top: 30px;
    text-align: center;
}

h5,
h2 {
    font-weight: bold;
}

.otherInfo {
    width: 100%;
    height: 100%;
    flex: 1;
    margin: 0;
    /* margin-left: 20px; */
    display: inline-block;
    margin: 10px;
    text-align: center;
    vertical-align: middle;
}


.otherItem {
    margin-left: 20%;
    display: flex;
    text-align: center;
    vertical-align: middle;
}

.btn {
    width: 150px;
    height: 35px;
    font-size: 14px;
    font-weight: bold;
    margin-top: 30px;
    margin-left: 15%;
    background-color: #66CCCC;
    border: 1px solid #66CCCC;
    text-align: center;
}

.btn:hover {
    transition: all 0.5s;
    color: white;
    background-color: #66CCFF;
    border: 1px solid #66CCFF;
}

.btn:focus {
    outline: 0;
    transition: all 0.5s;
    color: white;
    background-color: #66CCFF;
    border: 1px solid #66CCFF;
}

h3 {
    margin: 0;
    margin-left: 5px;
    margin-top: 2px;
}

.custom-tabs-label {
    display: flex;
}

.icon {
    color: black;
    width: 30px;
    height: 30px;
}

.is-activeIcon {

    color: #66CCCC;
    width: 30px;
    height: 30px;
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
</style>