angular.module('connectusApp').factory('callbackHandler', function ($timeout) {
    return {
        finished: function ($scope, saved) {
            $scope.loadingCounter--;
            $scope.saved = saved;
            if (!$scope.$$phase) 
                $scope.$apply();
            $timeout(function () {
                $scope.saved = false;
            }, 2000);
        }
    };
});