<template>
    <el-row :gutter="10" v-if="isAdmin()">
        <el-col :span="5" :offset="2">
            <el-card shadow="hover" class="newly">
                <div >
                    <div class="info">
                        <h1 class="newAddNumber">{{ newlyData[0] }}</h1>
                        <h5>新增用户</h5>
                    </div>
                    <div class="icon">
                        <icon data="@/icons/add-user.svg" class="svg-container" />
                    </div>
                </div>
            </el-card>
        </el-col>
        <el-col :span="5">
            <el-card shadow="hover" class="newly">
                <div>
                    <div class="info">
                        <h1 class="newAddNumber">{{ newlyData[1] }}</h1>
                        <h5>新增入驻</h5>
                    </div>
                    <div class="icon">
                        <icon data="@/icons/entry.svg" class="svg-container" />
                    </div>
                </div>
            </el-card>
        </el-col>
        <el-col :span="5">
            <el-card shadow="hover" class="newly">
                <div>
                    <div class="info">
                        <h1 class="newAddNumber">{{ newlyData[2] }}</h1>
                        <h5>新增景点</h5>
                    </div>
                    <div class="icon">
                        <icon data="@/icons/sights.svg" class="svg-container" />
                    </div>
                </div>
            </el-card>
        </el-col>
        <el-col :span="5">
            <el-card shadow="hover" class="newly">
                <div>
                    <div class="info">
                        <h1 class="newAddNumber">{{ newlyData[3] }}</h1>
                        <h5>新增游记</h5>
                    </div>
                    <div class="icon">
                        <icon data="@/icons/travel-enjoyment.svg" class="svg-container" />
                    </div>
                </div>
            </el-card>
        </el-col>
        <el-divider />
    </el-row>
    <el-row :gutter="20">
        <el-col :span="16">
            <charts></charts>
        </el-col>
        <el-col :span="8">
            <active></active>
        </el-col>
    </el-row>
</template>

<script setup>
import active from './active.vue';
import charts from './charts.vue';
import { isAdmin } from '@/utils/common';
import { ref } from 'vue';
import { onBeforeMount } from '@vue/runtime-core';
import { getNewlyInfo } from '@/api/common/index'

const newlyData = ref([0,0,0,0])
const getNewly = () => {
    return getNewlyInfo().then(res => {
        if (res.status === 200) {
            newlyData.value = res.data
        }
    })
}

onBeforeMount(() => {
    getNewly()
})
</script>

<style scoped>
.svg-container {
    height: 50px;
    width: 50px;
}
.newly{
    height: 80px;
}
h5{
    margin: 0;
}
</style>

<style>
.info {
    margin: 0 auto;
    float: left;
    width: 50%;
}

.icon {
    margin: 0 auto;
    float: right;
    width: 25%;
}

.newAddNumber {
    font-size: 25px;
    margin: 0 auto;
}
</style>