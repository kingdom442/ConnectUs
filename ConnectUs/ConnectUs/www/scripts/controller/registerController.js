
angular.module('connectusApp').controller('registerController', function ($scope, callbackHandler, registerService) {
    $scope.hideMenuToggle = true;
    $scope.loadingCounter = 0;

    $scope.register = function () {
        $scope.loadingCounter++;
        registerService.register($scope.username, $scope.password, function () {
            showAlert('Successfully registered');
            callbackHandler.finished($scope, false);
            menu.setMainPage('pages/login.html', { closeMenu: true });
        }, function () {
            showAlert('Registration failed');
            callbackHandler.finished($scope, false);
        });
    };
});
