import request from '@/utils/axios/request'
import qs from 'qs'

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
    })
}

export function getUser(id) {
    return request({
        url: '/api/user-manage/get',
        method: 'get',
        params: { id: id },
        urlType: '',
    })
}

//退出登录
export function logout() {
    return request({
        url: '/connect/logout',
        method: 'get',
        urlType: 'auth'
    })
}


export function registerByUser(data) {
    var params = {
        username: data.username,
        password: data.password,
        emailAddress: data.email,
        appName: process.env.VUE_APP_ClientId
    }
    data.appName = process.env.VUE_APP_ClientId
    return request({
        url: '/api/user-manage/register-user',
        method: 'post',
        data: data,
        urlType: '',
    })
}


export function editUser(id, data) {
    return request({
        url: '/api/user-manage/update',
        method: 'post',
        params: { id: id },
        data: data,
        urlType: '',
    })
}


export function updatePassword(data) {
    return request({
        url: '/api/user-manage/update-password',
        method: 'post',
        data: data,
        urlType: '',
    })
}

