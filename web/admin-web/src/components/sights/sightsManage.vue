<template>
    <el-row>
        <el-col :span="16" class="queryCol" :offset="8">
            <el-form :inline="true" :model="queryForm">
                <el-form-item label="景点名称">
                    <el-input v-model="queryForm.name" placeholder="请输入景点名称" />
                </el-form-item>
                <el-form-item label="地址">
                    <el-input v-model="queryForm.address" placeholder="请输入大概的地址" />
                </el-form-item>
                <el-form-item label="门票价钱">
                    <el-input-number v-model="queryForm.ticket" :min="0" @change="handleChange" />
                </el-form-item>
                <el-form-item label="是否显示已删除景点">
                    <el-switch v-model="queryForm.isDeleted" />
                </el-form-item>
            </el-form>
        </el-col>
    </el-row>
    <el-divider />
    <el-row>
        <el-col :span="2" :offset="1">
            <h3>景点管理</h3>
        </el-col>
        <el-col :span="3" class="options" :offset="18">
            <el-button round type="primary" class="revertbtn" @click="goAddSights()">新增景点</el-button>
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
                <el-table-column prop="name" label="景点名称" />
                <el-table-column prop="description" label="说明" />
                <el-table-column prop="address" label="地址" />
                <el-table-column prop="ticket" label="门票价钱" />
                <el-table-column prop="trafficGuide" label="交通指南">
                    <template #default="scope">
                        <el-popover effect="light" trigger="hover" placement="top" width="auto">
                            <template #default>
                                <div>{{ scope.row.trafficGuide === null ? "暂无" : scope.row.trafficGuide }}</div>
                            </template>
                            <template #reference>
                                <el-tag>{{ scope.row.trafficGuide === null ? "暂无" : scope.row.trafficGuide }}</el-tag>
                            </template>
                        </el-popover>
                    </template>
                </el-table-column>
                <el-table-column prop="selfDrivingGuide" label="自驾游指南">
                    <template #default="scope">
                        <el-popover effect="light" trigger="hover" placement="top" width="auto">
                            <template #default>
                                <div>{{ scope.row.selfDrivingGuide === null ? "暂无" : scope.row.selfDrivingGuide }}</div>
                            </template>
                            <template #reference>
                                <el-tag>{{ scope.row.selfDrivingGuide === null ? "暂无" : scope.row.selfDrivingGuide
                                }}</el-tag>
                            </template>
                        </el-popover>
                    </template>
                </el-table-column>
                <el-table-column prop="openTime" label="开放时间" />
                <el-table-column prop="lastModificationTime" label="更新时间" sortable />
                <el-table-column fixed="right" label="操作" width="200px">
                    <template #default="scope">
                        <el-button size="small" type="success" @click="goImage(scope.$index, scope.row)">图片</el-button>
                        <el-button size="small" v-if="!scope.row.isDeleted"
                            @click="goEdit(scope.$index, scope.row)">编辑</el-button>
                        <el-button size="small" v-if="!scope.row.isDeleted" type="danger"
                            @click="goDelete(scope.$index, scope.row)">删除</el-button>
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
import { getAll, deleteSights, getAllByCreateId } from '@/api/sights/sights';
import { onBeforeMount } from '@vue/runtime-core'
import drawerVue from './drawer.vue'
import { isAdmin } from '@/utils/common';
import store from '@/store';
import router from '@/router'
const { ref } = require("@vue/reactivity");


const loading = ref(false)
const showAnimation = ref(true)
const drawer = ref(null);
const queryForm = ref({
    isDeleted: false
})
const sightsData = ref([])

const currentPage = ref(1)
const pageSizes = ref([
    10, 50, 100, 500
])
const pageSize = ref(pageSizes.value[0])
const totalCount = ref(0)

const goSizeChange = (value) => {
    pageSize.value = value
    getSightsData()
}

const goCurrentChange = (value) => {
    currentPage.value = value
    getSightsData()
}

const onAfterLeave = () => {
    showAnimation.value = true
}
const refreshData = () => {
    queryForm.value = {
        isDeleted: false
    }
    showAnimation.value = !showAnimation.value
    getSightsData()
}

const getSightsData = () => {
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
                sightsData.value = res.data.items.map((item) => {
                    if (item.lastModificationTime === null) {
                        item.lastModificationTime = '暂无'
                    } else {
                        item.lastModificationTime = new Date(item.lastModificationTime).format('Y.m.d H:i:s')
                    }
                    return item
                })
                console.log(sightsData.value);
                loading.value = false
            }
        })
    } else {
        parms.createId = store.getters['identity/userInfo'].id
        return getAllByCreateId(parms).then(res => {
            if (res.status === 200) {
                totalCount.value = res.data.totalCount
                sightsData.value = res.data.items.map((item) => {
                    if (item.lastModificationTime === null) {
                        item.lastModificationTime = '暂无'
                    } else {
                        item.lastModificationTime = new Date(item.lastModificationTime).format('Y.m.d H:i:s')
                    }
                    return item
                })
                loading.value = false
            }
        })
    }
}

const goImage = (index, row) => {
    router.push({ name: 'ImageManage', path: '/image/manage', query: { id: row.id } })
}


const goAddSights = () => {
    drawer.value.title = '添加'
    drawer.value.btnName = '添加'
    drawer.value.showDrawer = true
    drawer.value.getGeoData()
}

const goEdit = (index, row) => {
    if (row.isDeleted) {
        ElMessage.warning("已删除项不可编辑");
        return
    }
    drawer.value.title = '编辑'
    drawer.value.btnName = '编辑'
    drawer.value.showDrawer = true
    drawer.value.sightsForm = row
    drawer.value.getGeoData()
    drawer.value.getSightsTag(row.id)
}

const goDelete = (index, row) => {
    ElMessageBox.confirm(
        '是否确定要删除该景点？',
        '删除操作',
        {
            type: 'warning',
            icon: markRaw(Delete),
        }
    ).then(() => {
        return deleteSights(row.id).then(res => {
            if (res.status === 204) {
                ElMessage.success("删除成功");
            }
        })
    })
}
const filter = () => {
    var data = sightsData.value

    if (queryForm.value.name !== '') {
        data = data.filter(x => !queryForm.value.name || x.name.includes(queryForm.value.name))
    }
    if (queryForm.value.address !== '') {
        data = data.filter(x => !queryForm.value.address || x.address.includes(queryForm.value.address))
    }
    if (queryForm.value.ticket !== '') {
        data = data.filter(x => !queryForm.value.ticket || x.ticket.includes(queryForm.value.ticket))
    }
    if (!queryForm.value.isDeleted) {
        data = data.filter(x => x.isDeleted === queryForm.value.isDeleted)
    }
    // console.log(data);
    return data
}


onBeforeMount(async () => {
    getSightsData()
})

// eslint-disable-next-line no-undef
defineExpose({
    loading, showAnimation, queryForm, currentPage, pageSize, sightsData,
    filter, onAfterLeave, refreshData, goSizeChange, goCurrentChange, getSightsData
})
</script>