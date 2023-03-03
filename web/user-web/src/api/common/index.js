import request from '@/utils/axios/request'

export function getGeoTree() {
    return request({
        url: '/api/geo-manage/get-tree',
        method: 'get',
        urlType: 'common',
    })
}

export function getCategoryTree(params) {
    return request({
        url: '/api/category-manage/get-tree',
        method: 'get',
        params: params,
        urlType: 'common',
    })
}


export function addCategory(data) {
    return request({
        url: '/api/category-manage/create',
        method: 'post',
        data: data,
        urlType: 'common',
    })
}


export function editCategory(id, data) {
    return request({
        url: '/api/category-manage/update',
        method: 'post',
        params: { id: id },
        data: data,
        urlType: 'common',
    })
}

export function deleteCategory(id) {
    return request({
        url: '/api/category-manage/delete',
        method: 'post',
        params: { id: id },
        urlType: 'common',
    })
}

export function addTag(data) {
    return request({
        url: '/api/tag-manage/create',
        method: 'post',
        data: data,
        urlType: 'common',
    })
}


export function editTag(id, data) {
    return request({
        url: '/api/tag-manage/update',
        method: 'post',
        params: { id: id },
        data: data,
        urlType: 'common',
    })
}

export function deleteTag(id) {
    return request({
        url: '/api/tag-manage/delete',
        method: 'post',
        params: { id: id },
        urlType: 'common',
    })
}

export function getHotTopByType(type) {
    return request({
        url: '/api/hottop-manage/get-by-type',
        method: 'get',
        params: { topType: type },
        urlType: 'common',
    })
}

export function getStarTravel(id) {
    return request({
        url: 'api/user-manage/all-by-id',
        method: 'get',
        params: { id: id },
        urlType: 'common',
    })
}

export function addStarTravel(data) {
    return request({
        url: 'api/user-manage/create',
        method: 'post',
        data: data,
        urlType: 'common',
    })
}

export function deleteStarTravel(id) {
    return request({
        url: 'api/user-manage/delete',
        method: 'post',
        params: { id: id },
        urlType: 'common',
    })
}