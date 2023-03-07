<template>
    <el-cascader popper-class="elcas" style="width: 100%;" v-model="mapIds" :options="geoData" :props="props"
        @change="ChangeGeo" filterable />
    <el-tabs v-model="tabs" type="card" class="sightsTabs" @tab-click="changeTabs">
        <el-tab-pane v-for="item in sightsData" :key="item.name" :label="item.title" :name="item.name">
            <div class="sightsInfo">
                <h2 class="sightsName">{{ item.name }}</h2>
                <el-carousel :interval="4000" type="card" height="400px" class="carou">
                    <el-carousel-item v-for="sightsImg in sightsImgs" :key="sightsImg">
                        <el-image :src="imageHandle(sightsImg)" style="width: 100%;height: 100%;" :fit="contain" />
                    </el-carousel-item>
                </el-carousel>
                <div class="title">
                    <h4 class="titleName">
                        <icon data="@/icons/sights-info.svg" class="svg-container icon" />
                        {{ staticSightsInfo.title }}
                    </h4>
                </div>
                <el-collapse class="elcollapse" accordion>
                    <el-collapse-item :name="index" v-for="sights, index in item.infos" :key="sights">
                        <template #title>
                            <icon data="@/icons/sights-info.svg" class="svg-container" />
                            {{ sights.title }}
                        </template>
                        {{ sights.content }}
                    </el-collapse-item>
                </el-collapse>

                <div class="title">
                    <h4 class="titleName">
                        <icon data="@/icons/activity.svg" class="svg-container icon" />
                        {{ staticSightsInfo.activeTitle }}
                    </h4>
                </div>
                <div class="activtiy">
                    <el-card class="card" :body-style="style" v-for="activity in acitvityList" :key="activity">
                        <el-image class="img" :src="imageHandle(activity.imgUrl)"
                            style="width: 100%;height: 230px;border-radius: 10px;" fit="fit" />
                        <div class="info">
                            <p>更新时间：{{ activity.lastModificationTime }}</p>
                            <h3>{{ activity.name }}</h3>
                            <p class="contentInfo">{{ activity.content }}</p>
                        </div>
                    </el-card>
                    <div class="pageDiv">
                        <el-pagination class="page" :page-size="pageSize" :pager-count="10" layout="prev, pager, next"
                            background :total="totalCount" hide-on-single-page @size-change="goSizeChange"
                            @current-change="goCurrentChange" />
                    </div>
                </div>

                <div class="title">
                    <h4 class="titleName">
                        <icon data="@/icons/travel.svg" class="svg-container icon" />
                        {{ staticSightsInfo.travelTitle }}
                    </h4>
                </div>

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
                    </div>
                </el-card>
                <div class="pageDiv">
                    <el-pagination class="page" :page-size="pageSize" :pager-count="10" layout="prev, pager, next"
                        background :total="totalCount" hide-on-single-page @size-change="goSizeChange"
                        @current-change="goCurrentChange" />
                </div>
            </div>
        </el-tab-pane>
    </el-tabs>
</template>

<script setup>
import { ref } from 'vue';
import { onBeforeMount } from '@vue/runtime-core';
import { useRouter } from 'vue-router';
import { getGeoTree } from '@/api/common';
import { getSights, getActivityList, getTravelList, getActivityIdListBySightsId, getTravelIdListBySightsId, getSightsByGeo } from '@/api/sights/index'
import { getImagesById } from '@/api/common/minio'
import { imageHandle } from '@/utils/common';
import { Match, getImageByDoc } from '@/utils/common/index'

const tabs = ref('')
const mapIds = ref([])
const geoData = ref([])
const router = useRouter()
const props = {
    expandTrigger: 'hover',
    value: 'id',
    label: 'name'
}
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
const totalCount = ref(1)

const goSizeChange = (value) => {
    pageSize.value = value
}

const goCurrentChange = (value) => {
    currentPage.value = value
}


const staticSightsInfo = ref({
    title: '景点简介',
    activeTitle: '近期热门活动',
    travelTitle: '游记列表'
})
const sightsData = ref([
    // {
    //     id: '3a0909a1-5c69-f900-7e6c-3d4c33e9f2e2',
    //     name: '九寨沟',
    //     title: '九寨沟',
    //     infos: [
    //         {
    //             title: '概述',
    //             content: '九寨沟位于阿坝州九寨沟县境内，沟中雪峰林立，林木茂密，水池浅滩五彩斑斓，晶莹的溪水穿行于森林和浅滩之间，加上藏家木楼、经幡和藏羌民族独特的传统习俗，九寨沟被人们称为“美丽的童话世界”。\r\n九寨沟主体呈“Y”字形，由树正沟、日则沟和则查洼沟三条沟组成，总长50余公里。景区内有环保观光车穿梭行驶，招手即停，游客一般会乘坐环保车逐一游览景点。'
    //         },
    //         {
    //             title: '开放时间',
    //             content: '5月1日-10月31日 07:30-17:00'
    //         }
    //     ],
    //     acitvityList: [
    //         {
    //             name: '疯狂电音节',
    //             imgUrl: 'https://www.otsuka.co.jp/img/index_im01_01.jpg.webp',
    //             changeTime: '2022.12.13',
    //             content: 'asdjfiajwoefnamoifnaodfmasdfainofuaisohfnauisnhfvcawipoenjfaiwopsefn923840jrmwfiaosdfpsawifujhq89i3fnjrmaswpdfcap9w83pur8932jmiowpfajuq98304pwanfnmapsifhnsap9fihjapsfnwaeiopfrmnqp943r8ijmqawfdriawpofjmaswofikaefoiwenfr'
    //         },
    //         {
    //             name: '疯狂电音节',
    //             imgUrl: 'https://www.otsuka.co.jp/img/index_im01_01.jpg.webp',
    //             changeTime: '2022.12.13',
    //             content: 'asdjfiajwoefnamoifnaodfmasdfainofuaisohfnauisnhfvcawipoenjfaiwopsefn923840jrmwfiaosdfpsawifujhq89i3fnjrmaswpdfcap9w83pur8932jmiowpfajuq98304pwanfnmapsifhnsap9fihjapsfnwaeiopfrmnqp943r8ijmqawfdriawpofjmaswofikaefoiwenfr'
    //         },
    //         {
    //             name: '疯狂电音节',
    //             imgUrl: 'https://www.otsuka.co.jp/img/index_im01_01.jpg.webp',
    //             changeTime: '2022.12.13',
    //             content: 'asdjfiajwoefnamoifnaodfmasdfainofuaisohfnauisnhfvcawipoenjfaiwopsefn923840jrmwfiaosdfpsawifujhq89i3fnjrmaswpdfcap9w83pur8932jmiowpfajuq98304pwanfnmapsifhnsap9fihjapsfnwaeiopfrmnqp943r8ijmqawfdriawpofjmaswofikaefoiwenfr'
    //         }
    //     ],
    //     travelList: [
    //         {
    //             id: '123',
    //             author: 'Ryncler',
    //             name: '珠峰的故乡，走进喜马拉雅秘境，越野藏东小环线',
    //             content: '说到 西藏 ，很多人都会想到 日光 之城 拉萨 ， 林芝 的桃花，还有 阿里 的广阔，这些都是耳熟能详的地方，随着近些年 西藏 旅行的热门，以前神秘的秘境 阿里 很多人都已经踏足，没有到过的人都会把 阿里 当作前往 西藏 必去的地方，究竟为什么 西藏 总是让人魂萦梦绕，它有着什么样的神奇力量，让大家对此流连忘返。雪域高原的巍峨雪山；镶嵌在大地上的湛蓝湖泊；无人区和草原上的广阔荒野；独树一帜的自然律动； 千百年来的历史文脉。',
    //             changeTime: '2023.1.30',
    //             comment: 26461,
    //             star: 2005,
    //             imgUrl: 'https://www.otsuka.co.jp/img/index_im01_01.jpg.webp'
    //         },
    //         {
    //             id: '123',
    //             author: 'Ryncler',
    //             name: '珠峰的故乡，走进喜马拉雅秘境，越野藏东小环线',
    //             content: '说到 西藏 ，很多人都会想到 日光 之城 拉萨 ， 林芝 的桃花，还有 阿里 的广阔，这些都是耳熟能详的地方，随着近些年 西藏 旅行的热门，以前神秘的秘境 阿里 很多人都已经踏足，没有到过的人都会把 阿里 当作前往 西藏 必去的地方，究竟为什么 西藏 总是让人魂萦梦绕，它有着什么样的神奇力量，让大家对此流连忘返。雪域高原的巍峨雪山；镶嵌在大地上的湛蓝湖泊；无人区和草原上的广阔荒野；独树一帜的自然律动； 千百年来的历史文脉。',
    //             changeTime: '2023.1.30',
    //             comment: 26461,
    //             star: 2005,
    //             imgUrl: 'https://www.otsuka.co.jp/img/index_im01_01.jpg.webp'
    //         },
    //     ]
    // }
])

const acitvityList = ref([])
const travelList = ref([])
const getGeoData = () => {
    return getGeoTree().then(res => {
        if (res.status === 200) {
            geoData.value = res.data
        }
    })
}

const sightsImgs = ref([])

const ChangeGeo = () => {
    getSightsByGeoId()
}
const getSightsByGeoId = () => {
    getSightsByGeo(mapIds.value.slice(-1)[0]).then(res => {
        if (res.status === 200) {
            sightsData.value = res.data.map(item => {
                item.title = item.name
                item.infos = [
                    {
                        title: '概述',
                        content: item.description
                    },
                    {
                        title: '开放时间',
                        content: item.openTime
                    },
                    {
                        title: '门票',
                        content: item.ticket
                    },
                    {
                        title: '交通指南',
                        content: item.trafficGuide
                    },
                    {
                        title: '自驾游指南',
                        content: item.selfDrivingGuide
                    }
                ]
                return item
            })
        }
    })
}

const changeTabs = (tab, event) => {
    sightsData.value.forEach(item => {
        if (tab.paneName === item.name) {
            getActivityById(item.id)
            getTravelById(item.id)
            getImagesById(item.id).then(res => {
                if (res.status === 200) {
                    sightsImgs.value = res.data.map(i => {
                        return i.imageURL
                    })
                }
            })
        }
    });
}
const getSightsInfo = () => {
    var id = router.currentRoute.value.query.id
    if (id !== undefined && id !== '' && id !== null) {
        getSights(id).then(res => {
            if (res.status === 200) {
                sightsData.value[0] = res.data
                sightsData.value[0].title = sightsData.value[0].name
                sightsData.value[0].infos = [
                    {
                        title: '概述',
                        content: res.data.description
                    },
                    {
                        title: '开放时间',
                        content: res.data.openTime
                    },
                    {
                        title: '门票',
                        content: res.data.ticket
                    },
                    {
                        title: '交通指南',
                        content: res.data.trafficGuide
                    },
                    {
                        title: '自驾游指南',
                        content: res.data.selfDrivingGuide
                    }
                ]
                tabs.value = sightsData.value[0].title
            }
        })
        getActivityById(id)
        getTravelById(id)
        getImagesById(id).then(res => {
            if (res.status === 200) {
                sightsImgs.value = res.data.map(i => {
                    return i.imageURL
                })
            }
        })
    }
}

const getActivityById = (id) => {
    getActivityIdListBySightsId(id).then(res => {
        if (res.status === 200) {
            getActivityList(res.data.map(i => {
                return i.activityId
            })).then(activity => {
                if (activity.status === 200) {
                    acitvityList.value = activity.data.map((item) => {
                        if (item.lastModificationTime === null) {
                            item.lastModificationTime = '暂无'
                        } else {
                            item.lastModificationTime = new Date(item.lastModificationTime).format('Y.m.d H:i:s')
                        }
                        return item
                    })
                    acitvityList.value.map((a) => {
                        getImagesById(a.id).then(res => {
                            if (res.status === 200) {
                                a.imgUrl = res.data.find(x => x.isMain === true).imageURL
                            }
                        })
                    })
                }
            })
        }
    })
}

const getTravelById = (id) => {
    getTravelIdListBySightsId(id).then(res => {
        if (res.status === 200) {
            getTravelList(res.data.map(i => {
                return i.travelsId
            })).then(travel => {
                if (travel.status === 200) {
                    travelList.value = travel.data.map((item) => {
                        item.imgUrl = getImageByDoc(item.content)
                        item.content = Match(item.content)
                        if (item.lastModificationTime === null) {
                            item.lastModificationTime = '暂无'
                        } else {
                            item.lastModificationTime = new Date(item.lastModificationTime).format('Y.m.d H:i:s')
                        }
                        return item
                    })
                }
            })
        }
    })
}
onBeforeMount(() => {
    getGeoData(), getSightsInfo()
})
</script>

<style  scoped>
.sightsInfo {
    margin-top: 40px;
}

.sightsTabs {
    margin-top: 20px;
}


.sightsName {
    text-align: center;
    margin-top: 30px;
}


.title {
    margin-top: 40px;
    color: white;
    background-color: #66CCCC;
    width: 200px;
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

.elcollapse {
    margin-top: 20px;
}

.activtiy {
    width: 100%;
    height: 100%;
    display: flex;
    align-items: center;
    justify-content: center;
    flex-wrap: wrap;
    justify-content: space-between;
}

.card {
    float: left;
    margin-top: 30px;
    height: 420px;
    border-radius: 10px;
    width: 32%;
    overflow: visible;
}

.info {
    margin-left: 10px;
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
.el-tabs__item {
    border-radius: 20px;
}

.el-tabs__item:hover {
    color: #66CCCC;
}

.el-tabs__item.is-active {
    color: #66CCCC;
}

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

.el-cascader .el-input.is-focus .el-input__wrapper {
    box-shadow: 0 0 0 1px #66CCCC;
}
</style>