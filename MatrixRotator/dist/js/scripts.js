(function () {
    var app = angular.module("MatrixRotator", [
        'ui.router',
        'LocalStorageModule',
        'angular-loading-bar',
        'ngFileUpload'
        //'ngMaterial',
        //'ngMessages'
    ]);
   
    app.config([
        'cfpLoadingBarProvider', function(cfpLoadingBarProvider) {
            cfpLoadingBarProvider.includeSpinner = false;
        }
    ]);

})();

angular.module('MatrixRotator').config(['$stateProvider', '$urlRouterProvider', '$locationProvider',
    function ($stateProvider, $urlRouterProvider, $locationProvider) {
        // Application routes
        $stateProvider
            .state('index',
                {
                    url: '/',
                    templateUrl: 'dist/views/mainView/mainView.html?v=' + sessionStorage["version"]
                });

        $urlRouterProvider.otherwise('/');

        $locationProvider.hashPrefix(''); // by default '!'
    }]);

(function () {
    angular.module('MatrixRotator')
        .controller('MasterCtrl', [
            '$scope',
            masterCtrl]);

    function masterCtrl($scope) {
    }
})();
(function () {
    'use strict';

    angular.module('MatrixRotator').controller('MainViewController', ['$scope', 'Upload', mainViewController]);

    function mainViewController($scope, Upload) {
        var vm = $scope;
        vm.file = null;

        // upload on file select or drop
        vm.upload = function (file) {
            vm.file = file;
        };

        vm.rotateRight = function() {
            Upload.upload({
                url: 'api/matrix/rotate',
                data: { file: vm.file, isRight: true }
            }).then(function (resp) {
                console.log('Success ' + resp.config.data.file.name + 'uploaded. Response: ' + resp.data);
            }, function (resp) {
                console.log('Error status: ' + resp.status);
            }, function (evt) {
                var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                console.log('progress: ' + progressPercentage + '% ' + evt.config.data.file.name);
            });
        }
    }
})();