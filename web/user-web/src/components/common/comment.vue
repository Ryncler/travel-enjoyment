<template>
    <div class="commentDiv">
        <h4>评论</h4>
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
                        <p class="reply">回复</p>
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
                        <p class="reply">回复</p>
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
import { onBeforeMount } from '@vue/runtime-core';
import { imageHandle } from '@/utils/common';
import { getCommentTree } from '@/api/comment';
import commentDrawerVue from './commentDrawer.vue'
import { getUser } from '@/api/identity/user';

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
const totalCount = ref(100)

const goSizeChange = (value) => {
    pageSize.value = value
}

const goCurrentChange = (value) => {
    currentPage.value = value
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
    drawer.value.getComments(id)
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