<template>
    <el-container id="contain">
        <el-header class="header">
            <headerVue></headerVue>
        </el-header>
        <el-main class="common-layout">
            <mainVue></mainVue>
        </el-main>
        <el-footer id="footer" class="common-layout" height="230px">
            <footerVue></footerVue>
        </el-footer>
    </el-container>
</template>

<script setup>
import { ref } from 'vue';
import { onBeforeMount, onMounted } from '@vue/runtime-core';
import elementResizeDetectorMaker from 'element-resize-detector';
import footerVue from '@/components/footer.vue';
import headerVue from '@/components/layout/header.vue';
import mainVue from '@/components/layout/main.vue'

const setBottom = () => {
    var visibleHight = document.documentElement.clientHeight
    var containHight = document.getElementById('contain')
    var footer = document.getElementById('footer')
    if (containHight.offsetHeight < visibleHight) {
        footer.style.marginTop = "482px";
    }
}
const listen = () => {
    const detector = elementResizeDetectorMaker()
    detector.listenTo(document.getElementById('contain'), (item) => {
        var footer = document.getElementById('footer')
        if (footer.offsetTop > 2000) {
            footer.style.marginTop = "80px";
        }
        if (footer.offsetTop < 400) {
            footer.style.marginTop = "482px";
        }
    })
}
onMounted(() => {
    listen(), setBottom()
})
</script>

<style scoped>
.el-header {
    padding: 0px;
}

.common-layout {
    align-items: center;
    margin: 0px 300px 0px 300px;
    display: flex;
    flex-wrap: wrap;
    margin-top: 10px;
}

.header {
    width: 100%;
    height: 100px;
}
</style>

<style></style>