<template>
    <el-row :gutter="0">
        <el-col :span="4">
            <div class="info">
                <el-avatar class="avatar" :size="150" src="https://empty" @error="errorHandler">
                    <img :src="imageHandle(travelData.avatar)" />
                </el-avatar>
                <h4 class="username">{{ travelData.userName }}</h4>
                <el-divider />
                <h5>出行日期：</h5>
                <h5 class="center">{{ travelData.departureTime }}</h5>
                <h5>出行天数：</h5>
                <h5 class="center">{{ travelData.travelDayNum }}天</h5>
                <h5>出行费用：</h5>
                <h5 class="center">{{ travelData.travelExpenses }}元</h5>
                <!-- <el-timeline>
                    <el-timeline-item v-for="(item, index) in travelData.contents" :key="index" type="primary"
                        hollow="true">
                        <h5>{{ item }}</h5>
                    </el-timeline-item>
                </el-timeline> -->
            </div>
        </el-col>
        <el-col class="contenCol" :span="19" :offset="1">
            <div class="otherInfo">
                <!-- <div class="otherItem firstItem">
                    <icon data="@/icons/address.svg" class="svg-container otherIcon" />
                    <p class="author">{{ travelData.ip }}</p>
                </div> -->
                <div class="otherItem firstItem">
                    <icon data="@/icons/comment.svg" class="svg-container otherIcon" />
                    <p class="author">{{ travelData.comment }} 评论</p>
                </div>
                <el-tooltip class="box-item" effect="light" content="点击即可收藏" placement="top">
                    <div class="otherItem" @click="goStar()">
                        <icon data="@/icons/star.svg"
                            :class="isStar === true ? 'svg-container otherIcon' : 'svg-container starIcon'" />
                        <p class="author">{{ travelData.star }} 收藏</p>
                    </div>
                </el-tooltip>
                <!-- <div class="otherItem">
                    <icon data="@/icons/heat.svg" class="svg-container otherIcon" />
                    <p class="author">{{ travelData.heat }} 热度</p>
                </div> -->
            </div>
            <el-divider />
            <h2 class="center">{{ travelData.travelsTitle }}</h2>
            <p class="content" v-html="travelData.content"></p>

            <commentVue ref="comment"></commentVue>
        </el-col>
    </el-row>
</template>

<script setup>
import { ref } from 'vue';
import store from '@/store'
import router from '@/router'
import { onBeforeMount } from '@vue/runtime-core';
import commentVue from '@/components/common/comment'
import { getTravel, getCommentCountByTravelId, getStarCountByTravelId } from '@/api/travel/index'
import { getImageByDoc } from '@/utils/common/index'
import { getUser } from '@/api/identity/user'
import { imageHandle } from '@/utils/common/index'
import { ElMessage } from 'element-plus'
import { addStarTravel, exitsUserTravel } from '@/api/common/index'

const comment = ref(null)
const isStar = ref(false)
const travelData = ref({})

const exitsStar = (id) => {
    var data = {
        userId: store.getters['identity/userInfo'].id,
        travelId: id
    }
    exitsUserTravel(data).then(res => {
        if (res.status === 200) {
            isStar.value = res.data
        }
    })
}

const getTravelInfo = () => {
    var id = router.currentRoute.value.query.id
    if (id !== undefined && id !== '' && id !== null) {
        getTravel(id).then(res => {
            if (res.status === 200) {
                travelData.value = res.data
                travelData.value.imgUrl = getImageByDoc(res.data.content)
                travelData.value.departureTime = new Date(res.data.departureTime).format('Y.m.d')
                getUser(res.data.creatorId).then(user => {
                    if (user.status === 200) {
                        travelData.value.avatar = user.data.avatar
                        travelData.value.author = user.data.userName
                    }
                })
                getCommentCountByTravelId(res.data.id).then(comment => {
                    if (comment.status === 200) {
                        travelData.value.comment = comment.data
                    }
                })
                getStarCountByTravelId(res.data.id).then(star => {
                    if (star.status === 200) {
                        travelData.value.star = star.data
                    }
                })
                exitsStar(res.data.id)
                comment.value.travelId = res.data.id
                comment.value.getTreeComment()
            }
        })
    }
}

const goStar = () => {
    if (isStar.value === false) {
        var data = {
            userId: store.getters['identity/userInfo'].id,
            travelId: travelData.value.id
        }
        addStarTravel(data).then(res => {
            if (res.status === 200) {
                ElMessage.success('收藏成功!')
            }
        })
    }
}


onBeforeMount(async () => {
    getTravelInfo()
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
    margin-left: 65%;
}

.content {
    margin-top: 30px;
}

.starIcon {
    margin-top: 2px;
    color: black
}
</style>