(function() {

    var app = angular.module("RevitApp");

    var EvaluationController = function($scope, $location, revitService, $log) {

        //Get form from service
        $scope.form = revitService.getForm();

        $scope.competenceEditMode = false;
        $scope.currentCompetenceIndex = null;

        $scope.editCompetence = function(competenceId) {
            for (var i in $scope.form.competences) {

                if ($scope.form.competences[i].competenceId == competenceId) {
                    $scope.currentCompetenceIndex = i;
                    break; //Stop this loop, we found it!
                }
            }
            $scope.toggleEditMode();
        };

        $scope.toggleEditMode = function() {

            //Invert edit mode
            $scope.competenceEditMode = $scope.competenceEditMode === false ? true : false;
            display();

             $scope.$broadcast('rzSliderForceRender');
        };

        var display = function() {
            if ($scope.competenceEditMode) {
                $(".show-when-edit").show();
                $(".hide-when-edit").hide();
            } else {
                $(".show-when-edit").hide();
                $(".hide-when-edit").show();
            }
        }

        //Save form to service
        $scope.saveForm = function() {

            alert("test");

            revitService.saveForm($scope.form);
        }


    };

    app.controller("EvaluationController", EvaluationController);

}());