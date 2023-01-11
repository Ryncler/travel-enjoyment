<template>
    <el-row>
        <el-col class="queryCol">
            <el-form :inline="true" :model="queryForm">
                <el-form-item label="用户名">
                    <el-input v-model="queryForm.userName" placeholder="请输入用户名" />
                </el-form-item>
                <el-form-item label="应用名称">
                    <el-input v-model="queryForm.applicationName" placeholder="请输入应用名称" />
                </el-form-item>
                <el-form-item label="请求URl">
                    <el-input v-model="queryForm.url" placeholder="请输入请求URl" />
                </el-form-item>
                <el-form-item label="相关性ID">
                    <el-input v-model="queryForm.correlationId" placeholder="请输入相关性ID" />
                </el-form-item>
                <el-form-item label="HTTP请求方法">
                    <el-select v-model="queryForm.httpMethod" clearable placeholder="请选择HTTP请求方法类型">
                        <el-option v-for="item in httpMethods" :key="item" :label="item" :value="item" />
                    </el-select>
                </el-form-item>
                <el-form-item label="HTTP状态码">
                    <el-select v-model="queryForm.httpStatusCode" clearable placeholder="请选择HTTP状态码">
                        <el-option v-for="item in httpStatusCodes" :key="item" :label="item" :value="item" />
                    </el-select>
                </el-form-item>
                <el-form-item>
                    <el-date-picker v-model="startAndEndDate" type="datetimerange" :shortcuts="shortcuts"
                        range-separator="-" start-placeholder="开始时间" end-placeholder="结束时间" />
                </el-form-item>
                <el-form-item label="最小执行持续时间">
                    <el-input-number v-model="queryForm.minExecutionDuration" :min="0" @change="handleChange" />
                </el-form-item>
                <el-form-item label="最大执行持续时间">
                    <el-input-number v-model="queryForm.maxExecutionDuration" :min="1" @change="handleChange" />
                </el-form-item>
                <el-form-item>
                    <el-button :loading="loading" round type="primary" class="revertbtn addbtn"
                        @click="goSerach()">搜索</el-button>
                </el-form-item>
            </el-form>
        </el-col>
    </el-row>
    <el-divider />
    <el-row>
        <el-col :span="2" :offset="1">
            <h3>审计日志管理</h3>
        </el-col>
    </el-row>
    <el-row>
        <el-col :span="24">
            <el-table :data="auditLogs" :loading="listLoading" height="600" style="width: 100%" size="large">
                <template #empty>
                    <el-empty :image-size="100" />
                </template>
                <el-table-column label="请求参数">
                    <template #default="scope">
                        <el-tooltip class="box-item" effect="light" content="状态码" placement="left">
                            <el-tag class="ml-2" effect="dark" :type="statusCodeFilter(scope.row.httpStatusCode)">{{
                                scope.row.httpStatusCode
                            }}</el-tag>
                        </el-tooltip>
                        <el-tooltip class="box-item" effect="light" content="请求方法" placement="top">
                            <el-tag class="ml-2" effect="dark" :type="methodFilter(scope.row.httpMethod)">{{
                                scope.row.httpMethod
                            }}</el-tag>
                        </el-tooltip>
                        <el-tooltip class="box-item" effect="light" content="响应时间" placement="right">
                            <el-tag class="ml-2" effect="dark" :type="durationFilter(scope.row.executionDuration)">{{
                                scope.row.executionDuration
                            }}s</el-tag>
                        </el-tooltip>
                        <p></p>
                        <el-tooltip class="box-item" effect="light" content="请求路径" placement="right">
                            <el-tag type="info">{{ scope.row.url }}</el-tag>
                        </el-tooltip>
                    </template>
                </el-table-column>
                <el-table-column prop="userName" label="用户名称" width="150" />
                <el-table-column prop="tenantName" label="租户名称" width="150" />
                <el-table-column prop="applicationName" label="应用名称" />
                <el-table-column prop="clientId" label="客户端ID" />
                <el-table-column prop="clientIpAddress" label="客户端IP" />
                <el-table-column prop="executionTime" label="请求时间" sortable />
                <el-table-column fixed="right" label="操作" width="200">
                    <template #default="scope">
                        <el-button size="small" @click="goDetails(scope.$index, scope.row)">详情</el-button>
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
import { getHttpStatusCode, getHttpMethod } from '@/utils/common/httpParams'
import { getAuditLogs } from '@/api/log/auditLog'
import { isNull } from '@/utils/common/index'
import drawerVue from './drawer.vue'

const loading = ref(false)
const listLoading = ref(false)
const showAnimation = ref(true)
const drawer = ref(null);
const currentPage = ref(1)
const pageSizes = ref([
    10, 50, 100, 500
])
const pageSize = ref(pageSizes.value[0])
const totalCount = ref(0)
const httpStatusCodes = getHttpStatusCode()
const httpMethods = getHttpMethod()

const queryForm = ref({
    maxResultCount: pageSize.value,
    skipCount: currentPage.value
})
const startAndEndDate = ref([])
const auditLogs = ref([])

const statusCodeFilter = (code) => {
    let type = 'success'
    switch (code) {
        case 401:
        case 403:
        case 404:
            type = 'warning'
            break
        case 500:
            type = 'danger'
            break
    }
    return type
}
const durationFilter = (duration) => {
    let type = 'success'
    if (duration > 2 * 1000) {
        type = 'warning'
    } else if (duration > 5 * 1000) {
        type = 'error'
    }
    return type
}
const methodFilter = (method) => {
    let type = 'success'
    switch (method.toUpperCase()) {
        case 'GET':
            type = ''
            break
        case 'PUT':
            type = 'warning'
            break
        case 'POST':
            type = 'success'
            break
        case 'DELETE':
            type = 'danger'
            break
        default:
            type = 'Info'
    }
    return type
}

const shortcuts = [
    {
        text: '上周',
        value: () => {
            const end = new Date()
            const start = new Date()
            start.setTime(start.getTime() - 3600 * 1000 * 24 * 7)
            return [start, end]
        },
    },
    {
        text: '上个月',
        value: () => {
            const end = new Date()
            const start = new Date()
            start.setTime(start.getTime() - 3600 * 1000 * 24 * 30)
            return [start, end]
        },
    },
    {
        text: '三个月前',
        value: () => {
            const end = new Date()
            const start = new Date()
            start.setTime(start.getTime() - 3600 * 1000 * 24 * 90)
            return [start, end]
        },
    },
]

const goSizeChange = (value) => {
    pageSize.value = value
    queryForm.value.maxResultCount = value
    getAuditLogData(queryForm.value)
}

const goCurrentChange = (value) => {
    currentPage.value = value
    queryForm.value.skipCount = value
    getAuditLogData(queryForm.value)
}

const goSerach = () => {
    loading.value = true
    if (startAndEndDate.value[0] !== undefined && startAndEndDate.value[1] !== undefined) {
        queryForm.value.startTime = startAndEndDate.value[0]
        queryForm.value.endTime = startAndEndDate.value[1]
    }
    queryForm.value.skipCount = currentPage.value
    queryForm.value.maxResultCount = pageSize.value
    getAuditLogData(queryForm.value)
}

const getAuditLogData = (params) => {
    listLoading.value = true
    return getAuditLogs(params).then(res => {
        if (res.status === 200) {
            totalCount.value = res.data.totalCount
            auditLogs.value = res.data.items.map((item) => {
                item.tenantName = isNull(item.tenantName)
                item.executionTime = new Date(item.executionTime).format('Y.m.d H:i:s')
                return item
            })
            listLoading.value = false
            loading.value = false
        }
    })
}

const goDetails = (index, row) => {
    drawer.value.showDrawer = true
    drawer.value.getDetails(row.id)
}

onBeforeMount(async () => {
    getAuditLogData()
})

// eslint-disable-next-line no-undef
defineExpose({
    showAnimation, queryForm, currentPage, pageSize, totalCount, drawer,
    getAuditLogData,
})
</script>

<style>
.addbtn {
    width: 200px;
    margin: 0 0 0 35%;
}

.queryCol {
    margin: 0 0 0 1.2%;
}

.rightShow {
    margin: 0 0 0 70%;
}

.searchbtn {
    margin-bottom: 0
}
</style>