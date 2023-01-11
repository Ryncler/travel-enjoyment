import { createStore } from 'vuex'
import identity from './modules/identity'
import breadcrumb from './modules/breadcrumb'

export default createStore({
  modules: {
    identity,
    breadcrumb
  },
})
