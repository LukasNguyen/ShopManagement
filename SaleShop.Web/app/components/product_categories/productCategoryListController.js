(function (app) {
    app.controller('productCategoryListController', productCategoryListController);
    productCategoryListController.$inject = ['$scope','apiService'];
    function productCategoryListController($scope,apiService) {
        $scope.productCategories = [];

        $scope.getProductCategories = getProductCategories;

        function getProductCategories() {
            apiService.get('/api/productcategory/getall', null,(result) => {
                $scope.productCategories = result.data;
            },(failure) => {
                console.log('Load product category failed');
            });
        }

        $scope.getProductCategories();
    }
})(angular.module('saleshop.product_categories'));