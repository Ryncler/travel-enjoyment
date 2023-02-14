<template>
    <section class="main">
        <transition name="fade-transform" mode="out-in">
            <router-view :key="key" />
        </transition>
    </section>
</template>


<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter
const keys = ref(router.path)

</script>

<style>
.main{
    width: 100%;
}
</style>