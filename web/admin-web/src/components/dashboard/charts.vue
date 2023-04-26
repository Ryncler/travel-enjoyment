<template>
    <el-row>
        <el-col class="sightsCol">
            <el-card class="sightsCol" :body-style="style" shadow="hover">
                <v-chart :option="option" autoresize />
            </el-card>
        </el-col>
        <elcol class="tagCol">
            <el-card class="tagCol" :body-style="tagStyle" shadow="hover">
                <v-chart :option="tagOption" autoresize />
            </el-card>
        </elcol>
    </el-row>
</template>

<script setup>
import { use } from 'echarts/core';
import { CanvasRenderer } from 'echarts/renderers';
import { LineChart, BarChart } from 'echarts/charts';
import {
    TitleComponent,
    TooltipComponent,
    LegendComponent,
    GridComponent,
    DatasetComponent,
    TransformComponent
} from 'echarts/components';
import { LabelLayout, UniversalTransition } from 'echarts/features';
import VChart from 'vue-echarts';
import { ref } from 'vue';
import { onBeforeMount } from '@vue/runtime-core';
import { isAdmin } from '@/utils/common';
import { getRecentHotSights, getRecentHotTags } from '@/api/common/index'

use([
    CanvasRenderer,
    LineChart, BarChart,
    TitleComponent,
    TooltipComponent,
    LegendComponent,
    GridComponent,
    DatasetComponent,
    TransformComponent,
    LabelLayout,
    UniversalTransition
]);

const style = ref({
    padding: '0px',
    width: '100%',
    height: '360px'
})
const tagStyle = ref({
    padding: '0px',
    width: '100%',
    height: '360px'
})

const timeData = () => {
    var i = 1
    var result = []
    while (i <= 24) {
        result.push(i)
        i++
    }
    return result
}

const getCensusData = () => {
    if (isAdmin()) {
        option.value.title.text = '近期热门景点'
        option.value.title.subtext = '近期前十景点的访问量'
    } else {
        option.value.title.text = '旗下景点24小时访问量'
        option.value.title.subtext = ''
    }
}

const getHotSights = () => {
    return getRecentHotSights().then(res => {
        if (res.status === 200) {
            res.data.map((item) => {
                option.value.legend.data.push(item.name)
                item.hourValue.splice(0, 1)
                option.value.series.push(
                    {
                        name: item.name,
                        data: item.hourValue,
                        type: 'line',
                        smooth: 0.5,
                    }
                )
            })
        }
    })
}

const getHotTags = () => {
    return getRecentHotTags().then(res => {
        if (res.status === 200) {
            res.data.map((item) => {
                tagOption.value.legend.data.push(item.name)
                item.hourValue.splice(0, 1)
                tagOption.value.series.push(
                    {
                        name: item.name,
                        data: item.hourValue,
                        type: 'bar',
                        barWidth: '20%'
                    }
                )
            })
        }
    })
}
onBeforeMount(() => {
    getCensusData()
    getHotSights()
    getHotTags()
})
const option = ref({
    title: {
        text: '',
        subtext: '',
        left: 'center',
    },
    tooltip: {
        trigger: 'axis',
        axisPointer: {
            type: 'cross',
            axis: 'auto',
            snap: true
        },
        formatter: function (params) {
            var result = params[0].axisValue + '小时<br/>'
            params.forEach((item) => {
                result += `${item.marker}${item.seriesName}：${item.data}<br/>`
            });
            return result
        }
    },
    xAxis: {
        name: '24小时',
        type: 'category',
        data: timeData()
    },
    yAxis: {
        name: '访问量',
        left: 'center',
    },
    legend: {
        orient: 'vertical',
        x: 'right',
        y: 'top',
        data: [],
    },
    series: []
});

const tagOption = ref({
    title: {
        text: '近期热门标签',
        subtext: '近期前十标签的使用次数',
        left: 'center',
    },
    tooltip: {
        trigger: 'axis',
        axisPointer: {
            type: 'cross',
            axis: 'auto',
            snap: true
        },
        formatter: function (params) {
            var result = params[0].axisValue + '小时<br/>'
            params.forEach((item) => {
                result += `${item.marker}${item.seriesName}：${item.data}<br/>`
            });
            return result
        }
    },
    xAxis: {
        name: '24小时',
        type: 'category',
        data: timeData()
    },
    yAxis: {
        name: '使用量',
        left: 'center',
    },
    legend: {
        orient: 'vertical',
        x: 'right',
        y: 'top',
        data: [],
    },
    calculable: true,
    series: []
})
</script>

<style>
.sightsCol {
    width: 100%;
    height: 360px;
}

.tagCol {
    width: 100%;
    height: 360px;
    margin-top: 10px;
}
</style>