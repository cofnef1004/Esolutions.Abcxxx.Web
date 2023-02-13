"use strict";

// Class definition
var KTGeneralTypedJsQLBongDas = function() {
    // Private functions
    var exampleBasic = function() {
        var typed = new Typed("#kt_typedjs_example_1", {
            strings: ["First sentence.", "Second sentence.", "Third sentense", "And some longer sentence"],
            typeSpeed: 30
        });
    }

    return {
        // Public Functions
        init: function() {
            exampleBasic();
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function() {
    KTGeneralTypedJsQLBongDas.init();
});
