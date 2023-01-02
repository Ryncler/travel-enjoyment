//主要用来操作window.sessionStorage的一些函数
const win = window.sessionStorage
export default {
    getItem(key) {
        try {
            return JSON.parse(win.getItem(key))
        } catch (err) {
            return null
        }
    },
    setItem(key, val) {
        win.setItem(key, JSON.stringify(val))
    },
    clear() {
        win.clear()
    },
    keys() {
        return win.keys()
    },
    removeItem(key) {
        win.removeItem(key)
    }
}