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