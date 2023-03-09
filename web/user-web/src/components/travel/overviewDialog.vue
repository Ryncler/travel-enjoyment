<template>
    <el-dialog v-model="showDialog" title="预览游记" width="60%" align-center lock-scroll>
        <el-row :gutter="0">
            <el-col :span="4">
                <div class="info">
                    <el-avatar class="avatar" :size="150" src="https://empty" @error="errorHandler">
                        <img :src="imageHandle(store.getters['identity/userInfo'].avatar)" />
                    </el-avatar>
                    <h4 class="username">{{ travelData.userName }}</h4>
                    <el-divider />
                    <h5>出行日期：</h5>
                    <h5 class="center">{{ new Date(travelData.departureTime).format('Y.m.d') }}</h5>
                    <h5>出行天数：</h5>
                    <h5 class="center">{{ travelData.travelDayNum }}天</h5>
                    <h5>出行费用：</h5>
                    <h5 class="center">{{ travelData.travelExpenses }}元</h5>
                </div>
            </el-col>
            <el-col class="contenCol" :span="19" :offset="1">
                <div class="otherInfo">
                    <div class="otherItem firstItem">
                        <icon data="@/icons/comment.svg" class="svg-container otherIcon" />
                        <p class="author">0 评论</p>
                    </div>
                    <div class="otherItem">
                        <icon data="@/icons/star.svg" class="svg-container otherIcon" />
                        <p class="author">0 收藏</p>
                    </div>
                </div>
                <el-divider />
                <h2 class="center">{{ travelData.travelsTitle }}</h2>
                <p class="content" v-html="travelData.content"></p>
            </el-col>
        </el-row>

        <template #footer>
            <span class="dialog-footer">
                <el-button @click="showDialog = false">关闭</el-button>
            </span>
        </template>
    </el-dialog>
</template>

<script setup>
import { ref } from 'vue';
import store from '@/store'
import { onBeforeMount } from '@vue/runtime-core';
import { imageHandle } from '@/utils/common/index'

const showDialog = ref(false)
const travelData = ref({})


// eslint-disable-next-line no-undef
defineExpose({
    showDialog, travelData
})
</script>

<style scoped>
.avatar {
    margin-left: 15%;
}

.username {
    margin-top: 20px;
    text-align: center;
    font-weight: bold;
}

.center {
    text-align: center;
}

h5,
h2 {
    font-weight: bold;
}

.contenCol {
    margin-top: 30px;
}

.otherInfo {
    flex: 1;
    margin: 0;
    /* margin-left: 20px; */
    display: flex;
    margin: 10px;
    margin-top: 30px;
    align-content: center;
    justify-content: center;
}

.otherIcon {
    margin-top: 2px;
}

.otherItem {
    display: flex;
    margin-left: 80px;
}

.firstItem {
    margin-left: 65%;
}

.content {
    margin-top: 30px;
}
</style>