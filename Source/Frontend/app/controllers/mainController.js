(function() {

    var app = angular.module("RevitApp");

    var MainController = function($scope, $interval, $location, selorRuleService) {

        var array= [6,6,5,1,1,1,null,null];

       $scope.suggestion= selorRuleService.getScoreSuggestion(array);



    
    };

    app.controller("MainController", MainController);

}());