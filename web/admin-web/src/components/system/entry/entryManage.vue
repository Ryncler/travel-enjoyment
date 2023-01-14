<template>
    <el-row>
        <el-col :span="21" class="queryCol" :offset="3">
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
                        <el-option v-for="item in applyStatus" :key="item.value" :label="item.label"
                            :value="item.value" />
                    </el-select>
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
                            <el-checkbox v-for="item in applyStatus" :key="item" :label="item">{{
                                item
                            }}</el-checkbox>
                        </el-checkbox-group>
                    </template>
                </el-table-column>
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
                        <el-button size="small" @click="goApply(scope.$index, scope.row)">编辑</el-button>
                        <el-button size="small" type="danger"
                            @click="goDeleteApply(scope.$index, scope.row)">删除</el-button>
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
</template>

<script setup>
import { ref } from '@vue/reactivity'

const loading = ref(false)
const showAnimation = ref(true)
const applyStatus = ref([])
const currentPage = ref(1)
const pageSizes = ref([
    10, 50, 100, 500
])
const pageSize = ref(pageSizes.value[0])
const totalCount = ref(0)

const entryData=ref([])
const queryForm = ref({
    name: '',
    email: '',
    isActive: null,
    status: null
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


const filter = () => {
    var data = entryData.value
    if (queryForm.value.clientId !== '') {
        data = data.filter(x => !queryForm.value.clientId || x.clientId.includes(queryForm.value.clientId))
    }
    if (queryForm.value.displayName !== '') {
        data = data.filter(x => !queryForm.value.displayName || x.displayName.includes(queryForm.value.displayName))
    }
    if (!queryForm.value.isDeleted) {
        data = data.filter(x => x.isDeleted === queryForm.value.isDeleted)
    }
    return data
}
</script>