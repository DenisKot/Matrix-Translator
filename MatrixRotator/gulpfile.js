/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp'),
    concat = require('gulp-concat'),
    uglify = require('gulp-uglify'),
    bower = require('gulp-bower'),
    less = require('gulp-less'),
    del = require('del'),
    newer = require('gulp-newer'),
    rev = require('gulp-rev'),
    gulpInject = require('gulp-inject'),
    series = require('stream-series'),
    plumber = require('gulp-plumber'),
    notify = require('gulp-notify'),
    gulpIf = require('gulp-if'),
    csso = require('gulp-csso'),
    htmlMinifier = require('html-minifier').minify,
    strip = require('gulp-strip-comments');

let configuration = 'development';//'production';

const vendors = require('./App/build/vendors.js.config.json'),
    lessFiles = require('./App/build/less.config.json'),
    vendorsCss = require('./App/build/vendors.css.config.json'),
    js = require('./App/build/js.config.json');

const paths = {
    dir: 'dist',
    js: js,
    views: ['App/views/**/*.html'],
    less: lessFiles,
    vendors: vendors,
    vendorsCss: vendorsCss
};

const mainView = {
    path: 'Views/Home',
    name: 'Index.cshtml'
};

// Restore bower
gulp.task("bower", function () {
    return bower()
        .pipe(gulp.dest('App/bower_components'));
});

gulp.task('clean', function () {
    return del(paths.dir);
});

// Minify and copy all Edoc_client script files to dashboard.min.js
gulp.task('build-scripts', function () {
    return gulp.src(paths.js)
        .pipe(plumber({
            errorHandler: notify.onError(function (error) {
                return {
                    title: 'build-scripts',
                    message: error.message
                };
            })
        }))
        //.pipe(babel({ presets: ['es2015'] }))
        //.pipe(angularFileSort())
        //.pipe(gulpIf(configuration === 'development', sourcemaps.init()))
        //.pipe(gulpIf(configuration !== 'development', uglify()))
        .pipe(concat('scripts.js'))
        //.pipe(gulpIf(configuration === 'development', sourcemaps.write('.')))
        .pipe(gulpIf(configuration !== 'development', strip()))
        .pipe(gulpIf(configuration !== 'development', rev()))
        .pipe(gulp.dest(paths.dir + '/js'));
});

// Minify and copy all 3rd party libs to vendors.min.js 
gulp.task('build-vendors-js', function () {
    return gulp.src(paths.vendors)
        .pipe(concat('vendors.min.js'))
        .pipe(gulpIf(configuration !== 'development', uglify()))
        .pipe(gulpIf(configuration !== 'development', rev()))
        .pipe(gulpIf(configuration !== 'development', strip()))
        .pipe(gulp.dest(paths.dir + '/js'));
});

// Compile less styles into styles.css
gulp.task('build-less', function () {
    return gulp.src(paths.less)
        .pipe(plumber({
            errorHandler: notify.onError(function (error) {
                return {
                    title: 'build-less',
                    message: error.message
                };
            })
        }))
        //.pipe(gulpIf(configuration === 'development', sourcemaps.init()))
        .pipe(less().on('error',
            function (e) {
                console.log(e);
            }))
        .pipe(concat('styles.css'))
        //.pipe(gulpIf(configuration === 'development', sourcemaps.write('.')))
        .pipe(gulpIf(configuration !== 'development', csso()))
        .pipe(gulpIf(configuration !== 'development', rev()))
        .pipe(gulp.dest(paths.dir + '/css'));
});

gulp.task('build-vendors-css', function () {
    return gulp.src(paths.vendorsCss)
        .pipe(concat('vendors.css'))
        .pipe(gulpIf(configuration !== 'development', rev()))
        .pipe(gulp.dest(paths.dir + '/css'));
});

gulp.task('copy-views', function () {
    return gulp.src(paths.views)
        .pipe(newer('dist'))
        .pipe(gulp.dest(paths.dir + '/views'));
});

gulp.task('inject:js', function () {
    var vendorsStream = gulp.src('dist/js/vendors*.js', { read: false });
    var scriptsStream = gulp.src('dist/js/scripts*.js', { read: false });

    return gulp.src(mainView.path + '/' + mainView.name)
        .pipe(gulpInject(series(vendorsStream, scriptsStream)))//, { addPrefix: '.', addRootSlash: false }))
        .pipe(gulp.dest(mainView.path));
});

gulp.task('inject:css', function () {
    var vendorsStream = gulp.src('dist/css/vendors*.css', { read: false });
    var stylesStream = gulp.src('dist/css/style*.css', { read: false });

    return gulp.src(mainView.path + '/' + mainView.name)
        .pipe(gulpInject(series(vendorsStream, stylesStream)))//, { addPrefix: '.', addRootSlash: false }))
        .pipe(gulp.dest(mainView.path));
});

gulp.task('inject', gulp.series('inject:js', 'inject:css'));
gulp.task('build-core', gulp.parallel('build-scripts', 'build-vendors-js', 'build-less', 'build-vendors-css', 'copy-views'));
gulp.task('build-core-min', gulp.parallel('build-scripts', 'build-less', 'copy-views'));

gulp.task("dev", gulp.series(developmentConfiguration, 'build-core', 'inject'));
gulp.task("dev-min", gulp.series(developmentConfiguration, 'build-core-min', 'inject'));
gulp.task('prod', gulp.series(productionConfiguration, 'bower', 'clean', 'build-core', 'inject'));

function developmentConfiguration(done) {
    configuration = 'development';
    done();
};

function productionConfiguration(done) {
    configuration = 'production';
    done();
};
