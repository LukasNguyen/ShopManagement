(function (app) {
    app.filter('status', function() {
        return function(input) {
            if (input == true)
                return "Kích hoạt";
            else
                return "Khóa";
        }
    });
})(angular.module('saleshop.common'));