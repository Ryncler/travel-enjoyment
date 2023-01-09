import store from '@/store'
import { createRouter, createWebHistory } from 'vue-router'


const Layout = () => import("@/views/layout/index.vue");

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
    component: Layout
  },
  {
    path: '/login',
    name: 'Login',
    component: () => import('@/views/identity/index'),
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
      {
        path: '/system/role',
        name: 'RoleManage',
        component: () => import('@/components/system/role/roleManage')
      }
    ]
  },
  {
    path: '/openiddict',
    name: 'OpenIddict',
    component: Layout,
    redirect: '/openiddict/application',
    children: [
      {
        path: '/openiddict/application',
        name: 'ApplicationManage',
        component: () => import('@/components/openIddict/application/applicationManage')
      },
      // {
      //   path: '/system/role',
      //   name: 'RoleManage',
      //   component: () => import('@/components/system/role/roleManage')
      // }
    ]
  }

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
