(function() {


var authenticationService = function($http, $log) {

			var getCurrentUserId= function(){

					return 1;

			}

			//Service API return
            return {

                getCurrentUserId:getCurrentUserId

            };

	    };


    var module = angular.module("RevitApp");
    module.factory("authenticationService", authenticationService);

}());