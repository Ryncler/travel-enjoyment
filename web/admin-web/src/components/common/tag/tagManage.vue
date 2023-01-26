<template>
    <el-row>
        <el-col :span="8" class="queryCol" :offset="16">
            <el-form :inline="true" :model="queryForm">
                <el-form-item label="名称">
                    <el-input v-model="queryForm.name" placeholder="请输入类别或标签名称" />
                </el-form-item>
                <el-form-item label="是否显示已删除标签">
                    <el-switch v-model="queryForm.isDeleted" />
                </el-form-item>
            </el-form>
        </el-col>
    </el-row>
    <el-divider />
    <el-row>
        <el-col :span="2" :offset="1">
            <h3>标签管理</h3>
        </el-col>
        <el-col :span="3" class="options" :offset="18">
            <el-button round type="primary" class="revertbtn" @click="goAdd()">新增类别</el-button>
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
            <el-table :data="filter()" :loading="loading" height="600" style="width: 100%" size="large" row-key="id"
                :tree-props="{ children: 'children', hasChildren: 'hasChildren' }">
                <template #empty>
                    <el-empty :image-size="100" />
                </template>
                <el-table-column prop="name" label="类别标签名称" />
                <el-table-column prop="parentName" label="父级类别标签" />
                <el-table-column prop="creationTime" label="创建时间" sortable />
                <el-table-column prop="lastModificationTime" label="更新时间" sortable />
                <el-table-column fixed="right" label="操作">
                    <template #default="scope">
                        <el-button size="small" @click="goEdit(scope.$index, scope.row)"
                            v-if="!scope.row.isDeleted">编辑</el-button>
                        <el-button size="small" type="success" @click="goAddTag(scope.$index, scope.row)"
                            v-if="scope.row.parentName === '暂无' && !scope.row.isDeleted">添加标签</el-button>
                        <el-button size="small" type="danger" @click="goDelete(scope.$index, scope.row)"
                            v-if="!scope.row.isDeleted">删除</el-button>
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
import { getCategoryTree, deleteCategory, deleteTag } from '@/api/common/index';
import { onBeforeMount } from '@vue/runtime-core'
import drawerVue from './drawer.vue'
const { ref } = require("@vue/reactivity");


const loading = ref(false)
const showAnimation = ref(true)
const drawer = ref(null);
const queryForm = ref({
    isDeleted: false
})
const categoryData = ref([])

const currentPage = ref(1)
const pageSizes = ref([
    10, 50, 100, 500
])
const pageSize = ref(pageSizes.value[0])
const totalCount = ref(0)

const goSizeChange = (value) => {
    pageSize.value = value
    getCategoryData()
}

const goCurrentChange = (value) => {
    currentPage.value = value
    getCategoryData()
}

const onAfterLeave = () => {
    showAnimation.value = true
}
const refreshData = () => {
    queryForm.value = {
        isDeleted: false
    }
    showAnimation.value = !showAnimation.value
    getCategoryData()
}

const getCategoryData = () => {
    loading.value = true
    var parms = {
        isall: true,
        maxResultCount: pageSize.value,
        skipCount: currentPage.value
    }
    return getCategoryTree(parms).then(res => {
        if (res.status === 200) {
            totalCount.value = res.data.totalCount
            categoryData.value = res.data.items.map((item) => {
                if (item.lastModificationTime === null) {
                    item.lastModificationTime = '暂无'
                } else {
                    item.lastModificationTime = new Date(item.lastModificationTime).format('Y.m.d H:i:s')
                }
                item.creationTime = new Date(item.creationTime).format('Y.m.d H:i:s')
                item.children = item.children.map((i) => {
                    if (i.lastModificationTime === null) {
                        i.lastModificationTime = '暂无'
                    } else {
                        i.lastModificationTime = new Date(i.lastModificationTime).format('Y.m.d H:i:s')
                    }
                    i.creationTime = new Date(i.creationTime).format('Y.m.d H:i:s')
                    return i
                })
                return item
            })
            loading.value = false
        }
    })
}

const goAddTag = (index, row) => {
    if (row.isDeleted) {
        ElMessage.warning("已删除项不可添加标签");
        return
    }
    drawer.value.title = '添加标签'
    drawer.value.btnName = '添加'
    drawer.value.showDrawer = true
    drawer.value.categoryId = row.id
}

const goAdd = () => {
    drawer.value.title = '添加类别'
    drawer.value.btnName = '添加'
    drawer.value.showDrawer = true
}

const goEdit = (index, row) => {
    if (row.isDeleted) {
        ElMessage.warning("已删除项不可编辑");
        return
    }
    if (row.parentName !== '暂无') {
        drawer.value.title = '编辑标签'
        drawer.value.btnName = '编辑'
        drawer.value.showDrawer = true
        console.log(row);
        drawer.value.categoryForm = row
    } else {
        drawer.value.title = '编辑类别'
        drawer.value.btnName = '编辑'
        drawer.value.showDrawer = true
        drawer.value.categoryForm = row
    }
}

const goDelete = (index, row) => {
    if (row.parentName === '暂无') {
        ElMessageBox.confirm(
            '是否确定要删除该类别？',
            '删除操作',
            {
                type: 'warning',
                icon: markRaw(Delete),
            }
        ).then(() => {
            return deleteCategory(row.id).then(res => {
                if (res.status === 204) {
                    ElMessage.success("删除成功");
                }
            })
        })
    } else {
        ElMessageBox.confirm(
            '是否确定要删除该标签？',
            '删除操作',
            {
                type: 'warning',
                icon: markRaw(Delete),
            }
        ).then(() => {
            return deleteTag(row.id).then(res => {
                if (res.status === 204) {
                    ElMessage.success("删除成功");
                }
            })
        })
    }
}
const filter = () => {
    var data = categoryData.value
    if (queryForm.value.name !== '') {
        data = data.filter(x => !queryForm.value.name || x.name.includes(queryForm.value.name))
    }
    if (!queryForm.value.isDeleted) {
        data = data.filter(x => x.isDeleted === queryForm.value.isDeleted)
    }
    return data
}


onBeforeMount(async () => {
    getCategoryData()
})

// eslint-disable-next-line no-undef
defineExpose({
    loading, showAnimation, queryForm, currentPage, pageSize, categoryData,
    filter, onAfterLeave, refreshData, goSizeChange, goCurrentChange, getCategoryData
})
</script>