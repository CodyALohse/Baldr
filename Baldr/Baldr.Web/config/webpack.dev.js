const webpackMerge = require('webpack-merge'); // used to merge webpack configs
const webpackMergeDll = webpackMerge.strategy({plugins: 'replace'});
const DllBundlesPlugin = require('webpack-dll-bundles-plugin').DllBundlesPlugin;

const commonConfig = require('./webpack.common.js'); // the settings that are common to prod and dev
const configHelper = require('./configHelper.js');

const ENV = process.env.ENV = process.env.NODE_ENV = 'development';

// All paths are realtive to this files location in the project.
module.exports = function (options) {
    return webpackMerge(commonConfig({env: ENV}), {

        // Location of webpack output
        output: {
        //    path: configHelper.root('ClientApp/dist'),
        //    filename: '[name].bundle.js',
        //    sourceMapFilename: '[file].map'
        },

        // File parsing rules and loaders
        module: {
            rules: [
               ]
        },

        resolve: {
            // Supported file types
            extensions: ['.ts', '.js'],

            // Alias paths in order to avoid messy imports everywhere.
            // Also allows for complex path configuration in one location. 
            alias: {
            }
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
                dllDir: configHelper.root('wwwroot/dist/dll'),
                webpackConfig: webpackMergeDll(commonConfig({env: ENV}), {
                  devtool: 'cheap-module-source-map',
                  plugins: []
                })
              }),
        ]
    });
}