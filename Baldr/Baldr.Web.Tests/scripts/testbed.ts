// https://github.com/AngularClass/angular-starter/blob/master/config/spec-bundle.js

//require('../node_modules/karma/');

Error.stackTraceLimit = 0; // "No stacktrace"" is usually best for app testing.

// Uncomment to get full stacktrace output. Sometimes helpful, usually not.
// Error.stackTraceLimit = Infinity; //

jasmine.DEFAULT_TIMEOUT_INTERVAL = 1000;

// // Cancel Karma's synchronous start,
//__karma__.loaded = function () { };

require('WebProjectNodeModules/core-js/es6');
require('WebProjectNodeModules/core-js/es7/reflect');

require('WebProjectNodeModules/zone.js/dist/zone');
require('WebProjectNodeModules/zone.js/dist/long-stack-trace-zone');
require('WebProjectNodeModules/zone.js/dist/proxy'); // since zone.js 0.6.15
require('WebProjectNodeModules/zone.js/dist/sync-test');
require('WebProjectNodeModules/zone.js/dist/jasmine-patch'); // put here since zone.js 0.6.14
require('WebProjectNodeModules/zone.js/dist/async-test');
require('WebProjectNodeModules/zone.js/dist/fake-async-test');

var testing = require('WebProjectNodeModules/@angular/core/testing');
var browser = require('WebProjectNodeModules/@angular/platform-browser-dynamic/testing');

testing.TestBed.initTestEnvironment(
  browser.BrowserDynamicTestingModule,
  browser.platformBrowserDynamicTesting()
);

/*
 * Ok, this is kinda crazy. We can use the context method on
 * require that webpack created in order to tell webpack
 * what files we actually want to require or import.
 * Below, context will be a function/object with file names as keys.
 * Using that regex we are saying look in ../src then find
 * any file that ends with spec.ts and get its path. By passing in true
 * we say do this recursively
 */
var testContext = require.context('./specs', true, /\.spec\.ts/);

/*
 * get all the files, for each file, call the context function
 * that will require the file and load it up here. Context will
 * loop and require those spec files here
 */
function requireAll(requireContext) {
  return requireContext.keys().map(requireContext);
}

// requires and returns all modules that match
var modules = requireAll(testContext);

//__karma__.start();