import request from '@/utils/axios/request'

export function addApplication(data) {
    return request({
        url: '/api/openiddict/create-application',
        method: 'post',
        data: data,
        urlType: '',
    })
}

export function editApplication(id, data) {
    return request({
        url: '/api/openiddict/update-application',
        method: 'post',
        params: { id: id },
        data: data,
        urlType: '',
    })
}


export function getAllApplications(params) {
    return request({
        url: '/api/openiddict/all-application',
        method: 'get',
        params: params,
        urlType: '',
    })
}


export function deleteApplication(id) {
    return request({
        url: '/api/openiddict/delete-application',
        method: 'post',
        params: { id: id },
        urlType: '',
    })
}

export function getGrantTypes() {
    return request({
        url: '/api/openiddict/get-grant-types',
        method: 'get',
        urlType: '',
    })
}

export function getClientTypes() {
    return request({
        url: '/api/openiddict/get-client-types',
        method: 'get',
        urlType: '',
    })
}

export function getConsentTypes() {
    return request({
        url: '/api/openiddict/get-consent-types',
        method: 'get',
        urlType: '',
    })
}