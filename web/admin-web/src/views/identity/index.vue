<template>
  <div class="login-container">
    <div class="left">
      <img class="loginimg" :src="LoginImage.url">
    </div>
    <div class="right">
      <el-form ref="loginForm" :model="loginForm" :rules="loginRules" class="login-form" auto-complete="on"
        label-position="left">
        <div class="title-container">
          <h1 v-if="isRegister" class="title"><em>欢迎您回来</em>...</h1>
          <h1 v-show="!isRegister" class="title"><em>欢迎您加入我们</em>...</h1>
        </div>

        <el-form-item prop="username">
          <icon data="@/icons/user.svg" class="svg-container" />
          <el-input v-model="loginForm.username" placeholder="用户名" name="username" type="text" tabindex="1"
            autocomplete="on" />
        </el-form-item>

        <div v-show="!isRegister">
          <el-form-item prop="email">
            <icon data="@/icons/email.svg" class="svg-container" />
            <el-input ref="email" v-model="loginForm.email" placeholder="邮箱" name="email" type="text" tabindex="2"
              autocomplete="on" />
          </el-form-item>
        </div>

        <el-form-item prop="password">
          <icon data="@/icons/password.svg" class="svg-container" />
          <el-input ref="password" v-model="loginForm.password" :type="password" placeholder="密码" name="password"
            tabindex="3" autocomplete="on" show-password />
        </el-form-item>

        <el-form-item v-show="!isRegister" prop="confimPassword">
          <icon data="@/icons/password.svg" class="svg-container" />
          <el-input ref="confimPassword" v-model="loginForm.confimPassword" :type="password" placeholder="确认密码"
            name="confimPassword" tabindex="4" autocomplete="on" show-password />
        </el-form-item>
        <div v-if="isRegister">
          <el-button :loading="loading" round type="primary" class="revertbtn" @click="goLogin()">登录</el-button>
          <el-button :loading="loading" round type="primary" class="revertbtn" @click="ShowRegister()">注册</el-button>
        </div>
        <div v-show="!isRegister">
          <el-button :loading="loading" round type="primary" class="revertbtn" @click="goRegister()">注册</el-button>
          <el-button :loading="loading" round type="primary" class="revertbtn" @click="ShowLogin()">登录</el-button>
        </div>
      </el-form>
    </div>
  </div>
</template>

<script>
import { login } from '@/api/identity/identity'
import store from '@/store'
import { Elmessage } from 'element-plus'

export default {
  // eslint-disable-next-line vue/multi-word-component-names
  name: 'Login',
  data() {
    const validateUsername = (rule, value, callback) => {
      if (value.length < 2) {
        callback(new Error('请输入您的用户名'))
      } else {
        callback()
      }
    }
    const validatePassword = (rule, value, callback) => {
      if (value.length < 6) {
        callback(new Error('请输入您的密码'))
      } else {
        callback()
      }
    }
    const validateConfimPassword = (rule, value, callback) => {
      if (value.length < 6) {
        callback(new Error('请再次输入您的密码'))
      }
      if (value !== this.loginForm.password) {
        callback(new Error('您输入的密码不一致'))
      } else {
        callback()
      }
    }
    const validateEmail = (rule, value, callback) => {
      // eslint-disable-next-line no-useless-escape
      const regEmail = /^([a-zA-z0-9]+[-_\.]?)+@[a-zA-z0-9]+\.[a-z]+$/
      if (!regEmail.test(value)) {
        callback(new Error('您输入的邮箱不符合规范'))
      } else {
        callback()
      }
    }
    return {
      isRegister: true,
      LoginImage: {
        url: 'https://img-prod-cms-rt-microsoft-com.akamaized.net/cms/api/am/imageFileData/RE4wHgx?ver=5481',
        hrefUrl: 'https://img-prod-cms-rt-microsoft-com.akamaized.net/cms/api/am/imageFileData/RE4wHgx?ver=5481'
      },
      loginForm: {
        username: 'admin',
        email: '1233@qq.com',
        password: 'aA10086.',
        confimPassword: 'aA10086.'
      },
      loginRules: {
        username: [{ required: true, trigger: 'blur', validator: validateUsername }],
        email: [{ required: true, trigger: 'blur', validator: validateEmail }],
        password: [{ required: true, trigger: 'blur', validator: validatePassword }],
        confimPassword: [{ required: true, trigger: 'blur', validator: validateConfimPassword }]
      },
      loading: false,
      redirect: undefined
    }
  },
  watch: {
    $route: {
      handler: function (route) {
        this.redirect = route.query && route.query.redirect
      },
      immediate: true
    }
  },
  methods: {
    goLogin() {
      this.$refs.loginForm.validate(valid => {
        if (valid) {
          this.loading = true
          login(this.loginForm).then(res => {
            store.commit('SetToken', res.access_token)
            Elmessage.success('登录成功！')
            this.loading = false
          }).catch(err => {
            Elmessage({
              message: '登录失败，错误：' + err,
              type: 'error',
            })
            this.loading = false
          })
        } else {
          // eslint-disable-next-line no-undef
          Elmessage.error('请检查错误项！')
          return false
        }
      })
    },
    handleRegister() {
      this.$refs.loginForm.validate(valid => {
        if (valid) {
          this.loading = true
        }
      })
    },
    ShowRegister() {
      this.isRegister = false
    },
    ShowLogin() {
      this.isRegister = true
    }
  }
}
</script>

<style>
@supports (-webkit-mask: none) and (not (cater-color: #000000)) {
  .login-container .el-input input {
    color: #000000;
  }
}

/* reset element-ui css */
.login-container {
  width: 100%;
  overflow: hidden;
  height: 100%;
}

.el-input {
  height: 47px;
  width: 91.5%;
}

.el-input__wrapper {
  box-shadow: 0 0 0 0px;
  cursor: default;
}

.el-input__wrapper:hover {
  box-shadow: 0 0 0 0px
}

.el-input__wrapper.is-focus {
  box-shadow: 0 0 0 0px
}

.el-form-item.is-error .el-input__wrapper {
  box-shadow: 0 0 0 0px
}

input {
  background: transparent;
  outline: medium;
  border-radius: 5px;
  -webkit-appearance: none;
  border-radius: 0px;
  padding: 12px 5px 12px 15px;
  color: #000000;
  height: 25px;
}

.el-form-item {
  border: 1px solid #66CCCC;
  border-radius: 5px;
}


.left,
.right {
  height: 100%;
  display: inline-block
}

.left {
  width: 50%;
  float: left;
}

.right {
  width: 50%
}

.loginimg {
  width: 100%;
  height: 100%;
}

.show-pwd {
  color: #66CCCC;
}

.revertbtn {
  width: 48.8%;
  margin: 20px 0 0 0;
  font-size: 15px;
  font-weight: bold;
  color: #66CCCC;
  transition: all 0.5s;
  background-color: #ffffff;
  border: 1px solid #66CCCC;
}

.revertbtn:hover {
  transition: all 0.5s;
  background-color: #66CCCC;
  border: 1px solid #66CCCC;
}

.revertbtn:focus {
  transition: all 0.5s;
  background-color: #66CCCC;
  border: 1px solid #66CCCC;
}
</style>

<style>
.login-container {
  min-height: 100%;
  width: 100%;
  background-color: #ffffff;
  overflow: hidden;
}

.login-form {
  position: relative;
  width: 620px;
  max-width: 100%;
  padding: 160px 35px 0;
  margin: 0 auto;
  overflow: hidden;
}

.svg-container {
  padding: 6px 5px 6px 15px;
  color: #66CCCC;
  vertical-align: middle;
  width: 30px;
  display: inline-block;
}

.title-container {
  position: relative;
}

.title {
  margin: 150px auto 40px auto;
  text-align: center;
  font-weight: bold;
}


.show-pwd {
  position: absolute;
  right: 10px;
  top: 7px;
  font-size: 16px;
  color: #000000;
  cursor: pointer;
  user-select: none;
}
</style>
