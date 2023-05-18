<template>
    <el-row>
        <el-col :span="16" class="queryCol" :offset="8">
            <el-form :inline="true" :model="queryForm">
                <el-form-item label="用户名">
                    <el-input v-model="queryForm.name" placeholder="请输入用户名" />
                </el-form-item>
                <el-form-item label="邮箱">
                    <el-input v-model="queryForm.email" placeholder="请输入邮箱" />
                </el-form-item>
                <el-form-item label="用户状态">
                    <el-select v-model="queryForm.status" clearable placeholder="请选择用户状态">
                        <el-option v-for="item in options" :key="item.value" :label="item.label" :value="item.value" />
                    </el-select>
                </el-form-item>
                <el-form-item label="是否显示已删除用户">
                    <el-switch v-model="queryForm.isDelete" />
                </el-form-item>
            </el-form>
        </el-col>
    </el-row>
    <el-divider />
    <el-row>
        <el-col :span="2" :offset="1">
            <h3>用户管理</h3>
        </el-col>
        <el-col :span="3" class="options" :offset="18">
            <el-button round type="primary" class="revertbtn" @click="goAddUser()">新增用户</el-button>
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
                <el-table-column prop="userName" label="用户名" width="200" />
                <el-table-column prop="sex" label="性别" width="100" :formatter="sexFormatter" />
                <el-table-column prop="email" label="邮箱" width="150" />
                <el-table-column prop="phone" label="电话" width="150" />
                <el-table-column label="头像" width="200">
                    <template #default="scope">
                        <el-image style="width: 100px; height: 100px" :src="imageHandle(scope.row.avatar)" :fit="fit">
                            <template #error>
                                <div class="image-slot">
                                    <icon data="@/icons/image.svg" />
                                </div>
                            </template>
                        </el-image>
                    </template>
                </el-table-column>
                <el-table-column label="角色" width="150">
                    <template #default="scope">
                        <el-checkbox-group v-model="scope.row.roles" :min="1" disabled>
                            <el-checkbox v-for="item in rolesData" :key="item" :label="item">{{
                                item
                            }}</el-checkbox>
                        </el-checkbox-group>
                    </template>
                </el-table-column>
                <el-table-column label="当前状态" width="100">
                    <template #default="scope">
                        <el-switch v-model="scope.row.active" disabled />
                    </template>
                </el-table-column>
                <el-table-column label="是否已删除" width="150">
                    <template #default="scope">
                        <el-switch v-model="scope.row.delete" disabled />
                    </template>
                </el-table-column>
                <el-table-column prop="createTime" label="创建时间" width="200" sortable />
                <el-table-column fixed="right" label="操作" width="250">
                    <template #default="scope">
                        <el-button size="small" @click="goEditUser(scope.$index, scope.row)">编辑</el-button>
                        <el-button size="small" type="danger" v-if="scope.row.userName != 'admin' && !scope.row.delete"
                            @click="goDeleteUser(scope.$index, scope.row)">删除</el-button>
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
import drawerVue from './drawer.vue'
import { onBeforeMount } from '@vue/runtime-core';
import { getAllUser, deleteUser } from '@/api/user/user';
import { getRoles, imageHandle } from '@/utils/common/index'

const loading = ref(false)
const showAnimation = ref(true)
const currentPage = ref(1)
const pageSizes = ref([
    10, 50, 100, 500
])
const pageSize = ref(pageSizes.value[0])
const totalCount = ref(0)

const options = ref([
    {
        label: '已激活',
        value: true,
    },
    {
        label: '未激活',
        value: false,
    }
])
const userData = ref([{}])
const rolesData = ref([])
const queryForm = ref({
    name: '',
    email: '',
    status: null,
    isDelete: false
})
const drawer = ref(null);

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
    getUserData()
}

const sexFormatter = (row, column, cellValue, index) => {
    return cellValue === true ? '男' : '女'
}

const goSizeChange = (value) => {
    pageSize.value = value
    getUserData()
}

const goCurrentChange = (value) => {
    currentPage.value = value
    getUserData()
}

const goEditUser = (index, row) => {
    if (row.delete) {
        ElMessage.warning("已删除用户不可编辑");
        return
    }
    drawer.value.title = '编辑'
    drawer.value.btnName = '编辑'
    row.password = process.env.VUE_APP_Password
    drawer.value.userForm = row
    drawer.value.showDrawer = true
    drawer.value.rolesData = rolesData.value
}

const goDeleteUser = (index, row) => {
    ElMessageBox.confirm(
        '是否确定要删除该用户？',
        '删除操作',
        {
            type: 'warning',
            icon: markRaw(Delete),
        }
    ).then(() => {
        return deleteUser(row.id).then(res => {
            if (res.status === 204) {
                ElMessage.success("删除成功");
            }
        })
    })
}

const goAddUser = () => {
    drawer.value.title = '添加'
    drawer.value.btnName = '添加'
    drawer.value.showDrawer = true
    drawer.value.rolesData = rolesData.value
}

const getUserData = () => {
    loading.value = true
    var parms = {
        isall: true,
        maxResultCount: pageSize.value,
        skipCount: currentPage.value
    }
    return getAllUser(parms).then(res => {
        if (res.status === 200) {
            totalCount.value = res.data.totalCount
            userData.value = res.data.items.map((item) => {
                return {
                    id: item.id,
                    userName: item.userName,
                    sex: item.sex,
                    email: item.email,
                    phone: item.phone,
                    roles: item.roles,
                    avatar: item.avatar,
                    active: item.active,
                    delete: item.delete,
                    createTime: new Date(item.creationTime).format('Y.m.d H:i:s')
                }
            })
            loading.value = false
        }
    })
}

const filter = () => {
    var data = userData.value
    if (queryForm.value.name !== '') {
        data = data.filter(x => !queryForm.value.name || x.userName.includes(queryForm.value.name))
    }
    if (queryForm.value.email !== '') {
        data = data.filter(x => !queryForm.value.email || x.email.includes(queryForm.value.email))
    }
    if (queryForm.value.status !== null) {
        if (queryForm.value.status.length !== 0) {
            data = data.filter(x => x.active === queryForm.value.status)
        }
    }
    if (!queryForm.value.isDelete) {
        data = data.filter(x => x.delete === queryForm.value.isDelete)
    }
    return data
}

onBeforeMount(async () => {
    getUserData()
    rolesData.value = await getRoles()
})

// eslint-disable-next-line no-undef
defineExpose({
    loading, options, showAnimation, drawer, userData, queryForm, currentPage, pageSize,
    goAddUser, close, getUserData, filter, goEditUser, onAfterLeave, sexFormatter, refreshData, goDeleteUser, goSizeChange, goCurrentChange
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

.options {
    padding: 15px 0 0 0;
}

.tooltip-base-box .box-item {
    width: 110px;
    margin-top: 10px;
}

.svg-container {
    outline: 2px solid #0000;
    outline-offset: 2px;
}

@keyframes rotate360 {
    100% {
        transform: rotate(360deg);
    }
}

.refresh-enter-active,
.refresh-leave-active {
    animation: rotate360 0.5s;
}

.refresh-enter-from,
.refresh-leave-to {
    animation: rotate360 0.5s reverse;
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