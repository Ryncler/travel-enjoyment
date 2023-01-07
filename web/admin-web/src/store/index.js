import { createStore } from 'vuex'
import storage from '@/utils/storageHandle'

export default createStore({
  state: {
    isRefresh: storage.getItem('isRefresh') || false,
    token: storage.getItem('token') || '',
    userinfo: storage.getItem('userinfo') || {},
  },
  getters: {
    getIsRefresh: state => state.isRefresh,
    getToken: state => state.token,
    getUserInfo: state => state.userinfo,
    getRoles: state => state.userinfo.roles,
    getRoleByName: (state) => (name) => {
      return state.userinfo.roles.find(x => x === name)
    },
  },
  mutations: {
    setRefresh: () => {
      storage.setItem('isRefresh', true)
    },
    setToken: (state, token) => {
      token = 'Bearer ' + token
      state.token = token
      storage.setItem('token', token)
    },
    setUserInfo: (state, data) => {
      state.userinfo = data.userinfo
      storage.setItem('userinfo', data)
    },
    removeAny: (state) => {
      state.userinfo = {}
      state.token = ''
      state.isRefresh = false
      storage.clear()
    }
  },
  actions: {
  },
  modules: {
  }
})
