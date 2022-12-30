import request from '@/utils/request'
import axios from 'axios'
import qs from 'qs'

const service = axios.create({
  baseURL: process.env.VUE_APP_AuthServiceURL,
  timeout: 5000
})
export function login(data) {
  var request = {
    username: data.username,
    password: data.password,
    client_id: process.env.VUE_APP_ClientId,
    client_secret: process.env.VUE_APP_ClientSercet,
    grant_type: process.env.VUE_APP_GrantType,
    scope: process.env.VUE_APP_Scopes
  }
  return service.post('/connect/token', qs.stringify(request), {
    headers: {
      'Content-Type': 'application/x-www-form-urlencoded'
    }
  })
}

export function register(data) {
  return request({
    url: '/api/account/register',
    method: 'post',
    data
  })
}

export function getInfo(token) {
  return service.get('/connect/userinfo', {
    headers: {
      'Authorization': token
    }
  })
}

export function logout() {
  return service.get('/connect/logout', {
  })
}
