(function() {

    var app = angular.module("RevitApp");

    var EvaluationController = function($scope, $location, revitService, $log) {

        $scope.form = revitService.getForm();

        $scope.competenceEditMode = false;

        $scope.toggleEditMode = function() {

            //Edit mode was on
            if ($scope.competenceEditMode === true) {

                //Ressign competence to scope.form

                for (var i in $scope.form.competences) {

                    if ($scope.form.competences[i].competenceId == $scope.currentCompetence.competenceId) {
                        $scope.form.competences[i] = $scope.currentCompetence;

                              $log.info( $scope.form.competences[i]);
                        $log.info("test");
                        break; //Stop this loop, we found it!
                    }
                }


            }

            //Invert edit mode
            $scope.competenceEditMode = $scope.competenceEditMode === false ? true : false;
        };

        $scope.editCompetence = function(competence) {

            $scope.toggleEditMode();
            $scope.currentCompetence = competence;

        };

        $scope.saveForm=function(){



            alert("test");
        }

    };

    app.controller("EvaluationController", EvaluationController);

}());