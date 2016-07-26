(function(){
    
    var app = angular.module("RevitApp", ["ngRoute",'rzModule']);
    
    app.config(function($routeProvider){
        $routeProvider
            .when("/main", {
                templateUrl: "app/partials/main.html",
                controller: "MainController"
            })
            .when("/login", {
                templateUrl: "app/partials/login.html",
                controller: "LoginController"
            })
            .when("/evaluation/juries/:juryId/forms/:formId/candidates/:candidateId",
            {
                templateUrl: "app/partials/evaluation.html",
                controller: "EvaluationController"
            })
            .when("/admin/forms/:formId",
            {
                templateUrl: "app/partials/form.html",
                controller: "FormController"
            })
            .when("/screening",{
                     templateUrl: "app/partials/screening.html",
                controller: "ScreeningController"
            })
            .when("/test/evaluation",
            {
                templateUrl: "app/partials/evaluation.html",
                controller: "EvaluationController"
            })
            .when("/test/form",
            {
                templateUrl: "app/partials/form.html",
                controller: "FormController"
            })
            .otherwise({redirectTo:"/screening"});
    });
    
}());