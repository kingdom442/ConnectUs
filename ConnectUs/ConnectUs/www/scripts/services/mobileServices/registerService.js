// http://go.microsoft.com/fwlink/?LinkID=290993&clcid=0x407


angular.module('connectusApp').service('registerService', function () {
    this.register = function (username, password, succCB, errCB) {
       if (!connectusClient)
                initMobileServiceClient();
        connectusClient.invokeApi('CustomRegistration', {
            method: 'POST',
            body: { username: username, password: password, email: '-' }
        }).done(function (response) {
            succCB();
        }, function (error) {
            errCB();
        });
    };
});
