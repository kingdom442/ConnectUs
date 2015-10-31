angular.module('connectusApp').controller('connectUsersController', function ($scope, $rootScope, callbackHandler, userComparisonService, userConnectService, connectStates) {
    $scope.loadingCounter = 0;
    $scope.equalEducations = [];
    $scope.equalWork = [];
    $scope.equalInterests = [];
    $scope.sportInterests = [];
    $scope.user = {};

    $scope.compareEducation = getBooleanFromLocalStorage('setting_education');
    $scope.compareWork = getBooleanFromLocalStorage('setting_work');
    $scope.compareTeams = getBooleanFromLocalStorage('setting_teams');
    $scope.compareAthletes = getBooleanFromLocalStorage('setting_athletes');
    $scope.compareObjects = [];
    if ($scope.compareEducation)
        $scope.compareObjects.push("EDUCATION");
    if ($scope.compareWork)
        $scope.compareObjects.push("WORK");
    if ($scope.compareTeams)
        $scope.compareObjects.push("TEAM");
    if ($scope.compareAthletes)
        $scope.compareObjects.push("ATHLETE");

    if ($rootScope.selectedUser) {
        $scope.user = $rootScope.selectedUser;
        $scope.loadingCounter++;
        userComparisonService.compareUsers($scope.user.userid, $scope.compareObjects, comparisonFinished, errCB);
        $rootScope.selectedUser = undefined;
    }
    else if ($rootScope.selectedUserComparison) {
        comparisonFinished($rootScope.selectedUserComparison);
        $scope.user.userid = $rootScope.selectedUserComparison.userId;
        $scope.user.username = $rootScope.selectedUserComparison.userName;
        $rootScope.selectedUserComparison = undefined;
    }

    function comparisonFinished(result) {

        var parsedEqualData = JSON.parse(result.equalJson);
        if (parsedEqualData.EducationList) {
            parsedEqualData.EducationList.forEach(function (edu) {
                $scope.equalEducations.push(new Education(edu.name, edu.type, edu.yearTo));
            });
        }
        if (parsedEqualData.WorkHistory) {
            parsedEqualData.WorkHistory.forEach(function (work) {
                $scope.equalWork.push(new Work(work.name, work.type, work.dateFrom, work.dateTo, work.city));
            });
        }
        if (parsedEqualData.Interests) {
            parsedEqualData.Interests.forEach(function (interest) {
                $scope.equalInterests.push(new Interest(interest.name, interest.description, interest.type));
            });
        }
        //if (!$scope.$$phase) {
        callbackHandler.finished($scope, false);
    }

    function errCB() {

    }

    $scope.loadConnectState = function () {
        $scope.loadingCounter++;   
        //requester = 0 if requester is current user, else 1
        userConnectService.loadConnectState($scope.user.userid, function (connectRequestId, state, requester) {
            $scope.connectState = connectStates.fromNr(state);
            $scope.connectRequestId = connectRequestId;
            $scope.requester = requester;
            callbackHandler.finished($scope, false);
        }, function () {
        });
    }

    //Send a connect request to the selected user
    $scope.sendConnectRequest = function () {
        $scope.loadingCounter++;
        userConnectService.sendConnectRequest($scope.user.userid, function () {
            $scope.connectState = connectStates.REQUESTED;
            callbackHandler.finished($scope, false);
        }, function(){
        });
    };

    $scope.acceptConnectRequest = function () {
        $scope.loadingCounter++;
        userConnectService.acceptConnectRequest($scope.user.userid, $scope.connectRequestId, function () {
            $scope.connectState = connectStates.CONNECTED;
            callbackHandler.finished($scope, false);
            $rootScope.selectedUserId = $scope.user.userid;
            menu.setMainPage('pages/userContact.html', { closeMenu: true });
        }, function () {
        });
    };

    $scope.rejectConnectRequest = function () {
        $scope.loadingCounter++;
        userConnectService.rejectConnectRequest($scope.user.userid, $scope.connectRequestId, function () {
            $scope.connectState = connectStates.REJECTED;
            callbackHandler.finished($scope, false);
        }, function () {
        });
    };
});