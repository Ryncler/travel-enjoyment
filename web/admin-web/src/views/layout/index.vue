<template>
    <div class="common-layout">
        <el-container>
            <el-header>
                <headerVue></headerVue>
            </el-header>
            <el-container>
                <el-aside width="200px">
                    <asideVue></asideVue>
                </el-aside>
                <el-main>
                    <mainVue></mainVue>
                </el-main>
            </el-container>
        </el-container>
    </div>
</template>

<script setup>
import mainVue from '@/components/layout/main.vue'
import headerVue from '@/components/layout/header.vue';
import asideVue from '@/components/layout/aside.vue';
import { onBeforeMount, onMounted } from '@vue/runtime-core';
import store from '@/store';
import { useRouter } from 'vue-router'

const router = useRouter()

onBeforeMount(() => {
    if (!store.getters['identity/isRefresh']) {
        store.commit('identity/setRefresh')
        router.go(0)
    }
})

onMounted(() => {
})

// eslint-disable-next-line no-undef
defineExpose({
    onBeforeMount
})
</script>

<style scoped>
.el-header {
    padding: 0px;
}
</style>