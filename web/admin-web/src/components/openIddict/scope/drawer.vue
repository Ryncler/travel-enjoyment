<template>
    <el-drawer v-model="showDrawer" :show-close="false">
        <template #header="{ close, titleId, titleClass }">
            <h4 :id="titleId" :class="titleClass">{{ title }}服务</h4>
            <el-button type="danger" @click="close">
                <icon data="@/icons/close.svg" class="closeIcon" />
                关闭
            </el-button>
        </template>
        <el-form :model="scopeForm">
            <el-form-item prop="name">
                <icon data="@/icons/user.svg" class="svg-container" />
                <el-input v-model="scopeForm.name" placeholder="服务ID" name="name" type="text" tabindex="1"
                    autocomplete="on" />
            </el-form-item>
            <el-form-item prop="displayName">
                <icon data="@/icons/user.svg" class="svg-container" />
                <el-input v-model="scopeForm.displayName" placeholder="服务名称" name="name" type="text" tabindex="1"
                    autocomplete="on" />
            </el-form-item>
            <el-form-item prop="description">
                <icon data="@/icons/user.svg" class="svg-container" />
                <el-input v-model="scopeForm.description" placeholder="服务说明" name="name" type="text" tabindex="1"
                    autocomplete="on" />
            </el-form-item>
            <el-form-item prop="resources">
                <icon data="@/icons/url.svg" class="svg-container" />
                <el-select v-model="scopeForm.resources" clearable validate-event multiple filterable allow-create
                    default-first-option :reserve-keyword="false" placeholder="资源列表">
                    <el-option v-for="item in scopeForm.resources" :key="item" :label="item" :value="item" />
                </el-select>
            </el-form-item>
            <el-form-item>
                <el-button :loading="loading" round type="primary" class="revertbtn addbtn"
                    @click="btnName === '添加' ? goAddScope() : goEditScope()">{{ btnName }}</el-button>
            </el-form-item>
        </el-form>
    </el-drawer>
</template>

<script setup>
import { ref } from '@vue/reactivity'
import { ElMessage } from 'element-plus'
import { addScope, editScope } from '@/api/openIddict/scope';

const loading = ref(false)
const showDrawer = ref(false);
const title = ref('');
const btnName = ref('')
const scopeForm = ref({})

const goAddScope = () => {
    loading.value = true
    scopeForm.value.resources = JSON.stringify(scopeForm.value.resources)
    addScope(scopeForm.value).then(res => {
        if (res.status === 200) {
            scopeForm.value.resources = JSON.parse(scopeForm.value.resources)
            ElMessage.success('添加成功！')
        }
    })
    loading.value = false
    return
}

const goEditScope = () => {
    loading.value = true
    scopeForm.value.resources = JSON.stringify(scopeForm.value.resources)
    editScope(scopeForm.value.id, scopeForm.value).then(res => {
        if (res.status === 200) {
            scopeForm.value.resources = JSON.parse(scopeForm.value.resources)
            ElMessage.success('编辑成功！')
        }
    })
    loading.value = false
    return
}

// eslint-disable-next-line no-undef
defineExpose({
    showDrawer, title, btnName, loading, scopeForm,
    goAddScope, goEditScope
});
</script>