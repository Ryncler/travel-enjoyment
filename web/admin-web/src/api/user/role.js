import request from '@/utils/axios/request'


export function addRole(data) {
    return request({
        url: '/api/identity/roles',
        method: 'post',
        data: data,
        urlType: '',
    })
}

export function editRole(id, data) {
    return request({
        url: '/api/identity/roles',
        method: 'put',
        params: { id: id },
        data: data,
        urlType: '',
    })
}


export function deleteRole(id) {
    return request({
        url: '/api/identity/roles',
        method: 'delete',
        params: { id: id },
        urlType: '',
    })
}


export function getAllRole(params) {
    return request({
        url: '/api/identity/roles',
        method: 'get',
        params: params,
        urlType: '',
    })
}