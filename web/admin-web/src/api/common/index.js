import request from '@/utils/axios/request'

export function getGeoTree(){
    return request({
        url: '/api/geo-manage/get-tree',
        method: 'get',
        urlType: 'common',
    })
}