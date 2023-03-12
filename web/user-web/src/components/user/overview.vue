<template>
    <el-row :gutter="0">
        <el-backtop class="top" :right="100" :bottom="100">
            <icon data="@/icons/back-top.svg" class="topIcon" />
        </el-backtop>
        <el-col :span="4">
            <h4>精选游记</h4>
        </el-col>
        <el-col :span="4" :offset="16">
            <el-link class="customTravel" type="primary" @click="showCustomTravel()" :underline="false">
                自定义精选游记
            </el-link>
        </el-col>
    </el-row>
    <el-row :gutter="0">
        <el-col :span="11">
            <el-card class="travel-card" :body-style="style" shadow="hover"
                @click="() => { router.push({ name: 'TravelInfo', path: 'info', query: { id: travelList[0].id } }) }">
                <div class="travel-info">
                    <el-image :src="travelList[0].imgUrl" :fit="fill" class="travel-img" />
                    <div class="content">
                        <h4 class="travel-name">
                            <icon data="@/icons/edit.svg" class="svg-container" />
                            {{ travelList[0].travelsTitle }}
                        </h4>
                        <p class="travel-contentInfo">{{ travelList[0].content }}</p>
                        <div class="otherInfo">
                            <div class="otherItem commentItem">
                                <icon data="@/icons/comment.svg" class="svg-container otherIcon" />
                                <p>{{ travelList[0].comment }}</p>
                            </div>
                            <div class="otherItem starItem">
                                <icon data="@/icons/star.svg" class="svg-container otherIcon" />
                                <p>{{ travelList[0].star }}</p>
                            </div>
                        </div>
                    </div>
                </div>
            </el-card>
        </el-col>
        <el-col :span="11" class="col">
            <el-card class="travel-card" :body-style="style" shadow="hover"
                @click="() => { router.push({ name: 'TravelInfo', path: 'info', query: { id: travelList[1].id } }) }">
                <div class="travel-info">
                    <el-image :src="travelList[1].imgUrl" :fit="fill" class="travel-img" />
                    <div class="content">
                        <h4 class="travel-name">
                            <icon data="@/icons/edit.svg" class="svg-container" />
                            {{ travelList[1].travelsTitle }}
                        </h4>
                        <p class="travel-contentInfo">{{ travelList[1].content }}</p>
                        <div class="otherInfo">
                            <div class="otherItem commentItem">
                                <icon data="@/icons/comment.svg" class="svg-container otherIcon" />
                                <p>{{ travelList[1].comment }}</p>
                            </div>
                            <div class="otherItem starItem">
                                <icon data="@/icons/star.svg" class="svg-container otherIcon" />
                                <p>{{ travelList[1].star }}</p>
                            </div>
                        </div>
                    </div>
                </div>
            </el-card>
        </el-col>
    </el-row>
    <el-row :gutter="0">
        <el-col :span="4">
            <h4 class="shareDeg">动态</h4>
        </el-col>
    </el-row>
    <el-row :gutter="0">
        <el-col :span="24">
            <el-timeline>
                <el-timeline-item v-for="(item, index) in trends" :key="index" type="primary" hollow="true">
                    <h5>{{ item.trendsTime }}</h5>
                    <div class="trendsInfo">
                        <div class="flexDiv" v-for="share in item.shareTravels" :key="share">
                            <p>分享了：</p>
                            <el-link type="primary" :underline="false"
                                @click="() => { router.push({ name: 'TravelInfo', path: 'info', query: { id: comment.travelId } }) }">
                                {{ share.travelName }}
                            </el-link>
                        </div>
                        <div class="flexDiv" v-for="comment in item.replyComments" :key="comment">
                            <p>评论了</p>
                            <el-link type="primary" :underline="false"
                                @click="() => { router.push({ name: 'TravelInfo', path: 'info', query: { id: comment.travelId } }) }">
                                {{ comment.travelName }}
                            </el-link>
                            <p class="reply">：{{ comment.replyContent }}</p>
                        </div>
                    </div>
                </el-timeline-item>
            </el-timeline>
            <el-button round type="primary" class=" btn" @click="loadTrend()">加载更多动态...</el-button>
        </el-col>
    </el-row>
    <drawerVue ref="drawer"></drawerVue>
</template>

<script setup>
import { ref } from 'vue';
import store from '@/store'
import router from '@/router'
import { onBeforeMount } from '@vue/runtime-core';
import drawerVue from './drawer'
import { getChoiceTravel, getUserTrends } from '@/api/identity/user';
import { Match, getImageByDoc } from '@/utils/common/index'
import { getCommentCountByTravelId, getStarCountByTravelId } from '@/api/travel/index'

const currentPage = ref(1)
const drawer = ref(null)
const style = ref({
    padding: '0',
    width: '100%',
    height: '100%',
})

const travelList = ref([
    {
        imgUrl: ''
    },
    {
        imgUrl: ''
    }
])
const choiceTravel = ref({})
const trends = ref([])

const loadTrend = () => {
    currentPage.value += 1
    getUserTrend()
}

const showCustomTravel = () => {
    drawer.value.showDrawer = true
    drawer.value.travelList = []
    drawer.value.skipCount = 1
    drawer.value.choiceTravelId = choiceTravel.value.id
    drawer.value.checkTravels[0] = choiceTravel.value.oneTravelsId
    drawer.value.checkTravels[1] = choiceTravel.value.twoTravelsId
    drawer.value.getTravelList()
}

const getUserChoiceTravel = () => {
    getChoiceTravel(store.getters['identity/userInfo'].id).then(res => {
        if (res.status === 200) {
            choiceTravel.value = res.data
            travelList.value = res.data.travels.map((item) => {
                item.imgUrl = getImageByDoc(item.content)
                item.content = Match(item.content)
                item.lastModificationTime = new Date(item.lastModificationTime).format('Y.m.d H:i:s')
                return item
            })
            travelList.value = travelList.value.map((item) => {
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

const getUserTrend = () => {
    var parms = {
        userId: store.getters['identity/userInfo'].id,
        isall: true,
        maxResultCount: 10,
        skipCount: currentPage.value
    }
    getUserTrends(parms).then(res => {
        if (res.status === 200) {
            res.data.items.forEach(item => {
                trends.value.push(item)
            });
        }
    })
}

onBeforeMount(() => {
    getUserChoiceTravel(), getUserTrend()
})
</script>

<style scoped>
.top {
    width: 80px;
    height: 80px;
}

.top:hover {
    background-color: #66CCCC;
}

.topIcon {
    width: 90%;
    height: 90%;
    color: #66CCCC;
}

.topIcon:hover {
    color: white;
}

h4 {
    margin-top: 20px;
    font-weight: bold;
}

h5 {
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
    width: 45%;
    height: 180px;
    border-radius: 20px;
    float: left;
}

.content {
    width: 55%;
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
    margin-left: 40px;
}

.otherIcon {
    margin-top: 2px;
}

.starItem,
.commentItem {
    margin-left: 20px;
}

.commentItem {
    margin-left: 50%;
}

.flexDiv {
    display: flex;
    margin-bottom: 15px;
}

p {
    margin: 0;
}

.reply {
    margin-left: 5px;
}

.trendsInfo {
    margin-left: 30px;
}

.btn {
    width: 300px;
    height: 35px;
    font-size: 14px;
    font-weight: bold;
    margin-top: 30px;
    margin-left: 35%;
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
</style>