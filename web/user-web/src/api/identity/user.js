import request from '@/utils/axios/request'

export function getUser(id) {
    return request({
        url: '/api/user/get',
        method: 'get',
        params: { id: id },
        urlType: '',
    })
}

export function getUserName(id) {
    return request({
        url: '/api/user-get/get-name',
        method: 'get',
        params: { id: id },
        urlType: '',
    })
}

export function updateUser(data) {
    return request({
        url: '/api/user/update',
        method: 'post',
        params: { id: data.id },
        data: data,
        urlType: '',
    })
}


export function updatePassword(data) {
    return request({
        url: '/api/user/update-password',
        method: 'post',
        data: data,
        urlType: '',
    })
}

export function deleteUser(id) {
    return request({
        url: '/api/user/delete',
        method: 'post',
        params: { id: id },
        urlType: '',
    })
}

export function getStarCountByUserId(id) {
    return request({
        url: '/api/user-travel-get/get-count-by-user',
        method: 'get',
        params: { id: id },
        urlType: '',
    })
}

export function getTravelCountByUserId(id) {
    return request({
        url: '/api/travels/get-count-by-user',
        method: 'get',
        params: { id: id },
        urlType: '',
    })
}

export function addChoiceTravel(data) {
    return request({
        url: '/api/user-choice-travel/create',
        method: 'post',
        data: data,
        urlType: '',
    })
}

export function updateChoiceTravel(data) {
    return request({
        url: '/api/user-choice-travel/update',
        method: 'post',
        params: { id: data.id },
        data: data,
        urlType: '',
    })
}

export function getChoiceTravel(id) {
    return request({
        url: '/api/user-choice-travel/get-by-user',
        method: 'get',
        params: { id: id },
        urlType: '',
    })
}

export function getUserTrends(params) {
    return request({
        url: '/api/user-trends/all-by-user',
        method: 'get',
        params: params,
        urlType: '',
    })
}