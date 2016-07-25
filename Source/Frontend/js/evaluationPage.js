(function() {
    $(document).ready(function() {

        bindResetSlider();

    });

    var bindResetSlider = function() {

        $(document).on("click", ".slider-resetter", function() {

            var sliderInput = $(this).closest(".slider-container").find(".slider-backup-input").val("null").change();

        });


    }

})();