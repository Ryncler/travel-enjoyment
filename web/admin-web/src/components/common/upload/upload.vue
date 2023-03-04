<template>
    <el-upload v-model:file-list="imgs" action="" :http-request="imgUpload" :on-success="onSuccess"
        :before-upload="beforeUpload" :on-remove="removeImg" list-type="picture">
        <el-button round type="primary" class="revertbtn addbtn uploadBtn">上传图片</el-button>
        <template #tip>
            只支持JPG,PNG等格式的图片
        </template>
    </el-upload>
</template>

<script setup>
import { ref } from 'vue';
import { onBeforeMount } from '@vue/runtime-core';
import { ElMessage } from 'element-plus'
import { upload, deleteObj, getImagesById } from '@/api/common/minio'
import { imageHandle } from '@/utils/common/index'

const imgUrls = ref([])
const imgs = ref([])

const bucketName = ref(process.env.VUE_APP_ImgBucketName)

const onSuccess = (response, uploadFile) => {
    // if (response !== undefined || response !== null) {
    //     imgs.value.push({
    //         name: uploadFile.name,
    //         url: imageHandle(response.data)
    //     })
    // }
    // imgUrls.value.push(response.data)
}
const imgUpload = (params) => {
    var data = new FormData()
    data.append('bucketName', bucketName.value)
    data.append('objectName', params.file.name)
    data.append('overrideExisting', true)
    data.append('file', params.file)
    upload(data).then((res) => {
        if (res.status === 200) {
            ElMessage.success('上传成功！')
            // imgs.value.push({
            //     name: params.file.name,
            //     url: imageHandle(res.data)
            // })
            imgUrls.value.push(res.data)
            params.onSuccess(res)
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

const removeImg = (uploadFile, uploadFiles) => {
    var data = {
        bucketName: bucketName.value,
        objectName: uploadFile.response.data
    }
    deleteObj(data).then(res => {
        if (res.status === 200) {
            imgUrls.value.splice(imgUrls.value.indexOf(uploadFile.response.data), 1)
            ElMessage.success('删除成功！')
        }
    })
}

// eslint-disable-next-line no-undef
defineExpose({
    imgUrls
})
</script>

<style>
.uploadBtn {
    margin: 0 10px 50px 15%;
}
</style>