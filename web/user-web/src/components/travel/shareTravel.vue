<template>
    <h3 class="travelName">游记标题</h3>
    <el-input v-model="travel.travelsTitle" placeholder="Title" clearable />
    <h3 class="travelName">出行时间</h3>
    <el-date-picker v-model="travel.departureTime" type="datetime" placeholder="Date" format="YYYY-MM-DD hh:mm:ss"
        value-format="YYYY-MM-DD h:m:s" />
    <h3 class="travelName">出行天数</h3>
    <el-input-number v-model="travel.travelDayNum" :min="1" @change="dayChange()" />
    <h3 class="travelName">出行费用</h3>
    <el-input-number v-model="travel.travelExpenses" :min="1" @change="dayChange()" />
    <h3 class="travelName">游记内容</h3>
    <editorVue ref="editor"></editorVue>

    <div>
        <el-button :loading="loading" round type="primary" class="btn" @click="goOverview()">预览</el-button>
        <el-button :loading="loading" round type="primary" class="btn" @click="goPublish()">发布</el-button>
    </div>
</template>

<script setup>
import { ref } from 'vue';
import { markRaw } from 'vue'
import { onBeforeMount } from '@vue/runtime-core';
import { Checked } from '@element-plus/icons-vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import editorVue from '@/components/common/editor'
import store from '@/store';
import { addTravel } from '@/api/travel'

const editor = ref()
const travel = ref({
    travelsTitle: '',
    departureTime: '',
    travelDayNum: '',
    travelExpenses: '',
    content: '',
    releaseId: store.getters['identity/userInfo'].id,
    releaseDate: new Date().toLocaleString()
})

const dayChange = () => {

}

const goOverview = () => {

}

const goPublish = () => {
    ElMessageBox.confirm(
        '是否确定要发布该游记？',
        '发布操作',
        {
            type: 'warning',
            icon: markRaw(Checked),
        }
    ).then(() => {
        travel.value.content = editor.value.getEditorValue()
        addTravel(travel.value).then(res => {
            if (res.status === 200) {
                ElMessage({
                    type: 'success',
                    message: '发布成功'
                })
            }
        })
    })
}

//获取富文本编辑器内容
// editor.value.getEditorValue()
</script>

<style scoped>
.btn {
    width: 300px;
    height: 35px;
    font-size: 14px;
    font-weight: bold;
    margin-left: 17%;
    background-color: #66CCCC;
    border: 1px solid #66CCCC;
}

.btn:hover {
    transition: all 0.5s;
    color: white;
    background-color: #66CCFF;
    border: 1px solid #66CCFF;
}

.btn:focus {
    outline: 0;
    transition: all 0.5s;
    color: white;
    background-color: #66CCFF;
    border: 1px solid #66CCFF;
}
</style>

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