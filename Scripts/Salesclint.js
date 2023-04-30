var Sales = angular.module('SalesMod', []);
Sales.controller("Salescont", function ($scope, $http) {
    $scope.GetSalesData = () => {
        $http(
            {
                method: 'GET',
                url: '/Salesforecasting/GetSalesList?Year=' + $scope.Year
            }).then(function successCallback(response) {
                $scope.Salesdata = JSON.parse(response.data);
                $scope.show_Deatils = true;
                $scope.Total_sales_Of_Year = $scope.Salesdata.Table[0].TotalSales;
                $scope.Total_sales_State = $scope.Salesdata.Table1;


            }, function errorCallback(response) {
                alert("Error while get  List!");
            });
    }
    $scope.Get_forecasted_SalesData = () => {
        if (($scope.forecasted_percentage <= 0 || $scope.forecasted_percentage > 100) || $scope.forecasted_percentage == undefined) {
            alert("Please enter number between 1 to 100");
            return false;
        }
        else {
            window.open("../Salesforecasting/forecastedSales?Year=" + btoa($scope.Year) + "&perc_=" + btoa($scope.forecasted_percentage), '_blank');
        }
    }
});
var forecaste = angular.module('forecastedMod', []);
forecaste.controller("forecastedcont", function ($scope, $http, $location) {
    var urls = $location.absUrl();
    var Parmsdata = new Array();
    if (urls.split('?').length > 1) {
        var params = urls.split('?')[1].split('&');
        for (var i = 0; i < params.length; i++) {
            var key = params[i].split('=')[0];
            var value = decodeURIComponent(params[i].split('=')[1]);
            Parmsdata[key] = value;
        }
    }
    if (Parmsdata["Year"] != null) {
        $scope.year = atob(Parmsdata["Year"]);
    }
    if (Parmsdata["perc_"] != null) {
        $scope.percentage = atob(Parmsdata["perc_"]);
    }
    $scope.Total_forecasted_sales_State = [];
    $scope.Get_forecasted_Data = () => {
        $http(
            {
                method: 'GET',
                url: '/Salesforecasting/GetforecastedSalesList?Year=' + $scope.year + '&perc_=' + $scope.percentage
            }).then(function successCallback(response) {
                $scope.Salesdata = JSON.parse(response.data);
                $scope.Total_forecasted_sales_Year = $scope.Salesdata.Table[0].forecasted_TotalSales;
                $scope.Total_forecasted_sales_State = $scope.Salesdata.Table1;
                $scope.statelist = $scope.Salesdata.Table1

            }, function errorCallback(response) {
                alert("Error while get  List!");
            });
    }

    $scope.OnChanges_State_Data = (item) => {
        $http(
            {
                method: 'GET',
                url: '/Salesforecasting/GetSpecficStateData?State=' + item + '&Year=' + $scope.year + '&percentage=' + $scope.Forecast_Percentage
                
            }).then(function successCallback(response) {
                $scope.Total_forecasted_sales_State = JSON.parse(response.data);

            }, function errorCallback(response) {
                alert("Error while get  List!");
            });
    }
    $scope.Get_forecasted_Data();
});