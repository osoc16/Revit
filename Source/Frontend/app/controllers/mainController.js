(function() {

    var app = angular.module("RevitApp");

    var MainController = function($scope, $interval, $location, selorRuleService) {

        var tests = [
             [6, 5,4,null],

            [6, 6, 5, 5, 3, 3, 2, null],
            [6, 6, 5, 1, 1, 1, null, null]

        ];

        var suggestions = [];
        for (var i = 0; i < tests.length; i++) {


            var suggestion=selorRuleService.getScoreSuggestion(tests[i]);
            suggestion.array=tests[i];
            suggestions.push(suggestion);

        }

        $scope.suggestions = suggestions;

    };

    app.controller("MainController", MainController);

}());