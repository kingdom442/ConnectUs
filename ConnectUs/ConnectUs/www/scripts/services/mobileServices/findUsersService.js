﻿
'use strict';


angular.module('connectusApp').factory('findUsersService',
    ['$rootScope',
    function ($rootScope) {
        var service = {};

        service.findAvailableUsersByGeoLocation = function (coords, findAlreadyComparedUsers, maxDistance, succCB, errCB) {
            if (connectusClient == null)
                initMobileServiceClient()
            if (coords == 0){
                coords.longitude = 0;
                coords.latitude = 0;
            }
            connectusClient.invokeApi('FindUsers', {
                method: 'POST',
                body: { "UserId": $rootScope.accountId, "MaxDistance": maxDistance, "Longitude": coords.longitude, "Latitude": coords.latitude, "AlreadyCompared": findAlreadyComparedUsers }
            }).done(function (response) {
                succCB(response.result);
            }, function (error) {
                console.error("Finds users failed:" + error);
                errCB();
            });
            
        };

        return service;
    }
]);