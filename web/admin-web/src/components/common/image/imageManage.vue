<template>
    <el-row>
        <el-col :span="2" :offset="1">
            <h3>图片管理</h3>
        </el-col>
        <el-col :span="5" class="options" :offset="16">
            <el-switch v-model="isDesc" size="large" class="switch" inline-prompt active-text="倒序" inactive-text="正序"
                style="--el-switch-on-color: #66CCCC; --el-switch-off-color: #f56c6c" />
            <el-upload :show-file-list="false" action="" :http-request="imgUpload" :before-upload="beforeUpload"
                list-type="picture">
                <el-button round type="primary" class="revertbtn" @click="goUpload()">添加图片</el-button>
            </el-upload>
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
            <el-table :data="filter()" :loading="loading" height="600" style="width: 100%" size="large"
                highlight-current-row @row-dblclick="switchMain">
                <template #empty>
                    <el-empty :image-size="100" />
                </template>
                <el-table-column label="图片" width="200">
                    <template #default="scope">
                        <el-image style="width: 200px; height: 100px" :src="imageHandle(scope.row.imageURL)" :fit="fit">
                            <template #error>
                                <div class="image-slot">
                                    <icon data="@/icons/image.svg" />
                                </div>
                            </template>
                        </el-image>
                    </template>
                </el-table-column>
                <el-table-column prop="linkId" label="匹配Id" />
                <el-table-column prop="imageURL" label="图片URL" />
                <el-table-column prop="isMain" label="是否是主图" />
                <el-table-column prop="creationTime" label="创建时间" />
                <el-table-column fixed="right" label="操作">
                    <template #default="scope">
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
</template>

<script setup>
import { ref } from 'vue';
import router from '@/router'
import { onBeforeMount } from '@vue/runtime-core';
import { markRaw } from 'vue'
import { Delete } from '@element-plus/icons-vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import { upload, deleteObj, deleteImage, getImageListById, addImage, updateImage } from '@/api/common/minio'
import { imageHandle } from '@/utils/common/index'

const loading = ref(false)
const showAnimation = ref(true)
const currentPage = ref(1)
const pageSizes = ref([
    10, 50, 100, 500
])
const pageSize = ref(pageSizes.value[0])
const totalCount = ref(0)

const isDesc = ref(true)
const searchId = ref(router.currentRoute.value.query.id)
const imgData = ref([])
const bucketName = ref(process.env.VUE_APP_ImgBucketName)

const onAfterLeave = () => {
    showAnimation.value = true
}
const refreshData = () => {
    showAnimation.value = !showAnimation.value
    getImageData()
}

const goSizeChange = (value) => {
    pageSize.value = value
    getImageData()
}

const goCurrentChange = (value) => {
    currentPage.value = value
    getImageData()
}

const goDelete = (index, row) => {
    ElMessageBox.confirm(
        '是否确定要删除该图片？',
        '删除操作',
        {
            type: 'warning',
            icon: markRaw(Delete),
        }
    ).then(() => {
        var data = {
            bucketName: bucketName.value,
            objectName: row.imageURL
        }
        deleteImage(row.id).then(res => {
            if (res.status === 204) {
                deleteObj(data).then(res => {
                    if (res.status === 204) {
                        ElMessage.success("删除成功");
                    }
                })
            }
        })
    })
}

const getImageData = () => {
    loading.value = true
    var parms = {
        linkId: searchId.value,
        isall: true,
        maxResultCount: pageSize.value,
        skipCount: currentPage.value,
        sorting: 'CreationTime'
    }
    return getImageListById(parms).then(res => {
        if (res.status === 200) {
            totalCount.value = res.data.totalCount
            imgData.value = res.data.items.map((item) => {
                item.creationTime = new Date(item.creationTime).format('Y.m.d H:i:s')
                return item
            })
            loading.value = false
        }
    })
}


const imgUpload = (params) => {
    var data = new FormData()
    data.append('bucketName', bucketName.value)
    data.append('objectName', params.file.name)
    data.append('overrideExisting', true)
    data.append('file', params.file)
    upload(data).then((res) => {
        if (res.status === 200) {
            var imageData = {
                linkId: searchId.value,
                imageURL: res.data
            }
            addImage(imageData).then(image => {
                if (image.status === 200) {
                    ElMessage.success('上传成功！')
                    getImageData()
                }
            })
        }
    })
}
const beforeUpload = (rawFile) => {
    const imgType = ['image/png', 'image/jpeg ']
    if (imgType.indexOf(rawFile.type) > 0) {
        ElMessage.error('该文件不是图片类型!')
        return false
    } else if (rawFile.size / 1024 / 1024 > 10) {
        ElMessage.error('图片大小不能大于10MB!')
        return false
    }
    return true
}


const filter = () => {
    if (isDesc.value === true)
        return imgData.value.reverse()
    if (isDesc.value === false)
        return imgData.value.reverse()
}

const switchMain = (row, column, event) => {
    var data = {
        linkId: row.linkId,
        isMain: true,
        imageURL: row.imageURL
    }
    updateImage(row.id, data).then(res => {
        if (res.status === 200) {
            ElMessage.success('设置主图成功')
        }
    })
}
onBeforeMount(async () => {
    getImageData()
})
</script>

<style>
.options {
    display: flex;
}

.switch {
    /* width: 100px; */
    height: 35px;
    margin-right: 20px;
}
</style>