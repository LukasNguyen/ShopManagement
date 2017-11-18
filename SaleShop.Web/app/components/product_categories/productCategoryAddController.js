(function (app) {

    app.controller('productCategoryAddController', productCategoryAddController);

    productCategoryAddController.$inject = ['apiService','$scope','notificationService','$state','commonService'];
    function productCategoryAddController(apiService, $scope, notificationService, $state, commonService) {


        $scope.productCategory = {
            CreatedDate: new Date(),
            Status:true
        };
        $scope.parentCategories = [];


        $scope.AddProductCategory = AddProductCategory;

        function AddProductCategory() {
            apiService.post('/api/productcategory/create', $scope.productCategory, (result) => {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới');
                $state.go('product_categories');
            }, (failure) => {
                notificationService.displayError('Thêm mới không thành công');
            });
        }

        $scope.flatFolders = [];
        function loadParentCategory() {
            apiService.get('/api/productcategory/getallparents', null, (result) => {
                $scope.parentCategories = commonService.getTree(result.data, "ID", "ParentID");
                $scope.parentCategories.forEach(function(item) {
                    recur(item, 0, $scope.flatFolders);
                })
            }, (failure) => {
                console.log('Cannot get list parent');
            });
        }
        function recur(item, level, arr) {
            arr.push({
                Name: times(level, '–') + ' ' + item.Name,
                ID: item.ID,
                Level: level,
                Indent: times(level, '–')
            });
            if (item.children) {
                item.children.forEach(function (item) {
                    recur(item, level + 1, arr);
                });
            }
        };

        function times(n, str) {
            var result = '';
            for (var i = 0; i < n; i++) {
                result += str;
            }
            return result;
        };

        loadParentCategory();   

        //Auto Make SEO alias
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }

    }

})(angular.module('saleshop.product_categories'));  