<template>
    <div style=" flex-direction: column;height: 100%;">
        <el-container>
            <el-header class="header">
                <headerVue></headerVue>
            </el-header>
            <el-main class="common-layout">
                <mainVue></mainVue>
            </el-main>
            <el-footer class="common-layout" height="230px">
                <footerVue></footerVue>
            </el-footer>
        </el-container>
    </div>
</template>

<script setup>
import { ref } from 'vue';
import { onBeforeMount } from '@vue/runtime-core';
import footerVue from '@/components/footer.vue';
import headerVue from '@/components/layout/header.vue';
import mainVue from '@/components/layout/main.vue'

</script>

<style scoped>
.el-header {
    padding: 0px;
}

.common-layout {
    align-items: center;
    margin: 0px 300px 0px 300px;
    display: flex;
    flex-wrap: wrap;
    margin-top: 10px;
}

.header {
    flex: 0 0 auto;
    display: flex;
    width: 100%;
    height: 100px;
}

</style>

<style></style>