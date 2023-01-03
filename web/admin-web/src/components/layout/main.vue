<template>
    <section class="main">
        <transition name="fade-transform" mode="out-in">
            <router-view :key="key" />
        </transition>
    </section>
</template>

<script>
import router from '@/router/index'

export default {
    // eslint-disable-next-line vue/multi-word-component-names
    name: 'Main',
    data() {
        return {
            key() {
                return router.path
            }
        }
    }
}
</script>

<style>
.main{
    
}
</style>