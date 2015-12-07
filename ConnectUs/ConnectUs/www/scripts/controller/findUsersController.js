

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
        $scope.noComparedUserFound = false;
        $scope.noUserFound = false;
        if ($scope.geolocationEnabled) {
            $scope.loadingCounter++;
            var onSuccess = function (position) {
                findUsersService.findAvailableUsersByGeoLocation(position.coords, findAlreadyComparedUsers, getItemFromLocalStorage('setting_searcharea'), function (users) {
                    onFindUsersSuccess(findAlreadyComparedUsers, users);
                }, function () {
                    callbackHandler.finished($scope, false);
                });
            };

            var onError = function (error) {
                $scope.geolocationAllowed = false;
                findUsersService.findAvailableUsersByGeoLocation(0, findAlreadyComparedUsers, getItemFromLocalStorage('setting_searcharea'), function (users) {
                    onFindUsersSuccess(findAlreadyComparedUsers, users);
                }, function () {
                    callbackHandler.finished($scope, false);
                });
            };

            var onFindUsersSuccess = function (findAlreadyComparedUsers, users) {
                if (users && users.length > 0 && users[0]) {
                    $.each(users, function (index, u) {
                        if (!findAlreadyComparedUsers)
                            $scope.users.push(new UserInfo(u.accountId, u.username, u.profilePicUrl, u.status, u.about, true, u.age, ""));
                        else
                            $scope.comparedusers.push(new UserInfo(u.accountId, u.username, u.profilePicUrl, u.status, u.about, true, u.age, ""));
                    });
                } else if (!findAlreadyComparedUsers) {
                    $scope.noUserFound = true;
                    $scope.users.push(new UserInfo(-1, 'Max Mustermann', 'https://maxcdn.icons8.com/Color/PNG/96/Users/dizzy_person-96.png', '@University', 'I am a computer science student in the fifth semester. Always interested into meeting new people.', true, '22', '', false, 'Max', 'Mustermann', '', '', '', '', false, ''));
                } else {
                    $scope.noComparedUserFound = true;
                }
                callbackHandler.finished($scope, false);
            };

            navigator.geolocation.getCurrentPosition(onSuccess, onError, { maximumAge: 30000, timeout: 5000, enableHighAccuracy: true });
        }
    };

    $scope.changeShowComparedUsers = function () {
        if ($scope.showComparedUsers)
            $scope.findUsers($scope.showComparedUsers);
        else {
            $scope.comparedusers = [];
            $scope.$apply();
        }
    };

});
