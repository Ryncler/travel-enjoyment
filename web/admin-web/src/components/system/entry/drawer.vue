<template>
    <el-drawer v-model="showDrawer" :show-close="false">
        <template #header="{ close, titleId, titleClass }">
            <h4 :id="titleId" :class="titleClass">审核入驻申请</h4>
            <el-button type="danger" @click="close">
                <icon data="@/icons/close.svg" class="closeIcon" />
                关闭
            </el-button>
        </template>
        <el-form :model="applyForm">
            <el-form-item prop="applyType">
                <icon data="@/icons/type.svg" class="svg-container" />
                <el-select v-model="applyForm.applyType" clearable placeholder="请选择是否通过申请">
                    <el-option v-for="item in applyTypes" :key="item" :label="item.title" :value="item.value" />
                </el-select>
            </el-form-item>
            <el-form-item prop="description" v-if="!applyForm.applyType">
                <icon data="@/icons/description.svg" class="svg-container" />
                <el-input v-model="applyForm.description" placeholder="不通过的原因" name="description" type="text"
                    tabindex="1" autocomplete="on" />
            </el-form-item>
            <el-form-item>
                <el-button :loading="loading" round type="primary" class="revertbtn addbtn"
                    @click="goChangeApply()">保存</el-button>
            </el-form-item>
        </el-form>
    </el-drawer>
</template>

<script setup>
import { ref } from '@vue/reactivity'
import { ElMessage } from 'element-plus'
import { verifyApply } from '@/api/user/user'

const loading = ref(false)
const showDrawer = ref(false);
const applyTypes = ref([
    {
        value: true,
        title: '通过',
    },
    {
        value: false,
        title: '不通过',
    },
])
const applyForm = ref({
    id: '',
    applyType: true,
    description: ''
})

const goChangeApply = () => {
    return verifyApply(applyForm.value).then(res => {
        if (res.status === 200) {
            ElMessage.success("保存成功");
        }
    })
}

// eslint-disable-next-line no-undef
defineExpose({
    loading, showDrawer, applyForm,
    goChangeApply
});
</script>

<style>
.addbtn {
    width: 200px;
    margin: 5% 0 0 35%;
}
</style>