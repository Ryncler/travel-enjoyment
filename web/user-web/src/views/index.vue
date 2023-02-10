<template>
    <div>
        <el-image style="width: 100%; height: 720px" :src="headerUrl" :fit="scale - down" />
    </div>

    <div class="contain">
        <headerVue></headerVue>
    </div>
    <div class="contain">
        <tagVue></tagVue>
    </div>
    <div class="contain">
        <sightsVue></sightsVue>
    </div>
    <div class="contain">
        <activityVue></activityVue>
    </div>
    <div class="contain">
        <travelVue></travelVue>
    </div>
    <div class="contain">
        <footerVue></footerVue>
    </div>
</template>

<script setup>
import { ref } from 'vue';
import store from '@/store'
import { onBeforeMount } from '@vue/runtime-core';
import headerVue from '@/components/home/header'
import tagVue from '@/components/home/tag'
import sightsVue from '@/components/home/sights'
import activityVue from '@/components/home/activity'
import travelVue from '@/components/home/travel'
import footerVue from '@/components/footer.vue';

const headerUrl = ref('https://www.otsuka.co.jp/img/index_im01_01.jpg.webp')
</script>

<style>
.contain {
    align-items: center;
    margin: 0px 300px 0px 300px;
    display: flex;
    margin-top: 30px;
}
</style>