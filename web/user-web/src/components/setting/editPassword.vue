<template>
    <el-form label-position="top" label-width="100%" size="large" :model="userInfo">
        <el-form-item label="新密码">
            <el-input v-model="userInfo.password" placeholder="Password" />
        </el-form-item>
        <el-form-item label="确认密码">
            <el-input v-model="userInfo.confirmPassword" placeholder="Confirm Password" />
        </el-form-item>
    </el-form>
    <el-button round type="primary" class="btn" @click="loadTrend()">保存</el-button>
</template>

<script setup>
import { ref } from 'vue';
import { onBeforeMount } from '@vue/runtime-core';

const userInfo = ref({
    password: '',
    confirmPassword: '',
})
</script>

<style scoped>
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
    background-color: #66CCFF;
    border: 1px solid #66CCFF;
}

.btn:focus {
    outline: 0;
    transition: all 0.5s;
    color: white;
    background-color: #66CCFF;
    border: 1px solid #66CCFF;
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