import request from '@/utils/axios/request'

export function getCommentTree(params) {
    return request({
        url: '/api/comment/get-tree-by-travel',
        method: 'get',
        params: params,
        urlType: '',
    })
}


export function getCommentList(id) {
    return request({
        url: '/api/comment/all-by-comment',
        method: 'get',
        params: { id: id },
        urlType: '',
    })
}

export function addComment(data) {
    return request({
        url: '/api/comment/create',
        method: 'post',
        data: data,
        urlType: '',
    })
}