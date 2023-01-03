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
                <el-main><el-empty :image-size="200" /></el-main>
            </el-container>
        </el-container>
    </div>
</template>

<script>
import headerVue from '@/components/layout/header.vue';
import asideVue from '@/components/layout/aside.vue';

export default {
    // eslint-disable-next-line vue/multi-word-component-names
    name: 'Layout',
    components: {
        headerVue,
        asideVue
    },
}
</script>

<style scoped>
.el-header {
    padding: 0px;
}
</style>