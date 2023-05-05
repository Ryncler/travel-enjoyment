import request from '@/utils/axios/request'

export function getStatus() {
    return request({
        url: '/api/entryinfo/get-status',
        method: 'get',
        urlType: '',
    })
}

export function getAllEntryInfo(params) {
    return request({
        url: '/api/entryinfo/all',
        method: 'get',
        params: params,
        urlType: '',
    })
}


export function editEntryInfo(data) {
    return request({
        url: '/api/entryinfo/update-name-code',
        method: 'post',
        data: data,
        urlType: '',
    })
}
