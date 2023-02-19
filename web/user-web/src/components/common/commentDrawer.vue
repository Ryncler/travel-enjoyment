<template>
    <el-drawer v-model="showDrawer" :show-close="false" size="40%">
        <template #header="{ close }">
            <h4 class="titleClass">回复列表</h4>
            <el-button type="danger" @click="close">
                <icon data="@/icons/close.svg" class="closeIcon" />
                关闭
            </el-button>
        </template>
        <el-card shadow="hover" class="card" :body-style="style" v-for="item in commentList" :key="item">
            <div class="card-info">
                <el-avatar class="avatar" :size="70" src="https://empty" @error="errorHandler">
                    <img :src="item.avatar" />
                </el-avatar>
                <p v-if="item.parentName === ''" class="username">{{ item.userName }}：</p>
                <p v-show="item.parentName !== ''" class="username">{{ item.userName }} <span class="replyText">回复</span> {{
                    item.parentName }}：
                </p>
                <p>{{ item.comment }}</p>
                <div class="last">
                    <p class="commentDate">{{ item.releaseDate }}</p>
                    <p class="reply">回复</p>
                </div>
            </div>
        </el-card>
    </el-drawer>
</template>

<script setup>
import { ref } from 'vue';
import { onBeforeMount } from '@vue/runtime-core';

const showDrawer = ref(false);
const commentList = ref([
    {
        id: '',
        avatar: 'https://pic3.zhimg.com/v2-58d652598269710fa67ec8d1c88d8f03_r.jpg?source=1940ef5c',
        userName: 'Ryncler',
        comment: '哇，好棒！asfawerfawefvsdfgvedsgr ',
        releaseDate: '2022.12.31'
    },
    {
        id: '',
        avatar: 'https://pic3.zhimg.com/v2-58d652598269710fa67ec8d1c88d8f03_r.jpg?source=1940ef5c',
        userName: 'AXB',
        parentName: 'Ryncler',
        comment: '哇，好棒！',
        releaseDate: '2023.1.10'
    }

])

const style = ref({
    padding: '0',
    width: '100%',
    height: '100%',
})

// eslint-disable-next-line no-undef
defineExpose({
    showDrawer
})
</script>

<style scoped>
h4 {
    font-weight: bold;
}

.card {
    height: 80px;
    border-radius: 20px;
    overflow: visible;
    margin-top: 20px;
}

.card-info {
    display: flex;
    width: 100%;
    height: 100%;
    display: flex;
    align-items: center;
    /* justify-content: space-between; */
}

.avatar {
    margin-left: 5px;
}

.username {
    margin-left: 10px;
    font-size: 18px;
    font-weight: bold;
    color: #66CCFF;
}

.last {
    margin-left: auto;
    justify-content: flex-end;
}

.commentDate {
    margin-right: 10px;
    margin-top: -3%;
}

.reply {
    margin-left: 40%;
    margin-top: 20px;
    font-size: 16px;
    font-weight: bold;
    color: #66CCFF;
}
</style>