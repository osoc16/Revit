(function() {

    var app = angular.module("RevitApp");

    var FormController = function($scope, $location, revitService, $log) {

        $scope.form = revitService.getGeneralForm();

        $scope.competenceEditMode = false;

        $scope.currentCompetenceIndex = null;



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
                lastName: lastName
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
                competenceId: null,
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





        $scope.assignJury = function(juryIndex) {

   
            var selectedJury = $scope.form.juries[juryIndex];

            var checkedCandidates = $(".assign-jury-checkbox:checked");

            var checkedCandidatesIndices = [];

            checkedCandidates.each(function() {
                checkedCandidatesIndices.push($(this).attr("data-candidate-index"));
            });



            for (var candidateCounter in checkedCandidatesIndices) {

                    var candidate = $scope.form.candidates[checkedCandidatesIndices[candidateCounter]];

                    var alreadyContainsJury = false;

                    for (var candidateJuryCounter in candidate.juries) {

                        if (selectedJury.juryId == candidate.juries[candidateJuryCounter].juryId) {

                            $log.info("selectedJuryId"+selectedJury.juryId);

                             $log.info("loopjury"+candidate.juries[candidateJuryCounter].juryId);

                            alreadyContainsJury = true;

                            $log.info("jury already added");
                            break;
                        }

                    }

                    if (!alreadyContainsJury) {

                        $log.info(candidate.firstName + " got jury " + selectedJury.firstName + " assigned");
                        candidate.juries.push(selectedJury);
                    }

            
            }

            $log.info(checkedCandidates);

        }
    }



    app.controller("FormController", FormController);

}());