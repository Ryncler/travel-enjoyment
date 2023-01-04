<template>
    <el-row>
        <el-col :span="19" class="queryCol" :offset="5">
            <el-form :inline="true" :model="queryFrom">
                <el-form-item label="用户名">
                    <el-input v-model="queryFrom.name" placeholder="请输入用户名" />
                </el-form-item>
                <el-form-item label="邮箱">
                    <el-input v-model="queryFrom.email" placeholder="请输入邮箱" />
                </el-form-item>
                <el-form-item label="用户状态">
                    <el-select v-model="queryFrom.status" clearable placeholder="请选择用户状态">
                        <el-option v-for="item in options" :key="item.value" :label="item.label" :value="item.value" />
                    </el-select>
                </el-form-item>
                <el-form-item label="是否显示已删除用户">
                    <el-switch v-model="queryFrom.isDelete" />
                </el-form-item>
                <el-form-item>
                    <el-button :loading="loading" round type="primary" class="revertbtn frombtn"
                        @click="goQuery()">搜索</el-button>
                    <el-button :loading="loading" round type="primary" class="revertbtn frombtn"
                        @click="goQuery()">重置</el-button>
                </el-form-item>
            </el-form>
        </el-col>
    </el-row>
    <el-divider />
    <el-row>
        <el-col :span="2" :offset="1">
            <h3>用户管理</h3>
        </el-col>
        <el-col :span="3" class="useroptions" :offset="18">
            <el-button round type="primary" class="revertbtn" @click="goAddUser()">新增用户</el-button>
            <el-tooltip class="box-item" effect="dark" content="刷新" placement="top">
                <transition name="refresh" @leave="onAfterLeave">
                    <icon v-if="showAnimation" @click="showAnimation = !showAnimation" data="@/icons/refresh.svg"
                        class="svg-container" style="width:60px;height:30px" />
                </transition>
            </el-tooltip>
        </el-col>
    </el-row>
    <el-row>
        <el-col :span="24">
            <el-table :data="userData" :loading="loading" height="550" style="width: 100%" size="large">
                <template #empty>
                    <el-empty :image-size="100" />
                </template>
                <el-table-column prop="userName" label="用户名" width="200" />
                <el-table-column prop="sex" label="性别" width="150" />
                <el-table-column prop="email" label="邮箱" width="200" />
                <el-table-column prop="phone" label="电话" width="200" />
                <el-table-column label="头像" width="200">
                    <template #default="scope">
                        <el-image style="width: 100px; height: 100px" :src="scope.row.avatar" :fit="fit">
                            <template #error>
                                <div class="image-slot">
                                    <icon data="@/icons/image.svg" />
                                </div>
                            </template></el-image>
                    </template>
                </el-table-column>
                <el-table-column label="当前状态" width="150">
                    <template #default="scope">
                        <el-switch v-model="scope.row.active" disabled />
                    </template>
                </el-table-column>
                <el-table-column label="是否已删除" width="200">
                    <template #default="scope">
                        <el-switch v-model="scope.row.delete" disabled />
                    </template>
                </el-table-column>
                <el-table-column prop="createTime" label="创建时间" width="200" />

                <el-table-column fixed="right" label="操作" width="150">
                    <template #default>
                        <el-button link type="primary" size="default">编辑</el-button>
                        <el-button link type="primary" size="default">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </el-col>
        <el-col :span="10" :offset="13">
            <el-pagination v-model:current-page="currentPage4" v-model:page-size="pageSize4"
                :page-sizes="[10, 50, 100, 500]" :small="small" :disabled="disabled" :background="background"
                layout="total, sizes, prev, pager, next, jumper" :total="500" @size-change="handleSizeChange"
                @current-change="handleCurrentChange" />
        </el-col>
    </el-row>

    <drawerVue ref="drawer" />
</template>

<script>
import { ref } from '@vue/reactivity'
import drawerVue from './drawer.vue'

export default {
    name: 'userManage',
    components: {
        drawerVue,
    },
    setup() {
        const drawer = ref(null);
        function goAddUser() {
            drawer.value.showDrawer = true
        }
        function close() {
            drawer.value.showDrawer = false
        }
        return { goAddUser, close, drawer }
    },
    data() {
        return {

            loading: false,
            showAnimation: true,
            queryFrom: {
                name: '',
                email: '',
                status: null,
                isDelete: false
            },
            userData: [
                {
                    userName: 'axb',
                    sex: '男',
                    email: '123@qq.com',
                    phone: '144544343',
                    active: true,
                    delete: true,
                    createTime: '2022.1.1'
                }
            ],
            options: [
                {
                    label: '已激活',
                    value: true,
                },
                {
                    label: '已关闭',
                    value: false,
                }
            ],


        }
    },
    methods: {
        goQuery() {

        },
        onAfterLeave() {
            this.showAnimation = true
        },

    }
}

</script>

<style lang="scss">
.el-row {
    margin-bottom: 20px;
}

.el-row:last-child {
    margin-bottom: 0;
}

.el-col {
    border-radius: 4px;
}

.frombtn {
    margin: 0 10px 0 0;
}

.useroptions {
    padding: 15px 0 0 0;
}

.tooltip-base-box .box-item {
    width: 110px;
    margin-top: 10px;
}

.svg-container {
    outline: 2px solid #0000;
    outline-offset: 2px;
}

@keyframes rotate360 {
    100% {
        transform: rotate(360deg);
    }
}

.refresh-enter-active,
.refresh-leave-active {
    animation: rotate360 0.5s;
}

.refresh-enter-from,
.refresh-leave-to {
    animation: rotate360 0.5s reverse;
}

th>.cell {
    font-size: 15px;
    font-weight: bold;
    color: black;
}

.image-slot {
    display: flex;
    justify-content: center;
    align-items: center;
    width: 100%;
    height: 100%;
    background: var(--el-fill-color-light);
    color: var(--el-text-color-secondary);
    font-size: 30px;
}

.closeIcon {
    color: white;
}

.el-form-item__content {
    flex-wrap: nowrap;
}
</style>