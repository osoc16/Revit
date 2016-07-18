(function(){
    
    var selorRuleService = function($http){
      
      var getEvaluationCategory=function(form){

            return 1;

      }
        
      return {
          getEvaluationCategory: getEvaluationCategory
      };
        
    };
    var module = angular.module("RevitApp");
    module.factory("selorRuleService", selorRuleService);
    
}());