import request from '@/utils/axios/request'

export function getScopes(){
    return request({
        url: '/api/BaseService/openiddictmanage/getallscope',
        method: 'get',
        urlType: '',
    })
}


export function addScope(data) {
    return request({
        url: '/api/BaseService/openiddictmanage/createscope',
        method: 'post',
        data: data,
        urlType: '',
    })
}

export function editScope(id, data) {
    return request({
        url: '/api/BaseService/openiddictmanage/updatescope',
        method: 'post',
        params: { id: id },
        data: data,
        urlType: '',
    })
}


export function getAllScopes(params) {
    return request({
        url: '/api/BaseService/openiddictmanage/getallscope',
        method: 'get',
        params: params,
        urlType: '',
    })
}


export function deleteScope(id) {
    return request({
        url: '/api/BaseService/openiddictmanage/deletescope',
        method: 'post',
        params: { id: id },
        urlType: '',
    })
}
