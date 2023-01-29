import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import { VueSvgIconPlugin } from '@yzfe/vue3-svgicon'
import '@yzfe/svgicon/lib/svgicon.css'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import zhCn from 'element-plus/dist/locale/zh-cn.mjs'
import ECharts from 'vue-echarts'


createApp(App).use(VueSvgIconPlugin, { tagName: 'icon' }).use(store).use(router).use(ElementPlus, {locale: zhCn,}).use('v-chart', ECharts).mount('#app')
