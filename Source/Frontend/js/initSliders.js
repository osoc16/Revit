(function() {
    $(document).ready(function() {


    	$(".my-ion-slider").each(function(){
		    $(this).ionRangeSlider({
            min: $(this).attr("data-min-score"),
            max: $(this).attr("data-max-score"),
            from:$(this).attr("data-proxy-val"),
            /*
    from_min: 10,
    from_max: 50,
            from_shadow: true,*/
            onUpdate:function(data){
            	console.log(data);
            }
        });
    	}); 
    });
})();