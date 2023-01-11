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
    component: Layout,
    meta: {
      'title': '首页'
    }
  },
  {
    path: '/login',
    name: 'Login',
    component: () => import('@/views/identity/index'),
    meta: {
      'title': '登录'
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
        component: () => import('@/components/system/user/userManage'),
        meta: {
          'title': '用户管理'
        }
      },
      {
        path: '/system/role',
        name: 'RoleManage',
        component: () => import('@/components/system/role/roleManage'),
        meta: {
          'title': '角色管理'
        }
      }
    ],
    meta: {
      'title': '系统管理'
    }
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
        component: () => import('@/components/openIddict/application/applicationManage'),
        meta: {
          'title': '应用管理'
        }
      },
      {
        path: '/openiddict/scope',
        name: 'ScopeManage',
        component: () => import('@/components/openIddict/scope/scopeManage'),
        meta: {
          'title': '服务管理'
        }
      }
    ],
    meta: {
      'title': '授权管理'
    }
  },
  {
    path: '/logging',
    name: 'Logging',
    component: Layout,
    redirect: '/logging/auditlog',
    children: [
      {
        path: '/logging/auditlog',
        name: 'AuditLog',
        component: () => import('@/components/logging/auditLogManage'),
        meta: {
          'title': '审计日志管理'
        }
      },
    ],
    meta: {
      'title': '日志管理'
    }
  }

]

const router = createRouter({
  history: createWebHistory(),
  routes
})

const setRouterPath = (to) => {
  var routerInfos = to.matched.filter(v => v.name)
  let tmpArr = []
  routerInfos.forEach((item) => {
    if (item.name == 'Home' || item.name == 'Layout') {
      return
    }
    tmpArr.push({
      name: item.name,
      path: item.path,
      title: item.meta.title
    })
  })
  if (tmpArr.length > 0) {
    //面包屑前面追加首页
    tmpArr.unshift({ name: 'Home', path: '/', title: '首页' })
  }
  store.commit('breadcrumb/setRouterInfo', tmpArr)
}

router.beforeEach((to, from, next) => {
  setRouterPath(to)
  if (to.name !== 'Login' && !isLogin()) {
    next({ path: '/login' })
  } else {
    next()
  }
})

export default router
