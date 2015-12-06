angular.module('connectusApp').factory('connectStates', function connectStatesFactory() {
    var connectStates = {
        NONE: 0,
        REQUESTED: 1,
        CONNECTED: 2,
        REJECTED: 3
    };
    connectStates.fromNr = function (nr) {
        if (nr == 0)
            return connectStates.NONE;
        else if (nr == 1)
            return connectStates.REQUESTED;
        else if (nr == 2)
            return connectStates.CONNECTED;
        else if (nr == 3)
            return connectStates.REJECTED;
    }
    return connectStates;
});