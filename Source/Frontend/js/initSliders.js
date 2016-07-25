(function() {
    $(document).ready(function() {


    	$(".my-ion-slider").each(function(){
		    $(this).ionRangeSlider({
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