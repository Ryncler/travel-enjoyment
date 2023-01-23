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
import { getInfo } from '@/api/identity/identity';
import { getUser } from '@/api/user/user';

const router = useRouter()

const getUserInfo = () => {
    getInfo().then(res => {
        if (res.status === 200) {
            getUser(res.data.sub).then(item => {
                if (item.status === 200) {
                    store.commit('identity/setUserInfo', item.data)
                }
            })
        }
    })
}

onBeforeMount(() => {
    if (!store.getters['identity/isRefresh']) {
        store.commit('identity/setRefresh')
        router.go(0)
    }
    getUserInfo()
})

onMounted(() => {
})

// eslint-disable-next-line no-undef
defineExpose({
    onBeforeMount, getUserInfo
})
</script>

<style scoped>
.el-header {
    padding: 0px;
}
</style>