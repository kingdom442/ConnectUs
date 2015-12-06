angular.module('connectusApp').service('eventService', function () {

    //Get the running and upcoming events
    this.getEvents = function (succCB, errCB) {
        //TODO: filter finished events (odata query)
        eventTable().orderBy("toDate").read().done(function (results) {
            succCB(results);
        }, function (err) {
            alert("Error: " + err);
        });
    }

    this.getParticipants = function (eventid, succCB, errCB) {
        connectusClient.invokeApi('GetEventParticipants', {
            method: 'POST',
            body: { "eventid": eventid }
        }).done(function (results) {
            succCB(results.result);
        }, function (error) {
            errCB(error);
        });
    }

    this.participate = function (eventid, userid, succCB, errCB) {
        connectusClient.invokeApi('ParticipateEvent', {
            method: 'POST',
            body: { "accountid": userid, "eventid": eventid}
        }).done(function (results) {
            succCB();
        }, function (error) {
            errCB(error);
        });
    }
});