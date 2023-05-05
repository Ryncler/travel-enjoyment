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
const { ref } = require("@vue/reactivity");
import { ElMessage } from 'element-plus'
import { getPermissions, updateRolePermission } from '@/api/user/permission';

const loading = ref(false)
const showDrawer = ref(false)
const permissionData = ref({})
const permissionTree = []
const permissionParentName = ref([])

const treeDefaultProps = ref({
    children: 'children',
    label: 'label'
})
const permissionsQuery = ref({})
const beforeCheckedKeyData = ref([])
const checkedKeyData = ref([])

const getPermissionData = () => {
    checkedKeyData.value = []
    beforeCheckedKeyData.value = []
    return getPermissions(permissionsQuery.value).then(res => {
        if (res.status === 200) {
            permissionData.value = res.data;
        }
    })
}

const getCheckedKey = (node, tree) => {
    var checkedkey;
    if (tree.checkedKeys.find(x => x == node.name) == undefined) {
        node.children.map((item) => {
            checkedkey = checkedKeyData.value.find(x => x.name == item.name)
            if (checkedkey != undefined)
                checkedkey.isGranted = false
        })
        checkedkey = checkedKeyData.value.find(x => x.name == node.name)
        if (checkedkey != undefined)
            checkedkey.isGranted = false
    } else {
        node.children.map((item) => {
            checkedkey = checkedKeyData.value.find(x => x.name == item.name)
            if (checkedkey != undefined)
                checkedkey.isGranted = true
        })
        checkedkey = checkedKeyData.value.find(x => x.name == node.name)
        if (checkedkey != undefined)
            checkedkey.isGranted = true
    }

    tree.checkedKeys.map((item) => {
        if (!checkedKeyData.value.find(x => x.name === item))
            checkedKeyData.value.push({
                name: item,
                isGranted: true
            })
    })
    tree.halfCheckedKeys.forEach(item => {
        if (!checkedKeyData.value.find(x => x.name === item))
            checkedKeyData.value.push({
                name: item,
                isGranted: true
            })
    })
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
            grantedProviders.findIndex(p => p.providerName != permissionsQuery.value.providerName) > -1
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
            if (parents[i].parentName !== null) {
                beforeCheckedKeyData.value.push(parents[i].name)
                checkedKeyData.value.push({
                    name: parents[i].name,
                    isGranted: true
                })
            }
            if (parents[i].name == "CommonService.Dashboard" || parents[i].name == "SightsService.UserTrends" || parents[i].name == "FeatureManagement.ManageHostFeatures")
                beforeCheckedKeyData.value.push(parents[i].name)
            // else {
            //     if (parents.find(x => x.parentName != parents[i].parentName) == undefined) {
            //         permissionParentName.value.push(parents[i].name)
            //         //beforeCheckedKeyData.value.push(parents[i].name)
            //         checkedKeyData.value.push({
            //             name: parents[i].name,
            //             isGranted: true
            //         })
            //     }
            // }
        }
        arr.push({
            name: parents[i].name,
            label,
            disabled: isGrantedByOtherProviderName(parents[i].grantedProviders),
            children: transformPermissionTree(permissions, parents[i].name)
        })
    }
    // permissionParentName.value.map((item) => {
    //     //var ss = beforeCheckedKeyData.value.filter(x => x == item)
    //     // console.log(ss);
    //     // console.log(ss.length)
    //     if (beforeCheckedKeyData.value.filter(x => x == item).length > 0)
    //         beforeCheckedKeyData.value.splice(beforeCheckedKeyData.value.indexOf(item), 1)
    // })
    return arr
}
// eslint-disable-next-line no-undef
defineExpose({
    showDrawer, permissionsQuery, treeDefaultProps, checkedKeyData, beforeCheckedKeyData,
    getPermissionData, transformPermissionTree, getCheckedKey, goAssignPermission
})
</script>