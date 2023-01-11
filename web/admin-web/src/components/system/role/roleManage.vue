<template>
    <el-row>
        <el-col :span="8" class="queryCol" :offset="16">
            <el-form :inline="true" :model="queryForm">
                <el-form-item label="角色名">
                    <el-input v-model="queryForm.name" placeholder="请输入角色名" />
                </el-form-item>
                <el-form-item label="是否显示私有角色">
                    <el-switch v-model="queryForm.isPublic" />
                </el-form-item>
            </el-form>
        </el-col>
    </el-row>
    <el-divider />
    <el-row>
        <el-col :span="2" :offset="1">
            <h3>角色管理</h3>
        </el-col>
        <el-col :span="3" class="options" :offset="18">
            <el-button round type="primary" class="revertbtn" @click="goAddRole()">新增角色</el-button>
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
                <el-table-column prop="name" label="角色名" width="400" />
                <el-table-column label="是否默认" width="350">
                    <template #default="scope">
                        <el-switch v-model="scope.row.isDefault" disabled />
                    </template>
                </el-table-column>
                <el-table-column label="是否静态" width="350">
                    <template #default="scope">
                        <el-switch v-model="scope.row.isStatic" disabled />
                    </template>
                </el-table-column>
                <el-table-column label="是否公开" width="350">
                    <template #default="scope">
                        <el-switch v-model="scope.row.isPublic" disabled />
                    </template>
                </el-table-column>
                <el-table-column fixed="right" label="操作" width="200">
                    <template #default="scope">
                        <el-button size="small" @click="goEditRole(scope.$index, scope.row)">编辑</el-button>
                        <el-button size="small" type="primary"
                            @click="goPermission(scope.$index, scope.row)">权限</el-button>
                        <el-button size="small" type="danger"
                            @click="goDeleteRole(scope.$index, scope.row)">删除</el-button>
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
    <permissionDrawerVue ref="permissionDrawer" />
</template>

<script setup>
import { ref } from '@vue/reactivity'
import { markRaw } from 'vue'
import { Delete } from '@element-plus/icons-vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import drawerVue from './drawer.vue'
import permissionDrawerVue from './permissionDrawer.vue'
import { onBeforeMount } from '@vue/runtime-core';
import { getRoles, deleteRole } from '@/api/user/role';

const loading = ref(false)
const showAnimation = ref(true)
const currentPage = ref(1)
const pageSizes = ref([
    10, 50, 100, 500
])
const pageSize = ref(pageSizes.value[0])
const totalCount = ref(0)

const roleData = ref([{}])
const queryForm = ref({
    name: '',
    isPublic: false
})
const drawer = ref(null);
const permissionDrawer = ref(null)

const onAfterLeave = () => {
    showAnimation.value = true
}
const refreshData = () => {
    queryForm.value = {
        name: '',
        isPublic: false
    }
    showAnimation.value = !showAnimation.value
    getRoleData()
}

const goSizeChange = (value) => {
    pageSize.value = value
    getRoleData()
}

const goCurrentChange = (value) => {
    currentPage.value = value
    getRoleData()
}

const goEditRole = (index, row) => {
    drawer.value.title = '编辑'
    drawer.value.btnName = '编辑'
    const data = row
    drawer.value.roleForm = data
    drawer.value.showDrawer = true
}

const goPermission = (index, row) => {
    permissionDrawer.value.showDrawer = true
    permissionDrawer.value.permissionsQuery.providerName = row.name === 'admin' ? 'R' : 'U'
    permissionDrawer.value.permissionsQuery.providerKey = row.name === 'admin' ? 'admin' : row.id
    permissionDrawer.value.getPermissionData()
}

const goDeleteRole = (index, row) => {
    ElMessageBox.confirm(
        '是否确定要删除该角色？',
        '删除操作',
        {
            type: 'warning',
            icon: markRaw(Delete),
        }
    ).then(() => {
        return deleteRole(row.id).then(res => {
            if (res.status === 204) {
                ElMessage.success("删除成功");
            }
        })
    })
}

const goAddRole = () => {
    drawer.value.title = '添加'
    drawer.value.btnName = '添加'
    drawer.value.showDrawer = true
}

const getRoleData = () => {
    loading.value = true
    var parms = {
        isall: true,
        maxResultCount: pageSize.value,
        skipCount: currentPage.value
    }
    return getRoles(parms).then(res => {
        if (res.status === 200) {
            totalCount.value = res.data.totalCount
            roleData.value = res.data.items
            loading.value = false
        }
    })
}

const filter = () => {
    var data = roleData.value
    if (queryForm.value.name !== '') {
        data = data.filter(x => !queryForm.value.name || x.name.includes(queryForm.value.name))
    }
    if (!queryForm.value.isPublic) {
        data = data.filter(x => x.isPublic !== queryForm.value.isPublic)
    }
    return data
}

onBeforeMount(() => {
    getRoleData()
})

// eslint-disable-next-line no-undef
defineExpose({
    loading, showAnimation, drawer, roleData, queryForm, currentPage, pageSize, permissionDrawer,
    goAddRole, getRoleData, filter, goEditRole, onAfterLeave, refreshData, goDeleteRole, goSizeChange, goCurrentChange, goPermission
})

</script>

<style lang="scss">
.el-row {
    margin-bottom: 20px;
}

.el-row:last-child {
    margin-bottom: 0;
}

.el-col {
    border-radius: 4px;
}

.frombtn {
    margin: 0 10px 0 0;
}

.tooltip-base-box .box-item {
    width: 110px;
    margin-top: 10px;
}

th>.cell {
    font-size: 15px;
    font-weight: bold;
    color: black;
}

.image-slot {
    display: flex;
    justify-content: center;
    align-items: center;
    width: 100%;
    height: 100%;
    background: var(--el-fill-color-light);
    color: var(--el-text-color-secondary);
    font-size: 30px;
}

.closeIcon {
    color: white;
}

.el-form-item__content {
    flex-wrap: nowrap;
}
</style>