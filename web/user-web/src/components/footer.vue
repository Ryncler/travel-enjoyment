<template>
    <div class="footer">
        <el-footer>
            <div class="info">
                <div class="linkInfo">
                    <p class="linkTag">友情链接：</p>
                    <a href="https://gitee">Gitee Ryncler</a>
                    <a href="https://gitee">传统节日介绍</a>
                    <p class="about">关于我们</p>
                    <p class="privacy">隐私政策</p>
                </div>
                <p class="explain">Copyright © Ryncler All Rights Reserved.</p>
            </div>
        </el-footer>
    </div>
</template>

<script setup>
import { ref } from 'vue';
import { onBeforeMount } from '@vue/runtime-core';

</script>

<style scoped>
.footer {
    width: 100%;
    height: 150px;
    margin-top: 80px;
    background-color: #66CCCC;
    border-radius: 20px 20px 0 0;
    text-align: center;
}

.info {
    width: 100%;
    height: 100%;
    margin-top: 50px;
}

.linkInfo {
    display: flex;
    color: white;
}

.linkTag {
    font-weight: bold;
}

.explain {
    color: white;
}

p,
a {
    margin: 15px;
}

a {
    color: white;
}

.privacy,
.about {
    font-weight: bold;
}

.about {
    margin-left: 55%;
}
</style>