(function () {
    angular.module('saleshop.products', ['saleshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('products',
            {
                url: '/products',
                templateUrl: '/app/components/products/productListView.html',
                parent:'base',
                controller: 'productListController'
            }).state('product_add',
            {
                url: '/product_add',
                templateUrl: '/app/components/products/productAddView.html',
                parent: 'base',
                controller: 'productAddController'
            }).state('product_import',
                {
                    url: '/product_import',
                    templateUrl: '/app/components/products/productImportView.html',
                    parent: 'base',
                    controller: 'productImportController'
                }).state('product_edit',
            {
                url: '/product_edit/:id',
                templateUrl: '/app/components/products/productEditView.html',
                parent: 'base',
                controller: 'productEditController'
            });
    }
})();