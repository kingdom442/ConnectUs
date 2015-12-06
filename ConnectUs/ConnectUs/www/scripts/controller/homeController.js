
angular.module('connectusApp').controller('homeController', function ($scope, $rootScope, $filter, userComparisonService, userService, contextService, eventService, loginService, callbackHandler) {
    $scope.username = $rootScope.username = window.localStorage.getItem('username');
    $scope.lastComparisons = [];
    $scope.events = [];
    $scope.loadingCounter = 0;
    $rootScope.pageStackCount = 1;

    function onPostCtxSuccess() {
       //Maybe some welcome indicator?
    }

    function onLastCmpSuccess(lastComparisonsResult) {
        lastComparisonsResult.forEach(function (comparison) {
            $scope.lastComparisons.push(new UserComparison(comparison.userId, comparison.userName, $filter('date')(new Date(comparison.date), 'dd.MM.yyyy'), comparison.equalData, comparison.profilePic));
        });
        callbackHandler.finished($scope, false);
    }


    $scope.showUserComparison = function (userCmp) {
        $rootScope.selectedUserComparison = userCmp;
        menu.setMainPage('pages/connectUsers.html', { closeMenu: true });
    };

    $scope.getEvents = function () {
        $scope.loadingCounter++;
        eventService.getEvents(function (events) {
            events.forEach(function (event) {
                $scope.events.push(new Event(event.id, event.name, event.description, event.place, $filter('date')(new Date(event.fromDate), 'dd.MM.yyyy'), $filter('date')(new Date(event.toDate), 'dd.MM.yyyy')));
            });
            callbackHandler.finished($scope, false);
        }, function () { });
    };

    $scope.init = function () {
        $scope.loadingCounter++;
        if ($rootScope.username == null) {
            userService.loadUser($rootScope.accountId, function (user) {
                callbackHandler.finished($scope, false);
                if (user.username == '-') {
                    $rootScope.openUsernamePrompt();

                } else {
                    $rootScope.username = user.username;
                    $scope.loadHomeData();
                }
            }, function () {
                callbackHandler.finished($scope, false);
            });
        } else {
            $scope.loadHomeData();
        }
    };

    $rootScope.openUsernamePrompt = function () {
        $rootScope.initSettings = {
            username: '',
            businessInterested: true,
            privateInterested: true
        };
        ons.createDialog('initialSettings.html').then(function (initialDialog) {
            initialDialog.show();
        });
        $scope.$apply();
    
    }

    var lastSelectedInterestIsBusiness = true;
    $rootScope.changeInitialInterest = function () {
        if (!$rootScope.initSettings.businessInterested && !$rootScope.initSettings.privateInterested) {
            if (lastSelectedInterestIsBusiness) {
                $rootScope.initSettings.privateInterested = true;
                lastSelectedInterestIsBusiness = false;
            } else {
                $rootScope.initSettings.businessInterested = true;
                lastSelectedInterestIsBusiness = false;
            }
        } else if ($rootScope.initSettings.businessInterested && !$rootScope.initSettings.privateInterested) {
            lastSelectedInterestIsBusiness = true;
        } else if ($rootScope.initSettings.businessInterested && !$rootScope.initSettings.privateInterested) {
            lastSelectedInterestIsBusiness = false;
        }
    }

    $rootScope.saveInitialSettings = function () {
        if (!$rootScope.initSettings.username || $rootScope.initSettings.username == '' || $rootScope.initSettings.username == '-')
            $rootScope.openUsernamePrompt();
        else {
            $rootScope.username = $rootScope.initSettings.username;
            $scope.username = $rootScope.initSettings.username;
            $scope.loadingCounter++;
            userService.changeAccountInfo($rootScope.accountId, $rootScope.initSettings.username, $rootScope.initSettings.businessInterested, $rootScope.initSettings.privateInterested, $scope.onChangeUserNameSuccess, $scope.onChangeUserNameFailure);
        }
    }

    $scope.onChangeUserNameSuccess = function () {
        callbackHandler.finished($scope, false);
        $scope.synchronizeProfile();
        $scope.loadHomeData();
        $scope.welcome = true;
    }

    $scope.closeWelcomeInfo = function () {
        $scope.welcome = false;
    }

    $scope.onChangeUserNameFailure = function () {
        callbackHandler.finished($scope, false);
        ons.notification.alert({
            message: 'An error has occurred! Please try it again later!'
                
        });
    }

    $scope.loadHomeData = function () {
        contextService.postContext(onPostCtxSuccess);
        $scope.getEvents();
        $scope.loadingCounter++;
        userComparisonService.getLastComparisonsForUser($rootScope.accountId, onLastCmpSuccess, function (err) { callbackHandler.finished($scope, false);  });
    };

    $scope.synchronizeProfile = function () {
        $scope.loadingCounter++;

        userService.syncInconsistentProfile(function () {
            callbackHandler.finished($scope, false);
        }, function () {
            callbackHandler.finished($scope, false);
        });
    }

    $scope.gotoEvent = function (event) {
        $rootScope.selectedEvent = event;
        menu.setMainPage('pages/event.html', { closeMenu: true });
    };
});
