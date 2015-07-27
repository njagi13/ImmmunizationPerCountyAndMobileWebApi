app.controller('ImmunizationCoverCtrl', [
    "$scope", "$http", "$timeout", function ($scope, $http, $timeout) {
        $scope.datatodisplay = [];
        $scope.message = "ImmunizationCoverCtrl";

        $scope.counties = function () {
            var url = '/Home/Getcounties';
            $http.get(url).then(function (res) {
                debugger;
                $scope.labels1 = res.data;
                $scope.labels = $scope.labels1;
                console.log($scope.alldata);
            });
        };
        $scope.counties();


        $scope.coverage = function () {
            var url = '/Home/GetCoverage';
            $http.get(url).then(function (res) {
                debugger;
                $scope.cov = res.data;
                $scope.data = $scope.cov;
            });
        };

        $scope.coverage();


      

        $scope.series = ['2011', '2012', '2011'];

        $scope.onClick = function (points, evt) {
            console.log(points, evt);
        };

        // Simulate async data update
        $timeout(function () {
            $scope.data;
        }, 3000);

    }]);