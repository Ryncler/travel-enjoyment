import request from '@/utils/axios/request'

export function getGeoTree() {
    return request({
        url: '/api/geo/get-tree',
        method: 'get',
        urlType: '',
    })
}

export function getCategoryTree(params) {
    return request({
        url: '/api/category/get-tree',
        method: 'get',
        params: params,
        urlType: '',
    })
}


export function addCategory(data) {
    return request({
        url: '/api/category/create',
        method: 'post',
        data: data,
        urlType: '',
    })
}


export function editCategory(id, data) {
    return request({
        url: '/api/category/update',
        method: 'post',
        params: { id: id },
        data: data,
        urlType: '',
    })
}

export function deleteCategory(id) {
    return request({
        url: '/api/category/delete',
        method: 'post',
        params: { id: id },
        urlType: '',
    })
}

export function addTag(data) {
    return request({
        url: '/api/tag/create',
        method: 'post',
        data: data,
        urlType: '',
    })
}


export function editTag(id, data) {
    return request({
        url: '/api/tag/update',
        method: 'post',
        params: { id: id },
        data: data,
        urlType: '',
    })
}

export function deleteTag(id) {
    return request({
        url: '/api/tag/delete',
        method: 'post',
        params: { id: id },
        urlType: '',
    })
}

export function getHotTopByType(type) {
    return request({
        url: '/api/hottop/get-by-type',
        method: 'get',
        params: { topType: type },
        urlType: '',
    })
}

export function addHotTop(data) {
    return request({
        url: '/api/hottop/create',
        method: 'post',
        data: data,
        urlType: '',
    })
}


export function getStarTravel(params) {
    return request({
        url: '/api/user-travel/all-by-user',
        method: 'get',
        params: params,
        urlType: '',
    })
}

export function addStarTravel(data) {
    return request({
        url: '/api/user-travel/create',
        method: 'post',
        data: data,
        urlType: '',
    })
}

export function deleteStarTravel(data) {
    return request({
        url: '/api/user-travel/delete-by-id',
        method: 'post',
        params: data,
        urlType: '',
    })
}

export function exitsUserTravel(data) {
    return request({
        url: '/api/user-travel/exits-by-id',
        method: 'post',
        params: data,
        urlType: '',
    })
}