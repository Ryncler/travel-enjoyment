import { getAllRoles } from '@/api/user/role'

export function getRoles() {
    return getAllRoles().then(res => {
        if (res.status === 200) {
            return res.data.items.map((item) => {
                return item.name
            })
        } else
            return
    })
}


export function isNull(item) {
    if (item === '' || item === undefined || item === null || item.length <= 0 || item === {} || Object.keys(item).length <= 0) {
        return '-'
    } else {
        return item
    }
}