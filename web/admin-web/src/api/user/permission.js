import request from '@/utils/axios/request'

export function getPermissions(params) {
    return request({
        url: '/api/permission/permissions',
        method: 'get',
        params: params,
        urlType: '',
    })
}

export function updateRolePermission(params, data) {
    return request({
        url: '/api/permission/permissions',
        method: 'put',
        params: params,
        data: { 'permissions': data },
        urlType: '',
    })
}