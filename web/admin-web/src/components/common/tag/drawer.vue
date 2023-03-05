<template>
    <el-drawer v-model="showDrawer" :show-close="false">
        <template #header="{ close, titleId, titleClass }">
            <h4 :id="titleId" :class="titleClass">{{ title }}</h4>
            <el-button type="danger" @click="close">
                <icon data="@/icons/close.svg" class="closeIcon" />
                关闭
            </el-button>
        </template>
        <el-form :model="categoryForm">
            <el-form-item prop="name">
                <icon data="@/icons/name.svg" class="svg-container" />
                <el-input v-model="categoryForm.name" placeholder="类别名称" name="name" type="text" tabindex="1"
                    autocomplete="on" />
            </el-form-item>
            <el-form-item>
                <el-button :loading="loading" round type="primary" class="revertbtn addbtn" @click="funcName()">{{
                    btnName
                }}</el-button>
            </el-form-item>
        </el-form>
    </el-drawer>
</template>

<script setup>
import { ref } from '@vue/reactivity'
import { ElMessage } from 'element-plus'
import { addCategory, addTag, editCategory, editTag } from '@/api/common/index';
import { addHotTop } from '@/api/common';

const loading = ref(false)
const showDrawer = ref(false);
const title = ref('');
const btnName = ref('')
const categoryId = ref('')
const categoryForm = ref({})

const funcName = () => {
    if (title.value === '添加类别')
        return goAdd()
    if (title.value === '添加标签')
        return goAddTag()

    if (title.value === '编辑类别')
        return goEdit()
    if (title.value === '编辑标签')
        return goEditTag()

}

const goAdd = () => {
    loading.value = true
    return addCategory(categoryForm.value).then(res => {
        if (res.status === 200) {
            ElMessage.success('添加成功！')
        }
        loading.value = false
    })
}

const goEdit = () => {
    loading.value = true
    return editCategory(categoryForm.value.id, categoryForm.value).then(res => {
        if (res.status === 200) {
            ElMessage.success('编辑成功！')
        }
        loading.value = false
    })
}

const goAddTag = () => {
    loading.value = true
    var data = {
        name: categoryForm.value.name,
        parentCategoryId: categoryId.value
    }
    return addTag(data).then(res => {
        if (res.status === 200) {
            var data = {
                linkId: res.data.id,
                topType: 3,
                weight: 1
            }
            addHotTop(data).then(res => {
                if (res.status === 200) {
                    ElMessage.success('添加成功！')
                }
            })
        }
        loading.value = false
    })
}

const goEditTag = () => {
    loading.value = true
    return editTag(categoryForm.value.id, categoryForm.value).then(res => {
        if (res.status === 200) {
            ElMessage.success('编辑成功！')
        }
        loading.value = false
    })
}

// eslint-disable-next-line no-undef
defineExpose({
    showDrawer, title, btnName, loading, categoryForm, categoryId,
    goAdd, goEdit
});
</script>

<style>
.addbtn {
    width: 200px;
    margin: 0 0 0 35%;
}

.el-input-number {
    width: auto;
}

.titlec {
    color: #72767b;
}
</style>