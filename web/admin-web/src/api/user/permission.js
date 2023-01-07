import request from '@/utils/axios/request'

export function getPermissions() {
    return request({
        url: '/api/permission-management/permissions',
        method: 'get',
        urlType: '',
    })
}