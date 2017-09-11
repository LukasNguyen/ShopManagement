(function (app) {

    app.controller('productAddController', productAddController);

    productAddController.$inject = ['apiService', '$scope', 'notificationService', '$state'];

    $scope.ckeditorOptions = {
        language: 'vi',
        height:'200px'
    };

    function productAddController(apiService, $scope, notificationService, $state) {
        $scope.product = {
            CreatedDate: new Date(),
            Status: true
        };
        $scope.productCategories = [];

        $scope.AddProduct = AddProduct;

        function AddProduct() {
            apiService.post('/api/product/create', $scope.product, (result) => {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới');
                $state.go('products');
            }, (failure) => {
                notificationService.displayError('Thêm mới không thành công');
            });
        }

        function loadProductCategories() {
            apiService.get('/api/productcategory/getallparents', null, (result) => {
                $scope.productCategories = result.data;
            }, (failure) => {
                console.log('Cannot get list parent');
            });
        }

        loadProductCategories();

    }

})(angular.module('saleshop.products'));  