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
                    router.push({ name: 'EditUserInfo' })
                    return
                }
                if (path.find(x => x === '1-3')) {
                    store.commit('identity/removeAny')
                    logout()
                    router.go(0)
                    return
                }
                break;
            case 'aside':
                if (path.find(x => x === '1-1')) {
                    router.push({ name: 'System' })
                    return
                }
                if (path.find(x => x === '1-2')) {
                    router.push({ name: 'RoleManage' })
                    return
                }
                if (path.find(x => x === '1-3')) {
                    router.push({ name: 'EntryManage' })
                    return
                }
                if (path.find(x => x === '2-1')) {
                    router.push({ name: 'OpenIddict' })
                    return
                }
                if (path.find(x => x === '2-2')) {
                    router.push({ name: 'ScopeManage' })
                    return
                }
                if (path.find(x => x === '3-1')) {
                    router.push({ name: 'Logging' })
                    return
                }
                break;
            default:
                break;
        }
    }
}