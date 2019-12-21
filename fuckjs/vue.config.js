module.exports = {
  publicPath: process.env.NODE_ENV === 'production' ? '/vue/' : '/',
  pluginOptions: {
    quasar: {
      importStrategy: 'kebab',
      rtlSupport: false
    }
  },
  transpileDependencies: [
    'quasar'
  ]
}
