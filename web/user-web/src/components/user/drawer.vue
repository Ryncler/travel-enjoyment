<template>
    <el-drawer v-model="showDrawer" :show-close="false" size="30%">
        <template #header="{ close }">
            <h4 class="titleClass">自定义精选游记</h4>
            <el-button type="danger" @click="close">
                <icon data="@/icons/close.svg" class="closeIcon" />
                关闭
            </el-button>
        </template>
        <el-input class="searchInput" v-model="searchTravel" placeholder="搜索游记" clearable :suffix-icon="Search" />
        <ul v-infinite-scroll="load" class="infinite-list" style="overflow: auto" :infinite-scroll-disabled="disabled">
            <el-checkbox-group v-model="checkTravels">
                <el-checkbox class="checkbox" v-for="item in travelList" :label="item.name" border :key="item" />
            </el-checkbox-group>
        </ul>
        <p v-if="loading" class="center">Loading...</p>
        <p v-if="noMore()" class="center">没有更多了...</p>
    </el-drawer>
</template>

<script setup>
import { ref } from 'vue';
import { onBeforeMount } from '@vue/runtime-core';
import { Search } from '@element-plus/icons-vue';

const showDrawer = ref(false);
const count = ref(0)
const loading = ref(false)
const totalPages = ref(0)
const checkTravels = ref([]);
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
        name: '四川九寨沟，五星级景区',
        content: '说到 西藏 ，很多人都会想到 日光 之城 拉萨 ， 林芝 的桃花，还有 阿里 的广阔，这些都是耳熟能详的地方，随着近些年 西藏 旅行的热门，以前神秘的秘境 阿里 很多人都已经踏足，没有到过的人都会把 阿里 当作前往 西藏 必去的地方，究竟为什么 西藏 总是让人魂萦梦绕，它有着什么样的神奇力量，让大家对此流连忘返。雪域高原的巍峨雪山；镶嵌在大地上的湛蓝湖泊；无人区和草原上的广阔荒野；独树一帜的自然律动； 千百年来的历史文脉。',
        changeTime: '2023.1.30',
        comment: 26461,
        star: 2002345,
        imgUrl: 'https://www.otsuka.co.jp/img/index_im01_01.jpg.webp'
    },
])

const load = () => {
    loading.value = false
    return;
    setTimeout(() => {
        count.value += 1
        getTravelList()
    }, 2000)
}
const getTravelList = () => {
    var params = {
        pageSize: 10
    }

    loading.value = false
}
const noMore = () => {
    return count.value >= totalPages.value - 1
}
const disabled = () => {
    return loading.value || noMore()
}

// eslint-disable-next-line no-undef
defineExpose({
    showDrawer, getTravelList
})
</script>

<style>
h4 {
    font-weight: bold;
}

.infinite-list {
    height: 600px;
    padding: 0;
    margin: 0;
    margin-top: 20px;
    list-style: none;
}

.center {
    text-align: center;
}

.checkbox {
    width: 100%;
    margin-right: 0;
    display: inline-flex;
    text-align: center;
}
</style>

<style>
.el-checkbox__input.is-checked+.el-checkbox__label {
    color: #66CCCC;
}

.el-checkbox.is-bordered.is-checked {
    border-color: #66CCCC;
}

.el-checkbox__input.is-checked .el-checkbox__inner {
    background-color: #66CCCC;
    border-color: #66CCCC;
}

.el-input__wrapper.is-focus {
    box-shadow: 0 0 0 1px #66CCCC;
}
</style>