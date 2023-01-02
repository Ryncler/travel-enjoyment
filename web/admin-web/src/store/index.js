import { createStore } from 'vuex'
import storage from '@/utils/storageHandle'

export default createStore({
  state: {
    token: storage.getItem('token') || '',
    userinfo: storage.getItem('userinfo') || {},
  },
  getters: {
    getToken: state => state.token,
    getUserInfo: state => state.userinfo,
    getRoles: state => state.userinfo.roles,
    getRoleByName: (state) => (name) => {
      return state.userinfo.roles.find(x => x === name)
    },
  },
  mutations: {
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
      storage.clear()
    }
  },
  actions: {
  },
  modules: {
  }
})
