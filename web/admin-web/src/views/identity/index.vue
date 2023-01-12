<template>
  <div class="login-container">
    <div class="left">
      <img class="loginimg" :src="LoginImage.url">
    </div>
    <div class="right">
      <el-form ref="loginForm" :model="loginForm" class="login-form" autocomplete="on" label-position="left"
        v-if="isRegister">
        <div class="title-container">
          <h1 class="title"><em>欢迎您回来</em>...</h1>
        </div>
        <el-form-item prop="username">
          <icon data="@/icons/user.svg" class="svg-container" />
          <el-input v-model="loginForm.username" placeholder="用户名" name="username" type="text" tabindex="1"
            autocomplete="on" />
        </el-form-item>
        <el-form-item prop="password">
          <icon data="@/icons/password.svg" class="svg-container" />
          <el-input v-model="loginForm.password" :type="password" placeholder="密码" name="password" tabindex="3"
            autocomplete="on" show-password />
        </el-form-item>
        <div>
          <el-button :loading="loading" round type="primary" class="revertbtn" @click="goLogin()">登录</el-button>
          <el-button :loading="loading" round type="primary" class="revertbtn" @click="ShowRegister()">注册</el-button>
        </div>
      </el-form>

      <el-form ref="registerForm" :model="registerForm" :rules="registerRules" class="login-form" autocomplete="on"
        label-position="left" v-show="!isRegister">
        <div class="title-container">
          <h1 class="title"><em>欢迎您加入我们</em>...</h1>
        </div>

        <el-form-item prop="username">
          <icon data="@/icons/user.svg" class="svg-container" />
          <el-input v-model="registerForm.username" placeholder="用户名" name="username" type="text" tabindex="1"
            autocomplete="on" />
        </el-form-item>

        <el-form-item prop="email">
          <icon data="@/icons/email.svg" class="svg-container" />
          <el-input v-model="registerForm.email" placeholder="邮箱" name="email" type="text" tabindex="2"
            autocomplete="on" />
        </el-form-item>

        <el-form-item prop="password">
          <icon data="@/icons/password.svg" class="svg-container" />
          <el-input v-model="registerForm.password" :type="password" placeholder="密码" name="password" tabindex="3"
            autocomplete="on" show-password />
        </el-form-item>

        <el-form-item prop="confimPassword">
          <icon data="@/icons/password.svg" class="svg-container" />
          <el-input v-model="registerForm.confimPassword" :type="password" placeholder="确认密码" name="confimPassword"
            tabindex="4" autocomplete="on" show-password />
        </el-form-item>

        <div>
          <el-button :loading="loading" round type="primary" class="revertbtn" @click="goRegister()">注册</el-button>
          <el-button :loading="loading" round type="primary" class="revertbtn" @click="ShowLogin()">登录</el-button>
        </div>
      </el-form>
    </div>
  </div>
</template>

<script>
import { login } from '@/api/identity/identity'
import { register } from '@/api/user/user'
import store from '@/store'
import { ElMessage } from 'element-plus'
import { useRouter } from 'vue-router'

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
        callback(new Error('密码长度小于6位，请重新输入您的密码'))
      } else {
        callback()
      }
    }
    const validateConfimPassword = (rule, value, callback) => {
      if (value.length < 6) {
        callback(new Error('密码长度小于6位，请重新输入您的密码'))
      }
      if (value !== this.registerForm.password) {
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
      router: useRouter(),
      isRegister: true,
      LoginImage: {
        url: 'https://img-prod-cms-rt-microsoft-com.akamaized.net/cms/api/am/imageFileData/RE4wHgx?ver=5481',
        hrefUrl: 'https://img-prod-cms-rt-microsoft-com.akamaized.net/cms/api/am/imageFileData/RE4wHgx?ver=5481'
      },
      loginForm: {
        username: 'admin',
        password: '',
      },
      registerForm: {
        username: 'admin',
        email: '',
        password: '',
        confimPassword: '',
      },
      registerRules: {
        username: [{ required: true, trigger: 'blur', validator: validateUsername }],
        email: [{ required: true, trigger: 'blur', validator: validateEmail }],
        password: [{ required: true, trigger: 'blur', validator: validatePassword }],
        confimPassword: [{ required: true, trigger: 'blur', validator: validateConfimPassword }]
      },
      loading: false,
    }
  },
  watch: {
  },
  methods: {
    goLogin() {
      this.$refs.loginForm.validate(valid => {
        if (valid) {
          this.loading = true
          login(this.loginForm).then(res => {
            if (res.status === 200) {
              store.commit('identity/setToken', res.data.access_token)
              ElMessage.success('登录成功！')
              this.router.push({ name: 'Home' })
            }
            this.loading = false
          }).catch(() => {
            this.loading = false
          })
        } else {
          ElMessage.error('请检查错误项！')
          return false
        }
      })
    },
    goRegister() {
      this.$refs.registerForm.validate(valid => {
        if (valid) {
          this.loading = true
          register(this.registerForm).then(res => {
            if (res.status === 200) {
              ElMessage.success('注册成功！')
              this.goLogin(this.loginForm)
            }
            this.loading = false
          }).catch(() => {
            this.loading = false
          })
        } else {
          ElMessage.error('请检查错误项！')
          return false
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
