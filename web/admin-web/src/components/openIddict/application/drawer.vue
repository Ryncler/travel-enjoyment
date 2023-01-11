<template>
    <el-drawer v-model="showDrawer" :show-close="false">
        <template #header="{ close, titleId, titleClass }">
            <h4 :id="titleId" :class="titleClass">{{ title }}应用</h4>
            <el-button type="danger" @click="close">
                <icon data="@/icons/close.svg" class="closeIcon" />
                关闭
            </el-button>
        </template>
        <el-form :model="applicationForm" :rules="applicationRules">
            <el-form-item prop="clientId">
                <icon data="@/icons/roles.svg" class="svg-container" />
                <el-input v-model="applicationForm.clientId" placeholder="应用ID" name="clientId" type="text" tabindex="1"
                    autocomplete="on" />
            </el-form-item>
            <el-form-item prop="displayName">
                <icon data="@/icons/name.svg" class="svg-container" />
                <el-input v-model="applicationForm.displayName" placeholder="应用名称" name="displayName" type="text"
                    tabindex="1" autocomplete="on" />
            </el-form-item>
            <el-form-item prop="clientSecret">
                <icon data="@/icons/token.svg" class="svg-container" />
                <el-input v-model="applicationForm.clientSecret" placeholder="应用密钥" name="displayName" type="text"
                    tabindex="1" autocomplete="on" />
            </el-form-item>
            <el-form-item prop="clientUrl">
                <icon data="@/icons/url.svg" class="svg-container" />
                <el-input v-model="applicationForm.clientUrl" placeholder="应用URl" name="clientUrl" type="text"
                    tabindex="1" autocomplete="on" />
            </el-form-item>
            <el-form-item prop="type">
                <icon data="@/icons/type.svg" class="svg-container" />
                <el-select v-model="applicationForm.type" clearable placeholder="请选择应用类型">
                    <el-option v-for="item in clientTypes" :key="item" :label="item" :value="item" />
                </el-select>
            </el-form-item>
            <el-form-item prop="consentType">
                <icon data="@/icons/permission.svg" class="svg-container" />
                <el-select v-model="applicationForm.consentType" clearable placeholder="请选择允许类型">
                    <el-option v-for="item in consentTypes" :key="item" :label="item" :value="item" />
                </el-select>
            </el-form-item>
            <el-form-item prop="redirectUrls">
                <icon data="@/icons/url.svg" class="svg-container" />
                <el-select v-model="applicationForm.redirectUrls" clearable validate-event multiple filterable
                    allow-create default-first-option :reserve-keyword="false" placeholder="重定向URl">
                    <el-option v-for="item in applicationForm.redirectUrls" :key="item" :label="item" :value="item" />
                </el-select>
            </el-form-item>
            <el-form-item prop="postLogoutRedirectUrls">
                <icon data="@/icons/url.svg" class="svg-container" />
                <el-select v-model="applicationForm.postLogoutRedirectUrls" clearable validate-event multiple filterable
                    allow-create default-first-option :reserve-keyword="false" placeholder="注销重定向URl">
                    <el-option v-for="item in applicationForm.postLogoutRedirectUrls" :key="item" :label="item"
                        :value="item" />
                </el-select>
            </el-form-item>
            <el-form-item prop="grantTypes">
                <icon data="@/icons/grant.svg" class="svg-container" />
                <el-select v-model="applicationForm.grantTypes" clearable multiple placeholder="认证列表">
                    <el-option v-for="item in grantTypes" :key="item" :label="item" :value="item" />
                </el-select>
            </el-form-item>
            <el-form-item prop="scopes">
                <icon data="@/icons/scope.svg" class="svg-container" />
                <el-select v-model="applicationForm.scopes" clearable multiple placeholder="服务列表">
                    <el-option v-for="item in scopes" :key="item" :label="item" :value="item" />
                </el-select>
            </el-form-item>
            <el-form-item>
                <el-button :loading="loading" round type="primary" class="revertbtn addbtn"
                    @click="btnName === '添加' ? goAddApplication() : goEditApplication()">{{ btnName }}</el-button>
            </el-form-item>
        </el-form>
    </el-drawer>
</template>

<script setup>
import { ref } from '@vue/reactivity'
import { ElMessage } from 'element-plus'
import { onMounted } from '@vue/runtime-core';
import { addApplication, editApplication, getClientTypes, getConsentTypes } from '@/api/openIddict/application';

const loading = ref(false)
const showDrawer = ref(false);
const title = ref('');
const btnName = ref('')
const grantTypes = ref([])
const scopes = ref([])
const clientTypes = ref([])
const consentTypes = ref([])
const applicationForm = ref({})

const goClientTypes = () => {
    return getClientTypes().then(res => {
        if (res.status === 200) {
            clientTypes.value = res.data
        }
    })
}

const goConsetTypes = () => {
    return getConsentTypes().then(res => {
        if (res.status === 200) {
            consentTypes.value = res.data
        }
    })
}

const goAddApplication = () => {
    loading.value = true
    return addApplication(applicationForm.value).then(res => {
        if (res.status === 200) {
            ElMessage.success('添加成功！')
        }
        loading.value = false
    })
}

const goEditApplication = () => {
    loading.value = true
    return editApplication(applicationForm.value.id, applicationForm.value).then(res => {
        if (res.status === 200) {
            ElMessage.success('编辑成功！')
        }
        loading.value = false
    })
}

const validateUrl = (rule, value, callback) => {
    const regUrl = /http([s])?:\/\/([a-zA-z0-9.]+[.]?)(:[0-9]+)?/
    if (!regUrl.test(value)) {
        callback(new Error('请输入正确的URl链接'))
    } else {
        callback()
    }
}

const applicationRules = ref({
    clientUrl: [{ required: true, trigger: 'blur', validator: validateUrl }],
    redirectUrls: [{ required: true, trigger: ['blur', 'change'], type: 'array', validator: validateUrl }],
    postLogoutRedirectUrls: [{ required: true, trigger: ['blur', 'change'], type: 'array', validator: validateUrl }]
})

onMounted(() => {
    goClientTypes()
    goConsetTypes()
})


// eslint-disable-next-line no-undef
defineExpose({
    showDrawer, applicationForm, title, btnName, loading, grantTypes, scopes,
    goAddApplication, goEditApplication
});
</script>

<style>
.addbtn {
    width: 200px;
    margin: 0 0 0 35%;
}
</style>