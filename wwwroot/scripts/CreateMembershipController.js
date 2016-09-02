(function () {
    'use strict';

    angular
        .module('app')
        .controller('CreateMembershipController', CreateMembershipController);

    CreateMembershipController.$inject = ['$location', '$scope', '$http', 'loyaltysFactory'];

    function CreateMembershipController($location, $scope, $http, loyaltysFactory, $window) {

        $scope.memberDetails = {
            MemberID: undefined,
            LoyaltyCardNo: undefined,
            ExpirationDate: undefined,
            MemberSiteURL: undefined
        };

        $scope.addmembership = function addmembership() {
            $('#loadTxt').html("Loading Add Membership...");
            setBusyLoaderVisibility(true);
            var data = $scope.memberDetails;
            loyaltysFactory.createMembership(data).then(function (items) {
                console.log('items');
                window.location = '/Membership/index';
            }, function (status) {
                console.log('Error=>' + status);
            });
        }       
    }

})();