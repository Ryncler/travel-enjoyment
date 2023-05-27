import request from '@/utils/axios/request'

export function upload(data) {
    return request({
        url: '/api/minio/upload',
        method: 'post',
        data: data,
        urlType: '',
        headers: { 'Content-Type': 'multipart/form-data' }
    })
}

export function getImagesById(id) {
    return request({
        url: '/api/image-get/all-by-linkid',
        method: 'get',
        params: { linkid: id },
        urlType: '',
    })
}