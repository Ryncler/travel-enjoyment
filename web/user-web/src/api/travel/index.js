import request from '@/utils/axios/request'

export function getAll(params) {
    return request({
        url: '/api/travels-manage/all',
        method: 'get',
        params: params,
        urlType: 'sights',
    })
}

export function getTravel(id) {
    return request({
        url: '/api/travels-manage/get',
        method: 'get',
        params: { id: id },
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

export function deleteTravels(id) {
    return request({
        url: '/api/travels-manage/delete',
        method: 'post',
        params: { id: id },
        urlType: 'sights',
    })
}

export function getCommentCountByTravelId(id) {
    return request({
        url: '/api/comment-manage/get-count',
        method: 'get',
        params: { id: id },
        urlType: 'sights',
    })
}

export function getStarCountByTravelId(id) {
    return request({
        url: '/api/user-manage/get-count-by-travel',
        method: 'get',
        params: { id: id },
        urlType: 'common',
    })
}

export function getTravelsByUserId(params) {
    return request({
        url: '/api/travels-manage/all-by-createid',
        method: 'get',
        params: params,
        urlType: 'sights',
    })
}