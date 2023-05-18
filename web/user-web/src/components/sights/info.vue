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
import { getUser } from '@/api/identity/user'
import { getCommentCountByTravelId, getStarCountByTravelId } from '@/api/travel/index'
import { addHotTop } from '@/api/common/index'

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
const sightsData = ref([])

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
            addHotTop({
                linkId: item.id,
                topType: 1,
                weight: 0
            })
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
                    travelList.value = travelList.value.map((item) => {
                        getUser(item.creatorId).then(user => {
                            if (user.status === 200) {
                                item.author = user.data.userName
                            }
                        })
                        getCommentCountByTravelId(item.id).then(comment => {
                            if (comment.status === 200) {
                                item.comment = comment.data
                            }
                        })
                        getStarCountByTravelId(item.id).then(star => {
                            if (star.status === 200) {
                                item.star = star.data
                            }
                        })
                        return item
                    });
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