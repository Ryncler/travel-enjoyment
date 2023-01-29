import store from '@/store'
import { createRouter, createWebHistory } from 'vue-router'


function isLogin() {
    var res = false
    const token = store.getters['identity/token']
    if (typeof token == 'string' && token.length > 0) {
        res = true
    }
    return res
}

const routes = [
    {
        path: '/',
        name: 'Home',
    },
    {
        path: '/login',
        name: 'Login',
        component: () => import('@/views/identity/index'),
        meta: {
            'title': '登录',
            'icon': 'login',
            'isShow': false
        }
    },
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

router.beforeEach((to, from, next) => {
    if (to.name !== 'Login' && !isLogin()) {
        next({ path: '/login' })
    } else {
        next()
    }
})

export default router
