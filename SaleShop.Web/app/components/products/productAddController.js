(function (app) {

    app.controller('productAddController', productAddController);

    productAddController.$inject = ['apiService', '$scope', 'notificationService', '$state','commonService'];


    function productAddController(apiService, $scope, notificationService, $state, commonService) {

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        };

        $scope.ChooseImage = function(){
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