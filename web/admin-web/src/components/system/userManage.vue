<template>
    <el-row>
        <el-col :span="24" class="queryCol">
            <el-form :inline="true" :model="queryFrom" class="demo-form-inline">
                <el-form-item label="用户名">
                    <el-input v-model="queryFrom.name" placeholder="请输入用户名" />
                </el-form-item>
                <el-form-item label="邮箱">
                    <el-input v-model="queryFrom.email" placeholder="请输入邮箱" />
                </el-form-item>
                <el-form-item>
                    <el-button :loading="loading" round type="primary" class="revertbtn"
                        @click="goQuery()">登录</el-button>
                </el-form-item>
            </el-form>
        </el-col>
    </el-row>
    <el-row>
        <el-col :span="24">
            <div class="grid-content ep-bg-purple" />
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
            },

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

.queryCol {
    float: right;
}

.grid-content {
    border-radius: 4px;
    min-height: 36px;
    color: red;
    background-color: red;
}
</style>