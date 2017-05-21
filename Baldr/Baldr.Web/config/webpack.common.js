const CheckerPlugin = require('awesome-typescript-loader').CheckerPlugin;
const DllBundlesPlugin = require('webpack-dll-bundles-plugin').DllBundlesPlugin;
const HtmlWebpackPlugin = require('html-webpack-plugin');
const webpack = require('webpack');

const configHelper = require('./configHelper');

// TODO 
// Minification for prod
// Hashing of files for browser cache
// Try to remove compilation of dlls each time webapack builds
// webpack.config.js at root is using process.env to determine env...not sure that is going to work when not running with node


// All paths are realtive to this files location in the project.
module.exports = function (options) {
    isProd = options.env === 'production'
    return {

        // Webpack compilation starts here.
        // Multiple entry points are possible. Useful for splitting out code features to be delivered to the client separately.
        entry: {
            'polyfills': configHelper.appPath('polyfills.ts'),
            'app': configHelper.appPath('main.ts')
        },

        // Location of webpack output
        output: {
            path: configHelper.projRootPath('wwwroot/dist/'),
            sourceMapFilename: '[file].map',
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

            modules: [configHelper.appPath('.'), configHelper.projRootPath('node_modules')],    

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

            // Creates the html script imports in index.html
            new HtmlWebpackPlugin({
                template: configHelper.projRootPath('Views/Home/Index_Template.cshtml'),
                title: 'Baldr',
                filename: configHelper.projRootPath('Views/Home/Index.cshtml'),
                inject: 'body'
            })
        ]
    }
}