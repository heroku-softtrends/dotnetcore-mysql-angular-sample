var app = angular.module('app');
app.factory('loyaltysFactory', ['$http', '$q', 'appService', function ($http, $q, appService) {

    var loyaltyObjects = {};

    //Get user details
    loyaltyObjects.getUser = function () {
        return appService.httpRequest("/User/GetUser", "GET", "", $q, $http)
    }

    //Update user details
    loyaltyObjects.updateUser = function (usr) {
        return appService.httpRequest("/User/UpdateUser", "POST", usr, $q, $http)
    }

    //Add membership
    loyaltyObjects.createMembership = function (member) {
        return appService.httpRequest("/Membership/AddMembership", "POST", member, $q, $http)
    }

     //Get membership
    loyaltyObjects.getMembership = function () {
        return appService.httpRequest("/Membership/GetMembership", "GET", "", $q, $http)
    }

    //Get Membership by id 
    loyaltyObjects.getMemberbyID = function (member) {
        return appService.httpRequest("/Membership/GetMemberbyID", "GET", member, $q, $http)

    }
    //Update membership
    loyaltyObjects.updateMembership = function (mem) {
        return appService.httpRequest("/Membership/UpdateMembership", "POST", mem, $q, $http)
    }

    return loyaltyObjects;
}]);