(function() {
    $(document).ready(function() {




        bindCollapsibles();

    });

    var bindCollapsibles = function() {
      // For use within normal web clients
      var isiPad = navigator.userAgent.match(/iPad/i) != null;

      if( !isiPad ){

      	$(document).on("click",".n-collapsible-toggler",function(){

      		$(this).closest(".n-collapsible-wrapper").children(".n-collapsible-content").toggle();

      	});
      } else {
        $(document).on("touchstart",".n-collapsible-toggler",function(){

      		$(this).closest(".n-collapsible-wrapper").children(".n-collapsible-content").toggle();

      	});
      }

    }


})();
