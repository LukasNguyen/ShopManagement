(function (app) {
    app.controller('productCategoryListController', productCategoryListController);
    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService','$ngBootbox'];
    function productCategoryListController($scope, apiService, notificationService, $ngBootbox) {

        //Phân trang
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProductCategories = getProductCategories;
        $scope.search = search;
        $scope.keyword = '';
        $scope.deleteProductCategory = deleteProductCategory;

        function deleteProductCategory(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa không ?').then(function() {
                var config = {
                    params: {
                        id: id
                    }
                };

                apiService.del('/api/productcategory/delete',
                    config,
                    (success) => {
                        notificationService.displaySuccess('Xóa thành công');
                        search();
                    },
                    (failure) => {
                        notificationService.displayError('Xóa không thành công');
                    });
            });
        }

        //End
        function search() {
            getProductCategories();
        }

        function getProductCategories(page) {

            page = page || 0;
            //Tạo đối tượng config có thuộc tính params
            var config = {
                params: {
                    keyword:$scope.keyword,
                    page: page,
                    pageSize:3
                }
            }

            apiService.get('/api/productcategory/getall', config, (result) => {

                if (result.data.TotalCount == 0) 
                    notificationService.displayWarning('Không tìm thấy bản ghi nào');
                //} else {
                //    notificationService.displayInfo("Tìm thấy " + result.data.TotalCount + " bản ghi");
                //}

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