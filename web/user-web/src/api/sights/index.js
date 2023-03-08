import request from '@/utils/axios/request'
import qs from 'qs'

export function getTagInfo(id) {
    return request({
        url: '/api/tag-manage/get',
        method: 'get',
        params: { id: id },
        urlType: 'common',
    })
}

export function getTagList(ids) {
    return request({
        url: '/api/tag-manage/all-by-ids',
        method: 'post',
        data: ids,
        urlType: 'common',
    })
}

export function getSightsList(ids) {
    return request({
        url: '/api/sights-manage/all-by-ids',
        method: 'post',
        data: ids,
        urlType: 'sights',
    })
}

export function getSightsBySearch(params) {
    return request({
        url: '/api/sights-manage/get-by-search',
        method: 'get',
        params: params,
        urlType: 'sights',
    })
}


export function getActivityList(ids) {
    return request({
        url: '/api/activity-manage/all-by-ids',
        method: 'post',
        data: ids,
        urlType: 'sights',
    })
}


export function getTravelList(ids) {
    return request({
        url: '/api/travels-manage/all-by-ids',
        method: 'post',
        data: ids,
        urlType: 'sights',
    })
}


export function getTagIdListBySightsId(id) {
    return request({
        url: '/api/sights-tag-manage/all-by-sightsid',
        method: 'get',
        params: { id: id },
        urlType: 'sights',
    })
}

export function getActivityIdListBySightsId(id) {
    return request({
        url: '/api/sights-activity-manage/all-by-sightsid',
        method: 'get',
        params: { id: id },
        urlType: 'sights',
    })
}

export function getTravelIdListBySightsId(id) {
    return request({
        url: '/api/sights-travels-manage/all-by-sightsid',
        method: 'get',
        params: { id: id },
        urlType: 'sights',
    })
}

export function getSights(id) {
    return request({
        url: '/api/sights-manage/get',
        method: 'get',
        params: { id: id },
        urlType: 'sights',
    })
}

export function getSightsByGeo(id) {
    return request({
        url: '/api/sights-manage/all-by-geo',
        method: 'get',
        params: { id: id },
        urlType: 'sights',
    })
}

export function addSightsTravel(data) {
    return request({
        url: '/api/sights-travels-manage/create',
        method: 'post',
        data: data,
        urlType: 'sights',
    })
}