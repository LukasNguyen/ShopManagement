﻿(function () {
    angular.module('saleshop', ['saleshop.products', 'saleshop.product_categories','saleshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider,$urlRouterProvider) {
        $stateProvider.state('home',
            {
                url: '/admin',
                templateUrl: '/app/components/home/homeView.html',
                controller:'homeController'
            });
        $urlRouterProvider.otherwise('/admin');
    }
})();