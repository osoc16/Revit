(function(){
    
    var app = angular.module("RevitApp", ["ngRoute"]);
    
    app.config(function($routeProvider){
        $routeProvider
            .when("/main", {
                templateUrl: "app/partials/main.html",
                controller: "MainController"
            })
            .otherwise({redirectTo:"/main"});
    });
    
}());