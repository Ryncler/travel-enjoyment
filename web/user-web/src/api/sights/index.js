import request from '@/utils/axios/request'
import qs from 'qs'

export function getTagList(){
    return request({
        url: '/api/user-manage/update',
        method: 'get',
        urlType: '',
    })
}