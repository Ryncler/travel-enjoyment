<template>
    <div class="commentDiv">
        <h4>评论</h4>
        <el-divider />
        <div>
            <el-card shadow="hover" class="card" :body-style="style">
                <div class="card-info">
                    <el-avatar class="avatar" :size="70" src="https://empty" @error="errorHandler">
                        <img :src="userData.avatar" />
                    </el-avatar>
                    <p class="username">{{ userData.userName }}：</p>
                    <p>{{ userData.comment }}</p>
                    <div class="last">
                        <p class="commentDate">{{ userData.releaseDate }}</p>
                        <p class="reply">回复</p>
                    </div>
                </div>
            </el-card>
            <el-card shadow="hover" class="childentCard" :body-style="style">
                <div class="card-info">
                    <el-avatar class="avatar" :size="70" src="https://empty" @error="errorHandler">
                        <img :src="userData.avatar" />
                    </el-avatar>
                    <p class="username">{{ userData.userName }} <span class="replyText">回复</span> {{ userData.parentName }}：
                    </p>
                    <p>{{ userData.comment }}</p>
                    <div class="last">
                        <p class="commentDate">{{ userData.releaseDate }}</p>
                        <p class="reply">回复</p>
                    </div>
                </div>
            </el-card>
            <el-button round type="primary" class=" btn" @click="showCommentDrawer()">查看更多回复...</el-button>
        </div>
        <div>
            <el-card shadow="hover" class="card" :body-style="style">
                <div class="card-info">
                    <el-avatar class="avatar" :size="70" src="https://empty" @error="errorHandler">
                        <img :src="userData.avatar" />
                    </el-avatar>
                    <p class="username">{{ userData.userName }}：</p>
                    <p>{{ userData.comment }}</p>
                    <div class="last">
                        <p class="commentDate">{{ userData.releaseDate }}</p>
                        <p class="reply">回复</p>
                    </div>
                </div>
            </el-card>
        </div>
    </div>
    <commentDrawerVue ref="drawer"></commentDrawerVue>
</template>

<script setup>
import { ref } from 'vue';
import { onBeforeMount } from '@vue/runtime-core';
import commentDrawerVue from './commentDrawer.vue'

const drawer = ref(null)
const userData = ref(
    {
        id: '',
        avatar: 'https://pic3.zhimg.com/v2-58d652598269710fa67ec8d1c88d8f03_r.jpg?source=1940ef5c',
        userName: 'Ryncler',
        parentName: '阿西吧',
        comment: '哇，好棒！asfawerfawefvsdfgvedsgr ',
        releaseDate: '2022.12.31'
    }
)
const style = ref({
    padding: '0',
    width: '100%',
    height: '100%',
})

const showCommentDrawer = () => {
    drawer.value.showDrawer = true
}
</script>

<style scoped>
h4 {
    font-weight: bold;
}

.commentDiv {
    margin-top: 100px;
}

.card {
    height: 80px;
    border-radius: 20px;
    overflow: visible;
    margin-top: 20px;
}

.childentCard {
    margin-left: 60px;
    height: 80px;
    border-radius: 20px;
    overflow: visible;
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

.replyText {
    font-size: 15px;
    font-weight: bold;
    color: black;
}

.btn {
    width: 300px;
    height: 35px;
    font-size: 14px;
    font-weight: bold;
    margin-top: 10px;
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
</style>