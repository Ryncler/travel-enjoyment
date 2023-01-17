<template>
    <el-row :gutter="20">
        <el-col :span="2" :offset="1">
            <h3>个人信息</h3>
        </el-col>
    </el-row>
    <el-row :gutter="20">
        <el-col :span="4" :offset="4">
            <div class="info">
                <el-upload class="avatar-uploader avatar" action="" :show-file-list="false"
                    :on-success="handleAvatarSuccess" :before-upload="beforeAvatarUpload">
                    <el-avatar :size="150" src="https://empty" @error="errorHandler">
                        <img :src="userForm.avatar" />
                    </el-avatar>
                </el-upload>
                <el-divider />
                <h4 class="username">{{ userForm.userName }}</h4>
                <p></p>
                <p class="profile">{{ userForm.profile }}</p>
            </div>
        </el-col>
        <el-col :span="12">
            <el-form :model="userForm" v-if="!showEditPassword">
                <el-form-item prop="username">
                    <icon data="@/icons/user.svg" class="svg-container" />
                    <el-input v-model="userForm.userName" placeholder="用户名" name="username" type="text" tabindex="1"
                        autocomplete="on" />
                </el-form-item>
                <el-form-item prop="sex">
                    <icon data="@/icons/sex.svg" class="svg-container" />
                    <el-tooltip class="box-item" effect="light" content="性别" placement="right">
                        <el-radio-group v-model="userForm.sex" class="ml-4" fill="#66CCCC" text-color="white"
                            :change="isChange">
                            <el-radio-button label=true size="large">男</el-radio-button>
                            <el-radio-button label=false size="large">女</el-radio-button>
                        </el-radio-group>
                    </el-tooltip>
                </el-form-item>
                <el-form-item prop="email">
                    <icon data="@/icons/email.svg" class="svg-container" />
                    <el-input v-model="userForm.email" placeholder="邮箱" name="email" type="text" tabindex="2"
                        autocomplete="on" />
                </el-form-item>
                <el-form-item prop="phone">
                    <icon data="@/icons/phone.svg" class="svg-container" />
                    <el-input v-model="userForm.phone" placeholder="手机号" name="phone" type="text" tabindex="2"
                        autocomplete="on" />
                </el-form-item>
                <el-form-item prop="profile">
                    <icon data="@/icons/profile.svg" class="svg-container" />
                    <el-input v-model="userForm.profile" placeholder="简介" name="profile" tabindex="2" autocomplete="on"
                        :rows="3" type="textarea" />
                </el-form-item>
                <el-form-item>
                    <el-button :loading="loading" round type="primary" class="revertbtn addbtn"
                        @click="goSave(1)">保存</el-button>
                    <el-button :loading="loading" round type="primary" class="revertbtn addbtn"
                        @click="goEditPassword()">更改密码</el-button>
                </el-form-item>
            </el-form>

            <el-form v-show="showEditPassword" :model="passwordForm" :rules="passwordRules">
                <el-form-item prop="oldPassword">
                    <icon data="@/icons/password.svg" class="svg-container" />
                    <el-input v-model="passwordForm.oldPassword" :type="password" placeholder="旧密码" name="oldPassword"
                        tabindex="1" autocomplete="on" show-password />
                </el-form-item>
                <el-form-item prop="newPassword">
                    <icon data="@/icons/password.svg" class="svg-container" />
                    <el-input v-model="passwordForm.newPassword" :type="password" placeholder="新密码" name="newPassword"
                        tabindex="2" autocomplete="on" show-password />
                </el-form-item>
                <el-form-item prop="confimPassword">
                    <icon data="@/icons/password.svg" class="svg-container" />
                    <el-input v-model="passwordForm.confimPassword" :type="password" placeholder="确认密码"
                        name="confimPassword" tabindex="3" autocomplete="on" show-password />
                </el-form-item>
                <el-form-item>
                    <el-button :loading="loading" round type="primary" class="revertbtn addbtn"
                        @click="goSave(2)">保存</el-button>
                    <el-button :loading="loading" round type="primary" class="revertbtn addbtn"
                        @click="goEditPassword()">返回</el-button>
                </el-form-item>
            </el-form>
        </el-col>
    </el-row>
</template>

<script setup>
import { getUser, editUser, updatePassword } from '@/api/user/user'
import store from '@/store'
import { onBeforeMount } from '@vue/runtime-core';
import { ElMessage } from 'element-plus'
const { ref } = require("@vue/reactivity")

const id = ref(store.getters['identity/userInfo'].sub)
const showEditPassword = ref(false)
const userForm = ref({})
const passwordForm = ref({
    oldPassword: '',
    newPassword: '',
    confimPassword: ''
})


const getUserInfo = () => {
    return getUser(id.value).then(res => {
        if (res.status === 200) {
            userForm.value = res.data
        }
    })
}

const goSave = (type) => {
    if (type === 1) {
        return editUser(id.value, userForm.value).then(res => {
            if (res.status === 200) {
                ElMessage.success("保存成功");
            }
        })
    }
    if (type === 2) {
        passwordForm.value.userId = id.value
        return updatePassword(passwordForm.value).then(res => {
            if (res.status === 200) {
                ElMessage.success("保存成功");
            }
        })
    }
}

const goEditPassword = () => {
    showEditPassword.value = !showEditPassword.value
}

const validatePassword = (rule, value, callback) => {
    console.log(value);
    if (value.length < 6) {
        callback(new Error('密码长度小于6位，请重新输入您的密码'))
    } else {
        callback()
    }
}

const validateConfimPassword = (rule, value, callback) => {
    if (value.length < 6) {
        callback(new Error('密码长度小于6位，请重新输入您的密码'))
    }
    if (value !== passwordForm.value.password) {
        callback(new Error('您输入的密码不一致'))
    } else {
        callback()
    }
}
const passwordRules = ref({
    oldPassword: [{ required: true, trigger: 'blur', validator: validatePassword }],
    newPassword: [{ required: true, trigger: 'blur', validator: validatePassword }],
    confimPassword: [{ required: true, trigger: 'blur', validator: validateConfimPassword }],
})
onBeforeMount(async () => {
    getUserInfo()
})

// eslint-disable-next-line no-undef
defineExpose({
    passwordForm,
});
</script>

<style>
.avatar-uploader .el-upload {
    border-radius: 6px;
    cursor: pointer;
    position: relative;
    overflow: hidden;
    transition: var(--el-transition-duration-fast);
}

.el-icon.avatar-uploader-icon {
    font-size: 28px;
    /* color: #8c939d; */
    width: 178px;
    height: 178px;
    text-align: center;
}

.avatar {
    margin-left: 22%;
}

.info {
    display: inline;
    justify-content: center;
    align-content: center;
}

.username {
    text-align: center;
}

.profile {
    text-align: center;
}

.addbtn {
    width: 200px;
    margin: 10% 10% 0 15%;
}
.el-form-item__error {
    margin-left: 28px;
}
</style>