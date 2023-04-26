<template>
    <el-row>
        <el-card class="elcard" shadow="hover">
            <h4 class="title">近期热门游记</h4>
            <el-card shadow="hover" :body-style="style" class="elcardItem" v-for="item in sightsData" :key="item.id">
                <el-image style="width: 80px; height: 80px;border-radius: 10px;" :src="item.imgUrl" fit="cover" />
                <div class="info">
                    <div class="sightsA">
                        <h4 class="sightsTitle">{{ item.travelsTitle }}</h4>
                        <p class="sightsConent">
                            {{ item.content }}
                        </p>
                    </div>
                    <div class="sightsB">
                        <p class="time">{{ item.lastModificationTime }}</p>
                        <p>作者：<a href="#">{{ item.author }}</a></p>
                    </div>
                </div>
            </el-card>
        </el-card>
    </el-row>
    <el-divider />
    <el-row>
        <el-card class="userElcard" shadow="hover">
            <h4 class="title">近期活跃用户</h4>
            <el-card shadow="hover" :body-style="style" class="userElcardItem" v-for="item in userData" :key="item.id">
                <el-image style="width: 80px; height: 80px;border-radius: 10px;" :src="imageHandle(item.avatar)"
                    fit="cover" />
                <div class="info">
                    <h4 class="userName">{{ item.userName }}</h4>
                    <p class="time">{{ item.lastTime }}</p>
                    <p>新发布游记：{{ item.travelNum }}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        评论：{{ item.commentNum }}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        新增收藏：{{ item.starNum }}
                    </p>
                </div>
            </el-card>
        </el-card>
    </el-row>
</template>

<script setup>
import { ref } from 'vue';
import { onBeforeMount } from '@vue/runtime-core';
import { Match, getImageByDoc, imageHandle } from '@/utils/common/index'
import { getRecentHotTravel, getRecentHotUser } from '@/api/common/index'
import { getUser } from '@/api/user/user'

const sightsData = ref([])
const userData = ref([])

const style = ref({
    padding: '0px',
    width: '100%',
    height: '80px'
})

const getTravels = () => {
    return getRecentHotTravel().then(res => {
        if (res.status === 200) {
            sightsData.value = res.data
            sightsData.value = sightsData.value.map((item) => {
                getUser(item.creatorId).then(user => {
                    if (user.status === 200) {
                        item.author = user.data.userName
                    }
                })
                item.imgUrl = getImageByDoc(item.content)
                item.content = Match(item.content)
                if (item.lastModificationTime === null) {
                    item.lastModificationTime = new Date(item.creationTime).format('Y.m.d')
                } else {
                    item.lastModificationTime = new Date(item.lastModificationTime).format('Y.m.d')
                }
                return item
            })
        }
    })
}

const getUsers = () => {
    return getRecentHotUser().then(res => {
        if (res.status === 200) {
            userData.value = res.data.map((item) => {
                item.lastTime = new Date(item.lastTime).format('Y.m.d')
                return item
            })
        }
    })
}

onBeforeMount(() => {
    getTravels()
    getUsers()
})
</script>

<style scoped>
.title {
    margin: 0 0 10px 0;
}

.userName {
    margin: 0 0 10px 0;
    float: left;
    width: 75%;
}

.sightsTitle {
    margin: 0 0 10px 0;
}

.sightsConent {
    margin: 0 0 0 0;
    overflow: hidden;
    text-overflow: ellipsis;
}

.elcardItem {
    margin: 10px;
    height: 80px;
    border-radius: 10px;
}

.elcard {
    width: 100%;
    height: 340px;
}

.userElcard {
    width: 100%;
    height: 300px;
}

.userElcardItem {
    margin: 10px 10px 30px 10px;
    height: 80px;
    border-radius: 10px;
}

.img {
    margin: 0 auto;
    float: left;
}

.info {
    margin: 0 auto;
    float: right;
    width: 80%;
}

.sightsA {
    margin: 10px 0 0 0;
    float: left;
    width: 60%;
}

.sightsB {
    margin: 0 10px 0 0;
    float: right;
}

.time {
    margin: 5px 0 0 30px;
}
</style>