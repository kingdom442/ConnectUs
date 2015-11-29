angular.module('connectusApp').controller('connectedUsersController', function ($scope, $rootScope, callbackHandler, userConnectService) {
    $scope.loadingCounter = 0;
    $rootScope.pageStackCount = 2;

    $scope.init = function () {
        $scope.loadConnectedUsers($rootScope.accountId);
    }

    $scope.loadConnectedUsers = function (userId) {
        $scope.loadingCounter++;
        userConnectService.loadConnectedUsers(userId, function (connectedUsers) {
            if (connectedUsers) {
                $scope.connectedUsers = connectedUsers;
            }
            callbackHandler.finished($scope, false);
        }, function (error) {
            callbackHandler.finished($scope, false);
        });
    };

    $scope.showUserDetails = function (selectedUser) {
        $rootScope.selectedUserId = selectedUser.accountId;
        menu.setMainPage('pages/userContact.html', { closeMenu: true })
    };

});