<template>
    <el-drawer v-model="showDrawer" :show-close="false">
        <template #header="{ close, titleId, titleClass }">
            <h4 :id="titleId" :class="titleClass">{{ titleName }}入驻申请</h4>
            <el-button type="danger" @click="close">
                <icon data="@/icons/close.svg" class="closeIcon" />
                关闭
            </el-button>
        </template>
        <el-form :model="applyForm" v-if="titleName === '审核'">
            <el-form-item prop="applyType">
                <icon data="@/icons/type.svg" class="svg-container" />
                <el-select v-model="applyForm.IsSuccess" clearable placeholder="请选择是否通过申请">
                    <el-option v-for="item in applyTypes" :key="item" :label="item.title" :value="item.value" />
                </el-select>
            </el-form-item>
            <el-form-item prop="description" v-if="!applyForm.IsSuccess">
                <icon data="@/icons/description.svg" class="svg-container" />
                <el-input v-model="applyForm.description" placeholder="不通过的原因" name="description" type="text"
                    tabindex="1" autocomplete="on" />
            </el-form-item>
            <el-form-item>
                <el-button :loading="loading" round type="primary" class="revertbtn addbtn"
                    @click="goChangeApply()">保存</el-button>
            </el-form-item>
        </el-form>

        <el-form :model="entryInfoForm" v-show="titleName === '编辑'">
            <el-form-item prop="companyName">
                <icon data="@/icons/company-name.svg" class="svg-container" />
                <el-input v-model="entryInfoForm.companyName" placeholder="公司名称" name="companyName" type="text"
                    tabindex="1" autocomplete="on" />
            </el-form-item>
            <el-form-item prop="unifiedCreditCode">
                <icon data="@/icons/unified-credit-code.svg" class="svg-container" />
                <el-input v-model="entryInfoForm.unifiedCreditCode" placeholder="统一社会信用代码" name="unifiedCreditCode"
                    type="text" tabindex="1" autocomplete="on" />
            </el-form-item>
            <el-form-item prop="failedDescription">
                <icon data="@/icons/description.svg" class="svg-container" />
                <el-input v-model="entryInfoForm.failedDescription" placeholder="驳回原因" name="failedDescription"
                    type="text" tabindex="1" autocomplete="on" />
            </el-form-item>
            <el-form-item>
                <el-button :loading="loading" round type="primary" class="revertbtn addbtn"
                    @click="goEditEntryInfo()">保存</el-button>
            </el-form-item>
        </el-form>
    </el-drawer>
</template>

<script setup>
import { ref } from '@vue/reactivity'
import { ElMessage } from 'element-plus'
import { verifyApply } from '@/api/user/user'
import { editEntryInfo } from '@/api/entryinfo/entry'

const loading = ref(false)
const showDrawer = ref(false);
const titleName = ref('')
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
    IsSuccess: true,
    description: ''
})

const entryInfoForm = ref({
    companyName: '',
    unifiedCreditCode: '',
    failedDescription: ''
})

const goChangeApply = () => {
    return verifyApply(applyForm.value).then(res => {
        if (res.status === 200) {
            ElMessage.success("保存成功");
        }
    })
}

const goEditEntryInfo = () => {
    return editEntryInfo(entryInfoForm.value).then(res => {
        if (res.status === 200) {
            ElMessage.success("编辑成功");
        }
    })
}

// eslint-disable-next-line no-undef
defineExpose({
    loading, showDrawer, applyForm, titleName, entryInfoForm,
    goChangeApply
});
</script>

<style>
.addbtn {
    width: 200px;
    margin: 5% 0 0 35%;
}
</style>