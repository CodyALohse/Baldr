const webpackMerge = require('webpack-merge'); // used to merge webpack configs
const webpackMergeDll = webpackMerge.strategy({plugins: 'replace'});
const DllBundlesPlugin = require('webpack-dll-bundles-plugin').DllBundlesPlugin;
const AddAssetHtmlPlugin = require('add-asset-html-webpack-plugin');

const commonConfig = require('./webpack.common.js'); // the settings that are common to prod and dev
const configHelper = require('./configHelper.js');

const ENV = process.env.ENV = process.env.NODE_ENV = 'development';

// All paths are realtive to this files location in the project.
module.exports = function (options) {
    return webpackMerge(commonConfig({env: ENV}), {

        devtool: 'inline-source-map',

        // Location of webpack output
        output: {
        },

        // File parsing rules and loaders
        module: {
            rules: [
               ]
        },

        resolve: {
        },

        plugins: [            
            new DllBundlesPlugin({
                bundles: {
                  polyfills: [
                    'core-js',
                    {
                      name: 'zone.js',
                      path: 'zone.js/dist/zone.js'
                    },
                    {
                      name: 'zone.js',
                      path: 'zone.js/dist/long-stack-trace-zone.js'
                    },
                  ],
                  vendor: [
                    '@angular/platform-browser',
                    '@angular/platform-browser-dynamic',
                    '@angular/core',
                    '@angular/common',
                    '@angular/forms',
                    '@angular/http',
                    '@angular/router',
                    //'@angularclass/hmr', // hot module reload
                    'rxjs',
                  ]
                },
                dllDir: configHelper.appBuildPath('dll'),
                webpackConfig: webpackMergeDll(commonConfig({env: ENV}), {
                  devtool: 'cheap-module-source-map',
                  plugins: []
                })
              }),

            new AddAssetHtmlPlugin([
                { filepath: configHelper.appBuildPath(`dll/${DllBundlesPlugin.resolveFile('polyfills')}`) },
                { filepath: configHelper.appBuildPath(`dll/${DllBundlesPlugin.resolveFile('vendor')}`) }
            ]),
        ]
    });
}