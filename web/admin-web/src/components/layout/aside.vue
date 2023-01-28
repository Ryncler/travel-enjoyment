<template>
    <el-menu :default-active="router.currentRoute.value.path" class="el-menu-vertical-demo" @open="handleOpen"
        @close="handleClose" @select="selectItem" router>
        <el-menu-item index="/dashboard">
            <el-icon>
                <icon data="@/icons/dashboard.svg" class="svg-container" />
            </el-icon>
            <span>Dashboard</span>
        </el-menu-item>

        <el-sub-menu v-for="item in store.getters['breadcrumb/menuInfo']" :key="item.name" :index=item.path>
            <template #title>
                <el-icon>
                    <icon :data="item.icon" class="svg-container" />
                </el-icon>
                <span>{{ item.title }}</span>
            </template>
            <el-menu-item-group>
                <el-menu-item v-for="child in item.children" :key="child.name" :index=child.path>
                    <el-icon>
                        <icon :data="child.icon" class="svg-container" />
                    </el-icon>
                    <span>{{ child.title }}</span>
                </el-menu-item>
            </el-menu-item-group>
        </el-sub-menu>
    </el-menu>
</template>

<script setup>
import store from '@/store'
import router from '@/router/index'

</script>