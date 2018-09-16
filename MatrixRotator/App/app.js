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
