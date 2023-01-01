import { ElMessage } from 'element-plus'

export function checkErrorCode(error) {
    if (error.response.data.error.code.includes('DuplicateUserName')) {
        ElMessage.error(`${error.message},错误：已有相同的用户名`)
        return true
    }
    if (error.response.data.error.code.includes('DuplicateEmail')) {
        ElMessage.error(`${error.message},错误：该邮箱已被使用`)
        return true
    }
    return false
}