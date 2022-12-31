import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import { VueSvgIconPlugin } from '@yzfe/vue3-svgicon'
import '@yzfe/svgicon/lib/svgicon.css'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'

createApp(App).use(VueSvgIconPlugin, { tagName: 'icon' }).use(store).use(router).use(ElementPlus).mount('#app')
