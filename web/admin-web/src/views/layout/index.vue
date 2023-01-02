<template>
    <div class="common-layout">
        <el-container>
            <el-header>
                <headerVue></headerVue>
            </el-header>
            <el-container>
                <el-aside width="200px">Aside</el-aside>
                <el-main>Main</el-main>
            </el-container>
        </el-container>
    </div>
</template>

<script>
import headerVue from '@/components/layout/header.vue';

export default {
    // eslint-disable-next-line vue/multi-word-component-names
    name: 'Layout',
    components: {
        headerVue
    },
}
</script>

<style scoped>
.el-header {
    padding: 0px;
}
</style>