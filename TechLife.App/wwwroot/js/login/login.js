/**
 * createIT main javascript file.
 */

var $devicewidth = (window.innerWidth > 0) ? window.innerWidth : screen.width;
var $deviceheight = (window.innerHeight > 0) ? window.innerHeight : screen.height;

(function ($) {

    if (document.getElementById('ct-js-wrapper')) {
        var snapper = new Snap({
            element: document.getElementById('ct-js-wrapper')
        });
    }
    $(document).ready(function () {
        if ($devicewidth > 767 && document.getElementById('ct-js-wrapper')) {
            snapper.disable();
        }
    })



})(jQuery);