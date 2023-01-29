import storage from '@/utils/common/storageHandle'

const state = () => ({
    isShow: false,
    token: storage.getItem('token') || '',
    userinfo: storage.getItem('userinfo') || {},
})

const getters = {
    isShow: (state) => {
        return state.isShow
    },
    token: (state) => {
        if (state.token === undefined) {
            state.token = storage.getItem('token')
        }
        return state.token
    },
    userInfo: (state) => {
        if (state.userinfo === undefined) {
            state.userinfo = storage.getItem('userinfo')
        }
        return state.userinfo
    },
    roles: (state) => {
        if (state.userinfo === undefined) {
            state.userinfo = storage.getItem('userinfo')
        }
        return state.userinfo.roles
    },
    adminRole: (state) => {
        if (state.userinfo === undefined) {
            state.userinfo = storage.getItem('userinfo')
        }
        let res = state.userinfo.role.find(x => x === 'admin')
        return res === undefined ? '' : res
    }
}

const mutations = {
    isShow: (state, data) => {
        state.isShow = data
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
        state.isShow = false
        storage.clear()
    }
}


export default {
    namespaced: true,
    state,
    getters,
    mutations
}