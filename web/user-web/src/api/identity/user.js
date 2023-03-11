import request from '@/utils/axios/request'

export function getUser(id) {
    return request({
        url: '/api/user-manage/get',
        method: 'get',
        params: { id: id },
        urlType: '',
    })
}

export function updateUser(data) {
    return request({
        url: '/api/user-manage/update',
        method: 'post',
        params: { id: data.id },
        data: data,
        urlType: '',
    })
}


export function updatePassword(data) {
    return request({
        url: '/api/user-manage/update-password',
        method: 'post',
        data: data,
        urlType: '',
    })
}

export function deleteUser(id) {
    return request({
        url: '/api/user-manage/delete',
        method: 'post',
        params: { id: id },
        urlType: '',
    })
}

export function getStarCountByUserId(id) {
    return request({
        url: '/api/user-manage/get-count-by-user',
        method: 'get',
        params: { id: id },
        urlType: 'common',
    })
}

export function getTravelCountByUserId(id) {
    return request({
        url: '/api/travels-manage/get-count-by-user',
        method: 'get',
        params: { id: id },
        urlType: 'sights',
    })
}

export function addChoiceTravel(data) {
    return request({
        url: '/api/user-choice-travel-manage/create',
        method: 'post',
        data: data,
        urlType: 'sights',
    })
}

export function updateChoiceTravel(data) {
    return request({
        url: '/api/user-choice-travel-manage/update',
        method: 'post',
        params: { id: data.id },
        data: data,
        urlType: 'sights',
    })
}

export function getChoiceTravel(id){
    return request({
        url: '/api/user-choice-travel-manage/get-by-user',
        method: 'get',
        params: { id: id },
        urlType: 'sights',
    })
}