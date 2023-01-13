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
        url: '/api/user-manage/register-entry',
        method: 'post',
        data: data,
        urlType: '',
    })
}

export function addUser(data) {
    return request({
        url: '/api/user-manage/create',
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


export function deleteUser(id) {
    return request({
        url: '/api/user-manage/delete',
        method: 'post',
        params: { id: id },
        urlType: '',
    })
}


export function getAllUser(params) {
    return request({
        url: '/api/user-manage/all',
        method: 'get',
        params: params,
        urlType: '',
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

export function updatePassword(data) {
    return request({
        url: '/api/user-manage/update-password',
        method: 'post',
        data: data,
        urlType: '',
    })
}
