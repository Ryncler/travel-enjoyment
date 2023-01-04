<template>
    <el-row>
        <el-col :span="18" class="queryCol" :offset="6">
            <el-form :inline="true" :model="queryFrom" class="demo-form-inline">
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
    <el-row>
        <el-col :span="2">
            <h4>用户管理</h4>
        </el-col>
        <el-col :span="8" class="useroptions" :offset="14" >
            <el-button round type="primary" class="revertbtn" @click="goQuery()">新增用户</el-button>
            <el-tooltip class="box-item" effect="dark" content="刷新" placement="top">
                <icon data="@/icons/password.svg" class="svg-container" />
            </el-tooltip>
        </el-col>
    </el-row>
    <el-row>

        <el-col :span="24">
            <el-table :data="tableData" height="600" style="width: 100%">
                <template #empty>
                    <el-empty :image-size="200" />
                </template>
                <el-table-column prop="date" label="Date" width="180" />
                <el-table-column prop="name" label="Name" width="180" />
                <el-table-column prop="address" label="Address" />
            </el-table>
        </el-col>
    </el-row>
</template>

<script>
export default {
    name: 'userManage',
    data() {
        return {
            loading: false,
            queryFrom: {
                name: '',
                email: '',
                status: null,
                isDelete: false
            },
            options: [
                {
                    label: '已开启',
                    value: true,
                },
                {
                    label: '已关闭',
                    value: false,
                }
            ]

        }
    },
    methods: {
        goQuery() {

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

.useroptions{
    margin: 15px 0 0 0;
}

.tooltip-base-box .box-item {
  width: 110px;
  margin-top: 10px;
}
</style>