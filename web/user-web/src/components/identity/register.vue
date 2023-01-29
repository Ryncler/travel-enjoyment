<template>
    <el-form ref="validData" :model="registerForm" :rules="registerRules" class="login-form" autocomplete="on"
        label-position="left">
        <div class="title-container">
            <h1 class="registerTitle"><em>欢迎您加入我们</em>...</h1>
            <h5 class="descriptionTitle">请填写以下信息提交，然后检查您的电子邮件激活账户</h5>
        </div>

        <el-form-item prop="username">
            <icon data="@/icons/user.svg" class="svg-container" />
            <el-input v-model="registerForm.username" placeholder="用户名" name="username" type="text" tabindex="1"
                autocomplete="on" />
        </el-form-item>

        <el-form-item prop="email">
            <icon data="@/icons/email.svg" class="svg-container" />
            <el-input v-model="registerForm.email" placeholder="邮箱" name="email" type="text" tabindex="2"
                autocomplete="on" />
        </el-form-item>

        <el-form-item prop="phone">
            <icon data="@/icons/phone.svg" class="svg-container" />
            <el-input v-model="registerForm.phone" placeholder="手机号" name="phone" type="text" tabindex="2"
                autocomplete="on" />
        </el-form-item>

        <el-form-item prop="password">
            <icon data="@/icons/password.svg" class="svg-container" />
            <el-input v-model="registerForm.password" :type="password" placeholder="密码" name="password" tabindex="3"
                autocomplete="on" show-password />
        </el-form-item>

        <el-form-item prop="confimPassword">
            <icon data="@/icons/password.svg" class="svg-container" />
            <el-input v-model="registerForm.confimPassword" :type="password" placeholder="确认密码" name="confimPassword"
                tabindex="4" autocomplete="on" show-password />
        </el-form-item>

        <div>
            <el-button :loading="loading" round type="primary" class="revertbtn addbtn"
                @click="goRegister()">注册</el-button>
            <el-button :loading="loading" round type="primary" class="revertbtn addbtn"
                @click="goLogin()">登录</el-button>
        </div>
    </el-form>
</template>

<script setup>
import { ref } from 'vue';
import store from '@/store'
import { ElMessage } from 'element-plus'
import { registerByUser } from '@/api/identity'
import { onBeforeMount } from '@vue/runtime-core';

const loading = ref(false)
const registerForm = ref({})
const validData = ref()

const validateUsername = (rule, value, callback) => {
    if (value === undefined || value.length < 2) {
        callback(new Error('请输入您的用户名'))
    } else {
        callback()
    }
}
const validatePassword = (rule, value, callback) => {
    if (value === undefined || value.length < 6) {
        callback(new Error('密码长度小于6位，请重新输入您的密码'))
    } else {
        callback()
    }
}
const validateConfimPassword = (rule, value, callback) => {
    if (value === undefined || value.length < 6) {
        callback(new Error('密码长度小于6位，请重新输入您的密码'))
    }
    if (value === undefined || value !== this.registerForm.password) {
        callback(new Error('您输入的密码不一致'))
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

const registerRules = {
    username: [{ required: true, trigger: 'blur', validator: validateUsername }],
    email: [{ required: true, trigger: 'blur', validator: validateEmail }],
    phone: [{ required: true, trigger: 'blur', validator: validatePhone }],
    password: [{ required: true, trigger: 'blur', validator: validatePassword }],
    confimPassword: [{ required: true, trigger: 'blur', validator: validateConfimPassword }]
}

const goRegister = () => {
    validData.value.validate(vali => {
        if (vali) {
            loading.value = true
            registerByUser(registerForm.value).then(res => {
                if (res.status === 204) {
                    ElMessage.success('注册成功！')
                    this.goLogin(this.loginForm)
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
const goLogin = () => {
    store.commit('identity/isShow', false)
}
</script>

<style>
.login-form {
    position: relative;
    width: 620px;
    max-width: 100%;
    padding: 230px 35px 0;
    margin: 0 auto;
    overflow: hidden;
}

.registerTitle {
    margin: auto auto 40px auto;
    text-align: center;
    font-weight: bold;
}

.descriptionTitle {
    text-align: right;
    color: red;
}

.addbtn {
    width: 200px;
    margin: 5% 10% 0 10%;
}

.el-form-item__error {
    left: 5%;
}
</style>