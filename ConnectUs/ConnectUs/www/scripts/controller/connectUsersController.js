angular.module('connectusApp').controller('connectUsersController', function ($scope, $rootScope, userComparisonService, userConnectService, connectStates) {
    $scope.dataLoading = true;
    $scope.equalEducations = [];
    $scope.equalWork = [];
    $scope.equalInterests = [];
    $scope.sportInterests = [];
    $scope.user = {};

    if ($rootScope.selectedUser) {
        $scope.user = $rootScope.selectedUser;
        userComparisonService.compareUsers($scope.user.userid, comparisonFinished, errCB);
        $rootScope.selectedUser = undefined;
    }
    else if ($rootScope.selectedUserComparison) {
        comparisonFinished($rootScope.selectedUserComparison);
        $scope.user.userid = $rootScope.selectedUserComparison.userId;
        $rootScope.selectedUserComparison = undefined;
    }

    function comparisonFinished(result) {
        $scope.dataLoading = false;

        var parsedEqualData = JSON.parse(result.equalJson);

        parsedEqualData.EducationList.forEach(function (edu) {
            $scope.equalEducations.push(new Education(edu.name, edu.type, edu.yearTo));
        });
        parsedEqualData.WorkHistory.forEach(function (work) {
            $scope.equalWork.push(new Work(work.name, work.type, work.dateFrom, work.dateTo, work.city));
        });
        parsedEqualData.Interests.forEach(function (interest) {
            $scope.equalInterests.push(new Interest(interest.name, interest.description, interest.type));
        });
        if (!$scope.$$phase) {
            $scope.$apply();
        }
    }

    function errCB() {

    }

    $scope.loadConnectState = function () {
        //requester = 0 if requester is current user, else 1
        userConnectService.loadConnectState($scope.user.userid, function (connectRequestId, state, requester) {
            $scope.dataLoading = false;
            $scope.connectState = connectStates.fromNr(state);
            $scope.connectRequestId = connectRequestId;
            $scope.requester = requester;
            $scope.$apply();
        }, function () {
        });
    }

    //Send a connect request to the selected user
    $scope.sendConnectRequest = function () {
        $scope.dataLoading = true;
        userConnectService.sendConnectRequest($scope.user.userid, function () {
            $scope.dataLoading = false;
            $scope.connectState = connectStates.REQUESTED;
            $scope.$apply();
        }, function(){
        });
    };

    $scope.acceptConnectRequest = function () {
        $scope.dataLoading = true;
        userConnectService.acceptConnectRequest($scope.user.userid, $scope.connectRequestId, function () {
            $scope.dataLoading = false;
            $scope.connectState = connectStates.CONNECTED;
            $scope.$apply();
        }, function () {
        });
    };

    $scope.rejectConnectRequest = function () {
        $scope.dataLoading = true;
        userConnectService.rejectConnectRequest($scope.user.userid, $scope.connectRequestId, function () {
            $scope.dataLoading = false;
            $scope.connectState = connectStates.REJECTED;
            $scope.$apply();
        }, function () {
        });
    };
});