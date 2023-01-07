<template>
    <el-drawer v-model="showDrawer" :show-close="false">
        <template #header="{ close, titleId, titleClass }">
            <h4 :id="titleId" :class="titleClass">{{ title }}权限</h4>
            <el-button type="danger" @click="close">
                <icon data="@/icons/close.svg" class="closeIcon" />
                关闭
            </el-button>
        </template>
        <el-form :model="roleForm">
            <el-form-item prop="name">
                <icon data="@/icons/user.svg" class="svg-container" />
                <el-input v-model="roleForm.name" placeholder="角色名" name="name" type="text" tabindex="1"
                    autocomplete="on" />
            </el-form-item>
            <el-form-item prop="isDefault">
                <icon data="@/icons/active.svg" class="svg-container" />
                <el-tooltip class="box-item" effect="light" content="是否默认" placement="right">
                    <el-switch v-model="roleForm.isDefault" class="ml-2"
                        style="--el-switch-on-color: #13ce66; --el-switch-off-color: #ff4949" />
                </el-tooltip>
            </el-form-item>
            <el-form-item prop="isPublic">
                <icon data="@/icons/eye-open.svg" class="svg-container" />
                <el-tooltip class="box-item" effect="light" content="是否公开" placement="right">
                    <el-switch v-model="roleForm.isPublic" class="ml-2"
                        style="--el-switch-on-color: #13ce66; --el-switch-off-color: #ff4949" />
                </el-tooltip>
            </el-form-item>
            <el-form-item>
                <el-button :loading="loading" round type="primary" class="revertbtn addbtn"
                    @click="btnName === '添加' ? goAddRole() : goEditRole()">{{ btnName }}</el-button>
            </el-form-item>
        </el-form>
    </el-drawer>
</template>

<script setup>
import { ElMessage } from 'element-plus'
import { ref } from 'vue'
import { addRole, editRole } from '@/api/user/role'

const loading = ref(false);
const showDrawer = ref(false);
const title = ref('');
const btnName = ref('')

const roleForm = ref({
    id: '',
    name: '',
    isDefault: false,
    isPublic: true
});


const goAddRole = () => {
    loading.value = true
    return addRole(roleForm.value).then(res => {
        loading.value = false
        if (res.status === 200) {
            ElMessage.success('添加成功！')
        }
    })
}

const goEditRole = () => {
    loading.value = true
    return editRole(roleForm.value.id, roleForm.value).then(res => {
        if (res.status === 200) {
            ElMessage.success('编辑成功！')
        }
        loading.value = false
    })
}

// eslint-disable-next-line no-undef
defineExpose({
    showDrawer, roleForm, title, btnName, addRole, loading, goAddRole, goEditRole
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

.ispublicSwitch {
    margin-left: 60%;
}
</style>