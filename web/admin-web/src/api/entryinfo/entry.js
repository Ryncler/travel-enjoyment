import request from '@/utils/axios/request'

export function getStatus() {
    return request({
        url: '/api/entryinfo-manage/get-status',
        method: 'get',
        urlType: '',
    })
}

export function getAllEntryInfo(params) {
    return request({
        url: '/api/entryinfo-manage/all',
        method: 'get',
        params: params,
        urlType: '',
    })
}


export function editEntryInfo(data) {
    return request({
        url: '/api/entryinfo-manage/update-name-code',
        method: 'post',
        data: data,
        urlType: '',
    })
}
