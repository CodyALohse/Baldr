const CheckerPlugin = require('awesome-typescript-loader').CheckerPlugin;
const HtmlWebpackPlugin = require('html-webpack-plugin');
const AddAssetHtmlPlugin = require('add-asset-html-webpack-plugin');
const webpack = require('webpack');

const configHelper = require('./configHelper');

// TODO 
// Minification for prod
// Hashing of files for browser cache


// All paths are realtive to this files location in the project.
module.exports = function (env) {
    isProd = env === 'production'
    return {

        // Webpack compilation starts here.
        // Multiple entry points are possible. Useful for splitting out code features to be delivered to the client separately.
        entry: {
            'polyfills': configHelper.appPath('polyfills.ts'),
            'app': configHelper.appPath('main.ts')
        },

        // Location of webpack output
        output: {
            path: configHelper.projRootPath('wwwroot/dist/app'),
            sourceMapFilename: '[file].map',
            filename: '[name].bundle.js',
            publicPath: 'dist/app'
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
                    exclude: [configHelper.projRootPath('node_modules'), /\.(spec|e2e)\.ts$/]
                },

                {
                  test: /\.html$/,
                  use: 'raw-loader',
                  //exclude: [helpers.root('src/index.html')]
                },

                {
                  test: /\.css$/,
                  use: ['to-string-loader', 'css-loader'],
                  exclude: [configHelper.appPath('.', 'styles')]
                },
            ]
        },

        resolve: {
            // Supported file types
            extensions: ['.ts', '.js'],

            modules: [configHelper.appPath('.'), configHelper.projRootPath('node_modules')],    // TODO including node_modules here slows down the build but without it there are resolve errors

            // Alias paths in order to avoid messy imports everywhere.
            // Also allows for complex path configuration in one location. 
            alias: {
                core: configHelper.appPath('app/core/')
            }
        },

        plugins: [
            new CheckerPlugin(),

            // Fixes issue where reflect is not being seen by another module
            new webpack.ProvidePlugin({ Reflect: 'core-js/es7/reflect' }),

            new webpack.DefinePlugin({
                IS_PROD: JSON.stringify(isProd)
            }),

            new webpack.DllReferencePlugin({
                context: configHelper.appPath('.'),
                manifest: require(configHelper.distPath('dll/vendor-manifest.json'))
            }),

            // Creates the html script imports in index.html
            new HtmlWebpackPlugin({
                template: configHelper.projRootPath('Views/Home/Index_Template.cshtml'),
                title: 'Baldr',
                filename: configHelper.projRootPath('Views/Home/Index.cshtml'),
                inject: 'body'
            }),

            new AddAssetHtmlPlugin([
                {
                    includeSourcemap: false,
                    publicPath: 'dist/dll/',
                    filepath: configHelper.distPath(`dll/vendor.bundle.js`)
                }
            ]),
        ]
    }
}