const CheckerPlugin = require('awesome-typescript-loader').CheckerPlugin;
const DllBundlesPlugin = require('webpack-dll-bundles-plugin').DllBundlesPlugin;
const webpack = require('webpack');

const configHelper = require('./configHelper');

// All paths are realtive to this files location in the project.
module.exports = function (options) {
    isProd = options.env === 'production'
    return {

        // Webpack compilation starts here.
        // Multiple entry points are possible. Useful for splitting out code features to be delivered to the client separately.
        entry: {
            'polyfills': configHelper.root('ClientApp/src/polyfills.ts'),
            'app': configHelper.root('ClientApp/src/main.ts')
        },

        // Location of webpack output
        output: {
            path: configHelper.root('wwwroot/dist/'),
            sourceMapFilename: '[file].map',
            filename: '[name].bundle.js'
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
                  exclude: [configHelper.root('ClientApp/src', 'styles')]
                },
            ]
        },

        resolve: {
            // Supported file types
            extensions: ['.ts', '.js'],
        
            modules: [configHelper.root('ClientApp/src'), configHelper.root('node_modules')],    

            // Alias paths in order to avoid messy imports everywhere.
            // Also allows for complex path configuration in one location. 
            alias: {
            }
        },

        plugins: [
            new CheckerPlugin(),
            new webpack.ProvidePlugin({ Reflect: 'core-js/es7/reflect' })
        ]
    }
}