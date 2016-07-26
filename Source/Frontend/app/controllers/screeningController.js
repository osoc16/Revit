(function() {

    var app = angular.module("RevitApp");

    var ScreeningController = function($scope, revitService,authenticationService, $log) {

        $scope.currentUserId=authenticationService.getCurrentUserId();
        $scope.searchTerm = "";

        var onApiCallError = function(reason) {
            $scope.error = reason;

            $log.info("api call fail");
        }



        var onGetScreenings = function(data) {

        $scope.curretUserId=authenticationService.getCurrentUserId();

            $scope.screenings=data;

        }

        $scope.search = function() {
            revitService.getScreenings($scope.searchTerm).then(onGetScreenings, onApiCallError);
        }

    };

    app.controller("ScreeningController", ScreeningController);

}());