<template>
    <el-drawer v-model="showDrawer" :show-close="false" size="35%">
        <template #header="{ close, titleId, titleClass }">
            <h4 :id="titleId" :class="titleClass">{{ title }}景点</h4>
            <el-button type="danger" @click="close">
                <icon data="@/icons/close.svg" class="closeIcon" />
                关闭
            </el-button>
        </template>
        <el-form :model="sightsForm">
            <el-form-item prop="name">
                <icon data="@/icons/name.svg" class="svg-container" />
                <el-input v-model="sightsForm.name" placeholder="景点名称" name="name" type="text" tabindex="1"
                    autocomplete="on" />
            </el-form-item>
            <el-form-item prop="description">
                <icon data="@/icons/description.svg" class="svg-container" />
                <el-input v-model="sightsForm.description" placeholder="景点说明" name="description" :rows="3" type="textarea"
                    tabindex="1" autocomplete="on" />
            </el-form-item>
            <el-form-item prop="address">
                <icon data="@/icons/address.svg" class="svg-container" />
                <el-input v-model="sightsForm.address" placeholder="地址" name="address" :rows="3" type="textarea"
                    tabindex="1" autocomplete="on" />
            </el-form-item>
            <el-form-item prop="mapId">
                <icon data="@/icons/map.svg" class="svg-container" />
                <el-cascader v-model="mapIds" :options="geoData" :props="props" @change="handleChange" filterable />
            </el-form-item>
            <el-form-item prop="ticket">
                <icon data="@/icons/ticket.svg" class="svg-container" />
                <el-input v-model="sightsForm.ticket" placeholder="门票价钱" name="ticket" :rows="3" type="textarea"
                    tabindex="1" autocomplete="on" />
            </el-form-item>
            <el-form-item prop="openTime">
                <icon data="@/icons/time.svg" class="svg-container" />
                <el-input v-model="sightsForm.openTime" placeholder="开放时间" name="openTime" :rows="3" type="textarea"
                    tabindex="1" autocomplete="on" />
            </el-form-item>
            <el-form-item prop="trafficGuide">
                <icon data="@/icons/traffic.svg" class="svg-container" />
                <el-input v-model="sightsForm.trafficGuide" placeholder="交通指南" name="trafficGuide" :rows="3" type="textarea"
                    tabindex="1" autocomplete="on" />
            </el-form-item>
            <el-form-item prop="SelfDrivingGuide">
                <icon data="@/icons/traffic.svg" class="svg-container" />
                <el-input v-model="sightsForm.selfDrivingGuide" placeholder="自驾游指南" name="SelfDrivingGuide" :rows="3"
                    type="textarea" tabindex="1" autocomplete="on" />
            </el-form-item>
            <el-form-item>
                <icon data="@/icons/tag.svg" class="svg-container" />
                <el-select v-model="sightsTag" clearable multiple multiple-limit="9" placeholder="标签列表" filterable remote
                    reserve-keyword remote-show-suffix :remote-method="getTagsByName">
                    <el-option v-for="item in tags" :key="item" :label="item.name" :value="item.name" />
                </el-select>
            </el-form-item>
            <el-form-item>
                <el-button :loading="loading" round type="primary" class="revertbtn addbtn"
                    @click="btnName === '添加' ? goAdd() : goEdit()">{{ btnName }}</el-button>
            </el-form-item>

        </el-form>
    </el-drawer>
</template>

<script setup>
import { ref } from '@vue/reactivity'
import { ElMessage } from 'element-plus'
import { addSights, editSights } from '@/api/sights/sights';
import { getGeoTree } from '@/api/common';
import { getAllParentArr } from '@/utils/common';
import { addHotTop, getTagListByName } from '@/api/common';
import { addSightsTags, getTagIdListBySightsId, editSightsTags } from '@/api/sights/tag';

const loading = ref(false)
const showDrawer = ref(false);
const title = ref('');
const btnName = ref('')
const sightsForm = ref({})
const oldSightsTag = ref([])
const sightsTag = ref([])
const tags = ref([])

const mapIds = ref([])
const geoData = ref([])
const props = {
    expandTrigger: 'hover',
    value: 'id',
    label: 'name'
}

const goAdd = () => {
    var num = 0
    loading.value = true
    sightsForm.value.mapId = mapIds.value.slice(-1)[0]
    addSights(sightsForm.value).then(res => {
        if (res.status === 200) {
            var data = {
                linkId: res.data.id,
                topType: 1,
                weight: 1
            }
            addHotTop(data).then(res => {
                if (res.status === 200) {
                    num += 1
                }
            })
            var sightsTagData = []
            sightsTag.value.map(i => {
                sightsTagData.push({
                    sightsId: res.data.id,
                    tagName: i
                })
            })
            addSightsTags(sightsTagData).then(res => {
                if (res.status === 200) {
                    num += 1
                }
            })
            loading.value = false
            ElMessage.success('添加成功！')
        } else {
            loading.value = false
        }
    }).catch(() => {
        loading.value = false
    })
}

const goEdit = () => {
    loading.value = true
    return editSights(sightsForm.value.id, sightsForm.value).then(res => {
        if (res.status === 200) {
            var oldTag = []
            var newTag = []
            sightsTag.value.map(i => {
                newTag.push({
                    sightsId: res.data.id,
                    tagName: i
                })
            })
            oldSightsTag.value.map(i => {
                oldTag.push({
                    sightsId: res.data.id,
                    tagName: i
                })
            })
            editSightsTags(oldTag, newTag);
            ElMessage.success('编辑成功！')
        }
        loading.value = false
    })
}

const getSightsTag = (id) => {
    getTagIdListBySightsId(id).then(res => {
        if (res.status === 200) {
            sightsTag.value = res.data.map(i => {
                return i.tagName
            })
            oldSightsTag.value = sightsTag.value
        }
    })
}

const getGeoData = () => {
    return getGeoTree().then(res => {
        if (res.status === 200) {
            geoData.value = res.data
            if (typeof sightsForm.value.mapId === 'string' && sightsForm.value.mapId.length > 0) {
                mapIds.value = getAllParentArr(res.data, sightsForm.value.mapId).reverse().map((res) => {
                    return res.id
                })
            }
        }
    })
}

const getTagsByName = (query) => {
    if (query) {
        loading.value = true
        getTagListByName(query).then(res => {
            if (res.status === 200) {
                tags.value = res.data
                loading.value = false
            }
        })
    }
    else {
        tags.value = []
    }
}
// eslint-disable-next-line no-undef
defineExpose({
    showDrawer, title, btnName, loading, sightsForm, mapIds,
    goAdd, goEdit, getGeoData, getSightsTag
});
</script>

<style>
.addbtn {
    width: 200px;
    margin: 0 0 0 35%;
}

.el-cascader {
    flex: 1;
}
</style>