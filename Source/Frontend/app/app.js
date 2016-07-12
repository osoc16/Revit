(function(){
    
    var app = angular.module("RevitApp", ["ngRoute"]);
    
    app.config(function($routeProvider){
        $routeProvider
            .when("/main", {
                templateUrl: "app/partials/main.html",
                controller: "MainController"
            })
            .when("/evaluation",
            {
        	  	templateUrl: "app/partials/evaluation.html",
                controller: "EvaluationController"
            })
            .when("/evaluation/competence/:competenceId",
            {
                templateUrl: "app/partials/competence.html",
                controller: "EvaluationController"
            })
            .otherwise({redirectTo:"/main"});
    });
    
}());