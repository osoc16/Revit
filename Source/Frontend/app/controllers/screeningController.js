(function() {

    var app = angular.module("RevitApp");

    //Controller for screening overview
    var ScreeningController = function($scope, revitService,authenticationService, $log) {

        $scope.currentUserId=authenticationService.getCurrentUserId();
        $scope.searchTerm = "";

        //Execute if API call failed
        var onApiCallError = function(reason) {
            $scope.error = reason;

            $log.info("api call fail");
        }

        //After data get 
        var onGetScreenings = function(data) {

        $scope.curretUserId=authenticationService.getCurrentUserId();

            $scope.screenings=data;

        }

        //Find screenings on API
        $scope.search = function() {
            revitService.getScreenings($scope.searchTerm).then(onGetScreenings, onApiCallError);
        }

    };

    app.controller("ScreeningController", ScreeningController);

}());