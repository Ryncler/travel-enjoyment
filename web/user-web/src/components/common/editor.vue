<template>
    <div id="summer"></div>
</template>

<script setup>
import { ref } from 'vue';
import { onMounted } from '@vue/runtime-core';
import $ from 'jquery'
import { ElMessage } from 'element-plus'
import { upload } from '@/api/common/minio'
import { imageHandle } from '@/utils/common';

const bucketName = ref(process.env.VUE_APP_ImgBucketName)

const starEditor = () => {
    $('#summer').summernote({
        lang: 'zh-CN',
        placeholder: '请输入内容',
        height: 500,
        toolbar: [
            [
                'style',
                ['style']
            ],
            [
                'font',
                ['bold', 'underline', 'clear']
            ],
            [
                'color',
                ['color']
            ],
            [
                'para',
                ['ul', 'ol', 'paragraph']
            ],
            [
                'table',
                ['table']
            ],
            [
                'insert',
                ['link', 'picture', 'video']
            ],
            [
                'view',
                ['fullscreen', 'codeview', 'help']
            ]
        ],
        callbacks: {
            onImageUpload: function (files) {
                console.log(files);
                for (let i = 0; i < files.length; i++) {
                    var data = new FormData()
                    data.append('bucketName', bucketName.value)
                    data.append('objectName', files[i].name)
                    data.append('overrideExisting', true)
                    data.append('file', files[i])
                    upload(data).then((res) => {
                        if (res.status === 200) {
                            ElMessage.success('上传成功！')
                            var img = `<p><img src="${imageHandle(res.data)}"</p>`
                            $('#summer').summernote('pasteHTML', img);
                        }
                    })
                }
            }
        }

    })
}

const getEditorValue = () => {
    var content = $('#summer').summernote('code')
    return content
}
const setEditorValue = (value) => {
    $('#summer').summernote('code', value)
}

onMounted(() => {
    starEditor()
})


// eslint-disable-next-line no-undef
defineExpose({
    getEditorValue, setEditorValue
})
</script>

<style></style>