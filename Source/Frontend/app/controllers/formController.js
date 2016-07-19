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


        $scope.addCompetence = function() {

            var newCompetence = {
                name: "New Competence",
                competenceId: null,
                description: "",
                dimensions: []
            };

            $scope.form.competences.push(newCompetence);


            $scope.editCompetence($scope.form.competences.length-1);


            $scope.competenceEditMode=true;

        }


        $scope.addDimension=function(){

        		var newDimension= {
        				name:"New Dimension",
        				description:""
        		};

        		$scope.form.competences[$scope.currentCompetenceIndex].dimensions.push(newDimension);
        }

        $scope.deleteDimension=function(dimensionIndex){


        	$scope.form.competences[$scope.currentCompetenceIndex].dimensions.splice(dimensionIndex,1);

        }
    }



    app.controller("FormController", FormController);

}());