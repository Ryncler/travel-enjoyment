<template>
    <el-form ref="validData" :model="loginForm" :rules="loginRules" class="login-form" label-position="left">
        <div class="title-container">
            <h1 class="loginTitle"><em>欢迎您回来</em>...</h1>
        </div>
        <el-form-item prop="username">
            <icon data="@/icons/user.svg" class="svg-container" />
            <el-input v-model="loginForm.username" placeholder="用户名" name="username" type="text" tabindex="1" />
        </el-form-item>
        <el-form-item prop="password">
            <icon data="@/icons/password.svg" class="svg-container" />
            <el-input v-model="loginForm.password" :type="password" placeholder="密码" name="password" tabindex="3"
                autocomplete="on" show-password />
        </el-form-item>
        <div>
            <el-button :loading="loading" round type="primary" class="revertbtn addbtn" @click="goLogin()">登录</el-button>
            <el-button :loading="loading" round type="primary" class="revertbtn addbtn" @click="goRegister()">注册</el-button>
        </div>
    </el-form>
</template>

<script setup>
import { ref, reactive } from 'vue';
import store from '@/store'
import router from '@/router'
import { login, getInfo, getUser } from '@/api/identity'
import { ElMessage } from 'element-plus'
import { onBeforeMount } from '@vue/runtime-core';

const loading = ref(false)
// eslint-disable-next-line no-undef
const showRegister = ref(store.getters['identity/isShow'])
const loginForm = ref({
    username: 'admin',
    password: 'aA10086'
})
const validData = ref()

const validateUserName = (rule, value, callback) => {
    if (value === undefined || value.length < 2)
        callback(new Error('用户名不能小于2位'))
    else
        callback()
}
const validatePassword = (rule, value, callback) => {
    if (value === undefined || value.length < 6)
        callback(new Error('密码长度不能小于6位'))
    else
        callback()
}
const loginRules = reactive({
    username: [{ required: true, trigger: 'blur', validator: validateUserName }],
    password: [{ required: true, trigger: 'blur', validator: validatePassword }]
})

const goLogin = () => {
    validData.value.validate(vali => {
        if (vali) {
            loading.value = true
            login(loginForm.value).then(res => {
                if (res.status === 200) {
                    store.commit('identity/setToken', res.data.access_token)
                    goUserInfo()
                    ElMessage.success('登录成功！')
                }
                loading.value = false
            }).catch(() => {
                loading.value = false
            })
        } else {
            ElMessage.error('请检查错误项！')
            return false
        }
    })
}

const goUserInfo = () => {
    getInfo().then(res => {
        if (res.status === 200) {
            getUser(res.data.sub).then(item => {
                if (item.status === 200) {
                    store.commit('identity/setUserInfo', item.data)
                    router.push({ name: 'Home' })
                }
            })
        }
    })
}

const goRegister = () => {
    store.commit('identity/isShow', true)
}

// eslint-disable-next-line no-undef
defineExpose({
    loading, loginForm, loginRules, showRegister,
    goRegister,
});
</script>

<style>
.login-form {
    position: relative;
    width: 620px;
    max-width: 100%;
    padding: 160px 35px 0;
    margin: 0 auto;
    overflow: hidden;
}

.loginTitle {
    margin: 100px auto 40px auto;
    text-align: center;
    font-weight: bold;
}

.addbtn {
    width: 200px;
    margin: 5% 8% 0 8%;
}

.el-form-item__error {
    left: 5%;
}
</style>