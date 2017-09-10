(function (app) {

    app.controller('productCategoryAddController', productCategoryAddController);

    productCategoryAddController.$inject = ['apiService','$scope','notificationService','$state'];
    function productCategoryAddController(apiService,$scope,notificationService,$state) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status:true
        };
        $scope.parentCategories = [];

        $scope.AddProductCategory = AddProductCategory;

        function AddProductCategory() {
            apiService.post('/api/productcategory/create', $scope.productCategory, (result) => {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới');
                $state.go('product_categories');
            }, (failure) => {
                console.log('Cannot get list parent');
                notificationService.displayError('Thêm mới không thành công');
            });
        }

        function loadParentCategory() {
            apiService.get('/api/productcategory/getallparents', null, (result) => {
                $scope.parentCategories = result.data;
            }, (failure) => {
                console.log('Cannot get list parent');
            });
        }

        loadParentCategory();   

    }

})(angular.module('saleshop.product_categories'));  