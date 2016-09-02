(function () {
    'use strict';

    angular.module('app').controller('EditMembershipController', EditMembershipController);
    EditMembershipController.$inject = ['$location', '$scope', '$http', 'loyaltysFactory'];

    function EditMembershipController($location, $scope, $http, loyaltysFactory, $window) {

        $('#loadTxt').html("Loading Update Membership...");
        setBusyLoaderVisibility(true);

        var queryString = location.search;
        var res = queryString.split("=");


        $scope.memberDetails = {
            MemberID: undefined,
            LoyaltyCardNo: undefined,
            ExpirationDate: undefined,
            MemberSiteURL: undefined,
            MembershipID: undefined
        };

        $scope.membership = {
            MemberID: undefined,
            LoyaltyCardNo: undefined,
            ExpirationDate: undefined,
            MemberSiteURL: undefined,
            MembershipID: undefined
        };

        $scope.membershipDetails = {
            MembershipID: undefined
        };

        //Load Membership
        $scope.MembershipID = res[1];
        $scope.membershipDetails.MembershipID = $scope.MembershipID;
        loyaltysFactory.getMemberbyID($scope.membershipDetails).then(function (items) {
            $scope.memberDetails = JSON.parse(items);
            $scope.MemberID = $scope.memberDetails[0].MemberID;
            $scope.LoyaltyCardNo = $scope.memberDetails[0].LoyaltyCardNo;
            $scope.MemberSiteURL = $scope.memberDetails[0].MemberSiteURL;
            $scope.MembershipID = $scope.memberDetails[0].MembershipID;
            var dt = new Date($scope.memberDetails[0].ExpirationDate);
            $scope.ExpirationDate = dt;
            setBusyLoaderVisibility(false);
        }, function (status) {
            console.log('Error=>' + status);
        });

        //Update Membership
        $scope.updatemembership = function updatemembership() {
            $('#loadTxt').html("Loading Update Membership...");
            setBusyLoaderVisibility(true);
            $scope.membership.MemberID = $scope.MemberID;
            $scope.membership.MemberSiteURL = $scope.MemberSiteURL;
            $scope.membership.ExpirationDate = $scope.ExpirationDate;
            $scope.membership.LoyaltyCardNo = $scope.LoyaltyCardNo;
            $scope.membership.MembershipID = $scope.MembershipID;
            var data = $scope.membership;
            loyaltysFactory.updateMembership(data).then(function (items) {
                console.log('items');
                window.location = '/Membership/index';
            }, function (status) {
                console.log('Error=>' + status);
            });
        }
    }

})();