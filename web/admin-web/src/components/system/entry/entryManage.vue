<template>
    <el-row>
        <el-col :span="24" class="queryCol">
            <el-form :inline="true" :model="queryForm">
                <el-form-item label="用户名">
                    <el-input v-model="queryForm.name" placeholder="请输入用户名" />
                </el-form-item>
                <el-form-item label="邮箱">
                    <el-input v-model="queryForm.email" placeholder="请输入邮箱" />
                </el-form-item>
                <el-form-item label="邮箱">
                    <el-input v-model="queryForm.phone" placeholder="请输入手机号" />
                </el-form-item>
                <el-form-item label="用户状态">
                    <el-select v-model="queryForm.isActive" clearable placeholder="请选择用户状态">
                        <el-option v-for="item in actives" :key="item.value" :label="item.label" :value="item.value" />
                    </el-select>
                </el-form-item>
                <el-form-item label="申请状态">
                    <el-select v-model="queryForm.status" clearable placeholder="请选择申请状态">
                        <el-option v-for="item in applyStatus" :key="item.id" :label="item.description"
                            :value="item.id" />
                    </el-select>
                </el-form-item>
                <el-form-item label="是否显示已删除申请">
                    <el-switch v-model="queryForm.isDelete" />
                </el-form-item>
            </el-form>
        </el-col>
    </el-row>
    <el-divider />
    <el-row>
        <el-col :span="2" :offset="1">
            <h3>入驻管理</h3>
        </el-col>
        <el-col :span="3" class="options" :offset="18">
            <el-button round type="primary" class="revertbtn" @click="goAddEntry()">新增入驻</el-button>
            <el-tooltip class="box-item" effect="dark" content="刷新" placement="top">
                <transition name="refresh" @leave="onAfterLeave">
                    <icon v-if="showAnimation" @click="refreshData()" data="@/icons/refresh.svg" class="svg-container"
                        style="width:60px;height:30px" />
                </transition>
            </el-tooltip>
        </el-col>
    </el-row>
    <el-row>
        <el-col :span="24">
            <el-table :data="filter()" :loading="loading" height="600" style="width: 100%" size="large">
                <template #empty>
                    <el-empty :image-size="100" />
                </template>
                <el-table-column prop="userName" label="用户名" />
                <el-table-column prop="email" label="邮箱" />
                <el-table-column prop="phone" label="电话" />
                <el-table-column prop="companyName" label="公司名称" />
                <el-table-column prop="unifiedCreditCode" label="统一社会信用代码" />
                <el-table-column prop="applyTiem" label="申请时间" sortable />
                <el-table-column label="当前申请状态">
                    <template #default="scope">
                        <el-checkbox-group v-model="scope.row.status" :min="1" disabled>
                            <el-checkbox v-for="item in applyStatus" :key="item.id" :true-label="item.id">{{
                                item.description
                            }}</el-checkbox>
                        </el-checkbox-group>
                    </template>
                </el-table-column>
                <el-table-column prop="failedDescription" label="驳回原因" />
                <el-table-column label="当前用户状态">
                    <template #default="scope">
                        <el-switch v-model="scope.row.active" disabled />
                    </template>
                </el-table-column>
                <el-table-column label="是否已删除">
                    <template #default="scope">
                        <el-switch v-model="scope.row.delete" disabled />
                    </template>
                </el-table-column>
                <el-table-column fixed="right" label="操作" width="200">
                    <template #default="scope">
                        <el-button size="small" @click="goApply(scope.$index, scope.row)" type="primary">审核</el-button>
                        <el-button size="small" @click="goEditEntryInfo(scope.$index, scope.row)">编辑</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </el-col>
        <el-col :span="10" :offset="13">
            <el-pagination v-model:current-page="currentPage" v-model:page-size="pageSize" :page-sizes="pageSizes"
                :small="small" :disabled="disabled" :background="background"
                layout="total, sizes, prev, pager, next, jumper" :total="totalCount" @size-change="goSizeChange"
                @current-change="goCurrentChange" />
        </el-col>
    </el-row>

    <drawerVue ref="drawer" />
</template>

<script setup>
import { ref } from '@vue/reactivity'
import { onBeforeMount } from '@vue/runtime-core';
import { getUserEntryInfos } from '@/api/user/user'
import { getStatus } from '@/api/entryinfo/entry'
import { isNull } from '@/utils/common/index'
import drawerVue from './drawer.vue'

const loading = ref(false)
const showAnimation = ref(true)
const applyStatus = ref([])
const currentPage = ref(1)
const pageSizes = ref([
    10, 50, 100, 500
])
const pageSize = ref(pageSizes.value[0])
const totalCount = ref(0)

const drawer = ref(null);
const entryData = ref([])
const queryForm = ref({
    name: '',
    email: '',
    phone: '',
    isActive: null,
    status: null,
    isDelete: false
})
const actives = ref([
    {
        label: '已激活',
        value: true,
    },
    {
        label: '未激活',
        value: false,
    }
])

const onAfterLeave = () => {
    showAnimation.value = true
}
const refreshData = () => {
    queryForm.value = {
        name: '',
        email: '',
        phone: '',
        isActive: null,
        status: null,
        isDelete: false
    }
    showAnimation.value = !showAnimation.value
}

const goSizeChange = (value) => {
    pageSize.value = value
}

const goCurrentChange = (value) => {
    currentPage.value = value
}


const getStatusData = () => {
    return getStatus().then(res => {
        applyStatus.value = res.data
    })
}

const getEntryInfoData = () => {
    loading.value = true
    var parmas = {
        isall: true,
        maxResultCount: pageSize.value,
        skipCount: currentPage.value
    }
    return getUserEntryInfos(parmas).then(async res => {
        if (res.status === 200) {
            totalCount.value = res.data.totalCount
            entryData.value = res.data.items.map((item) => {
                item.failedDescription = isNull(item.failedDescription)
                item.applyTiem = new Date(item.applyTiem).format('Y.m.d H:i:s')
                return item
            })
            loading.value = false
        }
    })
}

const goApply = (index, row) => {
    drawer.value.titleName = '审核'
    drawer.value.showDrawer = true
    drawer.value.applyForm.id = row.id
}

const goEditEntryInfo = (index, row) => {
    drawer.value.titleName = '编辑'
    drawer.value.showDrawer = true
    drawer.value.entryInfoForm = row
}

const filter = () => {
    var data = entryData.value
    if (queryForm.value.name !== '') {
        data = data.filter(x => !queryForm.value.name || x.userName.includes(queryForm.value.name))
    }
    if (queryForm.value.email !== '') {
        data = data.filter(x => !queryForm.value.email || x.email.includes(queryForm.value.email))
    }
    if (queryForm.value.phone !== '') {
        data = data.filter(x => !queryForm.value.phone || x.phone.includes(queryForm.value.phone))
    }
    if (queryForm.value.isActive !== null) {
        if (queryForm.value.isActive.length !== 0) {
            data = data.filter(x => x.active === queryForm.value.isActive)
        }
    }
    if (queryForm.value.status !== null) {
        if (queryForm.value.status.length !== 0) {
            console.log(queryForm.value.status);
            data = data.filter(x => x.status === queryForm.value.status)
        }
    }
    if (!queryForm.value.isDelete) {
        data = data.filter(x => x.isDeleted === queryForm.value.isDelete)
    }
    return data
}


onBeforeMount(async () => {
    getStatusData()
    getEntryInfoData()
})

// eslint-disable-next-line no-undef
defineExpose({
    loading, showAnimation, queryForm, currentPage, pageSize, drawer,
    filter,
})
</script>