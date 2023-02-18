<template>
    <div id="summer"></div>
</template>

<script setup>
import { ref } from 'vue';
import { onMounted } from '@vue/runtime-core';
import $ from 'jquery'

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
        ]
    })
}

const getEditorValue = () => {
    var content = $('#summer').summernote('code')
    return content
}

onMounted(() => {
    starEditor()
})


// eslint-disable-next-line no-undef
defineExpose({
    getEditorValue
})
</script>

<style></style>