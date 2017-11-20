(function (app) {
    app.controller('productListController', productListController);
    productListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function productListController($scope, apiService, notificationService, $ngBootbox, $filter) {

        //Phân trang
        $scope.products = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProducts = getProducts;
        $scope.search = search;
        $scope.keyword = '';
        $scope.deleteProduct = deleteProduct;
        $scope.selectAll = selectAll;
        $scope.isAll = false;
        $scope.deleteMultiple = deleteMultiple;

        //Delete all item that you selected
        function deleteMultiple() {

            var listId = [];
            $.each($scope.selected, function (index, item) {
                listId.push(item.ID);
            });

            var config = {
                params: {
                    checkedProducts: JSON.stringify(listId)
                }
            };
            apiService.del('/api/product/deletemulti',
                config,
                (result) => {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi');
                    search();
                },
                (failure) => {
                    notificationService.displayError('Xóa không thành công');
                });
        }

        //Select all item that you would like to delete
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.products, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.products, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        //chọn item ở từng phần tử sẽ enabled , disable nút delete all
        $scope.$watch("products", function (n, o) {
            var checked = $filter('filter')(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteProduct(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa không ?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };

                apiService.del('/api/product/delete',
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
            getProducts();
        }

        function getProducts(page) {

            page = page || 0;
            //Tạo đối tượng config có thuộc tính params
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 3
                }
            }

            apiService.get('/api/product/getall', config, (result) => {

                if (result.data.TotalCount == 0)
                    notificationService.displayWarning('Không tìm thấy bản ghi nào');
                //} else {
                //    notificationService.displayInfo("Tìm thấy " + result.data.TotalCount + " bản ghi");
                //}

                $scope.products = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, (failure) => {
                console.log('Load product failed');
            });
        }

        $scope.getProducts();

        $scope.exportExcel = exportExcel;

        function exportExcel() {
            var config = {
                params: {
                    filter: $scope.keyword
                }
            }
            apiService.get('/api/product/ExportXls', config, function (response) {
                if (response.status = 200) {
                    window.location.href = response.data;
                }
            }, function (error) {
                notificationService.displayError(error);

            });
        }

        $scope.exportPdf = exportPdf;

        function exportPdf(productId) {
            var config = {
                params: {
                    id: productId
                }
            }
            apiService.get('/api/product/ExportPdf', config, function (response) {
                if (response.status = 200) {
                    window.location.href = response.data;
                }
            }, function (error) {
                notificationService.displayError(error);

            });
        }
    }
})(angular.module('saleshop.products'));