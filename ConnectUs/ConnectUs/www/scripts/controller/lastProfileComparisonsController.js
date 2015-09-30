
angular.module('connectusApp').controller('lastProfileComparisonsController', function ($scope, $rootScope, $location) {
    $scope.lastComparisons = [
        {
            overlapPct: 14,
            otherusername: "Tricia",
            place: "Sugar Pie",
            background: "odd"
        },
        {
            overlapPct: 44,
            otherusername: "Nils",
            place: "JKU",
            background: "even"
        },
        {
            overlapPct: 54,
            otherusername: "Wolf",
            place: "Linz",
            background: "odd"
        }
    ];

});
