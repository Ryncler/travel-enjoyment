import { ElMessage } from 'element-plus'

export function checkErrorCode(error) {
    if (error.data !== '' && error.data.error.code !== null) {
        if (error.data.error.code.includes('DuplicateUserName')) {
            ElMessage.error(`服务器端出错,错误：已有相同的用户名`)
            return true
        }
        if (error.data.error.code.includes('DuplicateEmail')) {
            ElMessage.error(`服务器端出错,错误：该邮箱已被使用`)
            return true
        }
    }
    return false
}