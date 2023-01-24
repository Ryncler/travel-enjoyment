<template>
    <el-drawer v-model="showDrawer" :show-close="false">
        <template #header="{ close, titleId, titleClass }">
            <h4 :id="titleId" :class="titleClass">{{ title }}活动</h4>
            <el-button type="danger" @click="close">
                <icon data="@/icons/close.svg" class="closeIcon" />
                关闭
            </el-button>
        </template>
        <el-form :model="activityForm">
            <el-form-item prop="name">
                <icon data="@/icons/name.svg" class="svg-container" />
                <el-input v-model="activityForm.name" placeholder="活动名称" name="name" type="text" tabindex="1"
                    autocomplete="on" />
            </el-form-item>
            <el-form-item prop="description">
                <icon data="@/icons/description.svg" class="svg-container" />
                <el-input v-model="activityForm.description" placeholder="活动说明" name="description" type="text"
                    tabindex="1" autocomplete="on" />
            </el-form-item>
            <el-form-item prop="address">
                <icon data="@/icons/address.svg" class="svg-container" />
                <el-input v-model="activityForm.address" placeholder="地址" name="address" type="text" tabindex="1"
                    autocomplete="on" />
            </el-form-item>
            <el-form-item prop="ticket">
                <icon data="@/icons/ticket.svg" class="svg-container" />
                <el-input v-model="activityForm.ticket" placeholder="门票价钱" name="ticket" type="text" tabindex="1"
                    autocomplete="on" />
            </el-form-item>
            <el-form-item prop="openTime">
                <icon data="@/icons/ticket.svg" class="svg-container" />
                <el-input v-model="activityForm.openTime" placeholder="开放时间" name="openTime" type="text" tabindex="1"
                    autocomplete="on" />
            </el-form-item>
            <el-form-item prop="trafficGuide">
                <icon data="@/icons/ticket.svg" class="svg-container" />
                <el-input v-model="activityForm.trafficGuide" placeholder="交通指南" name="trafficGuide" :rows="3"
                    type="textarea" tabindex="1" autocomplete="on" />
            </el-form-item>
            <el-form-item prop="SelfDrivingGuide">
                <icon data="@/icons/ticket.svg" class="svg-container" />
                <el-input v-model="activityForm.selfDrivingGuide" placeholder="自驾游指南" name="SelfDrivingGuide" :rows="3"
                    type="textarea" tabindex="1" autocomplete="on" />
            </el-form-item>
            <el-form-item>
                <el-button :loading="loading" round type="primary" class="revertbtn addbtn"
                    @click="btnName === '添加' ? goAdd() : goEdit()">{{ btnName }}</el-button>
            </el-form-item>
        </el-form>
    </el-drawer>
</template>

<script setup>
import { ref } from '@vue/reactivity'
import { ElMessage } from 'element-plus'
import { addActivity, editActivity } from '@/api/sights/activity';

const loading = ref(false)
const showDrawer = ref(false);
const title = ref('');
const btnName = ref('')
const activityForm = ref({})

const goAdd = () => {
    loading.value = true
    return addActivity(activityForm.value).then(res => {
        if (res.status === 200) {
            ElMessage.success('添加成功！')
        }
        loading.value = false
    })
}

const goEdit = () => {
    loading.value = true
    return editActivity(activityForm.value.id, activityForm.value).then(res => {
        if (res.status === 200) {
            ElMessage.success('编辑成功！')
        }
        loading.value = false
    })
}

// eslint-disable-next-line no-undef
defineExpose({
    showDrawer, title, btnName, loading, activityForm,
    goAdd, goEdit
});
</script>

<style>
.addbtn {
    width: 200px;
    margin: 0 0 0 35%;
}
</style>