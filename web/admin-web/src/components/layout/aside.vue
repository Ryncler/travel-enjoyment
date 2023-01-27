<template>
    <el-menu default-active="2" class="el-menu-vertical-demo" @open="handleOpen" @close="handleClose"
        @select="selectItem">
        <el-menu-item index="0">
            <el-icon>
                <icon data="@/icons/dashboard.svg" class="svg-container" />
            </el-icon>
            <span>Dashboard</span>
        </el-menu-item>
        <div v-if="isAdmin()">
            <el-sub-menu index="1">
                <template #title>
                    <el-icon>
                        <icon data="@/icons/systemsetting.svg" class="svg-container" />
                    </el-icon>
                    <span>系统管理</span>
                </template>
                <el-menu-item-group>
                    <el-menu-item index="1-1">
                        <el-icon>
                            <icon data="@/icons/user.svg" class="svg-container" />
                        </el-icon>
                        <span>用户管理</span>
                    </el-menu-item>
                    <el-menu-item index="1-2">
                        <el-icon>
                            <icon data="@/icons/roles.svg" class="svg-container" />
                        </el-icon>
                        <span>角色管理</span>
                    </el-menu-item>
                    <el-menu-item index="1-3">
                        <el-icon>
                            <icon data="@/icons/entry.svg" class="svg-container" />
                        </el-icon>
                        <span>入驻管理</span>
                    </el-menu-item>
                    <el-menu-item index="1-4">
                        <el-icon>
                            <icon data="@/icons/tag.svg" class="svg-container" />
                        </el-icon>
                        <span>标签管理</span>
                    </el-menu-item>
                </el-menu-item-group>
            </el-sub-menu>

            <el-sub-menu index="2">
                <template #title>
                    <el-icon>
                        <icon data="@/icons/auth.svg" class="svg-container" />
                    </el-icon>
                    <span>授权管理</span>
                </template>
                <el-menu-item-group>
                    <el-menu-item index="2-1">
                        <el-icon>
                            <icon data="@/icons/application.svg" class="svg-container" />
                        </el-icon>
                        <span>应用管理</span></el-menu-item>
                    <el-menu-item index="2-2">
                        <el-icon>
                            <icon data="@/icons/scope.svg" class="svg-container" />
                        </el-icon>
                        <span>服务管理</span>
                    </el-menu-item>
                </el-menu-item-group>
            </el-sub-menu>

            <el-sub-menu index="3">
                <template #title>
                    <el-icon>
                        <icon data="@/icons/log.svg" class="svg-container" />
                    </el-icon>
                    <span>日志管理</span>
                </template>
                <el-menu-item-group>
                    <el-menu-item index="3-1">
                        <el-icon>
                            <icon data="@/icons/auditlog.svg" class="svg-container" />
                        </el-icon>
                        <span>审计日志管理</span>
                    </el-menu-item>
                </el-menu-item-group>
            </el-sub-menu>
        </div>
        <el-sub-menu index="4">
            <template #title>
                <el-icon>
                    <icon data="@/icons/sights.svg" class="svg-container" />
                </el-icon>
                <span>旅游管理</span>
            </template>
            <el-menu-item-group>
                <el-menu-item index="4-1">
                    <el-icon>
                        <icon data="@/icons/sights.svg" class="svg-container" />
                    </el-icon>
                    <span>景点管理</span>
                </el-menu-item>
                <el-menu-item index="4-2">
                    <el-icon>
                        <icon data="@/icons/activity.svg" class="svg-container" />
                    </el-icon>
                    <span>活动管理</span>
                </el-menu-item>
            </el-menu-item-group>
        </el-sub-menu>
    </el-menu>
</template>

<script setup>
import menuManage from '@/utils/menuManage'
import { isAdmin } from '@/utils/common';

const selectItem = (key, keyPath) => {
    menuManage.linkRouterByPath('aside', keyPath)
}
</script>