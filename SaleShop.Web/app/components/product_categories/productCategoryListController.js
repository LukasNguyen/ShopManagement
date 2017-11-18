(function(app) {
    app.controller('productCategoryListController', productCategoryListController);
    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function productCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter) {

        //Phân trang
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProductCategories = getProductCategories;
        $scope.search = search;
        $scope.keyword = '';
        $scope.deleteProductCategory = deleteProductCategory;
        $scope.selectAll = selectAll;
        $scope.isAll = false;
        $scope.deleteMultiple = deleteMultiple;

        $scope.loading = false;

        //Delete all item that you selected
        function deleteMultiple() {

            var listId = [];
            $.each($scope.selected, function(index, item) {
                listId.push(item.ID);
            });

            var config = {
                params: {
                    checkedProductCategories:JSON.stringify(listId)
                }
            };
            apiService.del('/api/productcategory/deletemulti',
                config,
                (result) => {
                    notificationService.displaySuccess('Xóa thành công '+ result.data + ' bản ghi');
                    search();
                },
                (failure) => {
                    notificationService.displayError('Xóa không thành công');
                });
        }

        //Select all item that you would like to delete
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = false;
                 });
                $scope.isAll = false;
            }
        }

        //chọn item ở từng phần tử sẽ enabled , disable nút delete all
        $scope.$watch("productCategories", function(n, o) {
            var checked = $filter('filter')(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled','disabled');
            }
        },true);

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
            $scope.loading = true;
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
                $scope.loading = false;
            },(failure) => {
                console.log('Load product category failed');
            });
        }

        $scope.getProductCategories();
    }
})(angular.module('saleshop.product_categories'));