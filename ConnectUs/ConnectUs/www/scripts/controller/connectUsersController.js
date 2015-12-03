angular.module('connectusApp').controller('connectUsersController', function ($scope, $rootScope, callbackHandler, userComparisonService, userConnectService, connectStates) {
    $scope.init = function () {
        $scope.loadingCounter = 0;
        $scope.equalEducations = [];
        $scope.equalWork = [];
        $scope.equalTeams = [];
        $scope.equalAthletes = [];
        $scope.user = {};
        $rootScope.pageStackCount = 2;

        $scope.compareEducation = getBooleanFromLocalStorage('setting_education');
        $scope.compareWork = getBooleanFromLocalStorage('setting_work');
        $scope.compareTeams = getBooleanFromLocalStorage('setting_teams');
        $scope.compareAthletes = getBooleanFromLocalStorage('setting_athletes');
        $scope.compareObjects = [];
        if ($scope.compareEducation)
            $scope.compareObjects.push("EDUCATION");
        if ($scope.compareWork)
            $scope.compareObjects.push("WORK");
        if ($scope.compareTeams)
            $scope.compareObjects.push("TEAM");
        if ($scope.compareAthletes)
            $scope.compareObjects.push("ATHLETE");

        if ($rootScope.selectedUser) {
            $scope.user = $rootScope.selectedUser;

            $scope.loadingCounter++;
            if ($scope.user.accountId != -1) {
                userComparisonService.compareUsers($scope.user.accountId, $scope.compareObjects, comparisonFinished, errCB);
            } else {
                var maxMustermannComparison = JSON.parse("{\"EducationList\":[{\"name\":\"HTL Leonding\",\"type\":\"High School\",\"yearTo\":2011},{\"name\":\"Johannes Kepler University Linz\",\"type\":\"College\"}],\"WorkHistory\":[{\"name\":\"solvistas GmbH\",\"type\":\"Company\",\"city\":\"Linz, Austria\",\"dateFrom\":\"2013-05-31\"},{\"name\":\"Catalysts\",\"type\":\"Company\",\"dateFrom\":\"2012-08-01\",\"dateTo\":\"2012-09-30\"}],\"Interests\":[{\"name\":\"Das Nationalteam\",\"type\":2},{\"name\":\"Julian Baumgartlinger\",\"type\":2},{\"name\":\"Black Wings Linz\",\"type\":2},{\"name\":\"FC Pasching\",\"type\":2},{\"name\":\"FC Barcelona\",\"type\":2},{\"name\":\"Roger Federer\",\"type\":2},{\"name\":\"Hermann Maier\",\"type\":2},{\"name\":\"Eva - Maria Brem\",\"type\":2},{\"name\":\"Stan Wawrinka\",\"type\":2},{\"name\":\"Felix Neureuther\",\"type\":2},{\"name\":\"Zlatko Junuzovic\",\"type\":2},{\"name\":\"David Alaba\",\"type\":2}]}");
                comparisonFinished(maxMustermannComparison);
            }
            $rootScope.selectedUser = undefined;
        }
        else if ($rootScope.selectedUserComparison) {
            $scope.loadingCounter++;
            comparisonFinished($rootScope.selectedUserComparison);
            $scope.user.profilePicUrl = $rootScope.selectedUserComparison.profilePicUrl;
            $scope.user.accountId = $rootScope.selectedUserComparison.userId;
            $scope.user.username = $rootScope.selectedUserComparison.userName;
            $rootScope.selectedUserComparison = undefined;
        }
        $scope.loadConnectState();
        $scope.loadEqualConnections();
    };
    
    function comparisonFinished(result) {
        var parsedEqualData;
        if (result.equalJson)
            parsedEqualData = JSON.parse(result.equalJson);
        else
            parsedEqualData = result;  //Max mUstermann
        if (parsedEqualData.EducationList) {
            parsedEqualData.EducationList.forEach(function (edu) {
                $scope.equalEducations.push(new Education(edu.name, edu.type, edu.yearTo));
            });
        }
        if (parsedEqualData.WorkHistory) {
            parsedEqualData.WorkHistory.forEach(function (work) {
                $scope.equalWork.push(new Work(work.name, work.type, work.dateFrom, work.dateTo, work.city));
            });
        }
        if (parsedEqualData.Interests) {
            parsedEqualData.Interests.forEach(function (interest) {
                if (interest.type == 2) {
                    $scope.equalTeams.push(new Interest(interest.name, interest.description, interest.type));
                } else if (interest.type == 4) {
                    $scope.equalAthletes.push(new Interest(interest.name, interest.description, interest.type));
                }
            });
        }
        callbackHandler.finished($scope, false);
    }

    function errCB() {

    }

    $scope.loadConnectState = function () {
        if ($scope.user.accountId != -1) {
            $scope.loadingCounter++;
            //requester = 0 if requester is current user, else 1
            userConnectService.loadConnectState($scope.user.accountId, function (connectRequestId, state, requester) {
                $scope.connectState = connectStates.fromNr(state);
                $scope.connectRequestId = connectRequestId;
                $scope.requester = requester;
                callbackHandler.finished($scope, false);
            }, function () {
            });
        } else {
            $scope.connectState = 0;
        }
    }

    $scope.loadEqualConnections = function () {
        if ($scope.user.accountId != -1) {
            $scope.loadingCounter++;
            //requester = 0 if requester is current user, else 1
            userComparisonService.getEqualConnections($scope.user.accountId, function (equalConnections) {
                $scope.equalConnections = equalConnections;
                callbackHandler.finished($scope, false);
            }, function () {
                callbackHandler.finished($scope, false);
            });
        }
    }

    //Send a connect request to the selected user
    $scope.sendConnectRequest = function () {
        $scope.loadingCounter++;
        userConnectService.sendConnectRequest($scope.user.accountId, function () {
            $scope.connectState = connectStates.REQUESTED;
            callbackHandler.finished($scope, true);
        }, function(){
        });
    };

    $scope.acceptConnectRequest = function () {
        $scope.loadingCounter++;
        userConnectService.acceptConnectRequest($scope.user.accountId, $scope.connectRequestId, function () {
            $scope.connectState = connectStates.CONNECTED;
            callbackHandler.finished($scope, true);
            $rootScope.selectedUserId = $scope.user.accountId;
            menu.setMainPage('pages/userContact.html', { closeMenu: true });
        }, function () {
        });
    };

    $scope.rejectConnectRequest = function () {
        $scope.loadingCounter++;
        userConnectService.rejectConnectRequest($scope.user.accountId, $scope.connectRequestId, function () {
            $scope.connectState = connectStates.REJECTED;
            callbackHandler.finished($scope, true);
        }, function () {
        });
    };
});