angular.module('connectusApp').controller('userContactController', function ($scope, $rootScope, callbackHandler, userService) {

    $scope.loadUserContact = function (userId) {
        $scope.loadingCounter++;
        userService.loadUserInfo(userId, function (userInfo) {
            if (userInfo) {
                $scope.userInfo = userInfo;
            }
            callbackHandler.finished($scope, false);
        }, function (error) {
            callbackHandler.finished($scope, false);
        });
    };

    $scope.loadUserContact($rootScope.selectedUserId);
});