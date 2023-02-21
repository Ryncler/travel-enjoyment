<template>
    <el-row :gutter="0">
        <el-col :span="4">
            <h4>精选游记</h4>
        </el-col>
        <el-col :span="4" :offset="16">
            <p class="customTravel">自定义精选游记</p>
        </el-col>
    </el-row>
    <el-row :gutter="0">
        <el-col :span="11">
            <el-card class="travel-card" :body-style="style" v-for="travel in travelList" :key="travel.id" shadow="hover">
                <div class="travel-info">
                    <el-image :src="travel.imgUrl" :fit="fill" class="travel-img" />
                    <div class="content">
                        <h4 class="travel-name">
                            <icon data="@/icons/edit.svg" class="svg-container" />
                            {{ travel.name }}
                        </h4>
                        <p class="travel-contentInfo">{{ travel.content }}</p>
                        <div class="otherInfo">
                            <div class="otherItem">
                                <icon data="@/icons/heat.svg" class="svg-container otherIcon" />
                                <p>{{ travel.heat }}</p>
                            </div>
                            <div class="otherItem commentItem">
                                <icon data="@/icons/comment.svg" class="svg-container otherIcon" />
                                <p>{{ travel.comment }}</p>
                            </div>
                            <div class="otherItem starItem">
                                <icon data="@/icons/star.svg" class="svg-container otherIcon" />
                                <p>{{ travel.star }}</p>
                            </div>
                        </div>
                    </div>
                </div>
            </el-card>
        </el-col>
        <el-col :span="11" class="col">
            <el-card class="travel-card" :body-style="style" v-for="travel in travelList" :key="travel.id" shadow="hover">
                <div class="travel-info">
                    <el-image :src="travel.imgUrl" :fit="fill" class="travel-img" />
                    <div class="content">
                        <h4 class="travel-name">
                            <icon data="@/icons/edit.svg" class="svg-container" />
                            {{ travel.name }}
                        </h4>
                        <p class="travel-contentInfo">{{ travel.content }}</p>
                        <div class="otherInfo">
                            <div class="otherItem">
                                <icon data="@/icons/heat.svg" class="svg-container otherIcon" />
                                <p>{{ travel.heat }}</p>
                            </div>
                            <div class="otherItem commentItem">
                                <icon data="@/icons/comment.svg" class="svg-container otherIcon" />
                                <p>{{ travel.comment }}</p>
                            </div>
                            <div class="otherItem starItem">
                                <icon data="@/icons/star.svg" class="svg-container otherIcon" />
                                <p>{{ travel.star }}</p>
                            </div>
                        </div>
                    </div>
                </div>
            </el-card>
        </el-col>
    </el-row>
    <el-row :gutter="0">
        <el-col :span="4">
            <h4 class="shareDeg">分享度</h4>
        </el-col>
    </el-row>
    <el-row :gutter="0">
        <el-col :span="24">

        </el-col>
    </el-row>
</template>

<script setup>
import { ref } from 'vue';
import { onBeforeMount } from '@vue/runtime-core';

const style = ref({
    padding: '0',
    width: '100%',
    height: '100%',
})

const travelList = ref([
    {
        id: '123',
        author: 'Ryncler',
        name: '珠峰的故乡，走进喜马拉雅秘境，越野藏东小环线',
        content: '说到西藏，很多人都会想到日光之城拉萨，林芝的桃花，还有阿里的广阔，这些都是耳熟能详的地方，随着近些年 西藏 旅行的热门，以前神秘的秘境 阿里 很多人都已经踏足，没有到过的人都会把 阿里 当作前往 西藏 必去的地方，究竟为什么 西藏 总是让人魂萦梦绕，它有着什么样的神奇力量，让大家对此流连忘返。雪域高原的巍峨雪山；镶嵌在大地上的湛蓝湖泊；无人区和草原上的广阔荒野；独树一帜的自然律动； 千百年来的历史文脉。',
        heat: 2034,
        comment: 26461,
        star: 2005,
        imgUrl: 'https://www.otsuka.co.jp/img/index_im01_01.jpg.webp'
    },
])
</script>

<style scoped>
h4 {
    margin-top: 20px;
    font-weight: bold;
}

.shareDeg {
    margin-top: 40px;
}

.customTravel {
    margin-top: 20px;
    font-weight: bold;
    color: #66CCFF;
    float: right;
}

.col {
    margin-left: 83px;
}

.travel-card {
    width: 100%;
    height: 180px;
    border-radius: 20px;
    overflow: visible;
}

.travel-info {
    height: 100%;
    height: 180px;
    display: flex;
    /* align-items: center; */
    justify-content: space-between;
}

.travel-img {
    width: 40%;
    height: 180px;
    border-radius: 20px;
    float: left;
}

.content {
    width: 60%;
    height: 100%;
    margin: 0;
    margin-left: 20px;
    display: inline-block;
    overflow: visible;
}

.travel-name {
    width: 100%;
    height: 20px;
    overflow: hidden;
    white-space: nowrap;
    text-overflow: ellipsis;
}

.travel-contentInfo {
    width: 95%;
    height: 60px;
    margin: 10px;
    overflow: hidden;
    text-overflow: ellipsis;
    display: -webkit-box;
    -webkit-line-clamp: 3;
    -webkit-box-orient: vertical;
}

.otherInfo {
    margin: 0;
    display: flex;
    margin-top: 25px;
}

.otherItem {
    display: flex;
    margin-left: 20px;
}

.otherIcon {
    margin-top: 2px;
}

.starItem,
.commentItem {
    margin-left: 15px;
}
</style>