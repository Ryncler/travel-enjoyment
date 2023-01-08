import { ElMessage } from 'element-plus'

export function checkErrorCode(error) {
    console.log(error);
    if (error.data !== '' && error.data.error.code !== null && error.data.error.code !== undefined) {
        ElMessage.error(error.data.error.message)
        return true
    }
    else if (error.data !== '' && error.data.error_description !== null && error.data.error_description !== undefined) {
        if (error.data.error_description.includes('Invalid username')) {
            ElMessage.error(`请求失败,错误：用户名或密码错误`)
            return true
        }
    }
    return false
}