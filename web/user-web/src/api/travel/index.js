import request from '@/utils/axios/request'

export function getAll(params) {
    return request({
        url: '/api/travels-manage/all',
        method: 'get',
        params: params,
        urlType: 'sights',
    })
}


export function addTravel(data) {
    return request({
        url: '/api/travels-manage/create',
        method: 'post',
        data: data,
        urlType: 'sights',
    })
}