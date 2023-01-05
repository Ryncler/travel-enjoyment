<template>
    <el-row>
        <el-col :span="16" class="queryCol" :offset="8">
            <el-form :inline="true" :model="queryForm">
                <el-form-item label="用户名">
                    <el-input v-model="queryForm.name" placeholder="请输入用户名" />
                </el-form-item>
                <el-form-item label="邮箱">
                    <el-input v-model="queryForm.email" placeholder="请输入邮箱" />
                </el-form-item>
                <el-form-item label="用户状态">
                    <el-select v-model="queryForm.status" clearable placeholder="请选择用户状态">
                        <el-option v-for="item in options" :key="item.value" :label="item.label" :value="item.value" />
                    </el-select>
                </el-form-item>
                <el-form-item label="是否显示已删除用户">
                    <el-switch v-model="queryForm.isDelete" />
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
            <el-table :data="filter()" :loading="loading" height="600" style="width: 100%" size="large">
                <template #empty>
                    <el-empty :image-size="100" />
                </template>
                <el-table-column prop="userName" label="用户名" width="200" />
                <el-table-column prop="sex" label="性别" width="100" />
                <el-table-column prop="email" label="邮箱" width="150" />
                <el-table-column prop="phone" label="电话" width="150" />
                <el-table-column label="头像" width="200">
                    <template #default="scope">
                        <el-image style="width: 100px; height: 100px" :src="scope.row.avatarUrl" :fit="fit">
                            <template #error>
                                <div class="image-slot">
                                    <icon data="@/icons/image.svg" />
                                </div>
                            </template>
                        </el-image>
                    </template>
                </el-table-column>
                <el-table-column label="角色" width="150">
                    <template #default="scope">
                        <el-checkbox-group v-model="scope.row.roles" :min="1" :max="2" disabled>
                            <el-checkbox v-for="item in getRoles()" :key="item" :label="item">{{
                                item
                            }}</el-checkbox>
                        </el-checkbox-group>
                    </template>
                </el-table-column>
                <el-table-column label="当前状态" width="150">
                    <template #default="scope">
                        <el-switch v-model="scope.row.active" disabled />
                    </template>
                </el-table-column>
                <el-table-column label="是否已删除" width="150">
                    <template #default="scope">
                        <el-switch v-model="scope.row.delete" disabled />
                    </template>
                </el-table-column>
                <el-table-column prop="createTime" label="创建时间" width="150" sortable />
                <el-table-column fixed="right" label="操作" width="250">
                    <template #default="scope">
                        <el-button size="small" @click="goEditUser(scope.$index, scope.row)">编辑</el-button>
                        <el-button size="small" @click="goEditUser(scope.$index, scope.row)">编辑权限</el-button>
                        <el-button size="small" type="danger"
                            @click="goDeleteUser(scope.$index, scope.row)">删除</el-button>
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
            drawer.value.title = '添加'
            drawer.value.btnName = '添加'
            drawer.value.showDrawer = true
        }
        function close() {
            drawer.value.showDrawer = true
        }
        return { goAddUser, close, drawer }
    },
    data() {
        return {
            loading: false,
            showAnimation: true,
            queryForm: {
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
                    roles: ['admin', 'user'],
                    avatarUrl: '',
                    active: true,
                    delete: true,
                    createTime: '2022.1.1'
                },
                {
                    userName: 'bbbbbbbbbbbbb',
                    sex: '男',
                    email: 'bbbbbb@qq.com',
                    phone: '144544343',
                    roles: ['user'],
                    avatarUrl: 'https://cube.elemecdn.com/0/88/03b0d39583f48206768a7534e55bcpng.png',
                    active: false,
                    delete: false,
                    createTime: '2022.1.1'
                },
                {
                    userName: 'dgvrtgbvdsgsre',
                    sex: '男',
                    email: 'ggggg@qq.com',
                    phone: '144544343',
                    roles: ['user'],
                    avatarUrl: '',
                    active: true,
                    delete: false,
                    createTime: '2022.1.1'
                }, {
                    userName: '2354356',
                    sex: '男',
                    email: '6678@qq.com',
                    phone: '144544343',
                    roles: ['user'],
                    avatarUrl: 'https://cube.elemecdn.com/0/88/03b0d39583f48206768a7534e55bcpng.png',
                    active: false,
                    delete: false,
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
        onAfterLeave() {
            this.showAnimation = true
        },
        goEditUser(index, row) {
            this.drawer.title = '编辑'
            this.drawer.btnName = '编辑'
            row.sex = row.sex === '男' ? 1 : 2;
            row.password = process.env.VUE_APP_Password
            this.drawer.userForm = row
            this.drawer.showDrawer = true
        },
        getRoles(){
            return ['admin','user']
        },
        filter() {
            var data = this.userData
            if (this.queryForm.name !== '') {
                data = data.filter(x => !this.queryForm.name || x.userName.includes(this.queryForm.name))
            }
            if (this.queryForm.email !== '') {
                data = data.filter(x => !this.queryForm.email || x.email.includes(this.queryForm.email))
            }
            if (this.queryForm.status !== null) {
                if (this.queryForm.status.length !== 0) {
                    data = data.filter(x => x.active === this.queryForm.status)
                }
            }
            if (!this.queryForm.isDelete) {
                data = data.filter(x => x.delete === this.queryForm.isDelete)
            }
            return data
        }
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