<template>
    <el-row :gutter="0">
        <el-backtop class="top" :right="100" :bottom="100">
            <icon data="@/icons/back-top.svg" class="topIcon" />
        </el-backtop>
        <el-col :span="8" :offset="16" class="col">
            <el-input class="searchInput" v-model="searchTravel" placeholder="搜索游记" clearable :suffix-icon="Search"
                @change="search()" />
        </el-col>
    </el-row>
    <el-row :gutter="0">
        <el-col :span="24">
            <el-card class="travel-card" :body-style="style" v-for="travel in travelList" :key="travel.id" shadow="hover">
                <div class="travel-info">
                    <el-image :src="travel.imgUrl" :fit="contain" class="travel-img"
                        @click="() => { router.push({ name: 'TravelInfo', path: 'info', query: { id: travel.id } }) }" />
                    <div class="content"
                        @click="() => { router.push({ name: 'TravelInfo', path: 'info', query: { id: travel.id } }) }">
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
                    <div class="operate">
                        <el-button round type="primary" class="operateBtn btn"
                            @click="removeStar(travel.id)">取消收藏</el-button>
                    </div>
                </div>
            </el-card>
            <el-button round type="primary" class="loadBtn btn" @click="load()">加载更多游记...</el-button>
        </el-col>
    </el-row>
</template>

<script setup>
import { ref } from 'vue';
import { markRaw } from 'vue'
import { onBeforeMount } from '@vue/runtime-core';
import router from '@/router'
import store from '@/store';
import { Search } from '@element-plus/icons-vue';
import { Delete } from '@element-plus/icons-vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import { getTravelList } from '@/api/sights/index'
import { getStarTravel, addStarTravel, deleteStarTravel } from '@/api/common/index'
import { getCommentCountByTravelId, getStarCountByTravelId } from '@/api/travel/index'
import { Match, getImageByDoc } from '@/utils/common/index'
import { getUser } from '@/api/identity/user'

const style = ref({
    padding: '0',
    width: '100%',
    height: '100%',
})

const currentPage = ref(1)
const searchTravel = ref('')
const travelList = ref([])

const tmpTravelData = ref([])
const search = () => {
    if (searchTravel.value.length <= 0) {
        travelList.value = tmpTravelData.value
        return
    }
    travelList.value = travelList.value.filter(x => x.travelsTitle.includes(searchTravel.value))
}

const load = () => {
    currentPage.value += 1
    getTravels()
}
const removeStar = (id) => {
    ElMessageBox.confirm(
        '是否确定要取消收藏该游记？',
        '取消收藏操作',
        {
            type: 'warning',
            icon: markRaw(Delete),
        }
    ).then(() => {
        var data = {
            userId: store.getters['identity/userInfo'].id,
            travelId: id
        }
        deleteStarTravel(data).then(res => {
            if (res.status === 204) {
                ElMessage({
                    type: 'success',
                    message: '操作成功'
                })
            }
        })
    })
}

const getTravels = () => {
    var data = {
        userId: store.getters['identity/userInfo'].id,
        maxResultCount: 10,
        skipCount: currentPage.value
    }
    getStarTravel(data).then(res => {
        if (res.status === 200) {
            getTravelList(res.data.map(item => {
                return item.travelId
            })).then(travel => {
                if (travel.status === 200) {
                    travel.data.map((item) => {
                        item.imgUrl = getImageByDoc(item.content)
                        item.unContent = item.content
                        item.content = Match(item.content)
                        item.comment = 0
                        item.star = 0
                        if (item.lastModificationTime === null) {
                            item.lastModificationTime = '暂无'
                        } else {
                            item.lastModificationTime = new Date(item.lastModificationTime).format('Y.m.d H:i:s')
                        }
                        travelList.value.push(item)
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
                    tmpTravelData.value = travelList.value
                }
            })
        }
    })
}

onBeforeMount(() => {
    getTravels()
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