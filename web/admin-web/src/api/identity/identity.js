import request from '@/utils/axios/request'
import qs from 'qs'
import store from '@/store'

//登录
export function login(data) {
    var req = {
        username: data.username,
        password: data.password,
        client_id: process.env.VUE_APP_ClientId,
        client_secret: process.env.VUE_APP_ClientSercet,
        grant_type: process.env.VUE_APP_GrantType,
        scope: process.env.VUE_APP_Scopes
    }
    return request.post('/connect/token', qs.stringify(req), {
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        }
    })
}

//获取用户信息
export function getInfo() {
    return request.get('/connect/userinfo', {
        headers: {
            'Authorization': store.getters.getToken
        }
    })
}

//退出登录
export function logout() {
    return request.get('/connect/logout', {
    })
}
