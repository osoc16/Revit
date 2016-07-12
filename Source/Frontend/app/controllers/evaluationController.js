(function() {

    var app = angular.module("RevitApp");

    var EvaluationController = function($scope,revitService) {


        $scope.form= revitService.getForm();

        $scope.test="test";



    };

    app.controller("EvaluationController", EvaluationController);

}());