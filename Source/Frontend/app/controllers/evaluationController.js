(function() {

    var app = angular.module("RevitApp");

    var EvaluationController = function($scope, $location, revitService, $log) {


        $scope.form = revitService.getForm();

        $scope.test = "test";

        $scope.competenceEditMode = false;

        $scope.toggleEditMode = function() {
            $scope.competenceEditMode = $scope.competenceEditMode === false ? true : false;
        };

        $scope.editCompetence = function(competence) {

            $scope.toggleEditMode();
            $scope.currentCompetence = competence;

        };

    };

    app.controller("EvaluationController", EvaluationController);

}());