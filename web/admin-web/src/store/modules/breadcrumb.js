import storage from '@/utils/storageHandle'

const state = () => ({
    routerInfos: storage.getItem('routerInfos') || [],
})

const getters = {
    routerInfos: (state) => {
        if (state.routerInfos === undefined || state.routerInfos.length <= 0) {
            state.routerInfos = storage.getItem('routerInfos')
        }
        return state.routerInfos
    }
}

const mutations = {
    setRouterInfo: (state, data) => {
        state.routerInfos = data
        storage.setItem('routerInfos', data)
    }
}

export default {
    namespaced: true,
    state,
    getters,
    mutations
}
