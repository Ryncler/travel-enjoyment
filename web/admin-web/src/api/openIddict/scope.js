import request from '@/utils/axios/request'

export function getScopes(){
    return request({
        url: '/api/BaseService/openiddictmanage/getallscope',
        method: 'get',
        urlType: '',
    })
}