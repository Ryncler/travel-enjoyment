<template>
    <el-menu default-active="/" class="elmenu" mode="horizontal" :ellipsis="false" router active-text-color="#66CCCC">
        <el-menu-item v-for="item in store.getters['menu/menuInfo']" :key="item.name" :index="item.path"
            class="itemMenu" style="border-bottom: 0px;">
            {{ item.title }}
        </el-menu-item>
        <el-menu-item index="/search" class="itemMenu searchItem">
            <icon data="@/icons/search.svg" class="svg-container search" />
        </el-menu-item>
        <div class="btnd">
            <el-button round type="primary" class=" btn">
                <icon data="@/icons/register.svg" class="svg-container icon" />
                注册
            </el-button>
            <el-button round type="primary" class="rigthbtn btn">
                <icon data="@/icons/login.svg" class="svg-container icon" />
                登录
            </el-button>
        </div>
    </el-menu>
    <!-- <div class="btnd" v-if="isLogin()">
        <el-dropdown :hide-on-click="false" class="dropMenu">
            <span class="el-dropdown-link">
                <el-avatar :size="100" :src="logoUrl" />
                <el-icon class="el-icon--right">
                    <arrow-down />
                </el-icon>
            </span>
            <template #dropdown>
                <el-dropdown-menu>
                    <el-dropdown-item>
                        <icon data="@/icons/edit-user.svg" class="svg-container icon" />
                        <p>编辑个人信息</p>
                    </el-dropdown-item>
                    <el-dropdown-item>
                        <icon data="@/icons/edit-user.svg" class="svg-container icon" />
                        <p>我的游记</p>
                    </el-dropdown-item>
                    <el-dropdown-item>
                        <icon data="@/icons/edit-user.svg" class="svg-container icon" />
                        <p>我的收藏</p>
                    </el-dropdown-item>
                    <el-dropdown-item>
                        <icon data="@/icons/edit-user.svg" class="svg-container icon" />
                        <p>账户设置</p>
                    </el-dropdown-item>
                    <el-dropdown-item divided>
                        <icon data="@/icons/edit-user.svg" class="svg-container icon" />
                        <p>退出</p>
                    </el-dropdown-item>
                </el-dropdown-menu>
            </template>
        </el-dropdown>
    </div> -->

</template>

<script setup>
import { ref } from 'vue';
import { onBeforeMount } from '@vue/runtime-core';
import store from '@/store'
import { ArrowDown } from '@element-plus/icons-vue'
import { isLogin } from '@/utils/common'

</script>

<style scoped>
.elmenu {
    width: 100%;
    height: 100px;
    box-shadow: 0 1px 2px 0;
    border-radius: 5px;
    Border-bottom: 0
}

.itemMenu {
    font-size: 28px;
    width: 300px;
    height: 100px;
}

.search {
    width: 100px;
    height: 45px;
}

.searchItem {
    margin-left: 10%;
    width: 200px;
}

.btnd{
    margin-top: 30px;
}

.btn {
    width: 100px;
    height: 40px;
    font-size: 15px;
    font-weight: bold;
    background-color: #66CCFF;
    border: 1px solid #66CCFF;
}

.btn:hover {
    transition: all 0.5s;
    background-color: #66CCCC;
    border: 1px solid #66CCCC;
}

.btn:focus {
    transition: all 0.5s;
    background-color: #66CCCC;
    border: 1px solid #66CCCC;
}

.rigthbtn {
    margin-left: 30px;
}

.icon {
    color: white;
    width: 20px;
    height: 20px;
    margin-right: 5px;
}
</style>