import { createStore } from 'vuex'
import identity from './modules/identity'
import menu from './modules/menu'

export default createStore({
    modules: {
        identity,
        menu
    },
})