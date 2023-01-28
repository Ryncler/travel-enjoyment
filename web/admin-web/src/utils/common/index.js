import { getAllRoles } from '@/api/user/role'
import store from '@/store';

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

String.prototype.GetIndexOfByNum = function (str, num) {
    var result = 0
    var index = this.indexOf(str)
    var count = 0
    while (index !== -1) {
        if (count == num) {
            result = this.indexOf(str, index)
            break;
        }
        count++
        index = this.indexOf(str, index + 1)
    }
    return this.substr(0, result)
}

export function imageHandle(img) {
    if (img !== undefined && img !== null) {
        if (img.includes('blob'))
            return img
        var minioIP = process.env.VUE_APP_MinioURL
        var bucketName = img.GetIndexOfByNum('-', 2)
        return `${minioIP}/${bucketName}/${img}`
    }
}

export function isAdmin() {
    const role = store.getters['identity/roles']
    if (role === undefined || role === null || role === [] || role.length <= 0) {
        return false
    }
    if (role.indexOf('admin') < 0 || role.indexOf('user') !== -1) {
        return false
    }console.log(154623);
    return true
}