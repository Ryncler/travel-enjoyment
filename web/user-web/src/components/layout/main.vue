<template>
    <section class="main">
        <transition name="fade-transform" mode="out-in">
            <!-- <infoVue></infoVue> -->
            <router-view :key="keys" />
        </transition>
    </section>
</template>


<script setup>
import { ref } from 'vue';
import router from '@/router/index'
import infoVue from '@/components/sights/info.vue';

const keys = ref("SightsInfo")

</script>

<style>

</style>