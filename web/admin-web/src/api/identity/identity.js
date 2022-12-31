import axios from 'axios'
import qs from 'qs'
import store from '@/store'

const service = axios.create({
    baseURL: process.env.VUE_APP_AuthServiceURL,
    timeout: 5000
})

//登录
export function login(data) {
    var request = {
        username: data.username,
        password: data.password,
        client_id: process.env.VUE_APP_ClientId,
        client_secret: process.env.VUE_APP_ClientSercet,
        grant_type: process.env.VUE_APP_GrantType,
        scope: process.env.VUE_APP_Scopes
    }
    return service.post('/connect/token', qs.stringify(request), {
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        }
    })
}

//获取用户信息
export function getInfo() {
    return service.get('/connect/userinfo', {
        headers: {
            'Authorization': store.getters.getToken
        }
    })
}

//退出登录
export function logout() {
    return service.get('/connect/logout', {
    })
}
