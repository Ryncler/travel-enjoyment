import storage from '@/utils/storageHandle'

const state = () => ({
    menuInfo: storage.getItem('menuInfo') || [],
    routerInfos: storage.getItem('routerInfos') || [],
})

const getters = {
    routerInfos: (state) => {
        if (state.routerInfos === undefined || state.routerInfos.length <= 0) {
            state.routerInfos = storage.getItem('routerInfos')
        }
        return state.routerInfos
    },
    menuInfo: (state) => {
        if (state.menuInfo === undefined || state.menuInfo.length <= 0) {
            state.menuInfo = storage.getItem('menuInfo')
        }
        return state.menuInfo
    },
}

const mutations = {
    setRouterInfo: (state, data) => {
        state.routerInfos = data
        storage.setItem('routerInfos', data)
    },
    setMenuInfo: (state, data) => {
        state.menuInfo = data
        storage.setItem('menuInfo', data)
    },
}

export default {
    namespaced: true,
    state,
    getters,
    mutations
}
