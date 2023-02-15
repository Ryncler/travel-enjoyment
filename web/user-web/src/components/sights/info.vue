<template>
    <el-cascader popper-class="elcas" style="width: 100%;" v-model="mapIds" :options="geoData" :props="props"
        @change="handleChange" filterable />

    <div class="sightsInfo">
        <h2 class="sightsName">{{ sightsData.name }}</h2>
        <el-carousel :interval="4000" type="card" height="400px" class="carou">
            <el-carousel-item v-for="item in activityList" :key="item">
                <el-image :src="item.imgUrl" :fit="contain" />
            </el-carousel-item>
        </el-carousel>
        <div class="title">
            <h4 class="titleName">
                <icon data="@/icons/sights-info.svg" class="svg-container icon" />
                {{ staticSightsInfo.title }}
            </h4>
        </div>
        <el-collapse class="elcollapse" accordion>
            <el-collapse-item :name="index" v-for="item, index in sightsData.infos" :key="item">
                <template #title>
                    <icon data="@/icons/sights-info.svg" class="svg-container" />
                    {{ item.title }}
                </template>
                {{ item.content }}
            </el-collapse-item>
        </el-collapse>

        <div class="title">
            <h4 class="titleName">
                <icon data="@/icons/activity.svg" class="svg-container icon" />
                {{ staticSightsInfo.activeTitle }}
            </h4>
        </div>
        <div class="activtiy">
            <el-card class="card" :body-style="style" v-for="item in sightsData.acitvityList" :key="item.id">
                <el-image class="img" :src="item.imgUrl" />
                <div class="info">
                    <p>更新时间：{{ item.changeTime }}</p>
                    <h3>{{ item.name }}</h3>
                    <p class="contentInfo">{{ item.content }}</p>
                </div>
            </el-card>
            <div class="pageDiv">
                <el-pagination class="page" :page-size="pageSize" :pager-count="10" layout="prev, pager, next"
                    :total="totalCount" hide-on-single-page @size-change="goSizeChange"
                    @current-change="goCurrentChange" />
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref } from 'vue';
import { onBeforeMount } from '@vue/runtime-core';
import { getGeoTree } from '@/api/common';

const mapIds = ref([])
const geoData = ref([])
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
const totalCount = ref(51)

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
const sightsData = ref({
    name: '九寨沟',
    infos: [
        {
            title: '概述',
            content: '九寨沟位于阿坝州九寨沟县境内，沟中雪峰林立，林木茂密，水池浅滩五彩斑斓，晶莹的溪水穿行于森林和浅滩之间，加上藏家木楼、经幡和藏羌民族独特的传统习俗，九寨沟被人们称为“美丽的童话世界”。\r\n九寨沟主体呈“Y”字形，由树正沟、日则沟和则查洼沟三条沟组成，总长50余公里。景区内有环保观光车穿梭行驶，招手即停，游客一般会乘坐环保车逐一游览景点。'
        },
        {
            title: '开放时间',
            content: '5月1日-10月31日 07:30-17:00'
        }
    ],
    acitvityList: [
        {
            name: '疯狂电音节',
            imgUrl: 'https://www.otsuka.co.jp/img/index_im01_01.jpg.webp',
            changeTime: '2022.12.13',
            content: 'asdjfiajwoefnamoifnaodfmasdfainofuaisohfnauisnhfvcawipoenjfaiwopsefn923840jrmwfiaosdfpsawifujhq89i3fnjrmaswpdfcap9w83pur8932jmiowpfajuq98304pwanfnmapsifhnsap9fihjapsfnwaeiopfrmnqp943r8ijmqawfdriawpofjmaswofikaefoiwenfr'
        },
        {
            name: '疯狂电音节',
            imgUrl: 'https://www.otsuka.co.jp/img/index_im01_01.jpg.webp',
            changeTime: '2022.12.13',
            content: 'asdjfiajwoefnamoifnaodfmasdfainofuaisohfnauisnhfvcawipoenjfaiwopsefn923840jrmwfiaosdfpsawifujhq89i3fnjrmaswpdfcap9w83pur8932jmiowpfajuq98304pwanfnmapsifhnsap9fihjapsfnwaeiopfrmnqp943r8ijmqawfdriawpofjmaswofikaefoiwenfr'
        },
        {
            name: '疯狂电音节',
            imgUrl: 'https://www.otsuka.co.jp/img/index_im01_01.jpg.webp',
            changeTime: '2022.12.13',
            content: 'asdjfiajwoefnamoifnaodfmasdfainofuaisohfnauisnhfvcawipoenjfaiwopsefn923840jrmwfiaosdfpsawifujhq89i3fnjrmaswpdfcap9w83pur8932jmiowpfajuq98304pwanfnmapsifhnsap9fihjapsfnwaeiopfrmnqp943r8ijmqawfdriawpofjmaswofikaefoiwenfr'
        }
    ]
})

const getGeoData = () => {
    return getGeoTree().then(res => {
        if (res.status === 200) {
            geoData.value = res.data
        }
    })
}

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

onBeforeMount(() => {
    getGeoData()
})
</script>

<style scoped>
.sightsInfo {
    margin-top: 40px;
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
    height: 450px;
    border-radius: 10px;
    width: 32%;
    overflow: visible;
}

.info {
    margin-left: 10px;
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