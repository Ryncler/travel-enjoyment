<template>
    <el-drawer v-model="showDrawer" :show-close="false">
        <template #header="{ close, titleId, titleClass }">
            <h4 :id="titleId" :class="titleClass">{{ title === '分配' ? '选择景点' : title + '活动' }}</h4>
            <el-button type="danger" @click="close">
                <icon data="@/icons/close.svg" class="closeIcon" />
                关闭
            </el-button>
        </template>
        <el-form :model="activityForm" v-if="title !== '分配'">
            <el-form-item prop="name">
                <icon data="@/icons/name.svg" class="svg-container" />
                <el-input v-model="activityForm.name" placeholder="活动名称" name="name" type="text" tabindex="1"
                    autocomplete="on" />
            </el-form-item>
            <el-form-item prop="content">
                <icon data="@/icons/description.svg" class="svg-container" />
                <el-input v-model="activityForm.content" placeholder="活动内容" name="content" :rows="5" type="textarea"
                    tabindex="1" autocomplete="on" />
            </el-form-item>
            <el-form-item>
                <el-button :loading="loading" round type="primary" class="revertbtn addbtn"
                    @click="btnName === '添加' ? goAdd() : goEdit()">{{ btnName }}</el-button>
            </el-form-item>
        </el-form>

        <el-form :model="sightsQueryForm" v-show="title === '分配'">
            <el-form-item>
                <icon data="@/icons/name.svg" class="svg-container" />
                <el-input v-model="sightsQueryForm.name" placeholder="请输入景点名称" />
            </el-form-item>
            <el-form-item>
                <icon data="@/icons/address.svg" class="svg-container" />
                <el-input v-model="sightsQueryForm.address" placeholder="请输入大概的地址" />
            </el-form-item>
            <el-form-item>
                <icon data="@/icons/ticket.svg" class="svg-container" />
                <el-input-number v-model="sightsQueryForm.ticket" :min="0" @change="handleChange"
                    placeholder="请输入大概的门票价钱" />
            </el-form-item>
            <el-form-item>
                <el-button :loading="loading" round type="primary" class="revertbtn addbtn" @click="goSearch()">{{
                    btnName
                }}</el-button>
            </el-form-item>
        </el-form>
        <el-divider />
        <h4 class="titlec" v-show="title === '分配'">景点列表</h4>
        <el-table v-show="title === '分配'" :data="sightsData" :loading="loading" height="550" style="width: 100%"
            size="large">
            <el-table-column prop="name" label="景点名称" />
            <el-table-column prop="address" label="地址" />
            <el-table-column prop="ticket" label="门票价钱" />
            <el-table-column fixed="right" label="操作">
                <template #default="scope">
                    <el-button size="small" type="success" @click="goSave(scope.$index, scope.row)">选择</el-button>

                </template>
            </el-table-column>
        </el-table>
    </el-drawer>
</template>

<script setup>
import { ref } from '@vue/reactivity'
import { markRaw } from 'vue'
import { SwitchButton } from '@element-plus/icons-vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import { addActivity, editActivity, saveSightsActivity } from '@/api/sights/activity';
import { getSightsBySearch } from '@/api/sights/sights';
import { addHotTop } from '@/api/common';

const loading = ref(false)
const showDrawer = ref(false);
const title = ref('');
const btnName = ref('')
const activityForm = ref({})
const sightsQueryForm = ref({})
const sightsData = ref([])
const goAdd = () => {
    loading.value = true
    return addActivity(activityForm.value).then(res => {
        if (res.status === 200) {
            var data = {
                linkId: res.data.id,
                topType: 4,
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

const goEdit = () => {
    loading.value = true
    return editActivity(activityForm.value.id, activityForm.value).then(res => {
        if (res.status === 200) {
            ElMessage.success('编辑成功！')
        }
        loading.value = false
    })
}

const goSearch = () => {
    loading.value = true
    return getSightsBySearch(sightsQueryForm.value).then(res => {
        if (res.status === 200) {
            sightsData.value = res.data
        }
        loading.value = false
    })
}

const goSave = (index, row) => {
    ElMessageBox.confirm(
        '是否确定要选择该景点？',
        '选择操作',
        {
            type: 'succe',
            icon: markRaw(SwitchButton),
        }
    ).then(() => {
        var data = {
            sightsId: row.id,
            activityId: activityForm.value.id
        }
        return saveSightsActivity(data).then(res => {
            if (res.status === 200) {
                ElMessage.success("保存成功");
            }
        })
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

.el-input-number {
    width: auto;
}

.titlec {
    color: #72767b;
}
</style>