const webpack = require('webpack');

module.exports = ({
   css: {
      loaderOptions: {
         sass: {
            additionalData: '@import "@/assets/styles/styles.scss";'
         }
      }
   },
   transpileDependencies: [
      // Specify the dependencies you want to transpile
      // например, 'vue-echarts', 'resize-detector', ...
   ],
   configureWebpack: {
      plugins: [
         new webpack.DefinePlugin({
            // Vue CLI is in maintenance mode, and probably won't merge my PR to fix this in their tooling
            // https://github.com/vuejs/vue-cli/pull/7443
            __VUE_PROD_HYDRATION_MISMATCH_DETAILS__: 'false',
         })
      ]
   }
})
