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
import { ElMessage } from 'element-plus'
import { onBeforeMount, onMounted } from '@vue/runtime-core';
import store from '@/store';
import { useRouter } from 'vue-router'

const router = useRouter()

const isAdminOrManage = () => {
    const role = store.getters['identity/roles']
    if (role === undefined || role === null || role === [] || role.length <= 0) {
        return false
    }
    if (role.indexOf('user') !== -1)
        return false
    return true
}

onBeforeMount(() => {
    if (!isAdminOrManage()) {
        ElMessage.error('该账号无权限访问!')
        router.push({ name: 'Login' })
        return
    }
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