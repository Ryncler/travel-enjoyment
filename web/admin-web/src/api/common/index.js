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

export function getTagListByName(name) {
    return request({
        url: '/api/tag-manage/all-by-name',
        method: 'get',
        params: { name: name },
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

export function addHotTop(data) {
    return request({
        url: '/api/hottop-manage/create',
        method: 'post',
        data: data,
        urlType: 'common',
    })
}

export function getRecentHotSights() {
    return request({
        url: '/api/dashboard-manage/get-recent-hot-sights',
        method: 'get',
        urlType: 'common',
    })
}

export function getRecentHotTags() {
    return request({
        url: '/api/dashboard-manage/get-recent-hot-tags',
        method: 'get',
        urlType: 'common',
    })
}

export function getNewlyInfo() {
    return request({
        url: '/api/dashboard-manage/get-newly-info',
        method: 'get',
        urlType: 'common',
    })
}

export function getRecentHotTravel() {
    return request({
        url: '/api/dashboard-manage/get-recent-hot-travel',
        method: 'get',
        urlType: 'common',
    })
}

export function getRecentHotUser() {
    return request({
        url: '/api/dashboard-manage/get-recent-hot-user',
        method: 'get',
        urlType: 'common',
    })
}