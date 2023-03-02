<template>
    <div class="travel">
        <div class="title">
            <h4 class="titleName">
                <icon data="@/icons/activity.svg" class="svg-container icon" />
                {{ title }}
            </h4>
        </div>
        <el-card class="card" :body-style="style" v-for="item, index in travelList" :key="item.id">
            <div class="info">
                <el-image :src="item.imgUrl" :fit="contain" class="img" />
                <p class="number">{{ index + 1 }}</p>
                <div class="content">
                    <h3 class="titleContent">{{ item.name }}</h3>
                    <p class="contentInfo">{{ item.content }}</p>
                    <div class="otherInfo">
                        <div class="otherItem firstItem">
                            <icon data="@/icons/user.svg" class="svg-container otherIcon" />
                            <p class="author">{{ item.author }}</p>
                        </div>
                        <div class="otherItem">
                            <icon data="@/icons/time.svg" class="svg-container otherIcon" />
                            <p class="author">{{ item.changeTime }}</p>
                        </div>
                        <div class="otherItem">
                            <icon data="@/icons/comment.svg" class="svg-container otherIcon" />
                            <p class="author">{{ item.comment }}</p>
                        </div>
                        <div class="otherItem">
                            <icon data="@/icons/star.svg" class="svg-container otherIcon" />
                            <p class="author">{{ item.star }}</p>
                        </div>
                    </div>
                </div>
            </div>
        </el-card>
        <el-button round type="primary" class=" btn">查看更多游记...</el-button>
    </div>
</template>

<script setup>
import { ref } from 'vue';
import { onBeforeMount } from '@vue/runtime-core';
import { getTravelList } from '@/api/sights/index'
import { getImagesById } from '@/api/common/minio'

const title = ref('游记TOP6')

const travelList = ref([
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
const style = ref({
    padding: '0',
    width: '100%',
    height: '100%',
})


const getTravels = (ids) => {
    getTravelList(ids).then(res => {
        if (res.status === 200) {
            travelList.value = res.data
            travelList.value.forEach(item => {
                getImagesById(item.id).then(res => {
                    if (res.status === 200) {
                        item.imgUrl = res.data.map(i => {
                                return i.imageURL
                        })[0]
                    }
                })
            })
        }
    })
}

// eslint-disable-next-line no-undef
defineExpose({
    title, getTravels
});
</script>

<style scoped>
.travel {
    width: 100%;
}

p {
    margin: 0;
}

.title {
    margin-top: 40px;
    color: white;
    background-color: #66CCCC;
    width: 150px;
    height: 35px;
    border-radius: 10px;
}

.titleName {
    text-align: center;
    padding-top: 5px;
}

.icon {
    color: white;
    width: 30px;
    height: 30px;
    margin-top: -2px;
}

.otherIcon {
    color: #66CCCC;
    width: 20px;
    height: 20px;
}

.card {
    position: relative;
    width: 100%;
    height: 200px;
    margin-top: 30px;
    border-radius: 20px;
    overflow: visible;
}

.info {
    width: 100%;
    height: 100%;
    display: flex;
    /* align-items: center; */
    justify-content: space-between;
}

.img {
    width: 300px;
    height: 200px;
    border-radius: 20px;
    float: left;
}

.content {
    flex: 1;
    margin: 0;
    margin-left: 20px;
    display: inline-block
}

.titleContent {
    width: 90%;
    height: 35px;
    margin: 10px;
}

.contentInfo {
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
    margin-left: 100px;
}

.firstItem {
    margin-left: 250px;
}

.btn {
    width: 400px;
    height: 50px;
    font-size: 18px;
    font-weight: bold;
    margin-top: 20px;
    margin-left: 35%;
    background-color: #66CCFF;
    border: 1px solid #66CCFF;
}

.btn:hover {
    transition: all 0.5s;
    color: white;
    background-color: #66CCCC;
    border: 1px solid #66CCCC;
}

.btn:focus {
    outline: 0;
    transition: all 0.5s;
    color: white;
    background-color: #66CCCC;
    border: 1px solid #66CCCC;
}

.number {
    position: absolute;
    margin: 0;
    color: white;
    font-size: 20px;
    width: 50px;
    top: -20px;
    right: -10px;
    height: 50px;
    text-align: center;
    border-radius: 50%;
    line-height: 50px;
    background-color: #66CCCC;
    z-index: 100;
}
</style>