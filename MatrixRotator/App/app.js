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
