(function() {


var authenticationService = function($http, $log) {

			var defaultUserId=1;

			var currentUserId=defaultUserId;


			var getCurrentUserId= function(){

					return currentUserId;

			}

			var setCurrentUserId=function(userId){
				alert("test");
				currentUserId=userId;
			}

			//Service API return
            return {

                getCurrentUserId:getCurrentUserId,
                 setCurrentUserId:setCurrentUserId

            };

	    };


    var module = angular.module("RevitApp");
    module.factory("authenticationService", authenticationService);

}());