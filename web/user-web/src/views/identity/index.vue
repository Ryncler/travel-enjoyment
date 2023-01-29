<template>
    <el-row :gutter="0" class="elrow">
        <el-col :span="12">
            <el-image :src="loginImage.url" class="loginImg" />
        </el-col>
        <el-col :span="12">
            <login v-if="!store.getters['identity/isShow']"></login>
            <register v-show="store.getters['identity/isShow']"></register>
        </el-col>
    </el-row>
</template>

<script setup>
import { ref } from 'vue';
import store from '@/store';
import login from '@/components/identity/login.vue';
import register from '@/components/identity/register.vue';

const loginImage = ref({
    url: 'https://img-prod-cms-rt-microsoft-com.akamaized.net/cms/api/am/imageFileData/RE4wHgx?ver=5481'
})

// eslint-disable-next-line no-undef
defineExpose({
})
</script>

<style>
.elrow {
    height: 100%;
    overflow: hidden;
}

.loginImg {
    width: 100%;
    height: 100%;
}
</style>