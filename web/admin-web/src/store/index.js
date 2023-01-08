import { createStore } from 'vuex'
import identity from './modules/identity'

export default createStore({
  modules: {
    identity
  },
})
