import request from '@/utils/axios/request'

export function addApplication(data) {
    return request({
        url: '/api/BaseService/openiddictmanage/createapplication',
        method: 'post',
        data: data,
        urlType: '',
    })
}

export function editApplication(id, data) {
    return request({
        url: '/api/BaseService/openiddictmanage/updateapplication',
        method: 'post',
        params: { id: id },
        data: data,
        urlType: '',
    })
}


export function getAllApplications(params) {
    return request({
        url: '/api/BaseService/openiddictmanage/getallapplication',
        method: 'get',
        params: params,
        urlType: '',
    })
}


export function deleteApplication(id) {
    return request({
        url: '/api/BaseService/openiddictmanage/deleteapplication',
        method: 'post',
        params: { id: id },
        urlType: '',
    })
}

export function getGrantTypes() {
    return request({
        url: '/api/BaseService/openiddictmanage/getgranttypes',
        method: 'get',
        urlType: '',
    })
}

export function getClientTypes() {
    return request({
        url: '/api/BaseService/openiddictmanage/getclienttypes',
        method: 'get',
        urlType: '',
    })
}

export function getConsentTypes() {
    return request({
        url: '/api/BaseService/openiddictmanage/getconsenttypes',
        method: 'get',
        urlType: '',
    })
}