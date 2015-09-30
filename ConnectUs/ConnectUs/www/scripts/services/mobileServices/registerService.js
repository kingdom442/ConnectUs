// http://go.microsoft.com/fwlink/?LinkID=290993&clcid=0x407


angular.module('connectusApp').service('registerService', function(){       
    this.register = function (username, password) {
       if (!connectusClient)
                initMobileServiceClient();
        connectusClient.invokeApi('CustomRegistration', {
            method: 'POST',
            body: { username: username, password: password, email: '-' }
        }).done(function (response) {
            showAlert('Successfully registered');
        }, function (error) {
            var xhr = error.request;
            showAlert('Error - status code: ' + xhr.status + '; body: ' + xhr.responseText);
        });
    };
});
