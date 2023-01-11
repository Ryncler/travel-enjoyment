<template>
    <el-row>
        <el-col :span="12" class="queryCol" :offset="12">
            <el-form :inline="true" :model="queryForm">
                <el-form-item label="服务ID">
                    <el-input v-model="queryForm.name" placeholder="请输入服务ID" />
                </el-form-item>
                <el-form-item label="服务名称">
                    <el-input v-model="queryForm.displayName" placeholder="请输入服务名称" />
                </el-form-item>
                <el-form-item label="是否显示已删除服务">
                    <el-switch v-model="queryForm.isDeleted" />
                </el-form-item>
            </el-form>
        </el-col>
    </el-row>
    <el-divider />
    <el-row>
        <el-col :span="2" :offset="1">
            <h3>服务管理</h3>
        </el-col>
        <el-col :span="3" class="options" :offset="18">
            <el-button round type="primary" class="revertbtn" @click="goAddScope()">新增服务</el-button>
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
                <el-table-column prop="name" label="服务ID" width="200" />
                <el-table-column prop="displayName" label="服务名称" width="250" />
                <el-table-column prop="description" label="服务说明" width="300" />
                <el-table-column label="资源列表" width="300">
                    <template #default="scope">
                        <div>
                            <li v-for="item in scope.row.resources" :key="item"
                                style="list-style-type: none;margin-bottom: 15px;">
                                {{ item }}
                            </li>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="是否已删除" width="200">
                    <template #default="scope">
                        <el-switch v-model="scope.row.isDeleted" disabled />
                    </template>
                </el-table-column>
                <el-table-column prop="creationTime" label="创建时间" width="200" sortable />
                <el-table-column fixed="right" label="操作" width="200">
                    <template #default="scope">
                        <el-button size="small" @click="goEditScope(scope.$index, scope.row)">编辑</el-button>
                        <el-button size="small" type="danger"
                            @click="goDeleteScope(scope.$index, scope.row)">删除</el-button>
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
import { markRaw } from 'vue'
import { Delete } from '@element-plus/icons-vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import { onBeforeMount } from '@vue/runtime-core';
import { getAllScopes, deleteScope } from '@/api/openIddict/scope';
import drawerVue from './drawer.vue'

const loading = ref(false)
const showAnimation = ref(true)
const currentPage = ref(1)
const pageSizes = ref([
    10, 50, 100, 500
])
const pageSize = ref(pageSizes.value[0])
const totalCount = ref(0)

const scopeData = ref([{}])
const queryForm = ref({
    name: '',
    displayName: '',
    isDeleted: false
})

const drawer = ref(null);

const onAfterLeave = () => {
    showAnimation.value = true
}
const refreshData = () => {
    queryForm.value = {
        name: '',
        displayName: '',
        isDeleted: false
    }
    showAnimation.value = !showAnimation.value
    getScopeData()
}

const goSizeChange = (value) => {
    pageSize.value = value
    getScopeData()
}

const goCurrentChange = (value) => {
    currentPage.value = value
    getScopeData()
}

const goDeleteScope = (index, row) => {
    ElMessageBox.confirm(
        '是否确定要删除该服务？',
        '删除操作',
        {
            type: 'warning',
            icon: markRaw(Delete),
        }
    ).then(() => {
        return deleteScope(row.id).then(res => {
            if (res.status === 204) {
                ElMessage.success("删除成功");
            }
        })
    })
}

const goAddScope = () => {
    drawer.value.title = '添加'
    drawer.value.btnName = '添加'
    drawer.value.showDrawer = true
}
const goEditScope = (index, row) => {
    if (row.isDeleted) {
        ElMessage.warning("已删除服务不可编辑");
        return
    }
    drawer.value.title = '编辑'
    drawer.value.btnName = '编辑'
    drawer.value.showDrawer = true
    drawer.value.scopeForm = row
}

const getScopeData = () => {
    loading.value = true
    var parms = {
        isall: true,
        maxResultCount: pageSize.value,
        skipCount: currentPage.value
    }
    return getAllScopes(parms).then(res => {
        if (res.status === 200) {
            totalCount.value = res.data.totalCount
            scopeData.value = res.data.items.map((item) => {
                item.resources = JSON.parse(item.resources)
                item.creationTime = new Date(item.creationTime).format('Y.m.d H:i:s')
                return item
            })
            loading.value = false
        }
    })
}

const filter = () => {
    var data = scopeData.value
    if (queryForm.value.name !== '') {
        data = data.filter(x => !queryForm.value.name || x.clientId.includes(queryForm.value.name))
    }
    if (queryForm.value.displayName !== '') {
        data = data.filter(x => !queryForm.value.displayName || x.displayName.includes(queryForm.value.displayName))
    }
    if (!queryForm.value.isDeleted) {
        data = data.filter(x => x.isDeleted === queryForm.value.isDeleted)
    }
    return data
}

onBeforeMount(async () => {
    getScopeData()
})

// eslint-disable-next-line no-undef
defineExpose({
    loading, showAnimation, scopeData, queryForm, currentPage, pageSize,
    goAddScope, goEditScope, getScopeData, filter, onAfterLeave, refreshData, goDeleteScope, goSizeChange, goCurrentChange
})
</script>

<style>

</style>