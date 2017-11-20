﻿(function (app) {
    app.controller('productImportController', productImportController);

    productImportController.$inject = ['apiService', '$http', 'authenticationService', '$scope', 'notificationService', '$state', 'commonService'];

    function productImportController(apiService, $http, authenticationService, $scope, notificationService, $state, commonService) {

        $scope.files = [];
        $scope.product = {};
        $scope.ImportProduct = ImportProduct;
        $scope.flatFolders = [];
        //listen for the file selected event
        $scope.$on("fileSelected", function (event, args) {
            $scope.$apply(function () {
                //add the file object to the scope's files collection
                $scope.files.push(args.file);
            });
        });

        function ImportProduct() {
   
            authenticationService.setHeader();
            $http({
                method: 'POST',
                url: "/api/Product/Import",
                //IMPORTANT!!! You might think this should be set to 'multipart/form-data' 
                // but this is not true because when we are sending up files the request 
                // needs to include a 'boundary' parameter which identifies the boundary 
                // name between parts in this multi-part request and setting the Content-type 
                // manually will not set this boundary parameter. For whatever reason, 
                // setting the Content-type to 'false' will force the request to automatically
                // populate the headers properly including the boundary parameter.
                headers: { 'Content-Type': undefined },
                //This method will allow us to change how the data is sent up to the server
                // for which we'll need to encapsulate the model data in 'FormData'
                transformRequest: function (data) {
                    var formData = new FormData();
                    //need to convert our json object to a string version of json otherwise
                    // the browser will do a 'toString()' on the object which will result 
                    // in the value '[Object object]' on the server.
                    formData.append("categoryId", angular.toJson(data.categoryId));
                    //now add all of the assigned files
                    for (var i = 0; i < data.files.length; i++) {
                        //add each file to the form data and iteratively name them
                        formData.append("file" + i, data.files[i]);
                    }
                    return formData;
                },
                //Create an object that contains the model and files which will be transformed
                // in the above transformRequest method
                data: { categoryId: $scope.product.categoryID, files: $scope.files }
            }).then(function (result, status, headers, config) {
                    notificationService.displaySuccess(result.data);
                    $state.go('products');
                },
                function (data, status, headers, config) {
                    notificationService.displayError(data);
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

    }

})(angular.module('saleshop.products'));