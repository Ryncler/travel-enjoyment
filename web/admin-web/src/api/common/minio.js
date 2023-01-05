import request from '@/utils/axios/request'

export function upload(data) {
    return request({
        url: '/upload',
        method: 'post',
        data: data,
        urlType: '',
        header: 'multipart/form-data'
    })
}