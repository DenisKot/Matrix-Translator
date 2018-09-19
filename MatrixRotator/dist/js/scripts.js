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

    angular.module('MatrixRotator').controller('MainViewController', ['$scope', 'Upload', '$http', mainViewController]);

    function mainViewController($scope, Upload, $http) {
        var vm = $scope;
        vm.file = null;
        vm.isLoading = false;
        vm.randomMatrix = null;
        vm.data = null;
        vm.error = null;
        vm.variants = new Array(12);
        vm.matrixSize = 3;
        
        vm.fileSelected = function (file) {
            vm.file = file;
            vm.data = null;
            vm.randomMatrix = null;
        };

        function makeRequest(requestType) {
            vm.isLoading = true;
            vm.data = null;
            vm.randomMatrix = null;
            vm.error = null;

            Upload.upload({
                url: 'api/matrix/' + requestType,
                data: { file: vm.file }
            }).then(function (resp) {
                console.log('Success ' + resp.config.data.file.name + 'uploaded. Response: ' + resp.data);
                vm.data = resp.data;
            }, function (resp) {
                console.log('Error status: ' + resp.status);
                var message = resp.data.ExceptionMessage;
                vm.error = message;
                //ToDo: Make error message ui
                //alert(message);
            }, function (evt) {
                var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                console.log('progress: ' + progressPercentage + '% ' + evt.config.data.file.name);
            }).finally(function() {
                vm.isLoading = false;
            });
        }

        vm.rotateRight = function() {
            makeRequest('rotateRight');
        };

        vm.rotateLeft = function() {
            makeRequest('rotateLeft');
        };

        vm.export = function() {
            var csvContent = "data:text/csv;charset=utf-8,";
            vm.data.forEach(function(rowArray) {
                let row = rowArray.join(";");
                csvContent += row + "\r\n";
            });

            var encodedUri = encodeURI(csvContent);
            var link = document.createElement("a");
            link.setAttribute("href", encodedUri);
            link.setAttribute("download", "result.csv");
            link.innerHTML = "";
            document.body.appendChild(link); // Required for FF

            link.click();
        };

        vm.generate = function(size) {
            vm.matrixSize = size;
            vm.randomMatrix = randomSquareArray(size);
        };

        function generatedMatrixRequest(requestType) {
            vm.isLoading = true;
            vm.data = null;
            vm.error = null;

            $http.post('api/matrix/' + requestType, vm.randomMatrix)
                .then(function (resp) {
                    vm.data = resp.data;
                }, function (resp) {
                    var message = resp.data.ExceptionMessage;
                    vm.error = message;
                }).finally(function () {
                    vm.isLoading = false;
                });
        };

        vm.rotateGeneratedRight = function() {
            generatedMatrixRequest("matrixRight");
        };

        vm.rotateGeneratedLeft = function() {
            generatedMatrixRequest("matrixLeft");
        };
    }
})();

function randomSquareArray(length) {
    var arr = [];
    for (var i = 0; i < length; i++) {
        arr.push(randomArray(length));
    }
    return arr;
}

function randomArray(length) {
    return Array.apply(null, Array(length)).map(function () {
        return Math.round(Math.random() * 10);
    });
}