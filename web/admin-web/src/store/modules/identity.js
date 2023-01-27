
import storage from '@/utils/storageHandle'

const state = () => ({
    isRefresh: storage.getItem('isRefresh') || false,
    token: storage.getItem('token') || '',
    userinfo: storage.getItem('userinfo') || {},
})

const getters = {
    isRefresh: (state) => {
        if (state.isRefresh === undefined) {
            state.isRefresh = storage.getItem('isRefresh')
        }
        return state.isRefresh
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
}


export default {
    namespaced: true,
    state,
    getters,
    mutations
}