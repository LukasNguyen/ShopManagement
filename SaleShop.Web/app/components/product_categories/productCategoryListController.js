(function (app) {
    app.controller('productCategoryListController', productCategoryListController);
    productCategoryListController.$inject = ['$scope','apiService'];
    function productCategoryListController($scope,apiService) {
        $scope.productCategories = [];

        //Phân trang
        $scope.page = 0;
        $scope.pagesCount = 0;
        //End


        $scope.getProductCategories = getProductCategories;

        function getProductCategories(page) {

            page = page || 0;
            //Tạo đối tượng config có thuộc tính params
            var config = {
                params: {
                    page: page,
                    pageSize:2
                }
            }

            apiService.get('/api/productcategory/getall', config,(result) => {
                $scope.productCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            },(failure) => {
                console.log('Load product category failed');
            });
        }

        $scope.getProductCategories();
    }
})(angular.module('saleshop.product_categories'));