<template>
    <div>
        <el-image style="width: 100%; height: 720px" :src="headerUrl" :fit="scale - down" />
    </div>

    <div class="contain">
        <headerVue></headerVue>
        <tagVue></tagVue>
        <sightsVue></sightsVue>
        <activityVue></activityVue>
        <travelVue></travelVue>
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

import { getHotTopByType } from '@/api/common';

const headerUrl = ref('https://www.otsuka.co.jp/img/index_im01_01.jpg.webp')

const getHotTop = () => {
    getHotTopByType(1).then(res => {
        if (res.status == 200) {
            store.commit('common/sightsTop', res.data)
        }
    })
}

onBeforeMount(() => {
    getHotTop()
})
</script>

<style>
.contain {
    align-items: center;
    margin: 0px 300px 0px 300px;
    display: flex;
    flex-wrap: wrap;
    margin-top: 10px;
}
</style>