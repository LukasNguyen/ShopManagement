(function (app) {

    app.controller('productEditController', productEditController);

    productEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService','$stateParams'];


    function productEditController(apiService, $scope, notificationService, $state, commonService, $stateParams) {

        $scope.loadProductDetail = loadProductDetail;
        function loadProductDetail() {
            apiService.get('/api/product/getbyid/' + $stateParams.id, null, (result) => {
                $scope.product = result.data;
            }, (error) => {
                notificationService.displayError(error.data);
            });
        }

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        };

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.product.Image = fileUrl;
            }
            finder.popup();
        }

        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }


        $scope.product = {
            UpdatedDate: new Date()
        };
        $scope.productCategories = [];

        $scope.UpdateProduct = UpdateProduct;

        function UpdateProduct() {
            apiService.put('/api/product/update', $scope.product, (result) => {
                notificationService.displaySuccess(result.data.Name + ' đã được cập nhật');
                $state.go('products');
            }, (failure) => {
                notificationService.displayError('Cập nhật không thành công');
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
        loadProductDetail();
    }

})(angular.module('saleshop.products'));  