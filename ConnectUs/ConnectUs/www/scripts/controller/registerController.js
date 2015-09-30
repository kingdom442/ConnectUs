
angular.module('connectusApp').controller('registerController', function ($scope, registerService) {

    $scope.register = function () {
        registerService.register($scope.username, $scope.password);
    };
});
