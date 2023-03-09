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
                    <img :src="imageHandle(item.avatar)" />
                </el-avatar>
                <p v-if="item.parentId === '00000000-0000-0000-0000-000000000000'" class="username">{{ item.userName }}：</p>
                <p v-show="item.parentId !== '00000000-0000-0000-0000-000000000000'" class="username">{{ item.userName }}
                    <span class="replyText">回复</span>
                    {{ getParentName(item.parentId) }}：
                </p>
                <p>{{ item.content }}</p>
                <div class="last">
                    <p class="commentDate">{{ item.releaseDate }}</p>
                    <p class="reply" @click="goComment(item)">回复</p>
                </div>
            </div>
        </el-card>
    </el-drawer>
</template>

<script setup>
import { ref } from 'vue';
import store from '@/store'
import { onBeforeMount } from '@vue/runtime-core';
import { imageHandle } from '@/utils/common';
import { getCommentList, addComment } from '@/api/comment';
import { getUser } from '@/api/identity/user';
import { ElMessageBox, ElMessage } from 'element-plus';

const showDrawer = ref(false);
const travelId = ref('')
const commentList = ref([])

const style = ref({
    padding: '0',
    width: '100%',
    height: '100%',
})

const getComments = (id) => {
    getCommentList(id).then(res => {
        if (res.status === 200) {
            commentList.value = res.data
            commentList.value = commentList.value.map((item) => {
                getUser(item.userId).then(user => {
                    if (user.status === 200) {
                        item.avatar = user.data.avatar
                        item.userName = user.data.userName
                        item.releaseDate = new Date(item.releaseDate).format('Y.m.d H:i:s')
                    }
                })
                return item
            })
        }
    })
}

const getParentName = (parentId) => {
    var res = commentList.value.find(x => x.id === parentId)
    if (res !== undefined)
        return res.userName
}

const goComment = (comment) => {
    var data = {
        travelsId: travelId.value,
        userId: store.getters['identity/userInfo'].id,
        parentId: comment.id,
        content: ''
    }
    if (comment.userId === data.userId) {
        ElMessage({
            type: 'warning',
            message: '您这么幽默的吗？'
        })
        return
    }
    ElMessageBox.prompt('请输入要回复的内容', 'Tips', {
        confirmButtonText: '保存',
        cancelButtonText: '取消',
        inputPattern: /\S+/,
        inputErrorMessage: '不能回复空的内容',
    }).then(({ value }) => {
        data.content = value
        console.log(data);
        addComment(data).then(res => {
            if (res.status === 200) {
                ElMessage({
                    type: 'success',
                    message: '回复成功'
                })
            }
        })
    }).catch(() => { })
}

// eslint-disable-next-line no-undef
defineExpose({
    showDrawer, travelId, getComments
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

.replyText {
    font-size: 15px;
    font-weight: bold;
    color: black;
}
</style>