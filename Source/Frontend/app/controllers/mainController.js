(function() {

    var app = angular.module("RevitApp");

    var MainController = function($scope, $interval, $location) {

        var decrementCountdown = function(){
            $scope.countdown -= 1;
            if($scope.countdown < 1){
                $scope.search($scope.username);
            }
        };

        var countdownInterval = null;
        var startCountdown = function(){
            countdownInterval = $interval(decrementCountdown, 1000, $scope.countdown);
        };
    
    };

    app.controller("MainController", MainController);

}());