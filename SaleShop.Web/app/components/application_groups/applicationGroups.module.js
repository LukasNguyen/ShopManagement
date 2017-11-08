(function () {
    angular.module('saleshop.application_groups', ['saleshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('application_groups', {
            url: "/application_groups",
            templateUrl: "/app/components/application_groups/applicationGroupListView.html",
            parent:'base',
            controller: "applicationGroupListController"
        }).state('add_application_group', {
            url: "/add_application_group",
            templateUrl: "/app/components/application_groups/applicationGroupAddView.html",
            parent: 'base',
            controller: "applicationGroupAddController"
        }).state('edit_application_group', {
            url: "/edit_application_group/:id",
            templateUrl: "/app/components/application_groups/applicationGroupEditView.html",
            parent: 'base',
            controller: "applicationGroupEditController"
        });
    }
})();