const path = require('path');
const CheckerPlugin = require('awesome-typescript-loader').CheckerPlugin;

module.exports = {
    entry: {
        app: './scripts/testbed.ts'
    },

    output: {
        path: path.resolve(__dirname, 'dist'),
        filename: 'tests-webpack.bundle.js'
    },

    module: {
        rules: [
            {
                test: /\.ts$/,
                include: /specs/,
                loader: 'awesome-typescript-loader?silent=true',
                exclude: ['/node_modules/', '../Baldr.Web/node_modules/']
            },
        ]
    },

    resolve: {
        extensions: ['.ts', '.js'],

        alias: {
            WebProjectNodeModules: path.resolve(__dirname, '../Baldr.Web/node_modules/')
        },
    },

    plugins: [new CheckerPlugin()]
}