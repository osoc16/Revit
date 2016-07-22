(function() {

    var app = angular.module("RevitApp");

    var EvaluationController = function($scope, $routeParams, $location, revitService, github, selorRuleService, $log) {


        var selectedCandidate = null;

        $scope.setCompetenceStatus = function(toUpdateCompetence) {
            if (isNaN(toUpdateCompetence.score)) {

                toUpdateCompetence.status = "neutral";
                toUpdateCompetence.statusMessage = "Not evaluated";

                return;

            }

            if (toUpdateCompetence.score == "null") {
                toUpdateCompetence.score = null;
            }

            if (!toUpdateCompetence.comment) {

                toUpdateCompetence.status = "warning";
                toUpdateCompetence.statusMessage = "No comment written";
                return;
            }


            toUpdateCompetence.status = "success";
            toUpdateCompetence.statusMessage = "Evaluated";


        }

        //API callback Functions
        var onGetEvaluationForm = function(data) {

            $log.info("Evaulation form fethced:");
            $log.info(data);
            $scope.form = data;

            //Set status for each competence
            for (var i in $scope.form.competences) {
                $scope.setCompetenceStatus($scope.form.competences[i]);
            }


            $(document).ready(function() {
                $('select').material_select();
            });

        }

        var onSaveEvaluationForm = function(data) {

            $log.info("Form has been saved successfully");

        }

        var onApiCallError = function(reason) {
            $scope.error = reason;
        }

        revitService.getEvaluationForm($routeParams.formId, $routeParams.juryId, $routeParams.candidateId).then(onGetEvaluationForm, onApiCallError);


        $scope.competenceEditMode = false;
        $scope.currentCompetenceIndex = null;


        $scope.saveEvaluationForm = function() {

            revitService.saveEvaluationForm($routeParams.formId, $routeParams.juryId, $routeParams.candidateId, $scope.form).then(onSaveEvaluationForm, onApiCallError);

        }

        $scope.goToCandidateEvaluationPage = function() {


            var cId = $(".candidate-selection-dropdown option:selected").val();

            alert(cId);

            $location.path("/evaluation/juries/" + $routeParams.juryId + "/forms/" + $routeParams.formId + "/candidates/" + cId);

        }

        //User interaction functions
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


            /* VALIDATION */


            var currentCompetence = $scope.form.competences[$scope.currentCompetenceIndex];

            $scope.setCompetenceStatus(currentCompetence);

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