import store from '@/store'
import { isAdmin } from '@/utils/common';
import { createRouter, createWebHashHistory, createWebHistory } from 'vue-router'


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
    redirect: '/dashboard',
    meta: {
      'title': '首页',
      'icon': 'dashboard',
      'isShow': false
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
    path: '/userinfo',
    name: 'UserInfo',
    component: Layout,
    redirect: '/userinfo/edit',
    children: [
      {
        path: '/userinfo/edit',
        name: 'EditUserInfo',
        component: () => import('@/views/user/editUserInfo'),
        meta: {
          'title': '编辑个人信息',
          'icon': 'edit',
          'isShow': false
        }
      },
    ],
    meta: {
      'title': '用户',
      'icon': 'user',
      'isShow': false
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
          'title': '用户管理',
          'icon': 'user',
          'isShow': true,
          'isAdmin': true
        }
      },
      {
        path: '/system/role',
        name: 'RoleManage',
        component: () => import('@/components/system/role/roleManage'),
        meta: {
          'title': '角色管理',
          'icon': 'roles',
          'isShow': true,
          'isAdmin': true
        }
      },
      {
        path: '/system/entry',
        name: 'EntryManage',
        component: () => import('@/components/system/entry/entryManage'),
        meta: {
          'title': '入驻管理',
          'icon': 'entry',
          'isShow': true,
          'isAdmin': true
        }
      },
      {
        path: '/common/tag',
        name: 'TagManage',
        component: () => import('@/components/common/tag/tagManage'),
        meta: {
          'title': '标签管理',
          'icon': 'tag',
          'isShow': true,
          'isAdmin': true
        }
      }
    ],
    meta: {
      'title': '系统管理',
      'icon': 'systemsetting',
      'isShow': true,
      'isAdmin': true
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
          'title': '应用管理',
          'icon': 'application',
          'isShow': true,
          'isAdmin': true
        }
      },
      {
        path: '/openiddict/scope',
        name: 'ScopeManage',
        component: () => import('@/components/openIddict/scope/scopeManage'),
        meta: {
          'title': '服务管理',
          'icon': 'scope',
          'isShow': true,
          'isAdmin': true
        }
      }
    ],
    meta: {
      'title': '授权管理',
      'icon': 'auth',
      'isShow': true,
      'isAdmin': true
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
          'title': '审计日志管理',
          'icon': 'auditlog',
          'isShow': true,
          'isAdmin': true
        }
      },
    ],
    meta: {
      'title': '日志管理',
      'icon': 'log',
      'isShow': true,
      'isAdmin': true
    }
  },
  {
    path: '/sights',
    name: 'Sights',
    component: Layout,
    redirect: '/sights/sights',
    children: [
      {
        path: '/sights/sights',
        name: 'SightsManage',
        component: () => import('@/components/sights/sightsManage'),
        meta: {
          'title': '景点管理',
          'icon': 'sights',
          'isShow': true,
          'isAdmin': false
        }
      },
      {
        path: '/sights/activity',
        name: 'ActivityManage',
        component: () => import('@/components/sights/activity/activityManage'),
        meta: {
          'title': '活动管理',
          'icon': 'activity',
          'isShow': true,
          'isAdmin': false
        }
      },
    ],
    meta: {
      'title': '旅游管理',
      'icon': 'sights',
      'isShow': true,
      'isAdmin': false
    }
  },
  {
    path: '/dashboard',
    name: 'Dashboard',
    component: Layout,
    redirect: '/dashboard/',
    children: [
      {
        path: '/dashboard/',
        name: 'Dashboard',
        component: () => import('@/components/dashboard/dashboard'),
      },
    ],
    meta: {
      'title': '仪表盘',
      'icon': 'dashboard',
      'isShow': false,
    }
  },

  {
    path: '/image',
    name: 'Image',
    component: Layout,
    redirect: '/image/manage',
    children: [
      {
        path: '/image/manage',
        name: 'ImageManage',
        component: () => import('@/components/common/image/imageManage'),
      },
    ],
    meta: {
      'title': '图片管理',
      'icon': 'image',
      'isShow': false,
    }
  },
]

const router = createRouter({
  history: createWebHashHistory(),
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

const setMenuInfo = () => {
  let tmpArr = []
  routes.forEach((item) => {
    if (!item.meta.isShow) {
      return
    }
    if (isAdmin() && item.meta.isAdmin) {
      tmpArr.push({
        name: item.name,
        path: item.path,
        icon: require(`@/icons/${item.meta.icon}.svg`),
        title: item.meta.title,
        children: item.children.map((res) => {
          return {
            name: res.name,
            path: res.path,
            icon: require(`@/icons/${res.meta.icon}.svg`),
            title: res.meta.title,
          }
        })
      })
    } else if (!item.meta.isAdmin) {
      tmpArr.push({
        name: item.name,
        path: item.path,
        icon: require(`@/icons/${item.meta.icon}.svg`),
        title: item.meta.title,
        children: item.children.map((res) => {
          return {
            name: res.name,
            path: res.path,
            icon: require(`@/icons/${res.meta.icon}.svg`),
            title: res.meta.title,
          }
        })
      })
    }
  })
  store.commit('breadcrumb/setMenuInfo', tmpArr)
}

router.beforeEach((to, from, next) => {
  setMenuInfo()
  setRouterPath(to)
  if (to.name !== 'Login' && !isLogin()) {
    next({ path: '/login' })
  } else {
    next()
  }
})

export default router
