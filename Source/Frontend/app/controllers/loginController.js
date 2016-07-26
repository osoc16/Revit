(function() {

    var app = angular.module("RevitApp");

    var LoginController = function($scope,authenticationService, $log) {

    	$scope.currentUserId= authenticationService.getCurrentUserId();

    	$scope.setUserId=function() {


    		authenticationService.setCurrentUserId($scope.currentUserId);

    	}

    };

    app.controller("LoginController", LoginController);

}());