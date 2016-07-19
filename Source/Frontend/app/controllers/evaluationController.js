(function() {

    var app = angular.module("RevitApp");

    var EvaluationController = function($scope, $location, revitService, selorRuleService, $log) {

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


            if ($scope.competenceEditMode == false) {
                var suggestion = selorRuleService.getFormScoreSuggestion($scope.form.competences);
                $scope.form.score = suggestion.score;
            }


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

        $scope.competenceEvaluated = function() {


            var suggestion = selorRuleService.getFormScoreSuggestion($scope.form.competences);

            $scope.form.score = suggestion.score;

            $scope.form.scoreMinLimit = suggestion.minScore;
            $scope.form.scoreMaxLimit = suggestion.maxScore;

            $log.info("comp eval");

        }


        $scope.dimensionEvaluated = function() {

            var suggestion = selorRuleService.getCompetenceScoreSuggestion($scope.form.competences[$scope.currentCompetenceIndex].dimensions);

            $scope.form.competences[$scope.currentCompetenceIndex].score = suggestion.score;
            $scope.form.competences[$scope.currentCompetenceIndex].scoreMinLimit = suggestion.minScore;
            $scope.form.competences[$scope.currentCompetenceIndex].scoreMaxLimit = suggestion.maxScore;

            $scope.competenceEvaluated();
        }

        //Get slider tick color
        var tickColor = function(value, min, max) {

            try {


                var defaultColor = "#ccc";
                var forbiddenColor = "red"

                if (isNaN(min) || isNaN(max)) {
                    return defaultColor;
                } else {

                    if (value < min)
                        return forbiddenColor;
                    if (value > max)
                        return forbiddenColor;
                    return defaultColor;

                }


            } catch (Ex) {


                return defaultColor;
            }
        }

        //Return tick color for form total
        $scope.tickColorForm = function(value) {

            var min;
            var max;

            try {
                min = $scope.form.scoreMinLimit;
                max = $scope.form.scoreMaxLimit;

            } catch (ex) {}

            return tickColor(value, min, max);
        }


        //Return tick color for competence total
       $scope.tickColorCompetence = function(value) {

            var min;
            var max;

            try {
                min = $scope.form.competences[$scope.currentCompetenceIndex].scoreMinLimit;
                max = $scope.form.competences[$scope.currentCompetenceIndex].scoreMaxLimit;
            } catch (ex) {}


            return tickColor(value, min, max);
        }

    };

    app.controller("EvaluationController", EvaluationController);

}());