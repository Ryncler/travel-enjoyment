<template>
    <el-row :gutter="0">
        <el-col :span="4">
            <div class="info">
                <el-avatar class="avatar" :size="150" src="https://empty" @error="errorHandler">
                    <img :src="travelData.avatar" />
                </el-avatar>
                <h4 class="username">{{ travelData.userName }}</h4>
                <el-divider />
                <h5>出行日期：</h5>
                <h5 class="center">{{ travelData.departureTime }}</h5>
                <h5>出行天数：</h5>
                <h5 class="center">{{ travelData.travelDayNum }}天</h5>
                <h5>游记目录：</h5>
                <el-timeline>
                    <el-timeline-item v-for="(item, index) in travelData.contents" :key="index" type="primary"
                        hollow="true">
                        <h5>{{ item }}</h5>
                    </el-timeline-item>
                </el-timeline>
            </div>
        </el-col>
        <el-col class="contenCol" :span="19" :offset="1">
            <div class="otherInfo">
                <div class="otherItem firstItem">
                    <icon data="@/icons/address.svg" class="svg-container otherIcon" />
                    <p class="author">{{ travelData.ip }}</p>
                </div>
                <div class="otherItem">
                    <icon data="@/icons/comment.svg" class="svg-container otherIcon" />
                    <p class="author">{{ travelData.comment }} 评论</p>
                </div>
                <div class="otherItem">
                    <icon data="@/icons/star.svg" class="svg-container otherIcon" />
                    <p class="author">{{ travelData.star }} 收藏</p>
                </div>
                <div class="otherItem">
                    <icon data="@/icons/heat.svg" class="svg-container otherIcon" />
                    <p class="author">{{ travelData.heat }} 热度</p>
                </div>
            </div>
            <el-divider />
            <h2 class="center">{{ travelData.travelsTitle }}</h2>
            <p class="content" v-html="travelData.content"></p>

            <commentVue></commentVue>
        </el-col>
    </el-row>
</template>

<script setup>
import { ref } from 'vue';
import { onBeforeMount } from '@vue/runtime-core';
import commentVue from '@/components/common/comment'

const travelData = ref({
    avatar: 'https://www.otsuka.co.jp/img/index_im01_01.jpg.webp',
    userName: 'Ryncler',
    departureTime: '2022.1.1',
    travelsTitle: '七年之痒，重归贡嘎。念念不忘，终有回响',
    ip: '广西',
    comment: 2340,
    star: 2394,
    heat: 23401,
    travelDayNum: 4,
    contents: [
        '前言',
        '九寨沟第一天',
        '九寨沟第二天',
    ],
    content: '<p>asdfaoiswfnjawioefnawfiawpfnpaosifnjawio</p><h1>sadfasifhnjsaiufenhaowsiufeahw</h1>'
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
    text-align: center;
}

h5,
h2 {
    font-weight: bold;
}

.contenCol {
    margin-top: 30px;
}

.otherInfo {
    flex: 1;
    margin: 0;
    /* margin-left: 20px; */
    display: flex;
    margin: 10px;
    margin-top: 30px;
    align-content: center;
    justify-content: center;
}

.otherIcon {
    margin-top: 2px;
}

.otherItem {
    display: flex;
    margin-left: 80px;
}

.firstItem {
    margin-left: 300px;
}

.content {
    margin-top: 30px;
}

</style>