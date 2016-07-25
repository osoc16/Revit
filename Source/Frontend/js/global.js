(function() {
    $(document).ready(function() {




        bindCollapsibles();

    });

    var bindCollapsibles = function() {

    	$(document).on("click",".n-collapsible-toggler",function(){

    		$(this).closest(".n-collapsible-wrapper").children(".n-collapsible-content").toggle();

    	});

    }


})();