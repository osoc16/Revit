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
            .when("/form",
            {
                templateUrl: "app/partials/form.html",
                controller: "FormController"
            })
            .when("/screening",{
                     templateUrl: "app/partials/screening.html",
                controller: "ScreeningController"
            })
            .otherwise({redirectTo:"/main"});
    });
    
}());