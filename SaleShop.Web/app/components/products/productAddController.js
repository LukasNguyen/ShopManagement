(function (app) {

    app.controller('productAddController', productAddController);

    productAddController.$inject = ['apiService', '$scope', 'notificationService', '$state','commonService'];


    function productAddController(apiService, $scope, notificationService, $state, commonService) {

        //Xử lý ckeditor
        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        };

        //Chọn hình ảnh
        $scope.ChooseImage = function(){
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                //Thêm $scope.$apply bắt angular thực hiện ngay function này
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                });
            }
            finder.popup();
        }

        //Tạo auto make SEO tiltle
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

            $scope.product.MoreImages = JSON.stringify($scope.moreImages);

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

        //Xử lý more images
        $scope.moreImages = [];
        $scope.ChooseMoreImage = ChooseMoreImage;
        function ChooseMoreImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {

                //Thêm $scope.$apply bắt angular thực hiện ngay function này
                $scope.$apply(function() {
                    $scope.moreImages.push(fileUrl);
                });
            }
            finder.popup();
        }


    }

})(angular.module('saleshop.products'));  