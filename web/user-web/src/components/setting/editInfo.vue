<template>
    <el-form ref="validData" label-position="top" label-width="100%" size="large" :model="userInfo" :rules="infoRules">
        <el-form-item label="用户名" prop="userName">
            <el-input v-model="userInfo.userName" placeholder="User Name" />
        </el-form-item>
        <el-form-item label="个人简介" prop="profile">
            <el-input type="textarea" :rows="4" v-model="userInfo.profile" placeholder="Self Profile" />
        </el-form-item>
        <el-form-item label="邮箱" prop="email">
            <el-input v-model="userInfo.email" placeholder="Email" />
        </el-form-item>
        <el-form-item label="手机号" prop="phone">
            <el-input v-model="userInfo.phone" placeholder="Phone" />
        </el-form-item>
    </el-form>
    <el-button round type="primary" class="btn" :loading="loading" @click="goSave()">保存</el-button>
</template>

<script setup>
import { ref } from 'vue';
import store from '@/store'
import { onBeforeMount } from '@vue/runtime-core';
import { ElMessage } from 'element-plus'
import { updateUser } from '@/api/identity/user';

const validData = ref()
const loading = ref(false)
const userInfo = ref(store.getters['identity/userInfo'])

const validateUsername = (rule, value, callback) => {
    if (value.length < 2) {
        callback(new Error('请输入您的用户名'))
    } else {
        callback()
    }
}

const validateEmail = (rule, value, callback) => {
    // eslint-disable-next-line no-useless-escape
    const regEmail = /^([a-zA-z0-9]+[-_\.]?)+@[a-zA-z0-9]+\.[a-z]+$/
    if (!regEmail.test(value)) {
        callback(new Error('您输入的邮箱不符合规范'))
    } else {
        callback()
    }
}
const validatePhone = (rule, value, callback) => {
    // eslint-disable-next-line no-useless-escape
    const regEmail = /^1(3[0-9]|4[01456879]|5[0-35-9]|6[2567]|7[0-8]|8[0-9]|9[0-35-9])\d{8}$/
    if (!regEmail.test(value)) {
        callback(new Error('您输入的手机号不符合规范'))
    } else {
        callback()
    }
}
const infoRules = {
    username: [{ required: true, trigger: 'blur', validator: validateUsername }],
    email: [{ required: true, trigger: 'blur', validator: validateEmail }],
    phone: [{ required: true, trigger: 'blur', validator: validatePhone }],
}
const goSave = () => {
    validData.value.validate(vali => {
        if (vali) {
            loading.value = true
            updateUser(userInfo.value).then(res => {
                if (res.status === 200) {
                    store.commit('identity/setUserInfo', res.data)
                    ElMessage.success('修改成功!')
                }
                loading.value = false
            }).catch(() => {
                loading.value = false
            })
        }
    })
}


// eslint-disable-next-line no-undef
defineExpose({
    userInfo
});
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