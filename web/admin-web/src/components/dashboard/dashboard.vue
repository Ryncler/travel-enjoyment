<template>
    <el-row :gutter="30">
        <el-col :span="5" :offset="2">
            <el-card shadow="hover">
                <div>
                    <div class="info">
                        <h1 class="newAddNumber">{{ 200}}</h1>
                        <h5>新增用户</h5>
                    </div>
                    <div class="icon">
                        <icon data="@/icons/add-user.svg" class="svg-container" />
                    </div>
                </div>
            </el-card>
        </el-col>
        <el-col :span="5">
            <el-card shadow="hover">
                <div>
                    <div class="info">
                        <h1 class="newAddNumber">{{ 200}}</h1>
                        <h5>新增入驻</h5>
                    </div>
                    <div class="icon">
                        <icon data="@/icons/entry.svg" class="svg-container" />
                    </div>
                </div>
            </el-card>
        </el-col>
        <el-col :span="5">
            <el-card shadow="hover">
                <div>
                    <div class="info">
                        <h1 class="newAddNumber">{{ 200}}</h1>
                        <h5>新增景点</h5>
                    </div>
                    <div class="icon">
                        <icon data="@/icons/sights.svg" class="svg-container" />
                    </div>
                </div>
            </el-card>
        </el-col>
        <el-col :span="5">
            <el-card shadow="hover">
                <div>
                    <div class="info">
                        <h1 class="newAddNumber">{{ 200 }}</h1>
                        <h5>新增游记</h5>
                    </div>
                    <div class="icon">
                        <icon data="@/icons/travel-enjoyment.svg" class="svg-container" />
                    </div>
                </div>
            </el-card>
        </el-col>
    </el-row>
    <el-divider />
    <el-row :gutter="20">
        <el-col :span="16">

        </el-col>
        <el-col :span="8">
            <active></active>
        </el-col>
    </el-row>

</template>

<script setup>
import active from './active.vue';
</script>

<style scoped>
.svg-container {
    height: 80px;
    width: 80px;
}
</style>

<style>
.info {
    margin: 0 auto;
    float: left;
    width: 50%;
}

.icon {
    margin: 0 auto;
    float: right;
    width: 40%;
}

.newAddNumber {
    font-size: 30px;
    margin: 0 auto;
}
</style>