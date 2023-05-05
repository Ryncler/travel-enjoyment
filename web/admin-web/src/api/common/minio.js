import request from '@/utils/axios/request'

export function upload(data) {
    return request({
        url: '/api/minio/upload',
        method: 'post',
        data: data,
        urlType: 'upload',
        headers: { 'Content-Type': 'multipart/form-data' }
    })
}

export function addImage(data) {
    return request({
        url: '/api/image/create',
        method: 'post',
        data: data,
        urlType: 'upload',
    })
}

export function updateImage(id, data) {
    return request({
        url: '/api/image/update',
        method: 'post',
        params: { id: id },
        data: data,
        urlType: 'upload',
    })
}

export function deleteImage(id) {
    return request({
        url: '/api/image/delete',
        method: 'post',
        params: { id: id },
        urlType: 'upload',
    })
}

export function getImagesById(id) {
    return request({
        url: '/api/image/all-by-linkid',
        method: 'get',
        params: { linkid: id },
        urlType: 'upload',
    })
}

export function getImageListById(params) {
    return request({
        url: '/api/image/all-page-by-linkid',
        method: 'get',
        params: params,
        urlType: 'upload',
    })
}


export function addImages(data) {
    return request({
        url: '/api/image/create-many',
        method: 'post',
        data: data,
        urlType: 'upload',
    })
}

export function deleteObj(data) {
    return request({
        url: '/api/minio/delete',
        method: 'post',
        data: data,
        urlType: 'upload',
    })
}