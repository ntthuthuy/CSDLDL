(function ($) {
    function Index() {
        var $this = this;
        function initialize() {

            $(".popup").on('click', function (e) {
                modelPopup(this);
            });

            function modelPopup(reff) {
                var url = $(reff).data('url');
            
                $.get(url).done(function (data) {
                    $('#modal-edit').find(".modal-dialog").html(data);
                    $('#modal-edit > .modal', data).modal("show");
                    $('select').select2()
                    bsCustomFileInput.init();
                });

            }
        }

        $this.init = function () {
            initialize();
        };
    }
    $(function () {
        var self = new Index();
        self.init();
    });
}(jQuery));