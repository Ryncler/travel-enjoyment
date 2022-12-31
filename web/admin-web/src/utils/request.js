import axios from 'axios'
import store from '@/store'
import { message } from 'element-plus'

const service = axios.create({
    baseURL: process.env.VUE_APP_BASE_API,
    timeout: 5000
})

service.interceptors.request.use(
    config => {
        var token = store.getters.getToken
        if (token !== '') {
            config.headers['Authorization'] = token
        }
        return config
    },
    error => {
        message.error(error)
        return error
    }
)

service.interceptors.response.use(
    response => {
        const res = response.data
        if (res.code !== 200) {
            message({
                message: res.message || res.error,
                type: 'error',
                duration: 5 * 1000
            })
        }
        return res
    },
    error => {
        message.error(error)
        return error
    }
)

export default service