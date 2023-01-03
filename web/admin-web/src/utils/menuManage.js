import store from '@/store'
import router from '@/router/index'
import { logout } from '@/api/identity/identity'

export default {
    linkRouterByPath(layoutType, path) {
        switch (layoutType) {
            case 'header':
                if (path.find(x => x === '0') || path.find(x => x === '1-1')) {
                    router.push({ name: 'Home' })
                    return
                }
                if (path.find(x => x === '1-2')) {
                    router.push({ name: 'userInfo' })
                    return
                }
                if (path.find(x => x === '1-3')) {
                    store.commit('removeAny')
                    logout()
                    router.go(0)
                    return
                }
                break;
            case 'aside':
                if (path.find(x => x === '1-1')) {
                    console.log(111);
                    router.push({ name: 'System' })
                    return
                }
                break;
            default:
                break;
        }
    }
}