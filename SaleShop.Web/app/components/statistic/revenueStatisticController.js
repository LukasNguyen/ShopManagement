(function (app) {

    app.controller('revenueStatisticController', revenueStatisticController);

    revenueStatisticController.$inject = ['apiService','$scope','notificationService','$filter'];


    function revenueStatisticController(apiService, $scope, notificationService, $filter) {

        $scope.tabledata = [];

        $scope.labels = [];

        $scope.series = ['Doanh số', 'Lợi nhuận'];

        $scope.chartdata = [];

        $scope.getStatistic = getStatistic;

        $scope.fromDate = new Date('1/1/2016');
        $scope.toDate = new Date('1/1/2018');

        function getStatistic() {

            var config = {
                params: {
                    fromDate: $filter('date')($scope.fromDate, 'MM/dd/yyyy'),
                    toDate: $filter('date')($scope.toDate, 'MM/dd/yyyy')
                }
            }

            apiService.get('api/statistic/getrevenue',
                config,
                function (response) {

                    var labels = [];
                    var revenuesData = [];
                    var benefitData = [];
                    var chartData = [];
                    $scope.tabledata = response.data;
                    $.each(response.data, function(i,item) {
                        labels.push($filter('date')(item.Date,'dd/MM/yyyy'));
                        revenuesData.push(item.Revenues);
                        benefitData.push(item.Benefit);
                    });

                    chartData.push(revenuesData);
                    chartData.push(benefitData);

                    $scope.labels = labels;

                    $scope.chartdata = chartData;

                    notificationService.displaySuccess('Biểu đồ đã được load lại');
                },
                function(error) {
                    notificationService.displayError('Không thể tải dữ liệu');
                });
        }

        getStatistic();
    }

})(angular.module('saleshop.statistics'));  