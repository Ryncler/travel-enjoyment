<template>
    <el-form ref="validData" label-position="top" autocomplete="on" label-width="100%" size="large" :model="userInfo"
        :rules="passwordRules">
        <el-form-item label="旧密码" prop="oldPassword">
            <el-input v-model="userInfo.oldPassword" placeholder="Old Password" type="password" />
        </el-form-item>
        <el-form-item label="新密码" prop="newPassword">
            <el-input v-model="userInfo.newPassword" placeholder="New Password" type="password" />
        </el-form-item>
        <el-form-item label="确认密码" prop="confirmPassword">
            <el-input v-model="userInfo.confirmPassword" placeholder="Confirm Password" type="password" />
        </el-form-item>
    </el-form>
    <el-button round type="primary" class="btn" :loading="loading" @click="goSave()">保存</el-button>
</template>

<script setup>
import { ref } from 'vue';
import store from '@/store'
import { onBeforeMount } from '@vue/runtime-core';
import { ElMessage } from 'element-plus'
import { updatePassword } from '@/api/identity/user';

const validData = ref()
const loading = ref(false)
const userInfo = ref({
    userId: store.getters['identity/userInfo'].id,
    oldPassword: '',
    newPassword: '',
    confirmPassword: '',
})

const validatePassword = (rule, value, callback) => {
    if (value === undefined || value.length < 6) {
        callback(new Error('密码长度小于6位，请重新输入您的密码'))
    } else {
        callback()
    }
}
const validateConfirmPassword = (rule, value, callback) => {
    if (value === undefined || value.length < 6) {
        callback(new Error('密码长度小于6位，请重新输入您的密码'))
    }
    if (value === undefined || value !== userInfo.value.newPassword) {
        callback(new Error('您输入的密码不一致'))
    } else {
        callback()
    }
}
const passwordRules = {
    oldPassword: [{ required: true, trigger: 'blur', validator: validatePassword }],
    newPassword: [{ required: true, trigger: 'blur', validator: validatePassword }],
    confirmPassword: [{ required: true, trigger: 'blur', validator: validateConfirmPassword }]
}

const goSave = () => {
    validData.value.validate(vali => {
        if (vali) {
            loading.value = true
            updatePassword(userInfo.value).then(res => {
                if (res.status === 204) {
                    ElMessage.success('修改成功!')
                }
                loading.value = false
            }).catch(() => {
                loading.value = false
            })
        }
    })
}

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