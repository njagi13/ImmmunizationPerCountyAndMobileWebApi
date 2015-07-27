var app = angular.module("app", ['ngRoute', 'chart.js']);
console.log("==> Application started...");


app.config(function ($routeProvider, $locationProvider) {
    console.log("==> Configuring routes...");

    $routeProvider.when('/', {
        controller: "ImmunizationCoverCtrl",
        templateUrl: '/Home/Index'
    });
    $locationProvider.html5Mode(false).hashPrefix('!');

    console.log("==> Finished configuring routes...");
});
