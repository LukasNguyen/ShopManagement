(function (app) {

    app.controller('productEditController', productEditController);

    productEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService','$stateParams'];


    function productEditController(apiService, $scope, notificationService, $state, commonService, $stateParams) {



        //Biến load nhiều hình ảnh
        $scope.moreImages = [];
        //Xử lý more images
        $scope.ChooseMoreImage = ChooseMoreImage;
        function ChooseMoreImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {

                //Thêm $scope.$apply bắt angular thực hiện ngay function này
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                });
            }
            finder.popup();
        }
        //Bỏ chọn tất cả ảnh
        $scope.DeleteAllSelectedImage = DeleteAllSelectedImage;
        function DeleteAllSelectedImage() {
            $scope.moreImages = [];
        }
        //Bỏ chọn ảnh được chọn
        $scope.UnselectedImage = UnselectedImage;
        function UnselectedImage(img) {
            console.log(Object.values(img));
            console.log($scope.moreImages);
            var imgSelected = Object.values(img)[0];
            console.log($scope.moreImages.indexOf(imgSelected));
            $scope.moreImages.splice($scope.moreImages.indexOf(imgSelected), 1);
            //  $scope.moreImages = $scope.moreImages.filter(function (e) { return e !== img })
        }

        $scope.loadProductDetail = loadProductDetail;
        function loadProductDetail() {
            apiService.get('/api/product/getbyid/' + $stateParams.id, null, (result) => {
                $scope.product = result.data;

                $scope.moreImages = JSON.parse($scope.product.MoreImages);
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
                //Thêm $scope.$apply bắt angular thực hiện ngay function này
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                });
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
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);

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