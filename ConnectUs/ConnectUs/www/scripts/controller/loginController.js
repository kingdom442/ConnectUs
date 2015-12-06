
angular.module('connectusApp').controller('loginController', function ($scope, $rootScope, callbackHandler, loginService) {
    $scope.hideMenuToggle = true;
    $scope.username = "";
    $scope.password = "";
    $scope.showCustomLogin = false;
    $scope.loadingCounter = 0;
    menu.closeMenu();
    var mySwiper = new Swiper('.swiper-container', {
        pagination: '.swiper-pagination',
        paginationClickable: true,
        // Optional parameters
        direction: 'horizontal',
        loop: true
    });


    function loginSuccess(response) {
        if (response.accountId) {
            loginService.setCredentials(response.accountId, response);
            $scope.$emit('eLogin');
        }
        callbackHandler.finished($scope, false);
    }


    function cbErr(error) {
        $scope.loginFailed = true;
        callbackHandler.finished($scope, false);
    }

    $scope.login = function () {
        $scope.loadingCounter++;
        $scope.loginFailed = false;
        loginService.login($scope.username, $scope.password, loginSuccess, cbErr);
    };
    $scope.fbLogin = function () {
        $scope.loadingCounter++;
        loginService.fbLogin(providerLoginSuccess, providerLoginErr);
    };
    $scope.liLogin = function () {
        $scope.loadingCounter++;
        loginService.liLogin(providerLoginSuccess, providerLoginErr);
    };

    function providerLoginSuccess(accountId, firstLogin, currentUser) {
        if (accountId) {
            loginService.setCredentials(accountId, currentUser);
            $scope.$emit('eLogin');
        }
        callbackHandler.finished($scope, false);
    }

    function providerLoginErr() {
        $scope.loginFailed = false;
        callbackHandler.finished($scope, false);
    }
});
