import request from '@/utils/axios/request'

export function addScope(data) {
    return request({
        url: '/api/openiddict-manage/create-scope',
        method: 'post',
        data: data,
        urlType: '',
    })
}

export function editScope(id, data) {
    return request({
        url: '/api/openiddict-manage/update-scope',
        method: 'post',
        params: { id: id },
        data: data,
        urlType: '',
    })
}


export function getAllScopes(params) {
    return request({
        url: '/api/openiddict-manage/all-scope',
        method: 'get',
        params: params,
        urlType: '',
    })
}


export function deleteScope(id) {
    return request({
        url: '/api/openiddict-manage/delete-scope',
        method: 'post',
        params: { id: id },
        urlType: '',
    })
}
