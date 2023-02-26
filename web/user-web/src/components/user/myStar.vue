<template>
    <el-row :gutter="0">
        <el-backtop class="top" :right="100" :bottom="100">
            <icon data="@/icons/back-top.svg" class="topIcon" />
        </el-backtop>
        <el-col :span="8" :offset="16" class="col">
            <el-input class="searchInput" v-model="searchTravel" placeholder="搜索游记" clearable :suffix-icon="Search" />
        </el-col>
    </el-row>
    <el-row :gutter="0">
        <el-col :span="24">
            <el-card class="travel-card" :body-style="style" v-for="travel in travelList" :key="travel.id" shadow="hover">
                <div class="travel-info">
                    <el-image :src="travel.imgUrl" :fit="contain" class="travel-img" />
                    <div class="content">
                        <h3 class="titleContent">{{ travel.name }}</h3>
                        <p class="travel-contentInfo">{{ travel.content }}</p>
                        <div class="otherInfo">
                            <div class="otherItem firstItem">
                                <icon data="@/icons/user.svg" class="svg-container otherIcon" />
                                <p class="author">{{ travel.author }}</p>
                            </div>
                            <div class="otherItem">
                                <icon data="@/icons/time.svg" class="svg-container otherIcon" />
                                <p class="author">{{ travel.changeTime }}</p>
                            </div>
                            <div class="otherItem">
                                <icon data="@/icons/comment.svg" class="svg-container otherIcon" />
                                <p class="author">{{ travel.comment }}</p>
                            </div>
                            <div class="otherItem">
                                <icon data="@/icons/star.svg" class="svg-container otherIcon" />
                                <p class="author">{{ travel.star }}</p>
                            </div>
                        </div>
                    </div>
                    <div class="operate">
                        <el-button round type="primary" class="operateBtn btn" @click="removeStar()">取消收藏</el-button>
                    </div>
                </div>
            </el-card>
            <el-button round type="primary" class="loadBtn btn" @click="loadTrend()">加载更多游记...</el-button>
        </el-col>
    </el-row>
</template>

<script setup>
import { ref } from 'vue';
import { markRaw } from 'vue'
import { onBeforeMount } from '@vue/runtime-core';
import router from '@/router'
import { Search } from '@element-plus/icons-vue';
import { Delete } from '@element-plus/icons-vue'
import { ElMessage, ElMessageBox } from 'element-plus'

const style = ref({
    padding: '0',
    width: '100%',
    height: '100%',
})

const travelList = ref([
    {
        id: '123',
        author: 'Ryncler',
        name: '珠峰的故乡，走进喜马拉雅秘境，越野藏东小环线23rwefasdfedrgbredhbsrhtbnserhtrtgwaerf2`131`23w13e42',
        content: '说到 西藏 ，很多人都会想到 日光 之城 拉萨 ， 林芝 的桃花，还有 阿里 的广阔，这些都是耳熟能详的地方，随着近些年 西藏 旅行的热门，以前神秘的秘境 阿里 很多人都已经踏足，没有到过的人都会把 阿里 当作前往 西藏 必去的地方，究竟为什么 西藏 总是让人魂萦梦绕，它有着什么样的神奇力量，让大家对此流连忘返。雪域高原的巍峨雪山；镶嵌在大地上的湛蓝湖泊；无人区和草原上的广阔荒野；独树一帜的自然律动； 千百年来的历史文脉。',
        changeTime: '2023.1.30',
        comment: 26461,
        star: 2005,
        imgUrl: 'https://www.otsuka.co.jp/img/index_im01_01.jpg.webp'
    },
    {
        id: '123',
        author: 'Ryncler',
        name: '珠峰的故乡，走进喜马拉雅秘境，越野藏东小环线',
        content: '说到 西藏 ，很多人都会想到 日光 之城 拉萨 ， 林芝 的桃花，还有 阿里 的广阔，这些都是耳熟能详的地方，随着近些年 西藏 旅行的热门，以前神秘的秘境 阿里 很多人都已经踏足，没有到过的人都会把 阿里 当作前往 西藏 必去的地方，究竟为什么 西藏 总是让人魂萦梦绕，它有着什么样的神奇力量，让大家对此流连忘返。雪域高原的巍峨雪山；镶嵌在大地上的湛蓝湖泊；无人区和草原上的广阔荒野；独树一帜的自然律动； 千百年来的历史文脉。',
        changeTime: '2023.1.30',
        comment: 26461,
        star: 2005,
        imgUrl: 'https://www.otsuka.co.jp/img/index_im01_01.jpg.webp'
    },
])

const loadTrend = () => {

}
const removeStar = () => {

}
const deleteTravel = () => {
    ElMessageBox.confirm(
        '是否确定要删除该游记？',
        '删除操作',
        {
            type: 'warning',
            icon: markRaw(Delete),
        }
    ).then(() => {
        
    })

}
</script>

<style scoped>
.top {
    width: 80px;
    height: 80px;
}

.top:hover {
    background-color: #66CCCC;
}

.col {
    display: flex;
}

.travel-card {
    height: 200px;
    margin-top: 30px;
    border-radius: 20px;
    overflow: visible;
}

.travel-info {
    width: 100%;
    height: 100%;
    display: flex;
    /* align-items: center; */
    justify-content: space-between;
}

.travel-img {
    width: 30%;
    height: 200px;
    border-radius: 20px;
    float: left;
}

.content {
    width: 60%;
    flex: 1;
    margin: 0;
    margin-left: 20px;
    display: inline-block
}

.operate {

    width: 10%;
}

.titleContent {
    width: 95%;
    height: 35px;
    margin: 10px;
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
    flex: 1;
    margin: 0;
    /* margin-left: 20px; */
    display: flex;
    margin: 10px;
    margin-top: 40px;
}

.author {
    margin-left: 10px;
}

.otherItem {
    display: flex;
    margin-left: 50px;
}

.firstItem {
    margin-left: 100px;
}

.img {
    border-radius: 10px;
}

.contentInfo {
    height: 80px;
    overflow: hidden;
    text-overflow: ellipsis;
    word-break: break-all;
    display: -webkit-box;
    -webkit-line-clamp: 4;
    -webkit-box-orient: vertical;
}

.btn {
    width: 150px;
    height: 35px;
    font-size: 14px;
    font-weight: bold;
    margin-right: 20px;
    background-color: #66CCCC;
    border: 1px solid #66CCCC;
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

.loadBtn {
    width: 300px;
    margin-top: 30px;
    margin-left: 35%;
}

.operateBtn {
    width: 90px;
    margin-top: 50px;
    margin-left: 0;
}

</style>

<style>
.el-input__wrapper.is-focus {
    box-shadow: 0 0 0 1px #66CCCC;
}
</style>