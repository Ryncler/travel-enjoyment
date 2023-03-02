import request from '@/utils/axios/request'

export function getByActivityId(id) {
    return request({
        url: '/api/sights-manage/get-by-activity',
        method: 'get',
        params: { id: id },
        urlType: 'sights',
    })
}

export function getSightsBySearch(params){
    return request({
        url: '/api/sights-manage/get-by-search',
        method: 'get',
        params: params,
        urlType: 'sights',
    })
}

export function getAll(params) {
    return request({
        url: '/api/sights-manage/all',
        method: 'get',
        params: params,
        urlType: 'sights',
    })
}

export function getAllByCreateId(params) {
    return request({
        url: '/api/sights-manage/all-by-createid',
        method: 'get',
        params: params,
        urlType: 'sights',
    })
}

export function addSights(data) {
    return request({
        url: '/api/sights-manage/create',
        method: 'post',
        data: data,
        urlType: 'sights',
    })
}


export function editSights(id, data) {
    return request({
        url: '/api/sights-manage/update',
        method: 'post',
        params: { id: id },
        data: data,
        urlType: 'sights',
    })
}

export function deleteSights(id) {
    return request({
        url: '/api/sights-manage/delete',
        method: 'post',
        params: { id: id },
        urlType: 'sights',
    })
}