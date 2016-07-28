/*
Application Name: Revit
Description: Facilitates the evaluation of candidartes based on certain metrics
Frontend Developer: Nicolas Legrand (nicolaslegrand009@gmail.com)
Backend Developer: Benjamin Pauwels (Pauwels.Benjamin@hotmail.com)
Coach: Wouter Vandenneucker (wouter@vandenneucker.be)
#oSoc2016
 */

(function(){
    
    var app = angular.module("RevitApp", ["ngRoute",'rzModule']);
    
    app.config(function($routeProvider){
        $routeProvider
            //Test Page
            .when("/main", {
                templateUrl: "app/partials/main.html",
                controller: "MainController"
            })
            //Fake login Page
            .when("/login", {
                templateUrl: "app/partials/login.html",
                controller: "LoginController"
            })
            //Evaluation Page
            .when("/evaluation/juries/:juryId/forms/:formId/candidates/:candidateId",
            {
                templateUrl: "app/partials/evaluation.html",
                controller: "EvaluationController"
            })
            //Form editing Page
            .when("/admin/forms/:formId",
            {
                templateUrl: "app/partials/form.html",
                controller: "FormController"
            })
            //Screening Overview Page
            .when("/screening",{
                     templateUrl: "app/partials/screening.html",
                controller: "ScreeningController"
            })
            .otherwise({redirectTo:"/screening"});
    });
    
}());