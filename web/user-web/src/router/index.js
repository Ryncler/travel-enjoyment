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
                    'title': '游记一览',
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
        redirect: '/shared/share',
        children: [
            {
                path: 'share',
                name: 'Shared',
                component: () => import('@/components/travel/shareTravel'),
                meta: {
                    'title': '分享游记',
                    'icon': 'travel',
                    'isMenu': false,
                }
            },
            {
                path: 'info',
                name: 'TravelInfo',
                component: () => import('@/components/travel/travelInfo'),
                meta: {
                    'title': '游记详情',
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
    {
        path: '/user',
        name: 'User',
        component: Layout,
        redirect: '/User/info',
        children: [
            {
                path: 'info',
                name: 'User',
                component: () => import('@/components/user/info'),
                meta: {
                    'title': '用户信息',
                    'icon': 'user',
                    'isMenu': false,
                }
            },
        ],
        meta: {
            'title': '用户信息',
            'isMenu': false,
        }
    },
    {
        path: '/edit',
        name: 'EditInfo',
        component: Layout,
        redirect: '/edit/info',
        children: [
            {
                path: 'info',
                name: 'EditInfo',
                component: () => import('@/components/setting/info'),
                meta: {
                    'title': '编辑个人资料',
                    'icon': 'user',
                    'isMenu': false,
                }
            },
        ],
        meta: {
            'title': '编辑个人资料',
            'isMenu': false,
        }
    }
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
    next()
})

export default router
