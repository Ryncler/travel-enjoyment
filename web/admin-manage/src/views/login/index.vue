<template>
  <div class="login-container">
    <div class="left">
      <img
        class="loginimg"
        :src="LoginImage.url"
      >
    </div>
    <div class="right">
      <el-form ref="loginForm" :model="loginForm" :rules="loginRules" class="login-form" auto-complete="on" label-position="left">
        <div class="title-container">
          <h1 v-if="isRegister" class="title"><em>欢迎您回来</em>...</h1>
          <h1 v-show="!isRegister" class="title"><em>欢迎您加入我们</em>...</h1>
        </div>

        <el-form-item prop="username">
          <span class="svg-container">
            <svg-icon icon-class="user" />
          </span>
          <el-input
            ref="username"
            v-model="loginForm.username"
            placeholder="用户名"
            name="username"
            type="text"
            tabindex="1"
            auto-complete="on"
          />
        </el-form-item>

        <div v-show="!isRegister">
          <el-form-item prop="email">
            <span class="svg-container">
              <svg-icon icon-class="email" />
            </span>
            <el-input
              ref="email"
              v-model="loginForm.email"
              placeholder="邮箱"
              name="email"
              type="text"
              tabindex="1"
              auto-complete="on"
            />
          </el-form-item>
        </div>

        <el-form-item prop="password">
          <span class="svg-container">
            <svg-icon icon-class="password" />
          </span>
          <el-input
            :key="passwordType"
            ref="password"
            v-model="loginForm.password"
            :type="passwordType"
            placeholder="密码"
            name="password"
            tabindex="2"
            auto-complete="on"
            @keyup.enter.native="handleLogin"
          />
          <span class="show-pwd" @click="showPwd">
            <svg-icon :icon-class="passwordType === 'password' ? 'eye' : 'eye-open'" />
          </span>
        </el-form-item>

        <el-form-item v-show="!isRegister" prop="confimPassword">
          <span class="svg-container">
            <svg-icon icon-class="password" />
          </span>
          <el-input
            :key="passwordType"
            ref="confimPassword"
            v-model="loginForm.confimPassword"
            :type="passwordType"
            placeholder="确认密码"
            name="confimPassword"
            tabindex="2"
            auto-complete="on"
            @keyup.enter.native="handleLogin"
          />
          <span class="show-pwd" @click="showPwd">
            <svg-icon :icon-class="passwordType === 'password' ? 'eye' : 'eye-open'" />
          </span>
        </el-form-item>
        <div v-if="isRegister">
          <el-button :loading="loading" round type="primary" class="revertbtn" @click.native.prevent="handleLogin">登录</el-button>
          <el-button :loading="loading" round type="primary" class="revertbtn" @click="ShowRegister()">注册</el-button>
        </div>
        <div v-show="!isRegister">
          <el-button :loading="loading" round type="primary" class="revertbtn" @click.native.prevent="handleRegister">注册</el-button>
          <el-button :loading="loading" round type="primary" class="revertbtn" @click="ShowLogin()">登录</el-button>
        </div>
      </el-form>
    </div>
  </div>
</template>

<script>
import { validUsername } from '@/utils/validate'

export default {
  name: 'Login',
  data() {
    const validateUsername = (rule, value, callback) => {
      if (!validUsername(value)) {
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
      passwordType: 'password',
      redirect: undefined
    }
  },
  watch: {
    $route: {
      handler: function(route) {
        this.redirect = route.query && route.query.redirect
      },
      immediate: true
    }
  },
  methods: {
    showPwd() {
      if (this.passwordType === 'password') {
        this.passwordType = ''
      } else {
        this.passwordType = 'password'
      }
      this.$nextTick(() => {
        this.$refs.password.focus()
      })
    },
    handleLogin() {
      this.$refs.loginForm.validate(valid => {
        if (valid) {
          this.loading = true
          this.$store.dispatch('user/login', this.loginForm).then(() => {
            this.$router.push({ path: this.redirect || '/' })
            this.loading = false
          }).catch(() => {
            this.loading = false
          })
        } else {
          console.log('error submit!!')
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

<style lang="scss">

$bg:#283443;
$cursor: #000000;

@supports (-webkit-mask: none) and (not (cater-color: $cursor)) {
  .login-container .el-input input {
    color: $cursor;
  }
}

/* reset element-ui css */
.login-container {
  width:100%;
  overflow:hidden;
  height: 100%;

  .el-input {
    display: inline-block;
    height: 47px;
    width: 85%;

.el-input__inner {
    border: none;
    outline: none;
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

      &:-webkit-autofill {
        box-shadow: 0 0 0px 1000px $bg inset !important;
        -webkit-text-fill-color: $cursor !important;
      }
    }
  }

  .el-form-item {
    border: 1px solid #66CCCC;
    border-radius: 5px;
  }
}

.left ,.right{
  height: 100%;
  display:inline-block
}
.left{
  width:50%;
  float: left;
}

.right{
  width:50%
}
.loginimg{
  width: 100%;
  height: 100%;
}
.show-pwd{
  color: #66CCCC;
}

.revertbtn {
    width:48.8%;
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

<style lang="scss" scoped>
$bg:#ffffff;
$dark_gray:#2dadf1;
$light_gray:#eee;

.login-container {
  min-height: 100%;
  width: 100%;
  background-color: $bg;
  overflow: hidden;

  .login-form {
    position: relative;
    width: 620px;
    max-width: 100%;
    padding: 160px 35px 0;
    margin: 0 auto;
    overflow: hidden;
  }

  .tips {
    font-size: 14px;
    color: #fff;
    margin-bottom: 10px;

    span {
      &:first-of-type {
        margin-right: 16px;
      }
    }
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

    .title {
      margin: 150px auto 40px auto;
      text-align: center;
      font-weight: bold;
    }
  }

  .show-pwd {
    position: absolute;
    right: 10px;
    top: 7px;
    font-size: 16px;
    color: $dark_gray;
    cursor: pointer;
    user-select: none;
  }
}
</style>
