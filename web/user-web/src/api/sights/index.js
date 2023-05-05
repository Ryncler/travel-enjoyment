import request from '@/utils/axios/request'

export function getTagInfo(id) {
    return request({
        url: '/api/tag/get',
        method: 'get',
        params: { id: id },
        urlType: '',
    })
}

export function getTagList(ids) {
    return request({
        url: '/api/tag/all-by-ids',
        method: 'post',
        data: ids,
        urlType: '',
    })
}

export function getSightsList(ids) {
    return request({
        url: '/api/sights/all-by-ids',
        method: 'post',
        data: ids,
        urlType: '',
    })
}

export function getSightsBySearch(params) {
    return request({
        url: '/api/sights/get-by-search',
        method: 'get',
        params: params,
        urlType: '',
    })
}


export function getActivityList(ids) {
    return request({
        url: '/api/activity/all-by-ids',
        method: 'post',
        data: ids,
        urlType: '',
    })
}


export function getTravelList(ids) {
    return request({
        url: '/api/travels/all-by-ids',
        method: 'post',
        data: ids,
        urlType: '',
    })
}


export function getTagIdListBySightsId(id) {
    return request({
        url: '/api/sights-tag/all-by-sightsid',
        method: 'get',
        params: { id: id },
        urlType: '',
    })
}

export function getActivityIdListBySightsId(id) {
    return request({
        url: '/api/sights-activity/all-by-sightsid',
        method: 'get',
        params: { id: id },
        urlType: '',
    })
}

export function getTravelIdListBySightsId(id) {
    return request({
        url: '/api/sights-travels/all-by-sightsid',
        method: 'get',
        params: { id: id },
        urlType: '',
    })
}

export function getSights(id) {
    return request({
        url: '/api/sights/get',
        method: 'get',
        params: { id: id },
        urlType: '',
    })
}

export function getSightsByGeo(id) {
    return request({
        url: '/api/sights/all-by-geo',
        method: 'get',
        params: { id: id },
        urlType: '',
    })
}

export function addSightsTravel(data) {
    return request({
        url: '/api/sights-travels/create',
        method: 'post',
        data: data,
        urlType: '',
    })
}