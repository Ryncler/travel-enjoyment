<template>
    <div>
        <div class="title">
            <h4 class="titleName">
                <icon data="@/icons/top.svg" class="svg-container icon" />
                {{ title }}
            </h4>
        </div>
        <div class="sightsInfo">
            <el-card class="card" :body-style="style" v-for="item, index in sightsList" :key="item.id"
                @click="goSightsInfo(item.id)">
                <el-image :src="imageHandle(item.imgUrl)" style="width: 100%;height: 200px;border-radius: 10px;" :fit="fit">
                    <template #error>
                        <div class="image-slot">
                            <icon data="@/icons/image.svg" />
                        </div>
                    </template>
                </el-image>
                <p class="number">{{ index + 1 }}</p>
                <div class="info">
                    <p>更新时间：{{ item.lastModificationTime }}</p>
                    <h3>{{ item.name }}</h3>
                    <div>
                        <el-check-tag checked size="large" class="tag" type="info" v-for="tag in item.tagList"
                            :key="tag">
                            #{{ tag.tagName }}
                        </el-check-tag>
                    </div>
                </div>
            </el-card>
        </div>
        <el-button round type="primary" class=" btn">查看更多景点...</el-button>
    </div>
</template>

<script setup>
import { ref } from 'vue';
import { onBeforeMount } from '@vue/runtime-core';
import { imageHandle } from '@/utils/common';
import router from '@/router'
import { getSightsList, getTagIdListBySightsId, getTagList } from '@/api/sights/index'
import { getImagesById } from '@/api/common/minio'

var title = ref('景点TOP6')
const sightsList = ref([])
const style = ref({
    padding: '0',
    width: '100%',
    height: '100%',
})

const getSights = (ids) => {
    getSightsList(ids).then(res => {
        if (res.status === 200) {
            sightsList.value = res.data
            sightsList.value.forEach(item => {
                getTagIdListBySightsId(item.id).then(res => {
                    if (res.status === 200) {
                        item.tagList = res.data
                    }
                })
                getImagesById(item.id).then(res => {
                    if (res.status === 200) {
                        item.imgUrl = res.data.find(x => x.isMain === true).imageURL
                    }
                })
            })
        }
    })
}

const goSightsInfo = (id) => {
    router.push({ name: 'Sights', path: 'info', query: { id: id } })
}
// eslint-disable-next-line no-undef
defineExpose({
    title, getSights
});
</script>

<style scoped>
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

.sightsInfo {
    width: 100%;
    height: 100%;
    display: flex;
    align-items: center;
    flex-wrap: wrap;
    justify-content: space-between;
}

.sightsInfo:after {
    flex: auto;
    margin-left: 10px;
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

.card {
    position: relative;
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

.tag {
    border-radius: 50px;
    margin: 5px;
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

.image-slot {
    display: flex;
    justify-content: center;
    align-items: center;
    width: 100%;
    height: 100%;
    background: var(--el-fill-color-light);
    color: var(--el-text-color-secondary);
    font-size: 30px;
}
</style>