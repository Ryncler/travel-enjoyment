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
            <el-card class="travel-card" :body-style="style" v-for="travel in travelList" :key="travel.id" shadow="hover">
                <div class="travel-info">
                    <el-image :src="travel.imgUrl" :fit="fill" class="travel-img" />
                    <div class="content">
                        <h4 class="travel-name">
                            <icon data="@/icons/edit.svg" class="svg-container" />
                            {{ travel.name }}
                        </h4>
                        <p class="travel-contentInfo">{{ travel.content }}</p>
                        <div class="otherInfo">
                            <div class="otherItem">
                                <icon data="@/icons/heat.svg" class="svg-container otherIcon" />
                                <p>{{ travel.heat }}</p>
                            </div>
                            <div class="otherItem commentItem">
                                <icon data="@/icons/comment.svg" class="svg-container otherIcon" />
                                <p>{{ travel.comment }}</p>
                            </div>
                            <div class="otherItem starItem">
                                <icon data="@/icons/star.svg" class="svg-container otherIcon" />
                                <p>{{ travel.star }}</p>
                            </div>
                        </div>
                    </div>
                </div>
            </el-card>
        </el-col>
        <el-col :span="11" class="col">
            <el-card class="travel-card" :body-style="style" v-for="travel in travelList" :key="travel.id" shadow="hover">
                <div class="travel-info">
                    <el-image :src="travel.imgUrl" :fit="fill" class="travel-img" />
                    <div class="content">
                        <h4 class="travel-name">
                            <icon data="@/icons/edit.svg" class="svg-container" />
                            {{ travel.name }}
                        </h4>
                        <p class="travel-contentInfo">{{ travel.content }}</p>
                        <div class="otherInfo">
                            <div class="otherItem">
                                <icon data="@/icons/heat.svg" class="svg-container otherIcon" />
                                <p>{{ travel.heat }}</p>
                            </div>
                            <div class="otherItem commentItem">
                                <icon data="@/icons/comment.svg" class="svg-container otherIcon" />
                                <p>{{ travel.comment }}</p>
                            </div>
                            <div class="otherItem starItem">
                                <icon data="@/icons/star.svg" class="svg-container otherIcon" />
                                <p>{{ travel.star }}</p>
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
                    <h5>{{ item.time }}</h5>
                    <div class="trendsInfo">
                        <div class="flexDiv" v-for="share in item.comment" :key="share">
                            <p>分享了：</p>
                            <el-link type="primary" :href="share.url" :underline="false">
                                {{ share.title }}
                            </el-link>
                        </div>
                        <div class="flexDiv" v-for="comment in item.comment" :key="comment">
                            <p>评论了</p>
                            <el-link type="primary" :href="comment.url" :underline="false">
                                {{ comment.title }}
                            </el-link>
                            <p class="reply">：{{ comment.reply }}</p>
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
import { onBeforeMount } from '@vue/runtime-core';
import drawerVue from './drawer'

const drawer = ref(null)
const style = ref({
    padding: '0',
    width: '100%',
    height: '100%',
})

const travelList = ref([
    {
        id: '123',
        author: 'Ryncler',
        name: '珠峰的故乡，走进喜马拉雅秘境，越野藏东小环线',
        content: '说到西藏，很多人都会想到日光之城拉萨，林芝的桃花，还有阿里的广阔，这些都是耳熟能详的地方，随着近些年 西藏 旅行的热门，以前神秘的秘境 阿里 很多人都已经踏足，没有到过的人都会把 阿里 当作前往 西藏 必去的地方，究竟为什么 西藏 总是让人魂萦梦绕，它有着什么样的神奇力量，让大家对此流连忘返。雪域高原的巍峨雪山；镶嵌在大地上的湛蓝湖泊；无人区和草原上的广阔荒野；独树一帜的自然律动； 千百年来的历史文脉。',
        heat: 2034,
        comment: 26461,
        star: 2005,
        imgUrl: 'https://www.otsuka.co.jp/img/index_im01_01.jpg.webp'
    },
])
const trends = ref([
    {
        time: '2022.1.1',
        share: [
            {
                url: 'http://r123.ax.com',
                title: '西藏之旅'
            },
            {
                url: 'http://r123.ax.com',
                title: '西藏之旅'
            },
        ],
        comment: [
            {
                url: 'http://r123.ax.com',
                title: '西藏之旅',
                reply: '很有帮助！！！'
            },
            {
                url: 'http://r1223.ax.com',
                title: '西藏之旅',
                reply: '很有帮助！！！'
            },
            {
                url: 'http://r1423.ax.com',
                title: '西藏之旅',
                reply: '很有帮助！！！'
            },
        ]
    },
    {
        time: '2022.1.15',
        share: {
            url: 'http://r123.ax.com',
            title: '西藏之旅'
        },
        comment: {
            url: 'http://r123.ax.com',
            title: '西藏之旅',
            reply: '很有帮助！！！'
        }
    },
    {
        time: '2022.2.1',
        share: {
            url: 'http://r123.ax.com',
            title: '西藏之旅'
        },
        comment: {
            url: 'http://r1223.ax.com',
            title: '西藏之旅',
            reply: '很有帮助！！！'
        }
    },
    {
        time: '2022.5.1',
        share: {
            url: 'http://r123.ax.com',
            title: '西藏之旅'
        },
        comment: {
            url: 'http://r123.ax.com',
            title: '西藏之旅',
            reply: '很有帮助！！！'
        }
    },
])

const loadTrend = () => {

}
const showCustomTravel = () => {
    drawer.value.showDrawer = true
}
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
    width: 40%;
    height: 180px;
    border-radius: 20px;
    float: left;
}

.content {
    width: 60%;
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
    margin-left: 20px;
}

.otherIcon {
    margin-top: 2px;
}

.starItem,
.commentItem {
    margin-left: 15px;
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