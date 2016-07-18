(function() {

    var selorRuleService = function($http) {

        var getCompetenceScoreSuggestion = function(dimensions) {

            return 1;

        };


        var getFormScoreSuggestion = function(competences) {


            return 1;
        }


        return {
            getCompetenceScoreSuggestion: getCompetenceScoreSuggestion,
            getFormScoreSuggestion: getFormScoreSuggestion
        };

    };


    var module = angular.module("RevitApp");
    module.factory("selorRuleService", selorRuleService);

}());