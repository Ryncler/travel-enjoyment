import request from '@/utils/axios/request'

export function getCommentTree(params) {
    return request({
        url: '/api/comment-manage/get-tree-by-travel',
        method: 'get',
        params: params,
        urlType: 'sights',
    })
}


export function getCommentList(id) {
    return request({
        url: '/api/comment-manage/all-by-comment',
        method: 'get',
        params: { id: id },
        urlType: 'sights',
    })
}

export function addComment(data) {
    return request({
        url: '/api/comment-manage/create',
        method: 'post',
        data: data,
        urlType: 'sights',
    })
}