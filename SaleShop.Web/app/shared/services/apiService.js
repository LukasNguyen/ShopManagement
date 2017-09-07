(function (app) {
    app.service('apiService', apiService);
    apiService.$inject = ['$http'];
    function apiService($http) {
        return {
            get:get
        }
        function get(url, params, success, failure) {
            $http.get(url, params).then(
               (result) => {
                success(result);
            }, (error) => {
                failure(error);
            });
        }
    }
})(angular.module('saleshop.common'));