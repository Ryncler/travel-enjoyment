import request from '@/utils/axios/request'
import qs from 'qs'

export function upload(data) {
    return request({
        url: '/api/minio-manage/upload',
        method: 'post',
        data: data,
        urlType: 'upload',
        headers: { 'Content-Type': 'multipart/form-data' }
    })
}

export function addImage(data) {
    return request({
        url: '/api/image-manage/create',
        method: 'post',
        data: data,
        urlType: 'upload',
    })
}

export function updateImage(id, data) {
    return request({
        url: '/api/image-manage/update',
        method: 'post',
        params: { id: id },
        data: data,
        urlType: 'upload',
    })
}

export function deleteImage(id) {
    return request({
        url: '/api/image-manage/delete',
        method: 'post',
        params: { id: id },
        urlType: 'upload',
    })
}

export function getImagesById(id) {
    return request({
        url: '/api/image-manage/all-by-linkid',
        method: 'get',
        params: { linkid: id },
        urlType: 'upload',
    })
}

export function getImageListById(params) {
    return request({
        url: '/api/image-manage/all-page-by-linkid',
        method: 'get',
        params: params,
        urlType: 'upload',
    })
}


export function addImages(data) {
    return request({
        url: '/api/image-manage/create-many',
        method: 'post',
        data: data,
        urlType: 'upload',
    })
}

export function deleteObj(data) {
    return request({
        url: '/api/minio-manage/delete',
        method: 'post',
        data: data,
        urlType: 'upload',
    })
}