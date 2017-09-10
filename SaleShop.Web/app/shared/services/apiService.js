(function (app) {
    app.service('apiService', apiService);
    apiService.$inject = ['$http','notificationService'];
    function apiService($http, notificationService) {
        return {
            get: get,
            post: post,
            put:put
        }
        function get(url, params, success, failure) {
            $http.get(url, params).then(
               (result) => {
                success(result);
            }, (error) => {
                failure(error);
            });
        }
        function post(url, data, success, failure) {
            $http.post(url, data).then(
                (result) => {
                    success(result);
                }, (error) => {
                    if(error.status === 401)
                        notificationService.displayError('Authenticate is required');
                    else if(failure!=null)
                        failure(error);
                });
        }
        function put(url, data, success, failure) {
            $http.put(url, data).then(
                (result) => {
                    success(result);
                }, (error) => {
                    if (error.status === 401)
                        notificationService.displayError('Authenticate is required');
                    else if (failure != null)
                        failure(error);
                });
        }
    }
})(angular.module('saleshop.common'));