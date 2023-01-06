<template>
    <el-drawer v-model="showDrawer" :show-close="false">
        <template #header="{ close, titleId, titleClass }">
            <h4 :id="titleId" :class="titleClass">{{ title }}用户</h4>
            <el-button type="danger" @click="close">
                <icon data="@/icons/close.svg" class="closeIcon" />
                关闭
            </el-button>
        </template>
        <el-form :model="userForm">
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
            <el-form-item prop="password">
                <icon data="@/icons/password.svg" class="svg-container" />
                <el-input v-model="userForm.password" :type="password" placeholder="密码" name="password" tabindex="3"
                    autocomplete="on" show-password />
            </el-form-item>
            <el-form-item prop="roles">
                <icon data="@/icons/roles.svg" class="svg-container" />
                <el-tooltip class="box-item" effect="light" content="角色" placement="right">
                    <el-checkbox-group v-model="userForm.roles" :min="1">
                        <el-checkbox v-for="item in getRoles()" :key="item" :label="item" border>{{
                            item
                        }}</el-checkbox>
                    </el-checkbox-group>
                </el-tooltip>
            </el-form-item>
            <el-form-item prop="active">
                <icon data="@/icons/active.svg" class="svg-container" />
                <el-tooltip class="box-item" effect="light" content="状态" placement="right">
                    <el-switch v-model="userForm.active" class="ml-2"
                        style="--el-switch-on-color: #13ce66; --el-switch-off-color: #ff4949" />
                </el-tooltip>
            </el-form-item>
            <el-form-item prop="avatar">
                <icon data="@/icons/image.svg" class="svg-container" />
                <el-tooltip class="box-item" effect="light" content="头像" placement="right">
                    <el-upload class="avatar-uploader" action="" :on-remove="removeAvatarImg"
                        :http-request="avatarUpload" :show-file-list="false" :on-success="handleAvatarSuccess"
                        :before-upload="beforeAvatarUpload">
                        <img v-if="userForm.avatar" :src="userForm.avatar" class="avatar" alt="头像" />
                        <el-icon v-else class="avatar-uploader-icon">
                            <Plus />
                        </el-icon>
                    </el-upload>
                </el-tooltip>
            </el-form-item>
            <el-form-item>
                <el-button :loading="loading" round type="primary" class="revertbtn addbtn"
                    @click="btnName === '添加' ? goAddUser() : goEditUser()">{{ btnName }}</el-button>
            </el-form-item>
        </el-form>
    </el-drawer>
</template>

<script setup>
import { Plus } from '@element-plus/icons-vue'
import { ElMessage } from 'element-plus'
import { ref } from 'vue'
import { upload } from '@/api/common/minio'
import { addUser, editUser } from '@/api/user/user'

const loading = ref(false);
const showDrawer = ref(false);
const title = ref('');
const btnName = ref('')

const userForm = ref({
    id: '',
    userName: '',
    sex: true,
    phone: '',
    email: '',
    roles: [],
    active: true,
    password: '',
    avatar: ''
});

const bucketName = ref(process.env.VUE_APP_AvatarBucketName);

const handleAvatarSuccess = (response, uploadFile) => {
    if (uploadFile.raw !== null) {
        userForm.value.avatar.value = URL.createObjectURL(uploadFile.raw)
    }
}

const avatarUpload = (params) => {
    const formData = new FormData()
    formData.append("file", params.file)
    formData.append("bucketName", bucketName.value)
    return upload(formData).then((res) => {
        if (res.data.status === 200) {
            ElMessage.success('上传成功！')
        }
    })
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

const goAddUser = () => {
    loading.value = true
    return addUser(userForm.value).then(res => {
        loading.value = false
        if (res.status === 200) {
            ElMessage.success('添加成功！')
        }
    })
}

const goEditUser = () => {
    loading.value = true
    return editUser(userForm.value.id, userForm.value).then(res => {
        if (res.status === 200) {
            ElMessage.success('编辑成功！')
        }
        loading.value = false
    })
}

const getRoles = () => {
    return ['admin', 'user']
}

// eslint-disable-next-line no-undef
defineExpose({
    showDrawer, userForm, title, btnName, addUser, loading, bucketName, beforeAvatarUpload, handleAvatarSuccess, avatarUpload, goAddUser, getRoles, goEditUser
});
</script>

<style scoped>
.avatar-uploader .avatar {
    width: 100px;
    height: 100px;
    display: block;
}
</style>
<style>
.avatar-uploader .el-upload {
    border: 1px dashed var(--el-border-color);
    border-radius: 6px;
    cursor: pointer;
    position: relative;
    overflow: hidden;
    transition: var(--el-transition-duration-fast);
}

.avatar-uploader .el-upload:hover {
    border-color: var(--el-color-primary);
}

.el-icon.avatar-uploader-icon {
    font-size: 28px;
    color: #8c939d;
    width: 100px;
    height: 100px;
    text-align: center;
}

.addbtn {
    width: 200px;
    margin: 0 0 0 35%;
}

.el-radio-button__original-radio:checked+.el-radio-button__inner {
    background-color: #66CCCC;
    border-color: #66CCCC;
    box-shadow: 1px 0 0 0 #66CCCC
}

.el-radio-button__inner:hover {
    color: var(--el-button-text-color, var(--el-text-color-regular));
}

.el-checkbox.is-bordered.is-checked {
    border-color: #66CCCC
}

.el-checkbox__input.is-checked .el-checkbox__inner {
    background-color: #66CCCC;
    border-color: #66CCCC
}

.el-checkbox__input.is-checked+.el-checkbox__label {
    color: var(--el-button-text-color, var(--el-text-color-regular));
}
</style>