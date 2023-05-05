import request from '@/utils/axios/request'

export function getAll(params) {
    return request({
        url: '/api/travels/all',
        method: 'get',
        params: params,
        urlType: '',
    })
}

export function getTravel(id) {
    return request({
        url: '/api/travels/get',
        method: 'get',
        params: { id: id },
        urlType: '',
    })
}

export function addTravel(data) {
    return request({
        url: '/api/travels/create',
        method: 'post',
        data: data,
        urlType: '',
    })
}

export function deleteTravels(id) {
    return request({
        url: '/api/travels/delete',
        method: 'post',
        params: { id: id },
        urlType: '',
    })
}

export function getCommentCountByTravelId(id) {
    return request({
        url: '/api/comment/get-count',
        method: 'get',
        params: { id: id },
        urlType: '',
    })
}

export function getStarCountByTravelId(id) {
    return request({
        url: '/api/user-travel/get-count-by-travel',
        method: 'get',
        params: { id: id },
        urlType: '',
    })
}

export function getTravelsByUserId(params) {
    return request({
        url: '/api/travels/all-by-createid',
        method: 'get',
        params: params,
        urlType: '',
    })
}