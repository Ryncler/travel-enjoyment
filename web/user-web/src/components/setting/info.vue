<template>
    <el-row :gutter="0">
        <el-col :span="4">
            <el-upload class="avatar-uploader avatar" action="" :show-file-list="false" :on-success="handleAvatarSuccess"
                :before-upload="beforeAvatarUpload" :http-request="avatarUpload">
                <el-avatar :size="150" src="https://empty" @error="errorHandler">
                    <img :src="imageHandle(avatar)" />
                </el-avatar>
            </el-upload>
            <h4 class="username">{{ store.getters['identity/userInfo'].userName }}</h4>
            <el-divider />
            <el-tabs v-model="tabs" tab-position="left" style="height: 200px" @tab-click="changeTabs">
                <el-tab-pane name="editInfo">
                    <template #label>
                        <div class="custom-tabs-label">
                            <icon data="@/icons/profile.svg"
                                :class="editInfoActive !== true ? 'svg-container icon' : 'svg-container is-activeIcon'" />
                            <h5>编辑个人资料</h5>
                        </div>
                    </template>
                </el-tab-pane>
                <el-tab-pane name="editPassword">
                    <template #label>
                        <div class="custom-tabs-label">
                            <icon data="@/icons/password.svg"
                                :class="editPasswordActive !== true ? 'svg-container icon' : 'svg-container is-activeIcon'" />
                            <h5>修改密码</h5>
                        </div>
                    </template>
                </el-tab-pane>
                <el-tab-pane name="other">
                    <template #label>
                        <div class="custom-tabs-label">
                            <icon data="@/icons/systemsetting.svg"
                                :class="otherActive !== true ? 'svg-container icon' : 'svg-container is-activeIcon'" />
                            <h5>其他设置</h5>
                        </div>
                    </template>
                </el-tab-pane>
            </el-tabs>
        </el-col>
        <el-col :span="19" :offset="1">
            <editInfoVue v-if="editInfoActive"></editInfoVue>
            <editPasswordVue v-if="editPasswordActive"></editPasswordVue>
            <editSetiingVue v-if="otherActive"></editSetiingVue>
        </el-col>
    </el-row>
</template>

<script setup>
import { ref } from 'vue';
import store from '@/store'
import { onBeforeMount } from '@vue/runtime-core';
import { ElMessage } from 'element-plus'
import editInfoVue from './editInfo'
import editPasswordVue from './editPassword'
import editSetiingVue from './editSetiing'
import { upload } from '@/api/common/minio'
import { imageHandle } from '@/utils/common/index'

const tabs = ref('editInfo')
const editInfoActive = ref(true)
const editPasswordActive = ref(false)
const otherActive = ref(false)
const avatar = ref(store.getters['identity/userInfo'].avatar)
const bucketName = ref(process.env.VUE_APP_AvatarBucketName);

const changeTabs = (tab, event) => {
    if (tab.paneName === 'editInfo') {
        editInfoActive.value = true
        editPasswordActive.value = false
        otherActive.value = false
    }
    if (tab.paneName === 'editPassword') {
        editPasswordActive.value = true
        editInfoActive.value = false
        otherActive.value = false
    }
    if (tab.paneName === 'other') {
        otherActive.value = true
        editInfoActive.value = false
        editPasswordActive.value = false
    }
}


const handleAvatarSuccess = (response, uploadFile) => {
    if (uploadFile.raw !== null) {
        avatar.value = URL.createObjectURL(uploadFile.raw)
    }
}

const beforeAvatarUpload = (rawFile) => {
    const imgType = ['image/png', 'image/jpeg ']
    if (imgType.indexOf(rawFile.type) > 0) {
        ElMessage.error('该文件不是图片类型!')
        return false
    } else if (rawFile.size / 1024 / 1024 > 2) {
        ElMessage.error('头像不能大小不能大于2MB!')
        return false
    }
    return true
}

const avatarUpload = (params) => {
    var data = new FormData()
    data.append('bucketName', bucketName.value)
    data.append('objectName', avatar.value)
    data.append('overrideExisting', true)
    data.append('file', params.file)
    return upload(data).then((res) => {
        if (res.status === 200) {
            ElMessage.success('上传成功！')
            avatar.value = res.data
        }
    })
}
</script>

<style scoped>
.avatar-uploader {
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
    margin-left: 15%;
}

h5 {
    font-weight: bold;
}

.username {
    margin-top: 20px;
    text-align: center;
    font-weight: bold;
}

.custom-tabs-label {
    display: flex;
    margin-left: 20%;
    text-align: center;
}

.icon {
    color: black;
    width: 18px;
    height: 18px;
    margin-top: 8px;
    margin-right: 5px;
}

.is-activeIcon {
    color: #66CCCC;
    width: 18px;
    height: 18px;
    margin-top: 8px;
    margin-right: 5px;
}
</style>


<style>
.el-tabs__item {
    border-radius: 20px;
}

.el-tabs__item:hover {
    color: #66CCCC;
}

.el-tabs__item.is-active {
    color: #66CCCC;
}

.el-tabs__active-bar {
    background-color: #66CCCC;
}

.el-tabs__header {
    width: 100%;
}

input[type=file] {
    display: none;
}
</style>