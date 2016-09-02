
    'use strict';
    angular.module('app').controller('EditUserController', EditUserController);

    EditUserController.$inject = ['$location', '$scope', '$http', 'loyaltysFactory','appService'];

    function EditUserController($location, $scope, $http, loyaltysFactory, appService) {

        $('#loadTxt').html("Loading update user account...");
        setBusyLoaderVisibility(true);
        $scope.userDetails = {
            FirstName: undefined,
            LastName: undefined,
            EmailID: undefined,
            Password: undefined,
            Gender: undefined,
            DateofBirth: undefined,
            MobileNumber: undefined,
            UserID:undefined
        };

        //Update user details
        $scope.updateuser = function updateuser() {
            $('#loadTxt').html("Loading update user account...");
            setBusyLoaderVisibility(true);

            $scope.userDetails.FirstName = $scope.FirstName;
            $scope.userDetails.LastName = $scope.LastName;
            $scope.userDetails.EmailID = $scope.EmailID;
            $scope.userDetails.Password = $scope.Password;
            $scope.userDetails.Gender = $scope.Gender;
            $scope.userDetails.DateofBirth = $scope.DateofBirth;
            $scope.userDetails.MobileNumber = $scope.MobileNumber;
            $scope.userDetails.UserID = $scope.UserID;
            var data = $scope.userDetails;

            loyaltysFactory.updateUser(data).then(function (items) {
                console.log('items');
                setBusyLoaderVisibility(false);
            }, function (status) {
                setBusyLoaderVisibility(false);
                console.log('Error=>' + status);
            });
        }

        //Load User details
        loyaltysFactory.getUser().then(function (items) {
           
            $scope.userDetails = JSON.parse(items);
            $scope.FirstName = $scope.userDetails[0].FirstName;
            $scope.LastName = $scope.userDetails[0].LastName;
            $scope.Gender = $scope.userDetails[0].Gender;
            $scope.EmailID = $scope.userDetails[0].EmailID;
            $scope.Password = $scope.userDetails[0].Password;
            $scope.UserID = $scope.userDetails[0].UserID;
            var dt = new Date($scope.userDetails[0].DateofBirth);
            $scope.DateofBirth = dt;
            $scope.MobileNumber = parseInt($scope.userDetails[0].MobileNumber);
            setBusyLoaderVisibility(false);
        }, function (status) {
            setBusyLoaderVisibility(false);
            console.log('Error=>' + status);
        });
    }

