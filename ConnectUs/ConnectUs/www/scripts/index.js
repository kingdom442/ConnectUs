// Eine Einführung zur leeren Vorlage finden Sie in der folgenden Dokumentation:
// http://go.microsoft.com/fwlink/?LinkID=397704
// So debuggen Sie Code beim Seitenladen in Ripple oder auf Android-Geräten/-Emulatoren: Starten Sie die App, legen Sie Haltepunkte fest, 
// und führen Sie dann "window.location.reload()" in der JavaScript-Konsole aus.
(function () {
    "use strict";

    document.addEventListener( 'deviceready', onDeviceReady.bind( this ), false );

    function onDeviceReady() {
        // Verarbeiten der Cordova-Pause- und -Fortsetzenereignisse
        document.addEventListener( 'pause', onPause.bind( this ), false );
        document.addEventListener( 'resume', onResume.bind( this ), false );
        
        if (navigator.notification) { // Override default HTML alert with native dialog
            window.alert = function (title, message) {
                navigator.notification.alert(
                    message,    // message
                    null,       // callback
                    title, // title
                    'OK'        // buttonName
                );
            };
        }
        // TODO: Cordova wurde geladen. Führen Sie hier eine Initialisierung aus, die Cordova erfordert.
    };

    function onPause() {
        // TODO: Diese Anwendung wurde ausgesetzt. Speichern Sie hier den Anwendungszustand.
    };

    function onResume() {
        // TODO: Diese Anwendung wurde erneut aktiviert. Stellen Sie hier den Anwendungszustand wieder her.
    };

    function showMessage(title, message) {
        alert(title, message);
    }

    // create the module and name it scotchApp
    var scotchApp = angular.module('connectusApp', ['ngRoute', 'ngAnimate']);

    // configure our routes
    scotchApp.config(function ($routeProvider) {
        $routeProvider

            // route for the home page
            .when('/', {
                templateUrl: 'pages/welcome.html',
                controller: 'welcomeController'
            })

            // route for the about page
            .when('/login', {
                templateUrl: 'pages/login.html',
                controller: 'loginController'
            })

            .when('/register', {
                templateUrl: 'pages/register.html',
                controller: 'registerController'
            })

            .when('/home', {
                templateUrl: 'pages/home.html',
                controller: 'homeController'
            });
    });
    // create the controller and inject Angular's $scope
    scotchApp.controller('welcomeController', function ($scope) {

        // create a message to display in our view
        $scope.message = 'Everyone come and see how good I look!';
    });
    scotchApp.controller('loginController', function ($scope) {

        // create a message to display in our view
        $scope.message = 'Everyone come and see how good I look!';
    });
    scotchApp.controller('registerController', function ($scope) {

        // create a message to display in our view
        $scope.message = 'Everyone come and see how good I look!';
    });
    scotchApp.controller('homeController', function ($scope) {

        // create a message to display in our view
        $scope.message = 'Everyone come and see how good I look!';
    });
} )();