
angular.module('connectusApp').controller('userProfileController', function ($scope, $rootScope, userService, callbackHandler, loginService) {
    $scope.userinfo = new UserInfo($rootScope.accountId, $rootScope.username, '', undefined, "Age", Date.now);
    $scope.editProfileImg = 'images/profile-icon.png';
    $scope.loadingCounter = 0;

    $scope.loadUserInfo = function () {
        $scope.loadingCounter++;
        userService.loadUserInfo($rootScope.accountId, function (userinfo) {
            if (userinfo) {
                $scope.userinfo = userinfo;
                $scope.editProfileImg = userinfo.profilePicUrl;
            }
            callbackHandler.finished($scope, false);
        }, function (error) {
            callbackHandler.finished($scope, false);
        });
    }

    $scope.synchronizeFBProfile = function () {
        $scope.loadingCounter++;
       
        userService.syncFBUserInfo(function () {
            callbackHandler.finished($scope, true);
        }, function (error) {
            callbackHandler.finished($scope, fálse);
            modalSyncFailed.show();
        });
    }

    $scope.save = function (form) {
        if (form.$valid) {
            $scope.loadingCounter++;
            userService.updateUserInfo($scope.userinfo, function (suc) {
                if ($scope.userinfo.userContact.phoneNumber || $scope.userinfo.userContact.eMail) {
                    userService.updateUserContact($scope.userinfo, function (suc) {
                    callbackHandler.finished($scope, true);
                    }, function (err) {
                        callbackHandler.finished($scope, false);
                    });
                } else {
                    callbackHandler.finished($scope, true);
                }
            }, function (err) {
                callbackHandler.finished($scope, false);
            });
        }
    };

    $scope.editProfile = function(){
        profileCarousel.next({
          animation: "slide",
          callback: function() {
            // Fired when the scroll is done
          }
        });
    }
    $scope.editPicture = function () {
        navigator.camera.getPicture(onSuccess, onFail, {
            quality: 50,
            destinationType: Camera.DestinationType.DATA_URL,
            encodingType: Camera.EncodingType.JPEG,
            destinationType: Camera.DestinationType.DATA_URL
        });

        function onSuccess(imageData) {
            $scope.editProfileImg = "data:image/jpeg;base64," + imageData;
            $scope.$apply();
        }

        function onFail(message) {
            alert('Failed because: ' + message);
        }
    }

    $scope.showAgePopover = function () {
        ageDialog.show();
    }

    $scope.goBack = function () {
        profileCarousel.prev({
            animation: "slide",
            callback: function () {
                // Fired when the scroll is done
            }
        });
    }

    $scope.init = function() {
        document.addEventListener("deviceready", onDeviceReady, false);
        $scope.loadUserInfo();
    }
    var initializing = true;
    $scope.changeFBConnection = function () {
        if (initializing) {
            initializing = false;
            return;
        }
        if ($scope.userinfo.fbConnected) {
            $scope.loadingCounter++;
            loginService.fbLogin(function () {
                callbackHandler.finished($scope, false);
            });
        } else {

        }
    }

    // device APIs are available
    function onDeviceReady() {
        document.addEventListener("backbutton", onBackKeyDown, false);
    }

    // Handle the back button
    //
    function onBackKeyDown() {
        if (profileCarousel.getActiveCarouselItemIndex() == 1) {
            profileCarousel.previous({
                animation: "slide",
                callback: function () {
                }
            });
        } else {
            menu.setMainPage('pages/home.html');
        }
    }

});
