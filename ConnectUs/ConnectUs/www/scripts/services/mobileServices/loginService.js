// http://go.microsoft.com/fwlink/?LinkID=290993&clcid=0x407

'use strict';

angular.module('connectusApp').factory('loginService',
    ['Base64', '$http', '$rootScope',
    function (Base64, $http, $rootScope) {
        var service = {};

        service.login = function (username, password, callback, cbErr) {
            if (!connectusClient)
                initMobileServiceClient();
            connectusClient.invokeApi('CustomLogin', {
                method: 'POST',
                body: { "username": username, "password": password }
            }).done(function (results) {
                callback(results.result);
              
            }, function (error) {
                cbErr(error);
                console.error("Login failed");    
            });

        };

        service.fbLogin = function (successCB, errCB) {
            providerLoginClient.login("facebook").then(function () {
                providerLoginClient.invokeApi('GetProviderToken?providerType=1', { method: 'GET' }).done(function (results) {
                    var token = results.result;
                    connectusClient.login("facebook", { "access_token": token }).then(function () {
                        connectusClient.invokeApi('InitialProviderSetup?providerType=1', { method: 'GET' })
                                .done(function (response) {
                                    successCB(response.result.accountId, response.result.firstLogin, connectusClient.currentUser);
                                }, function (error) {
                                    errCB();
                                });
                    }, function (error) {
                        errCB();
                    });
                }, function (error) {
                    errCB();
                });
            }, function (error) {
                errCB();
                console.error("Facebooklogin failed")
            });
        };

        service.liLogin = function (successCB, errCB) {
            connectusClient.login("linkedin").then(function () {
                connectusClient.invokeApi('InitialProviderSetup?providerType=2', { method: 'GET' })
                        .done(function (response) {
                            successCB(response.result.accountId, response.result.firstLogin, connectusClient.currentUser);
                        }, function (error) {
                            errCB();
                        });
            }, function (error) {
                errCB();
            });
        };

        service.setCredentials = function (userid, authresult) {
            $rootScope.accountId = userid;
            
            saveItemToLocalStorage('userid', userid);
            saveItemToLocalStorage('authresult', JSON.stringify(authresult));
        };

        service.clearCredentials = function () {
            removeItemFromLocalStorage('userid');
            removeItemFromLocalStorage('authresult');
        };

        service.tryLogin = function (success, noSuccess) {
            if (getItemFromLocalStorage("userid") == null || getItemFromLocalStorage("authresult") == null) {
                noSuccess();
                return;
            }
            service.setCredentials(getItemFromLocalStorage("userid"), JSON.parse(getItemFromLocalStorage('authresult')));
            var authresult = JSON.parse(getItemFromLocalStorage('authresult'));
            connectusClient.currentUser = {
                "userId": authresult.userId,
                "mobileServiceAuthenticationToken": authresult.mobileServiceAuthenticationToken
            };
            //connectusClient.invokeApi('CheckAuthToken', {
            //    method: 'GET'
            //}).done(function () {
            //    success();
            //}, function (error) {
            //    noSuccess();
            //});

            success();
        };

        return service;
    }])
.factory('Base64', function () {
    /* jshint ignore:start */
    var keyStr = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=';

    return {
        encode: function (input) {
            var output = "";
            var chr1, chr2, chr3 = "";
            var enc1, enc2, enc3, enc4 = "";
            var i = 0;

            do {
                chr1 = input.charCodeAt(i++);
                chr2 = input.charCodeAt(i++);
                chr3 = input.charCodeAt(i++);

                enc1 = chr1 >> 2;
                enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
                enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
                enc4 = chr3 & 63;

                if (isNaN(chr2)) {
                    enc3 = enc4 = 64;
                } else if (isNaN(chr3)) {
                    enc4 = 64;
                }

                output = output +
                    keyStr.charAt(enc1) +
                    keyStr.charAt(enc2) +
                    keyStr.charAt(enc3) +
                    keyStr.charAt(enc4);
                chr1 = chr2 = chr3 = "";
                enc1 = enc2 = enc3 = enc4 = "";
            } while (i < input.length);

            return output;
        },

        decode: function (input) {
            var output = "";
            var chr1, chr2, chr3 = "";
            var enc1, enc2, enc3, enc4 = "";
            var i = 0;

            // remove all characters that are not A-Z, a-z, 0-9, +, /, or =
            var base64test = /[^A-Za-z0-9\+\/\=]/g;
            if (base64test.exec(input)) {
                window.alert("There were invalid base64 characters in the input text.\n" +
                    "Valid base64 characters are A-Z, a-z, 0-9, '+', '/',and '='\n" +
                    "Expect errors in decoding.");
            }
            input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");

            do {
                enc1 = keyStr.indexOf(input.charAt(i++));
                enc2 = keyStr.indexOf(input.charAt(i++));
                enc3 = keyStr.indexOf(input.charAt(i++));
                enc4 = keyStr.indexOf(input.charAt(i++));

                chr1 = (enc1 << 2) | (enc2 >> 4);
                chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
                chr3 = ((enc3 & 3) << 6) | enc4;

                output = output + String.fromCharCode(chr1);

                if (enc3 != 64) {
                    output = output + String.fromCharCode(chr2);
                }
                if (enc4 != 64) {
                    output = output + String.fromCharCode(chr3);
                }

                chr1 = chr2 = chr3 = "";
                enc1 = enc2 = enc3 = enc4 = "";

            } while (i < input.length);

            return output;
        }
    };

    /* jshint ignore:end */
});