<template>
    <el-drawer v-model="showDrawer" :show-close="false" size="30%">
        <template #header="{ close }">
            <h4 class="titleClass">自定义精选游记</h4>
            <el-button type="danger" @click="close">
                <icon data="@/icons/close.svg" class="closeIcon" />
                关闭
            </el-button>
        </template>
        <el-input class="searchInput" v-model="searchTravel" placeholder="搜索游记" clearable :suffix-icon="Search" />
        <ul v-infinite-scroll="load" class="infinite-list" style="overflow: auto" infinite-scroll-distance="20px">
            <el-checkbox-group v-model="checkTravels" :min="1" :max="2" @change="goSave()"
                :infinite-scroll-disabled="isDisabled">
                <el-checkbox class="checkbox" v-for="item in travelList" :label="item.id" border :key="item">
                    {{ item.travelsTitle }}
                </el-checkbox>
            </el-checkbox-group>
        </ul>
        <p v-if="loading" class="center">Loading...</p>
        <p v-if="noMore()" class="center">没有更多了...</p>
    </el-drawer>
</template>

<script setup>
import { ref } from 'vue';
import store from '@/store'
import { onBeforeMount } from '@vue/runtime-core';
import { Search } from '@element-plus/icons-vue';
import { ElMessage } from 'element-plus'
import { getTravelsByUserId } from '@/api/travel/index'
import { addChoiceTravel, updateChoiceTravel } from '@/api/identity/user';

const showDrawer = ref(false);
const skipCount = ref(1)
const loading = ref(false)
const isDisabled = ref(false)
const totalPages = ref(0)
const choiceTravelId = ref('')
const checkTravels = ref([]);
const travelList = ref([])

const load = () => {
    loading.value = true
    if (skipCount.value >= totalPages.value - 1) {
        loading.value = false
        isDisabled.value = true
        return
    }
    setTimeout(() => {
        skipCount.value += 1
        getTravelList()
    }, 2000)
}
const getTravelList = () => {
    var params = {
        createId: store.getters['identity/userInfo'].id,
        skipCount: skipCount.value,
        pageSize: 10
    }
    getTravelsByUserId(params).then(res => {
        if (res.status === 200) {
            totalPages.value = (res.data.totalCount + params.pageSize - 1) / params.pageSize
            res.data.items.map((item) => {
                travelList.value.push(item)
            })
        }
        loading.value = false
    }).catch(() => {
        loading.value = false
    })
}
const noMore = () => {
    return skipCount.value >= totalPages.value - 1
}

const goSave = () => {
    if (checkTravels.value.length >= 2) {
        var data = {}
        if (choiceTravelId.value !== '' && choiceTravelId.value !== undefined) {
            data = {
                id: choiceTravelId.value,
                oneTravelsId: checkTravels.value[0],
                twoTravelsId: checkTravels.value[1],
            }
            updateChoiceTravel(data).then(res => {
                if (res.status === 200) {
                    ElMessage.success('更新成功!')
                }
            })
        } else {
            data = {
                oneTravelsId: checkTravels.value[0],
                twoTravelsId: checkTravels.value[1],
            }
            addChoiceTravel(data).then(res => {
                if (res.status === 200) {
                    ElMessage.success('更新成功!')
                }
            })
        }
    } else {
        ElMessage.warning('请选择两个精选游记!')
    }
}

// eslint-disable-next-line no-undef
defineExpose({
    showDrawer, travelList, skipCount, choiceTravelId, checkTravels, getTravelList
})
</script>

<style>
h4 {
    font-weight: bold;
}

.infinite-list {
    height: 600px;
    padding: 0;
    margin: 0;
    margin-top: 20px;
    list-style: none;
}

.center {
    text-align: center;
}

.checkbox {
    width: 100%;
    height: 30px;
    margin-right: 0;
    display: inline-flex;
    text-align: center;
}
</style>

<style>
.el-checkbox__input.is-checked+.el-checkbox__label {
    color: #66CCCC;
}

.el-checkbox.is-bordered.is-checked {
    border-color: #66CCCC;
}

.el-checkbox__input.is-checked .el-checkbox__inner {
    background-color: #66CCCC;
    border-color: #66CCCC;
}

.el-input__wrapper.is-focus {
    box-shadow: 0 0 0 1px #66CCCC;
}
</style>