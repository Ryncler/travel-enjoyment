import store from '@/store'
import { isLogin } from '@/utils/common'
import { createRouter, createWebHistory } from 'vue-router'

const routes = [
    {
        path: '/',
        name: 'Home',
        component: () => import('@/views/index'),
        meta: {
            'title': '首页',
            'isMenu': true,
        }
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
    {
        path: '/sights',
        name: 'Sights',
        component: () => import('@/views/index'),
        meta: {
            'title': '景点大全',
            'isMenu': true,
        }
    },
    {
        path: '/travel',
        name: 'Travel',
        component: () => import('@/views/index'),
        meta: {
            'title': '游记一览',
            'isMenu': true,
        }
    },
    {
        path: '/shared',
        name: 'Shared',
        component: () => import('@/views/index'),
        meta: {
            'title': '分享游记',
            'isMenu': true,
        }
    },
]

const router = createRouter({
    history: createWebHistory(),
    routes
})


const setMenuInfo = () => {
    let tmpArr = []
    routes.forEach((item) => {
        if (!item.meta.isMenu) {
            return
        }
        tmpArr.push({
            name: item.name,
            path: item.path,
            title: item.meta.title,
        })
        store.commit('menu/setMenuInfo', tmpArr)
    })
}

router.beforeEach((to, from, next) => {
    setMenuInfo()
    if (to.name !== 'Login' && !isLogin()) {
        next({ path: '/login' })
    } else {
        next()
    }
})

export default router
