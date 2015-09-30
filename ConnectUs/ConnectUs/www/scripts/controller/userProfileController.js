
angular.module('connectusApp').controller('userProfileController', function ($scope, $rootScope, $timeout, userService, loginService) {
    $scope.userinfo = new UserInfo($rootScope.accountId, $rootScope.username, '', undefined, "Age", Date.now);
    $scope.editProfileImg = 'images/profile-icon.png';

    $scope.loadUserInfo = function () {
        $scope.dataLoading = true;
        userService.loadUserInfo(function (userinfo) {
            if (userinfo) {
                $scope.userinfo = userinfo;
                $scope.editProfileImg = userinfo.profilePicUrl;
            }
            $scope.dataLoading = false;
            $scope.$apply();
        }, function (error) {
            $scope.dataLoading = false;
            $scope.$apply();
        });
    }

    $scope.synchronizeFBProfile = function () {
        $scope.dataLoading = true;
       
        userService.syncFBUserInfo(function () {
            $scope.dataLoading = false;
            $scope.saved = true;
            $scope.$apply();
            $timeout(function(){
                $scope.saved=false;
            }, 2000);
        }, function (error) {
            $scope.dataLoading = false;
            $scope.$apply();
            modalSyncFailed.show();
        });
    }

    $scope.save = function () {
        $scope.dataLoading = true;
        userService.updateUserInfo($scope.userinfo, function (suc) {
            $scope.goBack();
            $scope.dataLoading = false;
            $scope.$apply();
        }, function (err) {
            $scope.dataLoading = false;
            $scope.$apply();
        });
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
            $scope.dataLoading = true;
            loginService.fbLogin(function () {

                $scope.dataLoading = false;
                $scope.$apply();
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
