// ConfigHelper is used as a central location to define absolute paths
// used for configuration.

var path = require('path');

var PROJ_ROOT = path.resolve(__dirname, '..');
var APP_ROOT = path.resolve(__dirname, '../ClientApp/src');
var APP_BUILD_PATH = path.resolve(__dirname, '../ClientApp/build');
var DIST_PATH = path.resolve(__dirname, '../wwwroot/dist');

var rootPath = path.join.bind(path, PROJ_ROOT);
var appPath = path.join.bind(path, APP_ROOT);
var appBuildPath = path.join.bind(path, APP_BUILD_PATH);
var distPath = path.join.bind(path, DIST_PATH);

exports.projRootPath = rootPath;
exports.appPath = appPath;
exports.appBuildPath = appBuildPath;
exports.distPath = distPath;