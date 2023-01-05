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


export function addUser(data) {
    return request({
        url: '/adduser',
        method: 'post',
        data: data,
        urlType: '',
    })
}
