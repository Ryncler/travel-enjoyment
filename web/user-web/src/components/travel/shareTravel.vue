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
        <el-button :loading="loading" round type="primary" class="btn assign" @click="goShowDrawer()">分配景点</el-button>
        <el-button :loading="loading" round type="primary" class="btn" @click="goPublish()">发布</el-button>
    </div>

    <drawerVue ref="drawer" />
</template>

<script setup>
import { ref } from 'vue';
import { markRaw } from 'vue'
import { onBeforeMount } from '@vue/runtime-core';
import { Checked } from '@element-plus/icons-vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import drawerVue from './drawer.vue'
import editorVue from '@/components/common/editor'
import store from '@/store';
import { addTravel } from '@/api/travel'
import { addSightsTravel } from '@/api/sights/index';
import { addHotTop } from '@/api/common/index'

const drawer = ref(null);
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

const goShowDrawer = () => {
    drawer.value.showDrawer = true
}

const goOverview = () => {

}

const goPublish = () => {
    console.log(drawer.value.selectId);
    if (drawer.value.selectId === undefined || drawer.value.selectId === '') {
        ElMessageBox.alert('您还没有分配景点，请分配景点后重试', 'Tips', {
            confirmButtonText: 'OK',
        });
        return
    }
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
                var data = {
                    sightsId: drawer.value.selectId,
                    travelsId: res.data.id
                }
                var hotData = {
                    linkId: res.data.id,
                    topType: 2,
                    weight: 1
                }
                addSightsTravel(data).then(res => {
                    if (res.status === 200) {
                        addHotTop(hotData).then(hot => {
                            if (hot.status === 200)
                                ElMessage({
                                    type: 'success',
                                    message: '发布成功'
                                })
                        })
                    }
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
    width: 150px;
    height: 35px;
    font-size: 14px;
    font-weight: bold;
    margin-left: 16%;
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

.assign {
    background-color: #66CCCC;
    border: 1px solid #66CCCC;
}

.assign:hover {

    transition: all 0.5s;
    color: white;
    background-color: #67c23a;
    border: 1px solid #67c23a;
}

.assign:focus {
    outline: 0;
    transition: all 0.5s;
    color: white;
    background-color: #67c23a;
    border: 1px solid #67c23a;
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