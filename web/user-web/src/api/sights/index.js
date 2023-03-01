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

export function getTagIdListBySightsId(id) {
    return request({
        url: '/api/sights-tag-manage/all-by-sightsid',
        method: 'get',
        params: { id: id },
        urlType: 'sights',
    })
}