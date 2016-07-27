(function() {

    var app = angular.module("RevitApp");

    var EvaluationController = function($scope, $routeParams, $location, revitService, github, selorRuleService, $log) {


        var selectedCandidate = null;

        $scope.setCompetenceStatus = function(competenceIndex) {

            var toUpdateCompetence= $scope.form.competences[competenceIndex];

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

            $log.info("Evaulation form fetched:");
            $log.info(data);
            $scope.form = data;

            //Set status for each competence
            for (var i in $scope.form.competences) {

                //applyScoringRulesOnCompetenceLevel(i);
                $scope.setCompetenceStatus(i);

            }

           // applyScoringRulesOnFormLevel();

            $(document).ready(function() {
                $('select').material_select();
            });

        }

        var onSaveEvaluationForm = function(data) {

            $log.info("Form has been saved successfully");
            Materialize.toast('Evaluation form saved successfully', 1000);



        }

        var onApiCallError = function(reason) {
            $scope.error = reason;
            Materialize.toast('An error has occured while processing your request, try again later please', 3000);
        }

        revitService.getEvaluationForm($routeParams.formId, $routeParams.juryId, $routeParams.candidateId).then(onGetEvaluationForm, onApiCallError);


        $scope.competenceEditMode = false;
        $scope.currentCompetenceIndex = null;


        $scope.saveEvaluationForm = function() {

            revitService.saveEvaluationForm($routeParams.formId, $routeParams.juryId, $routeParams.candidateId, $scope.form).then(onSaveEvaluationForm, onApiCallError);

        }   

        $scope.goToCandidateEvaluationPage = function() {

            var cId = $(".candidate-selection-dropdown option:selected").val();
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
                $scope.saveEvaluationForm();
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

            revitService.saveForm($scope.form.formId,$scope.form);

        }



        $scope.competenceEvaluated = function() {

                applyScoringRulesOnFormLevel();


                $scope.setCompetenceStatus($scope.currentCompetenceIndex);
   
                 $log.info("comp eval");
        }

        var applyScoringRulesOnFormLevel=function(){


             var suggestion = selorRuleService.getFormScoreSuggestion($scope.form.competences);

            if(isNaN($scope.form.score)|| $scope.form.score>suggestion.maxScore || $scope.form.score<suggestion.minScore ){


            $scope.form.score = suggestion.score;
 
            }

            $scope.form.scoreMinLimit = suggestion.minScore;
            $scope.form.scoreMaxLimit = suggestion.maxScore;


            /* VALIDATION */

        }


        $scope.dimensionEvaluated = function() {


            applyScoringRulesOnCompetenceLevel($scope.currentCompetenceIndex);

            $scope.setCompetenceStatus($scope.currentCompetenceIndex);


        }


        var applyScoringRulesOnCompetenceLevel=function(competenceIndex){


           var suggestion = selorRuleService.getCompetenceScoreSuggestion($scope.form.competences[competenceIndex].dimensions);

            var competenceScore= $scope.form.competences[competenceIndex].score;

            if(isNaN(competenceScore)|| competenceScore> suggestion.maxScore || competenceScore< suggestion.minScore){
             
             $scope.form.competences[competenceIndex].score = suggestion.score;
 
            }

            $scope.form.competences[competenceIndex].scoreMinLimit = suggestion.minScore;
            $scope.form.competences[competenceIndex].scoreMaxLimit = suggestion.maxScore;

            applyScoringRulesOnFormLevel();


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