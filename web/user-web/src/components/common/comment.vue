<template>
    <div class="commentDiv">
        <div>
            <h4 style="float:left ; width:90%;">评论</h4>
            <p class="reply firstReply" @click="goComment({})">发表评论</p>
        </div>
        <el-divider />
        <div v-for="item in commentList" :key="item.id">
            <el-card shadow="hover" class="card" :body-style="style">
                <div class="card-info">
                    <el-avatar class="avatar" :size="70" src="https://empty" @error="errorHandler">
                        <img :src="imageHandle(item.avatar)" />
                    </el-avatar>
                    <p class="username">{{ item.userName }}：</p>
                    <p>{{ item.content }}</p>
                    <div class="last">
                        <p class="commentDate">{{ item.releaseDate }}</p>
                        <p class="reply" @click="goComment(item)">回复</p>
                    </div>
                </div>
            </el-card>
            <el-card shadow="hover" class="childentCard" :body-style="style" v-if="item.children.length > 0">
                <div class="card-info">
                    <el-avatar class="avatar" :size="70" src="https://empty" @error="errorHandler">
                        <img :src="imageHandle(item.children[0].avatar)" />
                    </el-avatar>
                    <p class="username">{{ item.children[0].userName }} <span class="replyText">回复</span>
                        {{ item.userName }}：
                    </p>
                    <p>{{ item.children[0].content }}</p>
                    <div class="last">
                        <p class="commentDate">{{ item.children[0].releaseDate }}</p>
                        <p class="reply" @click="goComment(item.children[0])">回复</p>
                    </div>
                </div>
            </el-card>
            <el-button round type="primary" class=" btn" @click="showCommentDrawer(item.id)"
                v-if="item.children.length > 0">查看更多回复...</el-button>
        </div>

        <div class="pageDiv">
            <el-pagination class="page" :page-size="pageSize" :pager-count="10" layout="prev, pager, next" background
                :total="totalCount" hide-on-single-page @size-change="goSizeChange" @current-change="goCurrentChange" />
        </div>
    </div>
    <commentDrawerVue ref="drawer"></commentDrawerVue>
</template>

<script setup>
import { ref } from 'vue';
import store from '@/store'
import { onBeforeMount } from '@vue/runtime-core';
import { ElMessageBox, ElMessage } from 'element-plus';
import { imageHandle } from '@/utils/common';
import { getCommentTree, addComment } from '@/api/comment';
import commentDrawerVue from './commentDrawer.vue'
import { getUser } from '@/api/identity/user';

const drawer = ref(null)
const travelId = ref('')
const commentList = ref([])
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
const totalCount = ref(1)

const goSizeChange = (value) => {
    pageSize.value = value
    getTreeComment()
}

const goCurrentChange = (value) => {
    currentPage.value = value
    getTreeComment()
}


const getTreeComment = () => {
    var parms = {
        id: travelId.value,
        childrenCount: 1,
        isall: true,
        maxResultCount: pageSize.value,
        skipCount: currentPage.value
    }
    getCommentTree(parms).then(res => {
        if (res.status === 200) {
            commentList.value = res.data.items
            commentList.value = commentList.value.map((item) => {
                getUser(item.userId).then(user => {
                    if (user.status === 200) {
                        item.avatar = user.data.avatar
                        item.userName = user.data.userName
                        item.releaseDate = new Date(item.releaseDate).format('Y.m.d H:i:s')
                    }
                })
                item.children.forEach(i => {
                    getUser(i.userId).then(u => {
                        if (u.status === 200) {
                            i.avatar = u.data.avatar
                            i.userName = u.data.userName
                            i.releaseDate = new Date(item.releaseDate).format('Y.m.d H:i:s')
                        }
                    })
                });
                return item
            })
        }
    })

}

const showCommentDrawer = (id) => {
    drawer.value.showDrawer = true
    drawer.value.travelId = travelId.value
    drawer.value.getComments(id)
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
    travelId, getTreeComment
})
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
.firstReply{
    margin: 0;
    padding-top: 10px;
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

.page {
    margin-top: 30px;
    text-align: center;
    align-content: center;
    justify-content: center;
}
</style>


<style>
.el-pagination.is-background .el-pager li.is-active {
    background-color: #66CCCC;
}


.el-pager li {
    width: 40px;
    height: 40px;
    border-radius: 10px;
}

.el-pagination button {
    border-radius: 10px;
    width: 40px;
    height: 40px;
}

.el-pager li:hover {
    color: #66CCCC;
}
</style>