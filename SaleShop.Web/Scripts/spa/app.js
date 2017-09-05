var myApp = angular.module("myModule", []);

myApp.controller("studentController", studentController); // register controller studentController sử dụng function parameter 2
myApp.controller("teacherController", teacherController);
myApp.controller("schoolController", schoolController);
//Register Service
myApp.service('validatorService', validatorService);
//Register Directive
myApp.directive('saleShopDirective', saleShopDirective);
schoolController.$inject = ['$scope', 'validatorService'];

//myController.$inject = ["$scope"]; // tiêm $scope vào controller


//Khởi tạo function controller
//function schoolController($scope) {
//    $scope.message = "This is message from school";
//}
function schoolController($scope, validatorService) {
    //$scope.message = "This is message from school";
    $scope.num = 1;
    $scope.CheckNumber = function() {
        $scope.message = validatorService.checkInput($scope.num);
    };
 
}
function studentController($scope,$rootScope) {
    //$rootScope.message = "This is message from student";
    $scope.message = "This is message from student";
}
function teacherController($scope) {
    $scope.message = "This is message from teacher"; // nếu scope và rootscope đều sử dụng biến message , thì nó sẽ sử dụng biến của scope
}
//rootScope áp dụng lên tất cả controller
//scope áp dụng trên controller chứa nó
//rootScope giống với Session trong ASP

//Khởi tạo function Service
function validatorService($window) {
    return {
        checkInput:checkInput // cái bên trái dùng cho public , cái bên phải internal trong hàm này
    }
    function checkInput(input) {
        if (input % 2 == 0) {
            $window.alert("Even");
            return input +" is even";
        } else {
            $window.alert("Odd");
            return input + " is odd";
        }
    }
}

function saleShopDirective() {
    return {
        //E là cho 1 phần tử element , A là 1 attribute ,
        //C là cho class , M là cho comment
        //Mặc định là EA 
        //template:"<h1>This is my first directive</h1>"
        restrict:"A",
        templateUrl: "/Scripts/spa/test.html"
    }
}