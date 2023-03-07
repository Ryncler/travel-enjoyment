import request from '@/utils/axios/request'

export function getUser(id) {
    return request({
        url: '/api/user-manage/get',
        method: 'get',
        params: { id: id },
        urlType: '',
    })
}