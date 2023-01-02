import store from '@/store'
import { createRouter, createWebHistory } from 'vue-router'

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
    redirect: '/home'
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
          name: 'Home'
        })
      }
    }
  },
  {
    path: '/home',
    name: 'Home',
    component: () => import('@/views/layout/index')
  },

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
