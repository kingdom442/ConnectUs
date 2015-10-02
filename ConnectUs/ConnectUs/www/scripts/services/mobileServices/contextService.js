

angular.module('connectusApp').service('contextService', function($rootScope){       
    this.postContext = function (successCB) {
        if (connectusClient == undefined)
            initMobileServiceClient()

        var onSuccess = function (position) {
            connectusClient.getTable('UserContext').insert({ "userId": $rootScope.accountId, "longitude": position.coords.longitude, "latitude": position.coords.latitude })
            .done(function (response) {
                successCB();
            }, function (error) {
                var xhr = error.request;
                showAlert('Error - status code: ' + xhr.status + '; body: ' + xhr.responseText);
            });
        };

        // onError Callback receives a PositionError object
        //
        function onError(error) {
            showAlert('code: ' + error.code + '\n' +
                  'message: ' + error.message + '\n', '');
        }

        navigator.geolocation.getCurrentPosition(onSuccess, onError);
    };

    this.showBlu = function(){
        bluetoothSerial.showBluetoothSettings(
            function (){
            }, 
            function(){
        
            });
    }
});
