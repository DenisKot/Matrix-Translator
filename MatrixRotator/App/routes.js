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
