import storage from '@/utils/common/storageHandle'

const state = () => ({
    sightsTop: storage.getItem('sightsTop') || [],
    travelTop: storage.getItem('travelTop') || [],
    tagTop: storage.getItem('tagTop') || []
})

const getters = {
    sightsTop: (state) => {
        if (state.sightsTop === undefined) {
            state.sightsTop = storage.getItem('sightsTop')
        }
        return state.sightsTop
    },
    travelTop: (state) => {
        if (state.travelTop === undefined) {
            state.travelTop = storage.getItem('travelTop')
        }
        return state.travelTop
    },
    tagTop: (state) => {
        if (state.tagTop === undefined) {
            state.tagTop = storage.getItem('tagTop')
        }
        return state.tagTop
    }
}

const mutations = {
    sightsTop: (state, data) => {
        state.sightsTop = data
    },
    travelTop: (state, data) => {
        state.travelTop = data
    },
    tagTop: (state, data) => {
        state.tagTop = data
    }
}


export default {
    namespaced: true,
    state,
    getters,
    mutations
}