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