import { createStore } from 'vuex'
import identity from './modules/identity'
import menu from './modules/menu'
import common from './modules/common'

export default createStore({
    modules: {
        identity,
        menu,
        common
    },
})