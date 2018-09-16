(function () {
    var app = angular.module("MatrixRotator", [
        'ui.router',
        'LocalStorageModule',
        'angular-loading-bar'
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

    angular.module('MatrixRotator').controller('MainViewController', ['$scope', mainViewController]);

    function mainViewController($scope) {
        var vm = $scope;
    }
})();