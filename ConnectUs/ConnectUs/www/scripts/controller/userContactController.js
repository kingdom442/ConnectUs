angular.module('connectusApp').controller('userContactController', function ($scope, $rootScope, callbackHandler, userService) {
    $rootScope.pageStackCount = 2;

    $scope.loadUserContact = function (userId) {
        $scope.loadingCounter++;
        userService.loadUserInfo(userId, function (userInfo) {
            if (userInfo) {
                $scope.user = userInfo;
            }
            callbackHandler.finished($scope, false);
        }, function (error) {
            callbackHandler.finished($scope, false);
        });
    };

    $scope.loadUserContact($rootScope.selectedUserId);
});