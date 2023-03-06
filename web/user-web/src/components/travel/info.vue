<template>
    <div class="travelInfo">
        <h2 class="travelName">游记列表</h2>
        <el-menu default-active="1" class="elmenu" mode="horizontal" :ellipsis="false" active-text-color="#66CCCC">
            <el-menu-item index="1" style="border-bottom: 0px;" class="itemMenu firstMenu" @click="getTravels()">
                全部
            </el-menu-item>
            <el-menu-item index="2" style="border-bottom: 0px;" class="itemMenu">

            </el-menu-item>
            <el-menu-item index="3" style="border-bottom: 0px;" class="searchMenu">
                <el-input class="searchInput" v-model="searchTravel" placeholder="搜索游记" clearable :suffix-icon="Search"
                    @change="search()" />
            </el-menu-item>
        </el-menu>
        <el-card class="travel-card" :body-style="style" v-for="travel in travelList" :key="travel.id">
            <div class="travel-info">
                <el-image :src="travel.imgUrl" :fit="contain" class="travel-img" />
                <div class="content">
                    <h3 class="titleContent">{{ travel.travelsTitle }}</h3>
                    <p class="travel-contentInfo">{{ travel.content }}</p>
                    <div class="otherInfo">
                        <div class="otherItem firstItem">
                            <icon data="@/icons/user.svg" class="svg-container otherIcon" />
                            <p class="author">{{ travel.author }}</p>
                        </div>
                        <div class="otherItem">
                            <icon data="@/icons/time.svg" class="svg-container otherIcon" />
                            <p class="author">{{ travel.lastModificationTime }}</p>
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
            </div>
        </el-card>
        <div class="pageDiv">
            <el-pagination class="page" :page-size="pageSize" :pager-count="10" layout="prev, pager, next" background
                :total="totalCount" hide-on-single-page @size-change="goSizeChange" @current-change="goCurrentChange" />
        </div>
    </div>
</template>

<script setup>
import { ref } from 'vue';
import { onBeforeMount } from '@vue/runtime-core';
import { Search } from '@element-plus/icons-vue';
import { getAll } from '@/api/travel/index'
import { Match, getImageByDoc } from '@/utils/common/index'

const style = ref({
    padding: '0',
    width: '100%',
    height: '100%',
})

const currentPage = ref(1)
const pageSizes = ref([
    10, 50, 100, 500
])
const pageSize = ref(pageSizes.value[0])
const totalCount = ref(51)

const goSizeChange = (value) => {
    pageSize.value = value
}

const goCurrentChange = (value) => {
    currentPage.value = value
}

const searchTravel = ref('')
const travelList = ref([
    {
        id: '123',
        author: 'Ryncler',
        name: '九寨沟西藏吞吞吐吐他就是你的福利',
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
        star: 2002345,
        imgUrl: 'https://www.otsuka.co.jp/img/index_im01_01.jpg.webp'
    },
])

const tmpTravelData = ref([
    {
        id: '123',
        author: 'Ryncler',
        name: '九寨沟西藏吞吞吐吐他就是你的福利',
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
        star: 2002345,
        imgUrl: 'https://www.otsuka.co.jp/img/index_im01_01.jpg.webp'
    },
])
const search = () => {
    if (searchTravel.value.length <= 0) {
        travelList.value = tmpTravelData.value
        return
    }
    travelList.value = travelList.value.filter(x => x.travelsTitle.includes(searchTravel.value))
}

const getTravels = () => {
    var parms = {
        isall: true,
        maxResultCount: pageSize.value,
        skipCount: currentPage.value
    }
    getAll(parms).then(res => {
        if (res.status === 200) {
            totalCount.value = res.data.totalCount
            travelList.value = res.data.items.map((item) => {
                item.imgUrl = getImageByDoc(item.content)
                item.content = Match(item.content)
                if (item.lastModificationTime === null) {
                    item.lastModificationTime = '暂无'
                } else {
                    item.lastModificationTime = new Date(item.lastModificationTime).format('Y.m.d H:i:s')
                }
                return item
            })
            tmpTravelData.value = travelList.value
        }
    })
}
onBeforeMount(async () => {
    getTravels()
})
</script>

<style scoped>
.travelInfo {
    margin-top: 40px;
}


.travelName {
    text-align: center;
    margin-top: 30px;
}

.elmenu {
    width: 100%;
    height: 40px;
    border-radius: 10px;
    border-bottom: 0;
}

.el-menu--horizontal .el-menu-item:not(.is-disabled):hover {
    color: #66CCCC;
    background-color: white;
    border-radius: 10px;
}

.el-menu--horizontal .el-menu-item:not(.is-disabled):focus {
    color: #66CCCC;
    background-color: white;
    border-radius: 10px;
}

.itemMenu {
    font-size: 20px;
    font-weight: bold;
    width: 150px;
    height: 40px;
    border-radius: 10px;
}

.firstMenu {
    padding-left: 0px;
    flex: 1;
}

.searchMenu {
    width: 350px;
    border-radius: 10px;
    margin-left: 55%;

    padding-right: 0px;
}

.searchInput {
    border-radius: 10px;
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
    width: 300px;
    height: 200px;
    border-radius: 20px;
    float: left;
}


p {
    margin: 0;
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
    display: flex;
    margin: 10px;
    margin-top: 30px;
}

.author {
    margin-left: 10px;
}

.otherItem {
    display: flex;
    margin-left: 80px;
}

.firstItem {
    margin-left: 250px;
}

.pageDiv {
    flex: 1;
}

.page {
    margin-top: 20px;
    text-align: center;
    align-content: center;
    justify-content: center;
}
</style>

<style>
.el-pagination.is-background .el-pager li.is-active {
    background-color: #66CCCC;
}


.el-pager li {
    width: 40px;
    height: 40px;
    border-radius: 10px;
}

.el-pagination button {
    border-radius: 10px;
    width: 40px;
    height: 40px;
}

.el-pager li:hover {
    color: #66CCCC;
}
</style>