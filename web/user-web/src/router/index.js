import store from '@/store'
import { isLogin } from '@/utils/common'
import { createRouter, createWebHistory, createWebHashHistory } from 'vue-router'

const Layout = () => import("@/views/layout/index.vue");

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
        component: Layout,
        redirect: '/sights/info',
        children: [
            {
                path: 'info',
                name: 'Sights',
                component: () => import('@/components/sights/info'),
                meta: {
                    'title': '景点信息',
                    'icon': 'sights',
                    'isMenu': false,
                }
            },
        ],
        meta: {
            'title': '景点大全',
            'isMenu': true,
        }
    },
    {
        path: '/travel',
        name: 'Travel',
        component: Layout,
        redirect: '/travel/info',
        children: [
            {
                path: 'info',
                name: 'Travel',
                component: () => import('@/components/travel/info'),
                meta: {
                    'title': '游记信息',
                    'icon': 'travel',
                    'isMenu': false,
                }
            },
        ],
        meta: {
            'title': '游记一览',
            'isMenu': true,
        }
    },
    {
        path: '/shared',
        name: 'Shared',
        component: Layout,
        redirect: '/shared/info',
        children: [
            {
                path: 'info',
                name: 'Shared',
                component: () => import('@/components/travel/travelInfo'),
                meta: {
                    'title': '分享游记',
                    'icon': 'travel',
                    'isMenu': false,
                }
            },
        ],
        meta: {
            'title': '分享游记',
            'isMenu': true,
        }
    },
]

const router = createRouter({
    history: createWebHashHistory(),
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
