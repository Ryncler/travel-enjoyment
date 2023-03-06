<template>
    <el-drawer v-model="showDrawer" :show-close="false">
        <template #header="{ close, titleId, titleClass }">
            <h4 :id="titleId" :class="titleClass">选择景点</h4>
            <el-button type="danger" @click="close">
                <icon data="@/icons/close.svg" class="closeIcon" />
                关闭
            </el-button>
        </template>

        <el-form :model="sightsQueryForm">
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
                <el-button :loading="loading" round type="primary" class="revertbtn addbtn"
                    @click="goSearch()">搜索</el-button>
            </el-form-item>
        </el-form>
        <el-divider />
        <h4 class="titlec">景点列表</h4>
        <el-table :data="sightsData" :loading="loading" height="550" style="width: 100%" size="large">
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
import { getSightsBySearch, addSightsTravel } from '@/api/sights/index';
import { addHotTop } from '@/api/common';

const loading = ref(false)
const showDrawer = ref(false);
const title = ref('');
const btnName = ref('')
const activityForm = ref({})
const sightsQueryForm = ref({})
const sightsData = ref([])
const selectId = ref('')

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
        selectId.value = row.id
        ElMessage({
            type: 'success',
            message: '选择成功'
        })
    })
}

// eslint-disable-next-line no-undef
defineExpose({
    selectId, showDrawer, title, btnName, loading, activityForm,
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