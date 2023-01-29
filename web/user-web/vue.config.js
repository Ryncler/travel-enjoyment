const { defineConfig } = require('@vue/cli-service')

const svgFilePath = 'src/icons/'

module.exports = defineConfig({
  publicPath: '/',
  outputDir: 'dist',
  assetsDir: 'static',
  lintOnSave: process.env.NODE_ENV === 'dev',
  devServer: {
    port: 66600,
    open: false,
    client: {
      overlay: {
        warnings: false,
        errors: true
      }
    }
  },
  configureWebpack: {
    name: "旅享服务平台",
    devtool: "source-map"
  },
  chainWebpack: (config) => {
    config.module
      .rule('vue-svgicon')
      .include.add(resolve(svgFilePath))
      .end()
      .test(/\.svg$/)
      .use('svgicon')
      .loader('@yzfe/svgicon-loader')
      .options({
        svgFilePath
      })

    config.module.rule('svg').exclude.add(resolve(svgFilePath)).end()

    // 推荐配置 transformAssetUrls
    config.module
      .rule('vue')
      .use('vue-loader')
      .loader('vue-loader')
      .tap((opt) => {
        opt.transformAssetUrls = opt.transformAssetUrls || {}
        opt.transformAssetUrls['icon'] = ['data']
        return opt
      })

    // 推荐配置 alias
    config.resolve.alias.set('@icon', svgFilePath)
  },
  transpileDependencies: [
    'vue-echarts',
    'resize-detector'
  ]
})

var path = require('path')
function resolve(dir) {
  return path.join(__dirname, './', dir)
}
