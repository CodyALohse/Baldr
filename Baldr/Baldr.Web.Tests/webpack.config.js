const path = require('path');

// 
const CheckerPlugin = require('awesome-typescript-loader').CheckerPlugin;

// All paths are realtive to this files location in the project.
module.exports = {

    // Webpack compilation starts here.
    // Multiple entry points are possible. Useful for splitting out code features to be delivered to the client separately.
    entry: {
        app: './scripts/testbed.ts'
    },

    // Location of webpack output
    output: {
        path: path.resolve(__dirname, 'dist'),
        filename: 'tests-webpack.bundle.js'
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
                            tsconfig: 'tsconfig.json'  
                        }
                    },
                    {
                        loader: 'angular2-template-loader'
                    }
                ]
            },
            {
                test: /\.html$/,
                use: 'raw-loader',
            },
            {
                test: /\.css$/,
                use: ['to-string-loader', 'css-loader'],
            },
        ]
    },

    resolve: {
        // Supported file types
        extensions: ['.ts', '.js'],

        // Alias paths in order to avoid messy imports everywhere.
        // Also allows for complex path configuration in one location. 
        // ***NOTE: Need to define the same alias in tsconfig file ***
        alias: {
            WebProjectNodeModules: path.resolve(__dirname, '../Baldr.Web/node_modules/'),
            WebProjectAppRoot: path.resolve(__dirname, '../Baldr.Web/ClientApp/src/')
        }
    },

    plugins: [
        new CheckerPlugin()
    ]
}