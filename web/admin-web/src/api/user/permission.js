import request from '@/utils/axios/request'
import qs from 'qs'

export function getPermissions(params) {
    return request({
        url: '/api/permission-management/permissions',
        method: 'get',
        params: params,
        urlType: '',
    })
}

export function updateRolePermission(params, data) {
    return request({
        url: '/api/permission-management/permissions',
        method: 'put',
        params: params,
        data: { 'permissions': data },
        urlType: '',
    })
}