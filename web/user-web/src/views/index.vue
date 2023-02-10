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
        <topSightsVue></topSightsVue>
    </div>
    <div class="contain">
        <activityVue></activityVue>
    </div>
</template>

<script setup>
import { ref } from 'vue';
import store from '@/store'
import { onBeforeMount } from '@vue/runtime-core';
import headerVue from '@/components/home/header'
import tagVue from '@/components/home/tag'
import topSightsVue from '@/components/home/topSights'
import activityVue from '@/components/home/activity'

const headerUrl = ref('https://www.otsuka.co.jp/img/index_im01_01.jpg.webp')
</script>

<style>
.contain {
    align-items: center;
    margin: 0px 300px 0px 300px;
    display: flex;
}
</style>