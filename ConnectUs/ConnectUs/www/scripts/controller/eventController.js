
angular.module('connectusApp').controller('eventController', function ($scope, $rootScope, callbackHandler, eventService) {
    $scope.loadingCounter = 0;
    $rootScope.pageStackCount = 2;
    $scope.accountId = $rootScope.accountId;

    $scope.init = function () {
        $scope.participate = 0;
        $scope.event = $rootScope.selectedEvent;
        eventService.getParticipants($scope.event.id, function (participators) {
            participators.forEach(function (participator) {
                if(participator.accountId == $scope.accountId)
                    $scope.participate = 1;
            });
            $scope.participators = participators;
            callbackHandler.finished($scope, false);
        }, function () {
            callbackHandler.finished($scope, false);
        });
    };

    $scope.registerEvent = function () {
        $scope.loadingCounter++;
        eventService.participate($scope.event.id, $rootScope.accountId, function () {
            $scope.participate = 1;
            callbackHandler.finished($scope, false);
        }, function () {
            callbackHandler.finished($scope, false);
        });
    };

});