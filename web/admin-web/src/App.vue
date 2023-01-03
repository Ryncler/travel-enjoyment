<template>
  <router-view v-if="isRouterAlive" />
</template>

<script>
export default {
  data() {
    return {
      isRouterAlive: true
    }
  },
  methods: {
    reload() {
      this.isRouterAlive = false
      this.$nextTick(() => {
        this.isRouterAlive = true
      })
    }
  },
  provide() {
    return {
      reload: this.reload
    }
  },
  components: {
  },
}
</script>
