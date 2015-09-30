
'use strict';


angular.module('connectusApp').factory('findUsersService',
    ['$rootScope',
    function ($rootScope) {
        var service = {};

        service.findAvailableUsersByGeoLocation = function (coords, findAlreadyComparedUsers, callback) {
            if (connectusClient == null)
                initMobileServiceClient()
            connectusClient.invokeApi('FindUsers', {
                method: 'POST',
                body: { "UserId": $rootScope.accountId, "MaxDistance": 1000, "Longitude": coords.longitude, "Latitude": coords.latitude, "AlreadyCompared": findAlreadyComparedUsers }
            }).done(function (response) {
                callback(response.result);
            }, function (error) {
                console.error("Finds users failed:" + error);
            });
            
        };

        return service;
    }
]);