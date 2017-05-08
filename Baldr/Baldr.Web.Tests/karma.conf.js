// Karma configuration
// Generated on Sun May 07 2017 13:17:37 GMT-0500 (Central Daylight Time)

module.exports = function(config) {
  config.set({

    // base path that will be used to resolve all patterns (eg. files, exclude)
    basePath: '',


    // frameworks to use
    // available frameworks: https://npmjs.org/browse/keyword/karma-adapter
    frameworks: ['jasmine'],



    // list of files / patterns to load in the browser
    files: [
        '../Baldr.Web/wwwroot/dist/vendor.js',
        '../Baldr.Web/wwwroot/dist/main-client.js',

        { pattern: 'scripts/**/*.spec.ts', included: true, watched: false, served: true },

        { pattern: '../Baldr.Web/Views/Home/*.cshtml', included: false, watched: true }
    ],


    // list of files to exclude
    exclude: [
    ],


    // preprocess matching files before serving them to the browser
    // available preprocessors: https://npmjs.org/browse/keyword/karma-preprocessor
    preprocessors: {
        'scripts/testbed.ts': ['webpack'],
        'scripts/**/*.spec.ts': ['webpack', 'sourcemap']
    },


    // test results reporter to use
    // possible values: 'dots', 'progress'
    // available reporters: https://npmjs.org/browse/keyword/karma-reporter
    reporters: ['progress'],


    // web server port
    port: 9876,


    // enable / disable colors in the output (reporters and logs)
    colors: true,


    // level of logging
    // possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
    logLevel: config.LOG_INFO,


    // enable / disable watching file and executing tests whenever any file changes
    autoWatch: true,


    // start these browsers
    // available browser launchers: https://npmjs.org/browse/keyword/karma-launcher
    browsers: ['Chrome'],

    mime: { 'application/javascript': ['ts','tsx'] },

    // Continuous Integration mode
    // if true, Karma captures browsers, runs the tests and exits
    singleRun: false,

    // Concurrency level
    // how many browser should be started simultaneous
    concurrency: Infinity,

    webpack: require('../Baldr.Web/webpack.config.js')().filter(config => config.target !== 'node'), // Test against client bundle, because tests run in a browser

    webpackMiddleware: { stats: 'errors-only' }

  })
}
