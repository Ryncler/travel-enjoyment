import request from '@/utils/axios/request'

export function getAll(params) {
    return request({
        url: '/api/activity-manage/all',
        method: 'get',
        params: params,
        urlType: 'sights',
    })
}

export function getAllByCreateId(params) {
    return request({
        url: '/api/activity-manage/all-by-createid',
        method: 'get',
        params: params,
        urlType: 'sights',
    })
}

export function addActivity(data) {
    return request({
        url: '/api/activity-manage/create',
        method: 'post',
        data: data,
        urlType: 'sights',
    })
}


export function editActivity(id, data) {
    return request({
        url: '/api/activity-manage/update',
        method: 'post',
        params: { id: id },
        data: data,
        urlType: 'sights',
    })
}

export function deleteActivity(id) {
    return request({
        url: '/api/activity-manage/delete',
        method: 'post',
        params: { id: id },
        urlType: 'sights',
    })
}

export function saveSightsActivity(data) {
    return request({
        url: '/api/sights-activity-manage/create',
        method: 'post',
        data: data,
        urlType: 'sights',
    })
}