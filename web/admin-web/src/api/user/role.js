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
        url: '/api/identity/roles/' + id,
        method: 'put',
        data: data,
        urlType: '',
    })
}


export function deleteRole(id) {
    return request({
        url: '/api/identity/roles/' + id,
        method: 'delete',
        urlType: '',
    })
}


export function getRoles(params) {
    return request({
        url: '/api/identity/roles',
        method: 'get',
        params: params,
        urlType: '',
    })
}

export function getAllRoles() {
    return request({
        url: '/api/identity/roles/all',
        method: 'get',
        urlType: '',
    })
}

export function addAllToAdmin() {
    return request({
        url: '/api/permission-manage/all-to-admin',
        method: 'get',
        urlType: '',
    })
}