(function() {

    var app = angular.module("RevitApp");

    var FormController = function($scope, $location, revitService, $log) {

            $scope.form=revitService.getForm();

      };

    app.controller("FormController", FormController);

}());