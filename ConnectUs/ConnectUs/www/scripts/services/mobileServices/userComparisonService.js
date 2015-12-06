

angular.module('connectusApp').service('userComparisonService', function ($rootScope) {

    this.compareUsers = function (compUserId, compareObjects, successCB, errCB) {
        connectusClient.invokeApi('CompareUsers', {
            method: 'POST',
            body: {
                "userId": $rootScope.accountId, "compUserId": compUserId,
                "compareObjects": compareObjects
            }
        }).done(function (response) {
            successCB(response.result);
        }, function (error) {
            console.error(error);
            errCB(error);
        });
    }

    this.getEqualConnections = function (compUserId, successCB, errCB) {
        connectusClient.invokeApi('EqualConnections', {
            method: 'POST',
            body: {
                "userId": $rootScope.accountId, "compUserId": compUserId
            }
        }).done(function (response) {
            successCB(response.result);
            //TODO implement callback
        }, function (error) {
            console.error(error);
            errCB(error);
        });
    }


    /* 
    Gets the last 3 comparisons, that another user has done for the given user.
    */
    this.getLastComparisonsForUser = function (userId, successCB, errCB) {
        connectusClient.invokeApi('UnseenComparisons', {
            method: 'POST',
            body: {
                "userId": userId
            }
        }).done(function (response) {
            successCB(response.result);
        }, function (error) {
            console.error(error);
            errCB(error);
        });
    }
});
