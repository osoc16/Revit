(function() {

    var app = angular.module("RevitApp");

    var EvaluationController = function($scope, $location, revitService, $log) {


        $scope.form = revitService.getForm();

        $scope.test = "test";


        $scope.competenceDetailMode=false;

        $scope.editCompetence = function(competence) {

            $scope.currentCompetence = competence;

        };




    };

    app.controller("EvaluationController", EvaluationController);

}());