<template>
    <el-card :body-style="style" class="card">
        <div class="tagInfo">
            <icon data="@/icons/up.svg" class="svg-container icon" />
            <p class="tagTitle">热门标签:</p>
            <div class="tagList">
                <p class="tags fadeInRight" v-for="item in tagList" :key="item.id">#{{ item.name }}</p>
            </div>
        </div>
    </el-card>
</template>

<script setup>
import { ref } from 'vue';
import { onBeforeMount } from '@vue/runtime-core';
import { getTagList } from '@/api/sights/index'

const style = ref({
    padding: '0',
    width: '100%',
    height: '80px',
})

const tagList = ref([])

const getTags = (ids) => {
    getTagList(ids).then(res => {
        if (res.status === 200) {
            console.log(res.data);
            tagList.value = res.data
        }
    })
}

// eslint-disable-next-line no-undef
defineExpose({
    getTags
})
</script>

<style scoped>
.card {
    width: 100%;
    margin: 10px 0 0 0;
    background-color: #66CCCC;
    border-radius: 10px;
}

.icon {
    color: white;
    width: 50px;
    height: 60px;
    margin-top: 10px;
    margin-left: 20px;
}

.tagTitle {
    margin: 0;
    margin-top: 30px;
    margin-left: 5px;
    text-align: center;
    font-size: 18px;
    font-weight: bold;
    color: white;
}

.tagInfo,
.tagList {
    height: 100%;
    display: flex;
}

.tags {
    margin: 0;
    margin-top: 32px;
    margin-left: 25px;
    color: white;
    font-weight: bold;
}

@keyframes fadeInRight {
    from {
        opacity: 0;
        transform: translate(1000px, 0);
    }

    to {
        opacity: 1;
        transform: translate(10px, 0);
    }
}

.fadeInRight {
    animation: fadeInRight 3.5s infinite;
}
</style>