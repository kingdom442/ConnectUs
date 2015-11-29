

angular.module('connectusApp').controller('findUsersController', function ($scope, $rootScope, callbackHandler, findUsersService) {
    $scope.users = [];
    $scope.comparedusers = [];
    $scope.selecteduser = undefined;
    $scope.noUserFound = false;
    $scope.showComparedUsers = false;
    $scope.geolocationAllowed = true;
    $scope.geolocationEnabled = getBooleanFromLocalStorage('setting_geolocation');
    $scope.loadingCounter = 0;
    $rootScope.pageStackCount = 2;

    $scope.findUsers = function (findAlreadyComparedUsers) {
        if ($scope.geolocationEnabled) {
            $scope.loadingCounter++;
            var onSuccess = function (position) {
                findUsersService.findAvailableUsersByGeoLocation(position.coords, findAlreadyComparedUsers, getItemFromLocalStorage('setting_searcharea'), function (users) {
                    if (users && users.length > 0 && users[0]) {
                        $.each(users, function (index, u) {
                            if (!findAlreadyComparedUsers)
                                $scope.users.push(new UserInfo(u.accountId, u.username, u.profilePicUrl, u.status, u.about, true, u.age, ""));
                            else
                                $scope.comparedusers.push(new UserInfo(u.accountId, u.username, u.profilePicUrl, u.status, u.about, true, u.age, ""));
                        });
                    } else {
                        $scope.noUserFound = true;
                    }
                    callbackHandler.finished($scope, false);
                }, function () {
                    callbackHandler.finished($scope, false);
                });

            };

            function onError(error) {
                $scope.users = [];
                callbackHandler.finished($scope, false);
                $scope.geolocationAllowed = false;
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

});
