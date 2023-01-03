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
    return request({
        url: '/connect/token',
        method: 'post',
        data: qs.stringify(req),
        urlType: 'auth',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        }
    })
}

//获取用户信息
export function getInfo() {
    return request({
        url: '/connect/userinfo',
        method: 'post',
        data: '',
        urlType: 'auth',
        headers: {
            'Authorization': store.getters.getToken
        }
    })
}

//退出登录
export function logout() {
    return request({
        url: '/connect/logout',
        method: 'get',
        urlType: 'auth',
        headers: {
            'Authorization': store.getters.getToken
        }
    })
}
