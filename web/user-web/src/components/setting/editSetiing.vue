<template>
    <!-- <h5>通知</h5>
    <div style="display: flex;">
        <p>评论消息</p>
        <el-switch v-model="userInfo.isNotice" class="switch" size="large"
            style="--el-switch-on-color: #13ce66; --el-switch-off-color: #ff4949" />
    </div> -->
    <el-form label-position="top" label-width="100%" size="large" :model="userInfo">
        <el-form-item label="注销账户">
            <el-input v-model="userInfo.userName" placeholder="请输入用户名确认注销" />
        </el-form-item>
    </el-form>
    <el-button round type="primary" class="btn" :loading="loading" @click="goDelete()">注销账户</el-button>
</template>

<script setup>
import { ref } from 'vue';
import store from '@/store'
import { onBeforeMount } from '@vue/runtime-core';
import { ElMessage } from 'element-plus'
import { deleteUser } from '@/api/identity/user';

const loading = ref(false)
const userInfo = ref({
    isNotice: true,
    userName: '',
})

const goDelete = () => {
    if (userInfo.value.userName === '' || userInfo.value.userName === null || userInfo.value.userName === undefined) {
        ElMessage.warning('未输入任何内容!')
        return
    }
    loading.value = true
    if (userInfo.value.userName === store.getters['identity/userInfo'].userName) {
        deleteUser(store.getters['identity/userInfo'].id).then(res => {
            if (res.status === 204) {
                ElMessage.success('注销成功!')
            }
            loading.value = false
        }).catch(() => {
            loading.value = false
        })
    } else {
        ElMessage.error('输入的用户名与当前登录用户不一致!')
        loading.value = false
    }
}
</script>

<style scoped>
p {
    margin: 0;
    margin-top: 10px;
}

h5 {
    font-weight: bold;
}

.switch {
    margin-left: 89%;
}

.btn {
    width: 250px;
    height: 35px;
    font-size: 14px;
    font-weight: bold;
    margin-top: 30px;
    margin-left: 35%;
    text-align: center;
    background-color: #66CCCC;
    border: 1px solid #66CCCC;
}

.btn:hover {
    transition: all 0.5s;
    color: white;
    background-color: #f56c6c;
    border: 1px solid #f56c6c;
}

.btn:focus {
    outline: 0;
    transition: all 0.5s;
    color: white;
    background-color: #f56c6c;
    border: 1px solid #f56c6c;
}
</style>

<style>
.el-input__wrapper,
.el-textarea__inner {
    border-radius: 20px;
}

.el-input__wrapper.is-focus,
.el-textarea__inner:focus {
    box-shadow: 0 0 0 1px #66CCCC;
}
</style>