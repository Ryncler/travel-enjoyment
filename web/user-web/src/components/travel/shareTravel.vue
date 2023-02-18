<template>
    <h3 class="travelName">游记标题</h3>
    <el-input v-model="travel.title" placeholder="Title" clearable />
    <h3 class="travelName">出行时间</h3>
    <el-date-picker v-model="travel.datetime" type="datetime" placeholder="Date" format="YYYY-MM-DD hh:mm:ss"
        value-format="YYYY-MM-DD h:m:s a" />
    <h3 class="travelName">出行天数</h3>
    <el-input-number v-model="travel.day" :min="1" @change="dayChange()" />
    <h3 class="travelName">游记内容</h3>
    <editorVue ref="editor"></editorVue>
</template>

<script setup>
import { ref } from 'vue';
import { onBeforeMount } from '@vue/runtime-core';
import editorVue from '@/components/common/editor'

const editor = ref()
const travel = ref({
    title: '',
    day: '',
    datetime: '',
})

const dayChange = () => {

}

//获取富文本编辑器内容
// editor.value.getEditorValue()
</script>

<style>
.travelName {
    margin-top: 30px;
}

.el-date-editor.el-input,
.el-date-editor.el-input__wrapper {
    width: 100%;
}

.el-date-picker {
    width: 100%;
}

.el-input-number {
    width: 100%;
}

.el-input-number .el-input__inner {
    text-align: left;

}
</style>