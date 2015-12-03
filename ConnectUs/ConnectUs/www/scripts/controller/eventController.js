
angular.module('connectusApp').controller('eventController', function ($scope, $rootScope, callbackHandler, eventService) {
    $scope.loadingCounter = 0;
    $rootScope.pageStackCount = 2;
    $scope.accountId = $rootScope.accountId;

    $scope.init = function () {
        $scope.participate = 0;
        $scope.event = $rootScope.selectedEvent;
        eventService.getParticipants($scope.event.id, function (participators) {
            if (participators.length == 0)
                $scope.noOtherParticipators = true;
            participators.forEach(function (participator) {
                if (participator.accountId == $scope.accountId) {
                    $scope.participate = 1;
                    if (participators.length == 1)
                        $scope.noOtherParticipators = true;
                }
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