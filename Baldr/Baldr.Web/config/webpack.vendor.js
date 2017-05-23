const webpack = require('webpack');
const configHelper = require('./configHelper.js');
const commonConfig = require('./webpack.common.js');

// All paths are realtive to this files location in the project.
module.exports = function(env) {

    var isProd = env === 'production'

    return {

        entry: {
            'vendor': [configHelper.appPath('vendor.ts')]
        },

        // Location of webpack output
        output: {
            path: configHelper.projRootPath('wwwroot/dist/dll'),
            filename: '[name].bundle.js',
            publicPath: 'dist/'
        },

        // File parsing rules and loaders
        module: {
            rules: [
                {
                    test: /\.ts$/,
                    use: [
                        {
                            loader: 'awesome-typescript-loader?silent=false',
                            options: {
                                configFileName: 'tsconfig.json',
                                useCache: !isProd
                            }
                        },
                        {
                            loader: 'angular2-template-loader'
                        }
                    ],
                    exclude: [/\.(spec|e2e)\.ts$/]
                }
               ]
        },

        resolve: {
            extensions: ['.ts', '.js'],
            modules: [configHelper.appPath('.'), configHelper.projRootPath('node_modules')]
        },

        plugins: [            
            // creates a lookup value for webpack to reference when compiling.
            new webpack.DefinePlugin({
                IS_PROD: JSON.stringify(isProd)
            }),

            new webpack.DllPlugin({
                path: configHelper.distPath('dll/[name]-manifest.json'),
                name: '[name]',
                context: configHelper.distPath('dll')
            })
        ]
    };
}
