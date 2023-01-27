<template>
    <el-row>
        <el-card class="elcard" shadow="hover">
            <h4 class="title">近期热门游记</h4>
            <el-card shadow="hover" :body-style="style" class="elcardItem" v-for="item in sightsData" :key="item.id">
                <el-image style="width: 80px; height: 80px;border-radius: 10px;" :src="item.url" fit="cover" />
                <div class="info">
                    <div class="sightsA">
                        <h4 class="sightsTitle">{{ item.author }}</h4>
                        <p class="sightsConent">
                            {{ item.content }}
                        </p>
                    </div>
                    <div class="sightsB">
                        <p class="time">{{ item.changTime }}</p>
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
                <el-image style="width: 80px; height: 80px;border-radius: 10px;" :src="item.avatar" fit="cover" />
                <div class="info">
                    <h4 class="userName">{{ item.userName }}</h4>
                    <p class="time">{{ item.changTime }}</p>
                    <p>新发布游记：{{ item.travelNumber }}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        评论：{{ item.commentNumber }}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        新增收藏：{{ item.starNumber }}
                    </p>
                </div>
            </el-card>
        </el-card>
    </el-row>
</template>

<script setup>
import { ref } from 'vue';

const sightsData = ref([
    {
        author: 'Ryncler',
        url: 'https://fuss10.elemecdn.com/e/5d/4a731a90594a4af544c0c25941171jpeg.jpeg',
        title: 'aaa',
        content: 'asdnhfiuashnfidsafuhawehowanfdsfcaadeswfaewrf',
        changTime: '2023.1.1'
    },
    {
        author: 'Axxb',
        url: 'https://fuss10.elemecdn.com/e/5d/4a731a90594a4af544c0c25941171jpeg.jpeg',
        title: 'aaa',
        content: 'asdnhfiuashnfidsafuhawehowanfdsfcaadeswfaewrf',
        changTime: '2022.10.1'
    },
    {
        author: 'Ryncler',
        url: 'https://fuss10.elemecdn.com/e/5d/4a731a90594a4af544c0c25941171jpeg.jpeg',
        title: 'aaa',
        content: 'asdnhfiuashnfidsafuhawehowanfdsfcaadeswfaewrf',
        changTime: '2023.1.1'
    },
])

const userData = ref([
    {
        userName: 'Ryncler',
        avatar: 'https://fuss10.elemecdn.com/e/5d/4a731a90594a4af544c0c25941171jpeg.jpeg',
        changTime: '2023.1.25',
        travelNumber: 2,
        commentNumber: 100,
        starNumber: 3
    },
    {
        userName: 'AXB',
        avatar: 'https://fuss10.elemecdn.com/e/5d/4a731a90594a4af544c0c25941171jpeg.jpeg',
        changTime: '2023.1.26',
        travelNumber: 1,
        commentNumber: 67,
        starNumber: 0
    }
])
const style = ref({
    padding: '0px',
    width: '100%',
    height: '80px'
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