import store from '@/store'
import { createRouter, createWebHistory } from 'vue-router'


const Layout = () => import("@/views/layout/index.vue");

function isLogin() {
  var res = false
  const token = store.getters.getToken
  if (typeof token == 'string' && token.length > 0) {
    res = true
  }
  return res
}

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Layout
  },
  {
    path: '/login',
    name: 'Login',
    component: () => import('@/views/identity/index'),
    beforeEnter: (to, from, next) => {
      if (!isLogin()) {
        next()
      } else {
        next({
          name: '/'
        })
      }
    }
  },
  {
    path: '/system',
    name: 'System',
    component: Layout,
    redirect: '/system/user',
    children: [
      {
        path: '/system/user',
        name: 'UserManage',
        component: () => import('@/components/system/user/userManage')
      },
      // {
      //   path: '/roleManage',
      //   name: 'RoleManage',
      //   component: () => import('@/components/system/roleManage')
      // },
      // {
      //   path: '/permissionManage',
      //   name: 'PermissionManage',
      //   component: () => import('@/components/system/permissionManage')
      // },
    ]
  }

]

const router = createRouter({
  history: createWebHistory(),
  routes
})

router.beforeEach((to, from, next) => {
  if (isLogin() || to.name === 'Login') {
    next()
  } else {
    next({
      name: 'Login'
    })
  }
})

export default router
