
angular.module('connectusApp').controller('registerController', function ($scope, registerService) {
    $scope.hideMenuToggle = true;

    $scope.register = function () {
        $scope.dataLoading = true;
        registerService.register($scope.username, $scope.password, function () {
            showAlert('Successfully registered');
            $scope.dataLoading = false;
            $scope.$apply();
            menu.setMainPage('pages/login.html', { closeMenu: true });
        }, function () {
            showAlert('Registration failed');
            $scope.dataLoading = false;
            $scope.$apply();
        });
    };
});
