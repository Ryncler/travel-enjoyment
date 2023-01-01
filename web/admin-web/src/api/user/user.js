import http from "@/utils/axios/http";

export function register(data) {
    var params = {
        username: data.username,
        password: data.password,
        emailAddress: data.email,
        appName: process.env.VUE_APP_ClientId
    }
    return http.post(`/api/account/register`, params)
}
