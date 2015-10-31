

angular.module('connectusApp').controller('findUsersController', function ($scope, $rootScope, callbackHandler, findUsersService) {
    $scope.users = [];
    $scope.comparedusers = [];
    $scope.selecteduser = undefined;
    $scope.noUserFound = false;
    $scope.showComparedUsers = false;
    $scope.geolocationAllowed = getBooleanFromLocalStorage('setting_geolocation');
    $scope.loadingCounter = 0;

    $scope.findUsers = function (findAlreadyComparedUsers) {
        if ($scope.geolocationAllowed) {
            $scope.loadingCounter++;
            var onSuccess = function (position) {
                findUsersService.findAvailableUsersByGeoLocation(position.coords, findAlreadyComparedUsers, function (users) {
                    if (users && users.length > 0 && users[0]) {
                        $.each(users, function (index, u) {
                            if (!findAlreadyComparedUsers)
                                $scope.users.push(new UserInfo(u.accountId, u.username, u.profilePicUrl, u.description, true, u.age, ""));
                            else
                                $scope.comparedusers.push(new UserInfo(u.accountId, u.username, u.profilePicUrl, u.description, true, u.age, ""));
                        });
                    } else {
                        $scope.noUserFound = true;
                    }
                    callbackHandler.finished($scope, false);
                });

            };

            function onError(error) {
                $scope.users = [];
            }

            navigator.geolocation.getCurrentPosition(onSuccess, onError);
        }
    };

    $scope.changeShowComparedUsers = function () {
        $scope.showComparedUsers = !$scope.showComparedUsers; //TODO: Fix, two way binding form showComparedUsers doesn't work
        if ($scope.showComparedUsers)
            $scope.findUsers($scope.showComparedUsers);
        else
            $scope.comparedusers = [];
    };

    $scope.showUser = function (selectedUser) {
        $scope.selectedUser = selectedUser;
        //ons.createDialog('userDetails.html', {parentScope: $scope}).then(function (dialog) {
        //    dialog.show();
        //});
        $scope.initConnect();
    };

    $scope.initConnect = function () {
        //userDialog.destroy();
        $rootScope.selectedUser = $scope.selectedUser;
        menu.setMainPage('pages/connectUsers.html', {  closeMenu: true })
    };
});
