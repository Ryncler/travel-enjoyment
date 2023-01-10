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
                    <el-input-number v-model="queryForm.minExecutionDuration" :min="1" @change="handleChange" />
                </el-form-item>
                <el-form-item label="最大执行持续时间">
                    <el-input-number v-model="queryForm.maxExecutionDuration" :min="0" @change="handleChange" />
                </el-form-item>
                <el-form-item label="是否包含异常日志">
                    <el-switch v-model="queryForm.hasException" />
                </el-form-item>
                <el-form-item label="是否包含详细信息" class="rightShow">
                    <el-switch v-model="queryForm.includeDetails" />
                </el-form-item>
                <el-form-item class="searchbtn">
                    <el-button :loading="loading" round type="primary" class="revertbtn addbtn"
                        @click="goSerach()">搜索</el-button>
                </el-form-item>
            </el-form>
        </el-col>
    </el-row>
    <el-divider />
</template>

<script setup>
import { ref } from '@vue/reactivity'

const showAnimation = ref(true)
const currentPage = ref(1)
const pageSizes = ref([
    10, 50, 100, 500
])
const pageSize = ref(pageSizes[0])
const totalCount = ref(0)

const queryForm = ref({})
const startAndEndDate = ref('')



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
// eslint-disable-next-line no-undef
defineExpose({
    showAnimation, queryForm, currentPage, pageSize, totalCount,
})
</script>

<style>
.addbtn {
    width: 200px;
    margin: 0 0 0 35%;
}
.queryCol{
    margin: 0 0 0 1.2%;
}
.rightShow {
    margin: 0 0 0 70%;
}

.searchbtn {
    margin-bottom: 0
}
</style>