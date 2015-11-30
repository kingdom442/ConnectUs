
angular.module('connectusApp').controller('settingsController', function ($scope, $rootScope) {
    $rootScope.pageStackCount = 2;

    $scope.settings = {
        bluetootAllowed: getBooleanFromLocalStorage('setting_bluetooth'),
        geolocationEnabled: getBooleanFromLocalStorage('setting_geolocation'),
        searchArea: getItemFromLocalStorage('setting_searcharea'),

        includeEducation: getBooleanFromLocalStorage('setting_education'),
        includeWork: getBooleanFromLocalStorage('setting_work'),
        includeTeams: getBooleanFromLocalStorage('setting_teams'),
        includeAthletes: getBooleanFromLocalStorage('setting_athletes')
    };

    $scope.changeSettings = function () {
        saveItemToLocalStorage('settings_bluetooth', $scope.settings.bluetoothAllowed);
        saveItemToLocalStorage('setting_geolocation', $scope.settings.geolocationEnabled);
        saveItemToLocalStorage('setting_education', $scope.settings.includeEducation);
        saveItemToLocalStorage('setting_work', $scope.settings.includeWork);
        saveItemToLocalStorage('setting_teams', $scope.settings.includeTeams);
        saveItemToLocalStorage('setting_athletes', $scope.settings.includeAthletes);
    }

    $scope.changeSearchArea = function () {
        menu.setSwipeable(false);
        saveItemToLocalStorage('setting_searcharea', $scope.settings.searchArea);
    }

    $scope.enableMenuSlide = function () {
        menu.setSwipeable(true);
    }
});