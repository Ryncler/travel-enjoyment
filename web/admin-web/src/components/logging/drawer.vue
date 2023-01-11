<template>
    <el-drawer v-model="showDrawer" :show-close="false" size="50%">
        <template #header="{ close, titleId, titleClass }">
            <h4 :id="titleId" :class="titleClass">日志详情</h4>
            <el-button type="danger" @click="close">
                <icon data="@/icons/close.svg" class="closeIcon" />
                关闭
            </el-button>
        </template>
        <el-descriptions :column="2" border>
            <el-descriptions-item label="用户名称" label-align="left" align="center">{{
                isNull(auditLog.userName)
            }}</el-descriptions-item>
            <el-descriptions-item label="租户名称" label-align="left" align="center">{{
                isNull(auditLog.tenantName)
            }}</el-descriptions-item>
            <el-descriptions-item label="客户端ID" label-align="left" align="center">{{
                isNull(auditLog.clientId)
            }}</el-descriptions-item>
            <el-descriptions-item label="客户端名称" label-align="left" align="center">{{
                isNull(auditLog.clientName)
            }}</el-descriptions-item>
            <el-descriptions-item label="应用名称" label-align="left" align="center">{{
                isNull(auditLog.applicationName)
            }}</el-descriptions-item>
            <el-descriptions-item label="状态码" label-align="left" align="center">{{
                auditLog.httpStatusCode
            }}</el-descriptions-item>
            <el-descriptions-item label="请求方法" label-align="left" align="center">{{
                auditLog.httpMethod
            }}</el-descriptions-item>
            <el-descriptions-item label="请求URl" label-align="left" align="center">{{
                auditLog.url
            }}</el-descriptions-item>
            <el-descriptions-item label="请求IP地址" label-align="left" align="center">{{
                auditLog.clientIpAddress
            }}</el-descriptions-item>
            <el-descriptions-item label="响应时间" label-align="left" align="center">{{
                auditLog.executionDuration
            }}s</el-descriptions-item>
            <el-descriptions-item label="请求时间" label-align="left" align="center">{{
                auditLog.executionTime
            }}</el-descriptions-item>
            <el-descriptions-item label="相关性ID" label-align="left" align="center">{{
                auditLog.correlationId
            }}</el-descriptions-item>
            <el-descriptions-item label="浏览器信息" label-align="left" align="center">{{
                auditLog.browserInfo
            }}</el-descriptions-item>
        </el-descriptions>
        <el-collapse accordion>
            <el-collapse-item name="1" v-if="auditLog.entityChanges && auditLog.entityChanges.length > 0">
                <template #title>
                    <h5>实体更改列表</h5>
                </template>
                <el-descriptions :column="2" border>
                    <el-descriptions-item label="实体ID" label-align="left" align="center">{{
                        auditLog.entityChanges.entityId
                    }}</el-descriptions-item>
                    <el-descriptions-item label="实体类型全名" label-align="left" align="center">{{
                        auditLog.entityChanges.entityTypeFullName
                    }}</el-descriptions-item>
                    <el-descriptions-item label="更改类型" label-align="left" align="center">{{
                        auditLog.entityChanges.changeType
                    }}</el-descriptions-item>
                    <el-descriptions-item label="更改时间" label-align="left" align="center">{{
                        auditLog.entityChanges.changeTime
                    }}</el-descriptions-item>
                    <el-descriptions-item label="扩展列表" label-align="left" align="center">{{
                        isNull(auditLog.entityChanges.extraProperties)
                    }}</el-descriptions-item>
                </el-descriptions>
            </el-collapse-item>
            <el-collapse-item name="2" v-if="auditLog.actions && auditLog.actions.length > 0">
                <template #title>
                    <h5>操作列表</h5>
                </template>
                <el-descriptions :column="2" border v-for="item in auditLog.actions" :key="item">
                    <el-descriptions-item label="服务名称" label-align="left" align="center">{{
                        item.serviceName
                    }}</el-descriptions-item>
                    <el-descriptions-item label="方法名称" label-align="left" align="center">{{
                        item.methodName
                    }}</el-descriptions-item>
                    <el-descriptions-item label="响应时间" label-align="left" align="center">{{
                        item.executionDuration
                    }}s</el-descriptions-item>
                    <el-descriptions-item label="请求时间" label-align="left" align="center">{{
                        item.executionTime
                    }}</el-descriptions-item>
                    <el-descriptions-item label="扩展列表" label-align="left" align="center">{{
                        isNull(item.extraProperties)
                    }}</el-descriptions-item>
                    <el-descriptions-item></el-descriptions-item>
                    <el-descriptions-item label="参数列表" label-align="left" align="center">{{
                        isNull(item.parameters)
                    }}</el-descriptions-item>
                    <el-divider />
                </el-descriptions>
            </el-collapse-item>
        </el-collapse>
    </el-drawer>
</template>

<script setup>
import { ref } from '@vue/reactivity'
import {isNull} from '@/utils/common/index'
import { getAuditLog } from '@/api/log/auditLog'

const auditLog = ref({})
const showDrawer = ref(false);

const close = () => {
    showDrawer.value = false
}

const getDetails = (id) => {
    return getAuditLog(id).then(res => {
        if (res.status === 200) {
            auditLog.value = res.data
            auditLog.value.executionTime = new Date(auditLog.value.executionTime).format('Y.m.d H:i:s')
            auditLog.value.entityChanges = auditLog.value.entityChanges.map((item) => {
                item.changeTime = new Date(item.changeTime).format('Y.m.d H:i:s')
                return item
            })
            auditLog.value.actions = auditLog.value.actions.map((item) => {
                item.executionTime = new Date(item.executionTime).format('Y.m.d H:i:s')
                item.parameters = JSON.parse(item.parameters)
                console.log(item.parameters);
                return item
            })
        }
    })
}

// eslint-disable-next-line no-undef
defineExpose({
    showDrawer, getDetails, close
});
</script>