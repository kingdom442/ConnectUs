angular.module('connectusApp').controller('connectUsersController', function ($scope, $rootScope, userComparisonService, userConnectService, connectStates) {
    $scope.dataLoading = true;
    $scope.equalEducations = [];
    $scope.equalWork = [];
    $scope.equalInterests = [];
    $scope.connectState = connectStates.NONE;
    if ($rootScope.selectedUser) {
        $scope.user = $rootScope.selectedUser;
        userComparisonService.compareUsers($scope.user.userid, comparisonFinished, errCB);
        $rootScope.selectedUser = undefined;
    }
    else if ($rootScope.selectedUserComparison) {
        comparisonFinished($rootScope.selectedUserComparison);
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
        if (!$scope.$$phase) {
            $scope.$apply();
        }
    }

    function errCB() {

    }

    //Send a connect request to the selected user
    $scope.connectRequest = function() {
        userConnectService.connectRequest($scope.user.userid, function () {
            $scope.connectState = connectStates.REQUESTED;
            $scope.$apply();
        }, function(){
        });
    };

});