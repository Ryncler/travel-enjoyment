import request from '@/utils/axios/request'

export function register(data) {
    var params = {
        username: data.username,
        password: data.password,
        emailAddress: data.email,
        appName: process.env.VUE_APP_ClientId
    }
    return request({
        url: '/api/account/register',
        method: 'post',
        data: params,
        urlType: '',
    })
}

export function registerByEntry(data) {
    return request({
        url: '/api/register/register-entry',
        method: 'post',
        data: data,
        urlType: '',
    })
}

export function addUser(data) {
    return request({
        url: '/api/user/create',
        method: 'post',
        data: data,
        urlType: '',
    })
}

export function editUser(id, data) {
    return request({
        url: '/api/user/update',
        method: 'post',
        params: { id: id },
        data: data,
        urlType: '',
    })
}


export function deleteUser(id) {
    return request({
        url: '/api/user/delete',
        method: 'post',
        params: { id: id },
        urlType: '',
    })
}


export function getAllUser(params) {
    return request({
        url: '/api/user/all',
        method: 'get',
        params: params,
        urlType: '',
    })
}


export function getUser(id) {
    return request({
        url: '/api/user/get',
        method: 'get',
        params: { id: id },
        urlType: '',
    })
}

export function updatePassword(data) {
    return request({
        url: '/api/user/update-password',
        method: 'post',
        data: data,
        urlType: '',
    })
}


export function getUserEntryInfos(params) {
    return request({
        url: '/api/user/get-entrys',
        method: 'get',
        params: params,
        urlType: '',
    })
}

export function verifyApply(data) {
    return request({
        url: '/api/user/verify',
        method: 'post',
        data: data,
        urlType: '',
    })
}