$(document).ready(function () {
    //Initialize Select2 Elements
    $('select').select2()
    bsCustomFileInput.init();
    //Initialize Select2 Elements
    $('.select2bs4').select2({
        theme: 'bootstrap4'
    })
    $('form').on("submit", function () {
        $('button').attr('readonly', 'readonly');
    });
    //$('button[type="submit"]').off("click").on("click", function () {

    //    $('button').attr('disabled', 'disabled');
    //});
    $(".popup").on('click', function (e) {
        modelPopup(this);
    });
    var navaCookie = getCookie("nava");
    if (!IsNullOrEmpty(navaCookie)) {
        var IsShow = navaCookie == "true" ? true : false;
        if (IsShow) {
            $("body").addClass("sidebar-collapse");
        }
    }


    $(".btn-confirm").off("click").on("click", function (modal_from = "") {
        var url = $(this).data("url");
        var title = $(this).data("title");
        var content = $(this).data("content");
        document.getElementById("btn-submit").href = url;
        $(".modal-title").html(title);
        $(".modal-body").html(content);
        $("#confirm-modal").modal("show");
    });
    $(".btn-delete").off("click").on("click", function () {
        var val = $(this).data("url");
        var title = $(this).data("title");

        $(".modal-body").html(title);
        $("#form-submit-delete").attr("action", val);

        $('#confirm-modal').modal('show');
    })
    $("#nav_show_hidd").off("click").on("click", function () {
        showHideNava();
    })
});
function imageIsLoaded(num, e) {
    var id = "img_" + num;
    document.getElementById(id).style.backgroundImage = "url('" + e.target.result + "')";
    var next = num + 1;
    var html = '<div class="col-sm-2"><div class="position-relative form-group add-images" id="img_' + next + '" style="background-image: url(\'https://i.stack.imgur.com/mwFzF.png\');height: 95%;background-size:100% "><label class="control-label"></label><input name="Images.Images" id="Images.Images" type="file" data-id="' + next + '" class="form-control custom-file-input upload-image" /></div></div>';
    $("#images").append(html);
    $(".upload-image").off("change").on("change", (function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();
            var number = $(this).data("id");
            reader.onload = imageIsLoaded.bind(reader, number);
            reader.readAsDataURL(this.files[0]);
        }
    }));

};

function showHideNava() {
    var navaCookie = getCookie("nava");
    if (!IsNullOrEmpty(navaCookie)) {
        var IsShow = navaCookie == "true" ? true : false;
        if (IsShow) {
            setCookie("nava", "false", 1)
        }
        else {
            setCookie("nava", "true", 1)
        }
    }
    else {
        setCookie("nava", "true", 1)
    }
}

function IsNullOrEmpty(str) {
    if (str == null || str == "") {
        return true;
    }
    else return false;
}
function AlertAction(str) {
    alert(str);
}
function getUrlVars(url) {
    var myJson = {};
    if (url != '') {
        var hash;
        var hashes = url.slice(url.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            myJson[hash[0]] = hash[1] != null ? hash[1].replace("+", " ") : "";
        }
    }
    return myJson;
}
function modelPopup(reff) {
    var url = $(reff).data('url');
    var input = $('#pageCurent').val();
    var data = getUrlVars(decodeURIComponent(input));

    $.ajax({
        url: url,
        type: "GET",
        data: data,
        success: function (data) {
            if (data != null) {
                $('#modal .modal-dialog').html(data);
                $('#modal').modal("show");
            }
            hideLoading();
        }
    });
}

function showLoading() {
    $('.wait').remove();
    const wait = document.createElement('div');
    wait.classList.add('wait');
    const loader = document.createElement('div');
    loader.classList.add('loader');
    wait.appendChild(loader);
    const body = document.querySelector('body');
    body.appendChild(wait);
}

function hideLoading() {
    $('.wait').remove();
}

function showNotification({ isSuccessed = false, message = "" }) {
    toastr.options = {
        "positionClass": "toast-top-right",
        "timeOut": "3000"
    }
    if (isSuccessed) {
        toastr.success(message);

    } else {
        toastr.error(message);
    }
}