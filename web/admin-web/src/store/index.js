import { createStore } from 'vuex'

export default createStore({
  state: {
    token: '',
    userid: '',
    username: '',
    roles: [],
    email: '',
    phone: '',
    avatar: '',
    profile: '',
  },
  getters: {
    getToken: state => state.token,
    getUserId: state => state.userid,
    getUserName: state => state.username,
    getRoles: state => state.roles,
    getRoleByName: (state) => (name) => {
      return state.roles.find(x => x === name)
    },
    getEmail: state => state.email,
    getPhone: state => state.phone,
    getAvatar: state => state.avatar,
    getProfile: state => state.profile,
  },
  mutations: {
    SetToken: (state, token) => {
      state.token = 'Bearer ' + token
    },
    SetUserId: (state, userId) => {
      state.userid = userId
    },
    SetUserName: (state, userName) => {
      state.username = userName
    },
    SetRoles: (state, roles) => {
      state.roles = roles
    },
    SetEmail: (state, email) => {
      state.email = email
    },
    SetPhone: (state, phone) => {
      state.phone = phone
    },
    SetAvatar: (state, avatar) => {
      state.avatar = avatar
    },
    SetProflie: (state, profile) => {
      state.profile = profile
    },
  },
  actions: {
  },
  modules: {
  }
})
