﻿angular.module('connectusApp').controller('appController', function ($scope, $rootScope, loginService) {
    ons.ready(function () {
        $scope.init();
    });
    ons.setDefaultDeviceBackButtonListener(onBackKeyDown);
    function onBackKeyDown() {
        if (menu.isMenuOpened()) {
            menu.toggleMenu();
            return true;
        }
        else if ($rootScope.pageStackCount == 1) { //Home
            if (ons.platform.isAndroid()) {
                ons.notification.confirm({
                    message: 'Close ConnectUs?',
                    callback: function (answer) {
                        if(answer == 1)
                            navigator.app.exitApp();
                    }
                });
            }
        }
        else if ($rootScope.pageStackCount == 0) {
            navigator.app.exitApp();
        } else {
            menu.setMainPage('pages/home.html', { closeMenu: true });
            return true;
        }
        return false; 
    }

    //for Windows Phone users
    if (window.WinJS && window.WinJS.Application) {
        window.WinJS.Application.onbackclick = function () { return onBackKeyDown(); };
    }

    $rootScope.pageStackCount = 0;

    $rootScope.$reset = function () {
        var whiteList = ["ons", "alert", "console", "menu"]; //Properties that should not be deleted
        for (var prop in $rootScope) {
            if (prop.substring(0, 1) !== '$' && $.inArray(prop, whiteList) == -1) {
                delete $rootScope[prop];
            }
        }
    }

    $scope.logout = function () {
        loginService.clearCredentials();
        $rootScope.$reset();
        menu.setSwipeable(false);
        connectusClient.logout();
        menu.setMainPage('pages/login.html', { closeMenu: true });
        $rootScope.pageStackCount = 0;
    }

    $scope.tryLogin = function () {
        loginService.tryLogin(function () {
            menu.setSwipeable(true);
            menu.setMainPage('pages/home.html', { closeMenu: true });
        }, function () {
            menu.setMainPage('pages/login.html', { closeMenu: true });
        });
    }

    $scope.compareUsers = function (selectedUser) {
        $rootScope.selectedUser = selectedUser;
        menu.setMainPage('pages/connectUsers.html', { closeMenu: true });
    }


    $scope.showUserPopup = function (selectedUser) {
        $rootScope.selectedUser = selectedUser;
        ons.createDialog('userDetails.html').then(function (userDialog) {
            userDialog.show();
        });
    }

    /*Initial setup*/
    $scope.init = function() {
        if (!connectusClient)
            initMobileServiceClient();
        $scope.tryLogin();
        $scope.$on('eLogin', function (event, args) {
            $scope.tryLogin();
        });
    };

});