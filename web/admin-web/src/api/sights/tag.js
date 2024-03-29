import request from '@/utils/axios/request'

export function addSightsTags(data) {
    return request({
        url: '/api/sights-tag/create-many',
        method: 'post',
        data: data,
        urlType: 'sights',
    })
}

export function deleteSightsTags(data) {
    return request({
        url: '/api/sights-tag/delete-many',
        method: 'post',
        data: data,
        urlType: 'sights',
    })
}

export function editSightsTags(oldTag, newTag) {
    deleteSightsTags(oldTag).then(res => {
        if (res.status === 204)
            addSightsTags(newTag)
    })
}

export function getTagIdListBySightsId(id) {
    return request({
        url: '/api/sights-tag/all-by-sightsid',
        method: 'get',
        params: { id: id },
        urlType: 'sights',
    })
}