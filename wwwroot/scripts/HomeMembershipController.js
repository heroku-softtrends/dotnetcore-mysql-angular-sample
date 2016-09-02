 
'use strict';
angular.module('app').controller('HomeMembershipController', HomeMembershipController);

HomeMembershipController.$inject = ['$location', '$scope', '$http','loyaltysFactory','$window'];

function HomeMembershipController($location, $scope, $http, loyaltysFactory, $window) {
 
    $('#loadTxt').html("Loading Membership...");
    setBusyLoaderVisibility(true);
    //Load Membership
    loyaltysFactory.getMembership().then(function (items) {
       
        $scope.membership = JSON.parse(items);
        setBusyLoaderVisibility(false);
     
    }, function (status) {
        console.log('Error=>' + status);
    });

    //Edit Membership
    $scope.editmembership = function editmembership(id) {        
        $window.location.href = '/Membership/Edit?id=' + id;
    }
}

 