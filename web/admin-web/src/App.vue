<template>
  <router-view />
</template>

<script>
import { nextTick, provide, ref } from '@vue/runtime-core'
export default {
  setup() {
    var isRouterAlive = ref(true)
    const reload = () => {
      isRouterAlive.value = false
      nextTick(() => {
        isRouterAlive.value = true
      })
    }

    provide('reloadRouter', reload)
  },
  components: {
  },
}
</script>
