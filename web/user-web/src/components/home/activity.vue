<template>
    <div class="activity">
        <div class="title">
            <h4 class="titleName">
                <icon data="@/icons/activity.svg" class="svg-container icon" />
                {{ title }}
            </h4>
        </div>
        <el-carousel :interval="4000" type="card" height="400px" class="carou">
            <el-carousel-item v-for="item in activityList" :key="item">
                <el-image :src="imageHandle(item.imgUrl)" :fit="contain" class="img" />
            </el-carousel-item>
        </el-carousel>
        <!-- <el-button round type="primary" class=" btn" @click="showCommentDrawer()">查看更多活动...</el-button> -->
    </div>
</template>

<script setup>
import { ref } from 'vue';
import { onBeforeMount } from '@vue/runtime-core';
import { getActivityList } from '@/api/sights/index'
import { getImagesById } from '@/api/common/minio'
import { imageHandle } from '@/utils/common';

var title = ref('近期热门景点活动')
const activityList = ref([
    {
        id: '',
        imgUrl: 'https://www.otsuka.co.jp/img/index_im01_01.jpg.webp',
    },
    {
        id: '',
        imgUrl: 'https://pic3.zhimg.com/v2-58d652598269710fa67ec8d1c88d8f03_r.jpg?source=1940ef5c',
    },
    {
        id: '',
        imgUrl: 'https://ts1.cn.mm.bing.net/th/id/R-C.df4462fabf18edd07195679a5f8a37e5?rik=FnNvr9jWWjHCVQ&riu=http%3a%2f%2fseopic.699pic.com%2fphoto%2f50059%2f8720.jpg_wh1200.jpg&ehk=ofb4q76uCls2S07aIlc8%2bab3H5zwrmj%2bhqiZ%2fyw3Ghw%3d&risl=&pid=ImgRaw&r=0',
    },
    {
        id: '',
        imgUrl: 'https://www.otsuka.co.jp/img/index_im01_01.jpg.webp',
    }
])

const getActivitys = (ids) => {
    getActivityList(ids).then(res => {
        if (res.status === 200) {
            activityList.value = res.data
            activityList.value.forEach(item => {
                getImagesById(item.id).then(res => {
                    if (res.status === 200) {
                        item.imgUrl = res.data.find(x => x.isMain === true).imageURL
                    }
                })
            })
        }
    })
}

// eslint-disable-next-line no-undef
defineExpose({
    title, getActivitys
});
</script>


<style scoped>
.activity {
    width: 100%;
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

.carou {
    margin-top: 20px;
    width: 100%;
}

.el-carousel__item h3 {
    color: #475669;
    opacity: 0.75;
    line-height: 200px;
    width: 100%;
    margin: 0;
    text-align: center;
}

.el-carousel__item:nth-child(2n) {
    background-color: #99a9bf;
}

.el-carousel__item:nth-child(2n + 1) {
    background-color: #d3dce6;
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

.img {
    width: 100%;
    height: 100%;
}
</style>