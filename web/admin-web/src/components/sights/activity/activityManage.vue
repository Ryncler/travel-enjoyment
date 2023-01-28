<template>
    <el-row>
        <el-col :span="8" class="queryCol" :offset="16">
            <el-form :inline="true" :model="queryForm">
                <el-form-item label="活动名称">
                    <el-input v-model="queryForm.name" placeholder="请输入活动名称" />
                </el-form-item>
                <el-form-item label="是否显示已删除活动">
                    <el-switch v-model="queryForm.isDeleted" />
                </el-form-item>
            </el-form>
        </el-col>
    </el-row>
    <el-divider />
    <el-row>
        <el-col :span="2" :offset="1">
            <h3>活动管理</h3>
        </el-col>
        <el-col :span="3" class="options" :offset="18">
            <el-button round type="primary" class="revertbtn" @click="goAdd()">新增活动</el-button>
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
                <el-table-column prop="name" label="活动名称" />
                <el-table-column label="所属景点名称">
                    <template #default="scope">
                        <el-tag class="ml-2" effect="dark"
                            :type="scope.row.sightsName === '暂无' ? 'warning' : 'success'">{{
                                scope.row.sightsName
                            }}</el-tag>
                    </template>
                </el-table-column>
                <el-table-column prop="content" label="活动内容" />
                <el-table-column prop="lastModificationTime" label="更新时间" sortable />
                <el-table-column fixed="right" label="操作">
                    <template #default="scope">
                        <el-button size="small" @click="goEdit(scope.$index, scope.row)">编辑</el-button>
                        <el-button size="small" type="success" @click="goAssign(scope.$index, scope.row)"
                            v-if="scope.row.sightsName === '暂无'">分配景点</el-button>
                        <el-button size="small" type="danger" @click="goDelete(scope.$index, scope.row)">删除</el-button>
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
import { markRaw } from 'vue'
import { Delete } from '@element-plus/icons-vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import { getAll, deleteActivity, getAllByCreateId } from '@/api/sights/activity';
import { getByActivityId } from '@/api/sights/sights';
import { onBeforeMount } from '@vue/runtime-core'
import drawerVue from './drawer.vue'
import { isAdmin } from '@/utils/common';
import store from '@/store';
const { ref } = require("@vue/reactivity");


const loading = ref(false)
const showAnimation = ref(true)
const drawer = ref(null);
const queryForm = ref({
    isDeleted: false
})
const activityData = ref([])

const currentPage = ref(1)
const pageSizes = ref([
    10, 50, 100, 500
])
const pageSize = ref(pageSizes.value[0])
const totalCount = ref(0)

const goSizeChange = (value) => {
    pageSize.value = value
    getActivityData()
}

const goCurrentChange = (value) => {
    currentPage.value = value
    getActivityData()
}

const onAfterLeave = () => {
    showAnimation.value = true
}
const refreshData = () => {
    queryForm.value = {
        isDeleted: false
    }
    showAnimation.value = !showAnimation.value
    getActivityData()
}

const getActivityData = () => {
    loading.value = true
    var parms = {
        isall: true,
        maxResultCount: pageSize.value,
        skipCount: currentPage.value
    }
    if (isAdmin()) {
        return getAll(parms).then(res => {
            if (res.status === 200) {
                totalCount.value = res.data.totalCount
                activityData.value = res.data.items.map((item) => {
                    if (item.lastModificationTime === null) {
                        item.lastModificationTime = '暂无'
                    } else {
                        item.lastModificationTime = new Date(item.lastModificationTime).format('Y.m.d H:i:s')
                    }
                    return item
                })
                activityData.value.forEach(item => {
                    getByActivityId(item.id).then(res => {
                        if (res.status === 200) {
                            item.sightsName = res.data.name
                        } else {
                            item.sightsName = '暂无'
                        }
                    })
                });
                loading.value = false
            }
        })
    } else {
        parms.createId = store.getters['identity/userInfo'].id
        return getAllByCreateId(parms).then(res => {
            if (res.status === 200) {
                totalCount.value = res.data.totalCount
                activityData.value = res.data.items.map((item) => {
                    if (item.lastModificationTime === null) {
                        item.lastModificationTime = '暂无'
                    } else {
                        item.lastModificationTime = new Date(item.lastModificationTime).format('Y.m.d H:i:s')
                    }
                    return item
                })
                activityData.value.forEach(item => {
                    getByActivityId(item.id).then(res => {
                        if (res.status === 200) {
                            item.sightsName = res.data.name
                        } else {
                            item.sightsName = '暂无'
                        }
                    })
                });
                loading.value = false
            }
        })
    }
}


const goAdd = () => {
    drawer.value.title = '添加'
    drawer.value.btnName = '添加'
    drawer.value.showDrawer = true
}

const goEdit = (index, row) => {
    if (row.isDeleted) {
        ElMessage.warning("已删除项不可编辑");
        return
    }
    drawer.value.title = '编辑'
    drawer.value.btnName = '编辑'
    drawer.value.showDrawer = true
    drawer.value.activityForm = row
}

const goAssign = (index, row) => {
    drawer.value.title = '分配'
    drawer.value.btnName = '搜索'
    drawer.value.showDrawer = true
    drawer.value.activityForm = row
}

const goDelete = (index, row) => {
    ElMessageBox.confirm(
        '是否确定要删除该活动？',
        '删除操作',
        {
            type: 'warning',
            icon: markRaw(Delete),
        }
    ).then(() => {
        return deleteActivity(row.id).then(res => {
            if (res.status === 204) {
                ElMessage.success("删除成功");
            }
        })
    })
}
const filter = () => {
    var data = activityData.value

    if (queryForm.value.name !== '') {
        data = data.filter(x => !queryForm.value.name || x.name.includes(queryForm.value.name))
    }
    if (queryForm.value.address !== '') {
        data = data.filter(x => !queryForm.value.address || x.address.includes(queryForm.value.address))
    }
    if (queryForm.value.ticket !== '') {
        data = data.filter(x => !queryForm.value.ticket || x.ticket >= queryForm.value.ticket)
    }
    if (!queryForm.value.isDeleted) {
        data = data.filter(x => x.isDeleted === queryForm.value.isDeleted)
    }
    // console.log(data);
    return data
}


onBeforeMount(async () => {
    getActivityData()
})

// eslint-disable-next-line no-undef
defineExpose({
    loading, showAnimation, queryForm, currentPage, pageSize, activityData,
    filter, onAfterLeave, refreshData, goSizeChange, goCurrentChange, getActivityData
})
</script>