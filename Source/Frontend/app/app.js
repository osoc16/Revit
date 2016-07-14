(function(){
    
    var app = angular.module("RevitApp", ["ngRoute",'rzModule']);
    
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
            .otherwise({redirectTo:"/main"});
    });
    
}());