(function (app) {
    app.controller('loginController', ['$scope', 'loginService', '$injector', 'notificationService',
        function ($scope, loginService, $injector, notificationService) {

            $scope.loginData = {
                userName: "",
                password: ""
            };

            $scope.loginSubmit = function () {
                loginService.login($scope.loginData.userName, $scope.loginData.password).then(function (response) {
                    //console.log(response);
                    //if (response.data.error === 'invalid_grant') {
                    //    notificationService.displayError("Tài khoản hoặc mật khẩu không đúng");
                    //}
                    //else {
                    //    console.log("4343");
                    //    notificationService.displayInfo("Đăng nhập thành công");
                    //    var stateService = $injector.get('$state'); //inject động 1 cái state
                    //    stateService.go('home');
                    //}
                    //console.log(response);
                    if (response != null && response.data.error !=undefined) {
                        notificationService.displayError("Tài khoản hoặc mật khẩu không đúng");
                    }
                    else {
                        notificationService.displayInfo("Đăng nhập thành công");
                        var stateService = $injector.get('$state'); //inject động 1 cái state
                        stateService.go('home');
                    }
                });
            }
        }]);
})(angular.module('saleshop'));