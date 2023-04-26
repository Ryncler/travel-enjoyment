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
    }
    return true
}

export function getAllParentArr(list, id) {
    for (let i in list) {
        if (list[i].id === id) {
            //查询到返回该数组对象
            return [list[i]];
        }
        if (list[i].children) {
            let node = getAllParentArr(list[i].children, id);
            if (node !== undefined) {
                //查询到把父节点连起来
                return node.concat(list[i]);
            }
        }
    }
}

export function getImageByDoc(doc) {
    var div = document.createElement('div');
    div.innerHTML = doc;
    var imgs = div.getElementsByTagName("img")
    if (imgs.length > 0)
        return imgs[0].getAttribute("src")
    return ''
}

export function Match(value) {
    if (value.match(/<b>(.*)<\/b>/) !== null && value.match(/<b>(.*)<\/b>/).length > 0) {
        var b = value.match(/<b>(.*?)<\/b>/)[1]
        if (b.includes('<') || b.includes('>'))
            return ''
        return value.match(/<b>(.*?)<\/b>/)[1]
    }


    if (value.match(/<p>(.*)<\/p>/) !== null && value.match(/<p>(.*)<\/p>/).length > 0) {
        var p = value.match(/<p>(.*?)<\/p>/)[1]
        if (p.includes('<') || p.includes('>'))
            return ''
        return value.match(/<p>(.*?)<\/p>/)[1]
    }
}