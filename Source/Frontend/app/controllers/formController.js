(function() {

    var app = angular.module("RevitApp");

    //Controller for admin Page for form editing
    var FormController = function($scope,$routeParams, $location, revitService, $log) {
 
        //API callback Functions
        //
        //after Get General form
        var onGetGeneralForm=function(data){
            $scope.form = data;
            $log.info(data);

                    $(document).ready(function() {
                $('select').material_select();
            });
        }
        //after Save general form
        var onSaveGeneralForm=function(data){

            $log.info("Form successfully saved");

             $log.info("Form has been saved successfully");
            Materialize.toast('Form saved successfully', 1000);
        }
        //after API call error
        var onApiCallError=function(reason){
            $scope.error = reason;
            Materialize.toast('An error has occured while processing your request, try again later please', 3000);
        }

        revitService.getGeneralForm($routeParams.formId).then(onGetGeneralForm,onApiCallError);

        $scope.competenceEditMode = false;

        $scope.currentCompetenceIndex = null;

        //Send form to API for save
        $scope.saveGeneralForm= function(){
            revitService.saveGeneralForm($routeParams.formId,$scope.form).then(onSaveGeneralForm, onApiCallError);;
        }

        //Visual competence edit mode
        $scope.toggleCompetenceEditMode = function() {
            //Invert edit mode
            $scope.competenceEditMode = $scope.competenceEditMode === false ? true : false;
        }

        //Delete competence from form
        $scope.deleteCompetence = function(competenceIndex) {

            $scope.form.competences.splice(competenceIndex, 1);
        }

        //Edit competence (set to current competence)
        $scope.editCompetence = function(competenceIndex) {
            $scope.currentCompetenceIndex = competenceIndex;

            $(document).ready(function() {
                Materialize.updateTextFields();
            });
        }

        //Removen jury member from form
        $scope.deleteJury = function(juryIndex) {
            $scope.form.juries.splice(juryIndex, 1);
        }

        //Remove candidate from form
        $scope.deleteCandidate = function(candidateIndex) {
            $scope.form.candidates.splice(candidateIndex, 1);
        }

        //Add candidate to form
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

        //Add jury to form
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

        //Add competence to evaluation form
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

        //Add dimension to current competence
        $scope.addDimension = function() {

            var newDimension = {
                name: "New Dimension",
                description: ""
            };

            $scope.form.competences[$scope.currentCompetenceIndex].dimensions.push(newDimension);
        }

        //Delete dimension from current competence
        $scope.deleteDimension = function(dimensionIndex) {
            $scope.form.competences[$scope.currentCompetenceIndex].dimensions.splice(dimensionIndex, 1);
        }

        //Assign a jury to a candidate
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

        //Assign jury to selected candidates
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

        //Remove a jury assignment from a candidate
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