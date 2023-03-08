<template>
    <div class="travel">
        <div class="title">
            <h4 class="titleName">
                <icon data="@/icons/activity.svg" class="svg-container icon" />
                {{ title }}
            </h4>
        </div>
        <el-card class="card" :body-style="style" v-for="item, index in travelList" :key="item.id"
            @click="goDetails(item.id)">
            <div class="info">
                <el-image :src="item.imgUrl" :fit="contain" class="img" />
                <p class="number">{{ index + 1 }}</p>
                <div class="content">
                    <h3 class="titleContent">{{ item.travelsTitle }}</h3>
                    <p class="contentInfo">{{ item.content }}</p>
                    <div class="otherInfo">
                        <div class="otherItem firstItem">
                            <icon data="@/icons/user.svg" class="svg-container otherIcon" />
                            <p class="author">{{ item.author }}</p>
                        </div>
                        <div class="otherItem">
                            <icon data="@/icons/time.svg" class="svg-container otherIcon" />
                            <p class="author">{{ item.lastModificationTime }}</p>
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
        <el-button round type="primary" class=" btn" @click="goTravel()">查看更多游记...</el-button>
    </div>
</template>

<script setup>
import { ref } from 'vue';
import { onBeforeMount } from '@vue/runtime-core';
import { getTravelList } from '@/api/sights/index'
import { Match, getImageByDoc } from '@/utils/common/index'
import { getUser } from '@/api/identity/user'
import { getCommentCountByTravelId, getStarCountByTravelId } from '@/api/travel/index'
import router from '@/router'

const title = ref('游记TOP6')

const travelList = ref([])
const style = ref({
    padding: '0',
    width: '100%',
    height: '100%',
})

const getTravels = (ids) => {
    getTravelList(ids).then(res => {
        if (res.status === 200) {
            travelList.value = res.data.map((item) => {
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

const goDetails = (id) => {
    router.push({ name: 'TravelInfo', path: 'info', query: { id: id } })
}

const goTravel = () => {
    router.push({ name: 'Travel' })
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