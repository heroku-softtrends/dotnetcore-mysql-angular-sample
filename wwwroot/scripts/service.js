
var app = angular.module('app', ['ngCookies']);

app.service('appService', function ($cookies) {

    return {
        httpRequest: function (url, method, data, $q, $http) {
            var deferred = $q.defer();
            $http({
                url: url,
                method: method,
                params: data
            }).success(function (response) {
                deferred.resolve(response);
            }).error(function (error) {
                deferred.reject(error);
            });
            return deferred.promise;
        },
        dateCompare: function (date1, date2) {
            var now = new Date();
            var then = new Date(date);

            return date1 > date2 ? true : false;
        },
        getDate: function (date) {
            var dt = new Date(date);
            if (!dt.isValid())
                return null;

            return dt;
        },
        resetAllInputFiles: function () {
            if ($('input[type=file]')) {
                $('input[type=file]').each(function () {
                    $(this).val('');
                });
            }
        }
    }
});
