const webpackMerge = require('webpack-merge'); // used to merge webpack configs

const commonConfig = require('./webpack.common.js'); // the settings that are common to prod and dev
const configHelper = require('./configHelper.js');

const ENV = process.env.ENV = process.env.NODE_ENV = 'development';

// All paths are realtive to this files location in the project.
module.exports = function (options) {
    return webpackMerge(commonConfig({env: ENV}), {

        devtool: 'cheap-module-eval-source-map',

        // Location of webpack output
        output: {
        },

        // File parsing rules and loaders
        module: {
            rules: [ ]
        },

        resolve: {
        },

        plugins: [ ]
    });
}