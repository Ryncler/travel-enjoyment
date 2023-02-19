<template>
    <div>
        <div class="title">
            <h4 class="titleName">
                <icon data="@/icons/top.svg" class="svg-container icon" />
                {{ title }}
            </h4>
        </div>
        <div class="sightsInfo">
            <el-card class="card" :body-style="style" v-for="item, index in sightsList" :key="item.id">
                <el-image :src="item.imgUrl" />
                <p class="number">{{ index + 1 }}</p>
                <div class="info">
                    <p>更新时间：{{ item.changeTime }}</p>
                    <h3>{{ item.name }}</h3>
                    <div>
                        <el-check-tag checked size="large" class="tag" type="info" v-for="item in tagList" :key="item.id">{{
                            item.name }}</el-check-tag>
                    </div>
                </div>
            </el-card>
        </div>
        <el-button round type="primary" class=" btn">查看更多景点...</el-button>
    </div>
</template>

<script setup>
import { ref } from 'vue';
import { onBeforeMount } from '@vue/runtime-core';

var title = ref('景点TOP6')
const sightsList = ref([
    {
        id: '123',
        name: '九寨沟',
        changeTime: '2023.1.30',
        imgUrl: 'https://www.otsuka.co.jp/img/index_im01_01.jpg.webp'
    },
    {
        id: '123',
        name: '九寨沟',
        changeTime: '2023.1.30',
        imgUrl: 'https://www.otsuka.co.jp/img/index_im01_01.jpg.webp'
    },
    {
        id: '123',
        name: '九寨沟',
        changeTime: '2023.1.30',
        imgUrl: 'https://www.otsuka.co.jp/img/index_im01_01.jpg.webp'
    },
    {
        id: '123',
        name: '九寨沟',
        changeTime: '2023.1.30',
        imgUrl: 'https://www.otsuka.co.jp/img/index_im01_01.jpg.webp'
    },
    {
        id: '123',
        name: '九寨沟',
        changeTime: '2023.1.30',
        imgUrl: 'https://www.otsuka.co.jp/img/index_im01_01.jpg.webp'
    },
    {
        id: '123',
        name: '九寨沟',
        changeTime: '2023.1.30',
        imgUrl: 'https://www.otsuka.co.jp/img/index_im01_01.jpg.webp'
    },
])
const tagList = ref([
    {
        id: '196415',
        name: '#九寨沟'
    },
    {
        id: '2134',
        name: '#园博园'
    },
    {
        id: '23425rwr',
        name: '#万里长城'
    },
    {
        id: '196415',
        name: '#故宫'
    },
    {
        id: '196415',
        name: '#故宫'
    },
    {
        id: '196415',
        name: '#故宫'
    },
    {
        id: '196415',
        name: '#玉林五彩田园'
    },
    {
        id: '196415',
        name: '#玉林五彩田园'
    },
    {
        id: '196415',
        name: '#玉林五彩田园'
    },
    {
        id: '196415',
        name: '#玉林五彩田园'
    },
])
const style = ref({
    padding: '0',
    width: '100%',
    height: '100%',
})
// eslint-disable-next-line no-undef
defineExpose({
    title
});
</script>

<style scoped>
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

.sightsInfo {
    width: 100%;
    height: 100%;
    display: flex;
    align-items: center;
    flex-wrap: wrap;
    justify-content: space-between;
}

.sightsInfo:after {
    flex: auto;
    margin-left: 10px;
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

.card {
    position: relative;
    float: left;
    margin-top: 30px;
    height: 450px;
    border-radius: 10px;
    width: 32%;
    overflow: visible;
}

.info {
    margin-left: 10px;
}

.tag {
    border-radius: 50px;
    margin: 5px;
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
</style>