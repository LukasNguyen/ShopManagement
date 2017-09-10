(function (app) {

    app.controller('productCategoryEditController', productCategoryEditController);

    productCategoryEditController.$inject = ['apiService', '$scope', 'notificationService', '$state','$stateParams','commonService'];
    function productCategoryEditController(apiService, $scope, notificationService, $state, $stateParams,commonService) {
        $scope.productCategory = {
            UpdatedDate: new Date(),
            Status: true
        };
        $scope.parentCategories = [];

        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }

        function loadProductCategoryDetail() {
            apiService.get('/api/productcategory/getbyid/' + $stateParams.id,null,(result) => {
                $scope.productCategory = result.data;
            },(error) => {
                notificationService.displayError(error.data);
            });
        }


        $scope.UpdateProductCategory = UpdateProductCategory;

        function UpdateProductCategory() {
            apiService.put('/api/productcategory/update', $scope.productCategory, (result) => {
                notificationService.displaySuccess(result.data.Name + ' đã được cập nhật');
                $state.go('product_categories');
            }, (failure) => {
                console.log('Cannot get list parent');
                notificationService.displayError('Cập nhật không thành công');
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
        loadProductCategoryDetail();
    }

})(angular.module('saleshop.product_categories'));  