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
                                <div>{{ scope.row.trafficGuide }}</div>
                            </template>
                            <template #reference>
                                <el-tag>{{ scope.row.trafficGuide }}</el-tag>
                            </template>
                        </el-popover>
                    </template>
                </el-table-column>
                <el-table-column prop="selfDrivingGuide" label="自驾游指南">
                    <template #default="scope">
                        <el-popover effect="light" trigger="hover" placement="top" width="auto">
                            <template #default>
                                <div>{{ scope.row.selfDrivingGuide }}</div>
                            </template>
                            <template #reference>
                                <el-tag>{{ scope.row.selfDrivingGuide }}</el-tag>
                            </template>
                        </el-popover>
                    </template>
                </el-table-column>
                <el-table-column prop="openTime" label="开放时间" />
                <el-table-column prop="changeTime" label="更新时间" sortable />
                <el-table-column fixed="right" label="操作">
                    <template #default="scope">
                        <el-button size="small" @click="goEdit(scope.$index, scope.row)">编辑</el-button>
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
import { getAll } from '@/api/sights/sights';
import { onBeforeMount } from '@vue/runtime-core'
import drawerVue from './drawer.vue'
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
}

const goCurrentChange = (value) => {
    currentPage.value = value
}

const onAfterLeave = () => {
    showAnimation.value = true
}
const refreshData = () => {
    queryForm.value = {
        isDeleted: false
    }
    showAnimation.value = !showAnimation.value
}

const getSightsData = () => {
    loading.value = true
    var parms = {
        isall: true,
        maxResultCount: pageSize.value,
        skipCount: currentPage.value
    }
    return getAll(parms).then(res => {
        if (res.status === 200) {
            totalCount.value = res.data.totalCount
            sightsData.value = res.data.items.map((item) => {
                item.changeTime = new Date(item.changeTime).format('Y.m.d H:i:s')
                return item
            })
            loading.value = false
        }
    })
}


const goAddSights = () => {
    drawer.value.title = '添加'
    drawer.value.btnName = '添加'
    drawer.value.showDrawer = true
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
        data = data.filter(x => !queryForm.value.ticket || x.ticket >= queryForm.value.ticket)
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