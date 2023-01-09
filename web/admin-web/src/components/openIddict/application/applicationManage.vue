<template>
    <el-row>
        <el-col :span="12" class="queryCol" :offset="12">
            <el-form :inline="true" :model="queryForm">
                <el-form-item label="应用ID">
                    <el-input v-model="queryForm.clientId" placeholder="请输入应用ID" />
                </el-form-item>
                <el-form-item label="应用名称">
                    <el-input v-model="queryForm.displayName" placeholder="请输入应用名称" />
                </el-form-item>
                <el-form-item label="是否显示已删除应用">
                    <el-switch v-model="queryForm.isDeleted" />
                </el-form-item>
            </el-form>
        </el-col>
    </el-row>
    <el-divider />
    <el-row>
        <el-col :span="2" :offset="1">
            <h3>应用管理</h3>
        </el-col>
        <el-col :span="3" class="options" :offset="18">
            <el-button round type="primary" class="revertbtn" @click="goAddApplication()">新增应用</el-button>
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
                <el-table-column prop="clientId" label="应用ID" width="120" />
                <el-table-column prop="displayName" label="应用名称" width="120" />
                <el-table-column prop="type" label="应用类型" width="100" />
                <el-table-column prop="consentType" label="允许类型" width="150" />
                <el-table-column prop="clientUrl" label="应用URl" width="150" />
                <el-table-column label="重定向URl" width="180">
                    <template #default="scope">
                        <div>
                            <li v-for="item in scope.row.redirectUrls" :key="item"
                                style="list-style-type: none;margin-bottom: 15px;">
                                {{ item }}
                            </li>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="注销重定向URl" width="180">
                    <template #default="scope">
                        <div>
                            <li v-for="item in scope.row.postLogoutRedirectUrls" :key="item"
                                style="list-style-type: none;margin-bottom: 15px;">
                                {{ item }}
                            </li>
                        </div>
                    </template>
                </el-table-column>
                <el-table-column label="认证列表" width="180">
                    <template #default="scope">
                        <el-checkbox-group v-model="scope.row.grantTypes" :min="1" disabled>
                            <el-checkbox v-for="item in grantTypeData" :key="item" :label="item">{{
                                item
                            }}</el-checkbox>
                        </el-checkbox-group>
                    </template>
                </el-table-column>
                <el-table-column label="服务列表" width="180">
                    <template #default="scope">
                        <el-checkbox-group v-model="scope.row.scopes" :min="1" disabled>
                            <el-checkbox v-for="item in scopeData" :key="item.name" :label="item.name">{{
                                item.displayName
                            }}</el-checkbox>
                        </el-checkbox-group>
                    </template>
                </el-table-column>
                <el-table-column label="是否已删除" width="120">
                    <template #default="scope">
                        <el-switch v-model="scope.row.isDeleted" disabled />
                    </template>
                </el-table-column>
                <el-table-column prop="creationTime" label="创建时间" width="180" sortable />
                <el-table-column fixed="right" label="操作" width="200">
                    <template #default="scope">
                        <el-button size="small" @click="goEditApplication(scope.$index, scope.row)">编辑</el-button>
                        <el-button size="small" type="danger"
                            @click="goDeleteApplication(scope.$index, scope.row)">删除</el-button>
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
import { getAllApplications, deleteApplication, getGrantTypes } from '@/api/openIddict/application';
import { getScopes } from '@/api/openIddict/scope';
import drawerVue from './drwaer.vue'

const loading = ref(false)
const showAnimation = ref(true)
const currentPage = ref(1)
const pageSizes = ref([
    10, 50, 100, 500
])
const pageSize = ref(pageSizes[0])
const totalCount = ref(0)

const applicationData = ref([{}])
const grantTypeData = ref([])
const scopeData = ref([])
const commonScope = ref([
    {
        name: 'address',
        displayName: 'Address'
    },
    {
        name: 'phone',
        displayName: 'Phone'
    },
    {
        name: 'profile',
        displayName: 'Profile'
    },
    {
        name: 'roles',
        displayName: 'Roles'
    }
])
const queryForm = ref({
    clientId: '',
    displayName: '',
    isDeleted: false
})

const drawer = ref(null);

const onAfterLeave = () => {
    showAnimation.value = true
}
const refreshData = () => {
    queryForm.value = {
        clientId: '',
        displayName: '',
        isDeleted: false
    }
    showAnimation.value = !showAnimation.value
    getApplicationData()
}

const goSizeChange = (value) => {
    pageSize.value = value
    getApplicationData()
}

const goCurrentChange = (value) => {
    currentPage.value = value
    getApplicationData()
}

const goEditApplication = (index, row) => {
    if (row.isDeleted) {
        ElMessage.warning("已删除应用不可编辑");
        return
    }
    drawer.value.title = '编辑'
    drawer.value.btnName = '编辑'
    drawer.value.showDrawer = true
    drawer.value.grantTypes = grantTypeData.value
    drawer.value.scopes = scopeData.value.map((item) => {
        return item.name
    })
    console.log(row);
    drawer.value.applicationForm = row
}

const goDeleteApplication = (index, row) => {
    ElMessageBox.confirm(
        '是否确定要删除该应用？',
        '删除操作',
        {
            type: 'warning',
            icon: markRaw(Delete),
        }
    ).then(() => {
        return deleteApplication(row.id).then(res => {
            if (res.status === 204) {
                ElMessage.success("删除成功");
            }
        })
    })
}

const goAddApplication = () => {
    drawer.value.title = '添加'
    drawer.value.btnName = '添加'
    drawer.value.showDrawer = true
    drawer.value.grantTypes = grantTypeData.value
    drawer.value.scopes = scopeData.value.map((item) => {
        return item.name
    })
}

const getApplicationData = () => {
    loading.value = true
    var parms = {
        isall: true,
        maxResultCount: pageSize.value,
        skipCount: currentPage.value
    }
    return getAllApplications(parms).then(res => {
        if (res.status === 200) {
            totalCount.value = res.data.totalCount
            applicationData.value = res.data.items.map((item) => {
                item.creationTime = new Date(item.creationTime).format('Y.m.d H:i:s')
                return item
            })
            loading.value = false
        }
    })
}

const getGrantTypeData = () => {
    return getGrantTypes().then(res => {
        grantTypeData.value = res.data
    })
}

const getScopeData = () => {
    return getScopes().then(res => {
        scopeData.value = res.data.items
        commonScope.value.forEach(item => {
            scopeData.value.unshift(item)
        });
    })
}

const filter = () => {
    var data = applicationData.value
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

onBeforeMount(async () => {
    getApplicationData()
    getGrantTypeData()
    getScopeData()
})

// eslint-disable-next-line no-undef
defineExpose({
    loading, showAnimation, applicationData, queryForm, currentPage, pageSize, grantTypeData, scopeData, drawer,
    goAddApplication, goEditApplication, getApplicationData, filter, onAfterLeave, refreshData, goDeleteApplication, goSizeChange, goCurrentChange, getGrantTypeData, getScopeData
})

</script>


<style>
.svg-container {
    outline: 2px solid #0000;
    outline-offset: 2px;
}

.el-form-item__content {
    flex-wrap: nowrap;
}

.el-form-item__error {
    margin-left: 28px;
}
</style>