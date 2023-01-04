<template>
    <el-drawer v-model="showDrawer" :show-close="false">
        <template #header="{ close, titleId, titleClass }">
            <h4 :id="titleId" :class="titleClass">添加用户</h4>
            <el-button type="danger" @click="close">
                <icon data="@/icons/close.svg" class="closeIcon" />
                关闭
            </el-button>
        </template>
        <el-form :model="addUser">
            <el-form-item prop="username">
                <icon data="@/icons/user.svg" class="svg-container" />
                <el-input v-model="addUser.username" placeholder="用户名" name="username" type="text" tabindex="1"
                    autocomplete="on" />
            </el-form-item>
            <el-form-item prop="sex">
                <icon data="@/icons/sex.svg" class="svg-container" />
                <el-tooltip class="box-item" effect="light" content="性别" placement="right">
                    <el-radio-group v-model="addUser.sex" class="ml-4" fill="#66CCCC" text-color="white">
                        <el-radio-button label="1" size="large">男</el-radio-button>
                        <el-radio-button label="2" size="large">女</el-radio-button>
                    </el-radio-group>
                </el-tooltip>
            </el-form-item>
            <el-form-item prop="email">
                <icon data="@/icons/email.svg" class="svg-container" />
                <el-input v-model="addUser.email" placeholder="邮箱" name="email" type="text" tabindex="2"
                    autocomplete="on" />
            </el-form-item>
            <el-form-item prop="phone">
                <icon data="@/icons/phone.svg" class="svg-container" />
                <el-input v-model="addUser.phone" placeholder="手机号" name="phone" type="text" tabindex="2"
                    autocomplete="on" />
            </el-form-item>
            <el-form-item prop="password">
                <icon data="@/icons/password.svg" class="svg-container" />
                <el-input v-model="addUser.password" :type="password" placeholder="密码" name="password" tabindex="3"
                    autocomplete="on" show-password />
            </el-form-item>
            <el-form-item prop="avatar">
                <icon data="@/icons/image.svg" class="svg-container" />
                <el-tooltip class="box-item" effect="light" content="头像" placement="right">
                    <el-upload class="avatar-uploader"
                        action="https://run.mocky.io/v3/9d059bf9-4660-45f2-925d-ce80ad6c4d15" :show-file-list="false"
                        :on-success="handleAvatarSuccess" :before-upload="beforeAvatarUpload">
                        <img v-if="imageUrl" :src="imageUrl" class="avatar" alt="头像" />
                        <el-icon v-else class="avatar-uploader-icon">
                            <Plus />
                        </el-icon>
                    </el-upload>
                </el-tooltip>
            </el-form-item>
            <el-form-item>
                <el-button :loading="loading" round type="primary" class="revertbtn" @click="goAddUser()">添加</el-button>
            </el-form-item>
        </el-form>
    </el-drawer>
</template>

<script setup>
import { Plus } from '@element-plus/icons-vue'
import { ref } from 'vue'
const showDrawer = ref(false);
const addUser = ref({
    username: 'admin',
    email: '1233@qq.com',
    password: 'aA10086.',
});

const beforeAvatarUpload = () => {

};

defineExpose({
    showDrawer, addUser, beforeAvatarUpload
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
</style>