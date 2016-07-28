(function() {

    var app = angular.module("RevitApp");

    var FormController = function($scope,$routeParams, $location, revitService, $log) {


        //Data fetch functions
        var onGetGeneralForm=function(data){
            $scope.form = data;
            $log.info(data);

                    $(document).ready(function() {
                $('select').material_select();
            });
        }

        var onSaveGeneralForm=function(data){

            $log.info("Form successfully saved");

             $log.info("Form has been saved successfully");
            Materialize.toast('Form saved successfully', 1000);



        }

        var onApiCallError=function(reason){
            $scope.error = reason;
            Materialize.toast('An error has occured while processing your request, try again later please', 3000);
        }

        revitService.getGeneralForm($routeParams.formId).then(onGetGeneralForm,onApiCallError);

        $scope.competenceEditMode = false;

        $scope.currentCompetenceIndex = null;


        $scope.saveGeneralForm= function(){
            revitService.saveGeneralForm($routeParams.formId,$scope.form).then(onSaveGeneralForm, onApiCallError);;
        }


        $scope.toggleCompetenceEditMode = function() {
            //Invert edit mode
            $scope.competenceEditMode = $scope.competenceEditMode === false ? true : false;
        }
        $scope.deleteCompetence = function(competenceIndex) {

            $scope.form.competences.splice(competenceIndex, 1);

        }

        $scope.editCompetence = function(competenceIndex) {
            $scope.currentCompetenceIndex = competenceIndex;

            $(document).ready(function() {
                Materialize.updateTextFields();
            });
        }


        $scope.deleteJury = function(juryIndex) {
            $scope.form.juries.splice(juryIndex, 1);
        }

        $scope.deleteCandidate = function(candidateIndex) {
            $scope.form.candidates.splice(candidateIndex, 1);
        }


        $scope.addCandidate = function(firstName, lastName) {

            if (!firstName || !lastName) {
                return;
            }

            $scope.form.candidates.push({
                firstName: firstName,
                lastName: lastName,
                juries:[]
            });

            $(".new-candidate-input").val("");
        }


        $scope.addJury = function(firstName, lastName) {

            if (!firstName || !lastName) {
                return;
            }

            $scope.form.juries.push({
                firstName: firstName,
                lastName: lastName
            });

            $(".new-jury-input").val("");
        }



        $scope.addCompetence = function() {

            var newCompetence = {
                name: "New Competence",
                description: "",
                dimensions: []
            };

            $scope.form.competences.push(newCompetence);


            $scope.editCompetence($scope.form.competences.length - 1);


            $scope.competenceEditMode = true;

        }


        $scope.addDimension = function() {

            var newDimension = {
                name: "New Dimension",
                description: ""
            };

            $scope.form.competences[$scope.currentCompetenceIndex].dimensions.push(newDimension);
        }

        $scope.deleteDimension = function(dimensionIndex) {


            $scope.form.competences[$scope.currentCompetenceIndex].dimensions.splice(dimensionIndex, 1);

        }


        var assignJuryToCandidate = function(juryIndex, candidateIndex) {

            var selectedJury = $scope.form.juries[juryIndex];

            var candidate = $scope.form.candidates[candidateIndex];
            var alreadyContainsJury = false;

            for (var candidateJuryCounter in candidate.juries) {

                if (selectedJury.juryId == candidate.juries[candidateJuryCounter].juryId) {

                    $log.info("selectedJuryId" + selectedJury.juryId);

                    $log.info("loopjury" + candidate.juries[candidateJuryCounter].juryId);

                    alreadyContainsJury = true;

                    $log.info("jury already added");
                    break;
                }

            }

            if (!alreadyContainsJury) {

                $log.info(candidate.firstName + " got jury " + selectedJury.firstName + " assigned");

                if(!candidate.juries){
                    candidate.juries=[];
                }

                candidate.juries.push(selectedJury);
            }




        }

        $scope.assignJury = function(juryIndex) {

            $('select').material_select();

            var checkedCandidates = $(".assign-jury-checkbox:checked");

            var checkedCandidatesIndices = [];

            if(checkedCandidates.length==0|| isNaN(juryIndex)){                  
                return;
            }

            checkedCandidates.each(function() {
                checkedCandidatesIndices.push($(this).attr("data-candidate-index"));
            });

            for (var candidateCounter in checkedCandidatesIndices) {

                assignJuryToCandidate(juryIndex, checkedCandidatesIndices[candidateCounter]);

            }

        }



        $scope.removeJuryFromCandidate = function(candidateIndex, juryIndex) {

            var candidate = $scope.form.candidates[candidateIndex];

            $scope.form.candidates[candidateIndex].juries.splice(juryIndex,1);

            /*

            for (var candidateJuryIndex in candidate.juries) {

                var jury = candidate.juries[candidateJuryIndex];

                if (jury.juryId == toRemoveJuryId) {

                    candidate.juries.splice(candidateIndex, 1);

                    $log.info(jury.firstName + " removed as jury for " + candidate.firstName);

                    break;

                }

            }*/



        }
    }



    app.controller("FormController", FormController);

}());