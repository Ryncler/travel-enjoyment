import request from '@/utils/axios/request'

export function getAll(params) {
    return request({
        url: '/api/travels-manage/all',
        method: 'get',
        params: params,
        urlType: 'sights',
    })
}
