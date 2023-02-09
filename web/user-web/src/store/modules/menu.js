import storage from '@/utils/common/storageHandle'

const state = () => ({
    menuInfo: storage.getItem('menuInfo') || [],
})

const getters = {
    menuInfo: (state) => {
        if (state.menuInfo === undefined || state.menuInfo.length <= 0) {
            state.menuInfo = storage.getItem('menuInfo')
        }
        return state.menuInfo
    },
}

const mutations = {
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