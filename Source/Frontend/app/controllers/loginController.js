(function() {

    var app = angular.module("RevitApp");

    //Fake login controller
    var LoginController = function($scope,authenticationService, $log) {

    	$scope.currentUserId= authenticationService.getCurrentUserId();

    	//Set current authenticated user id
    	$scope.setUserId=function() {
    		authenticationService.setCurrentUserId($scope.currentUserId);
    	}
    };

    app.controller("LoginController", LoginController);

}());