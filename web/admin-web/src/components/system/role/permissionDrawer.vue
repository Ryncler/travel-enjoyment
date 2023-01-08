<template>
    <el-drawer v-model="showDrawer" :show-close="false">
        <template #header="{ close, titleId, titleClass }">
            <h4 :id="titleId" :class="titleClass">分配权限</h4>
            <el-button type="danger" @click="close">
                <icon data="@/icons/close.svg" class="closeIcon" />
                关闭
            </el-button>
        </template>

        <el-form label-position="top">
            <el-tabs tab-position="left">
                <el-tab-pane v-for="group in permissionData.groups" :key="group.name" :label="group.displayName">
                    <el-form-item :label="group.displayName">
                        <el-tree ref="permissionTree" :data="transformPermissionTree(group.permissions)"
                            :props="treeDefaultProps" show-checkbox node-key="name" default-expand-all
                            :default-checked-keys="beforeCheckedKeyData" @check="getCheckedKey" />
                    </el-form-item>
                </el-tab-pane>
            </el-tabs>
            <el-divider />
            <el-form-item>
                <el-button :loading="loading" round type="primary" class="revertbtn addbtn"
                    @click="goAssignPermission()">分配</el-button>
            </el-form-item>
        </el-form>
    </el-drawer>
</template>

<script setup>
import store from '@/store';
import { ElMessage } from 'element-plus'
import { getPermissions, updateRolePermission } from '@/api/user/permission';

const { ref } = require("@vue/reactivity");

const loading = ref(false)
const showDrawer = ref(false)
const permissionData = ref({})

const treeDefaultProps = ref({
    children: 'children',
    label: 'label'
})
const permissionsQuery = ref({})
const beforeCheckedKeyData = ref([])
const checkedKeyData = ref([])

const getPermissionData = () => {
    return getPermissions(permissionsQuery.value).then(res => {
        if (res.status === 200) {
            permissionData.value = res.data;
        }
    })
}

const getCheckedKey = (node, tree) => {
    checkedKeyData.value = tree.checkedKeys.map((item) => {
        return {
            name: item,
            isGranted: true
        }
    })

    beforeCheckedKeyData.value.forEach(item => {
        if (!checkedKeyData.value.find(x => x.name === item)) {
            checkedKeyData.value.push({
                name: item,
                isGranted: false
            })
        }
    });
}

const goAssignPermission = () => {
    loading.value = true
    return updateRolePermission(permissionsQuery.value, checkedKeyData.value).then(res => {
        if (res.status === 204) {
            ElMessage.success("分配成功");
        }
        loading.value = false
    })

}

const isGrantedByOtherProviderName = (grantedProviders) => {
    if (grantedProviders.length) {
        return (
            grantedProviders.findIndex(p => p.providerName !== permissionsQuery.value.providerName) > -1
        )
    }
    return false
}

const transformPermissionTree = (permissions, name = null) => {
    const arr = []
    if (!permissions || !permissions.some(v => v.parentName === name)) { return arr }
    const parents = permissions.filter(v => v.parentName === name)
    for (const i in parents) {
        let label = ''
        if (permissionsQuery.value.providerName === 'R') {
            label = parents[i].displayName
        } else if (permissionsQuery.value.providerName === 'U') {
            label =
                parents[i].displayName +
                ' ' +
                parents[i].grantedProviders.map(provider => {
                    return `${provider.providerName}: ${provider.providerKey}`
                })
        }
        if (parents[i].isGranted && beforeCheckedKeyData.value.indexOf(parents[i].name) === -1) {
            beforeCheckedKeyData.value.push(parents[i].name)
        }
        arr.push({
            name: parents[i].name,
            label,
            disabled: isGrantedByOtherProviderName(
                parents[i].grantedProviders
            ),
            children: transformPermissionTree(permissions, parents[i].name)
        })
    }
    return arr
}
// eslint-disable-next-line no-undef
defineExpose({
    showDrawer, permissionsQuery, treeDefaultProps, checkedKeyData, beforeCheckedKeyData,
    getPermissionData, transformPermissionTree, getCheckedKey, goAssignPermission
})
</script>