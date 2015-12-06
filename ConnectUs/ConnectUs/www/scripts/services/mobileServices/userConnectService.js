

angular.module('connectusApp').service('userConnectService', function ($rootScope, connectStates) {

    //Send a connect request to the selected user
    this.sendConnectRequest = function (connectUserId, successCB, errCB) {
        connectTable().insert({
            RequestUserId: $rootScope.accountId,
            ConnectUserId: connectUserId, 
            Accepted: null
        }).done(function (result) {
            successCB();
        }, function (err) {
            console.error("Error: " + err);
        });
    }

    this.acceptConnectRequest = function (connectUserId, connectRequestId, successCB, errCB) {
        connectTable().update({
            id: connectRequestId,
            Accepted: true
        }).done(function (result) {
            successCB();
        }, function (err) {
            console.error("Error: " + err);
        });
    }

    this.rejectConnectRequest = function (connectUserId, connectRequestId, successCB, errCB) {
        connectTable().update({
            id: connectRequestId,
            Accepted: false
        }).done(function (result) {
            successCB();
        }, function (err) {
            console.error("Error: " + err);
        });
    }

    this.loadConnectState = function (connectUserId, successCB, errCB) {
        connectTable().where({
            RequestUserId: $rootScope.accountId,
            ConnectUserId: connectUserId
        }).select("Id", "Accepted").take(1).read().done(function (result) {
            if (result.length == 0) {
                connectTable().where({
                    RequestUserId: connectUserId,
                    ConnectUserId: $rootScope.accountId
                }).select("Id", "Accepted").take(1).read().done(function (result) {
                    if (result.length == 0) {
                        successCB(undefined, connectStates.NONE, 0);
                    }
                    else if (result[0].accepted == true)
                        successCB(result[0].id, connectStates.CONNECTED, 1);
                    else if (result[0].accepted == false)
                        successCB(result[0].id, connectStates.REJECTED, 1);
                    else
                        successCB(result[0].id, connectStates.REQUESTED, 1);
                }, function (err) {
                    console.error("Error: " + err);
                });
            }
            else if (result[0].accepted == true)
                successCB(result[0].id, connectStates.CONNECTED, 0);
            else if (result[0].accepted == false)
                successCB(result[0].id, connectStates.REJECTED, 0);
            else
                successCB(result[0].id, connectStates.REQUESTED, 0);
        }, function (err) {
            console.error("Error: " + err);
        });
    }

    this.loadConnectedUsers = function (userId, successCB, errCB) {
        connectusClient.invokeApi('ConnectedUsers', {
            method: 'POST',
            body: { "accountid": userId }
        }).done(function (response) {
            successCB(response.result);
        }, function (error) {
            console.error(error);
            errCB(error);
        });
    };
});
