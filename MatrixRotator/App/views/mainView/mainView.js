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
                data: { file: vm.file }
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