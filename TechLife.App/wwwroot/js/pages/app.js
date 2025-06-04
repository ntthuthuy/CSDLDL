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
        e.preventDefault();
        modelPopup(this);
    });

    $(".modal-popup").on('click', function (e) {
        e.preventDefault();
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

function defaultConfig(parent) {

    parent?.querySelectorAll('.datepicker').forEach(function (e) {
        $(e).datepicker({
            format: 'dd/mm/yyyy',
            autoclose: true
        });
    })
    parent?.querySelectorAll('.select2').forEach(function (e) {
        $(e).select2({
            width: "100%"
        });
    })
    parent?.querySelectorAll('.modal-popup').forEach(function (e) {
        $(e).click(function (event) {
            event.preventDefault();
            modelPopup(this);
        })
    })
}

function modelPopup(reff) {
    var url = $(reff).data('url');
    var input = $('#pageCurent').val();
    var data = getUrlVars(decodeURIComponent(input));
    var type = $(reff).data('type');
    $('#modal .modal-dialog').removeClass('modal-sm modal-lg modal-xl');
    $('#modal .modal-dialog').html('');

    $.ajax({
        url: url,
        type: "GET",
        data: data,
        success: function (data) {
            if (data != null) {
                $('#modal .modal-dialog').html(data);
                $('#modal').modal("show");
                if (type)
                    $('#modal .modal-dialog').addClass(type)
            }
            hideLoading();
        },
        error: function (xhr, status, error) {
            hideLoading();
            showNotification({ isSuccessed: false, message: xhr.responseText });
        },
        complete: function (xhr, status) {
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
        positionClass: "toast-top-right",
        timeOut: 3000
    }

    if (isSuccessed) {
        toastr.success(message);

    } else {
        toastr.error(message);
    }

}

function dropModal() {
    $('#modal').modal('hide');
    $('#modal .modal-dialog').removeClass('modal-sm modal-lg modal-xl');
    $('#modal .modal-dialog').html('');
}

function loadContent(url, element, callback = null) {
    showLoading();
    if (!isNullOrWhiteSpace(url)) {
        $.ajax({
            url: decodeURIComponent(url),
            type: "GET",
            success: function (data) {
                hideLoading();
                if (!isNullOrWhiteSpace(data)) {

                    $(element).html(data);
                    defaultConfig(element);
                }
                if (callback) {
                    callback();
                }
            }
        });
    }
}

function isNullOrWhiteSpace(str) {
    return str === undefined
        || str === null
        || typeof str !== 'string'
        || str.match(/^\s*$/) !== null;
}

function submitData(postUrl, data, callback = null) {
    showLoading();
    $.ajax({
        url: postUrl,
        type: "POST",
        data: data,
        success: function (data) {
            hideLoading();
            showNotification(data);
            if (callback) {
                callback();
            }
        }
    });
}

function debounce(fn, ms) {
    let timer;

    return function () {
        // Nhận các đối số
        const args = arguments;
        const context = this;

        if (timer) clearTimeout(timer);

        timer = setTimeout(() => {
            fn.apply(context, args);
        }, ms)
    }
}

function ConvertMoneyByThis(value, id) {    // chuyển số 00000000 sang định dạng tiền 00.000.000
    value = Numbers(value);
    str_rev = strrev(value);
    strplit = str_split(str_rev, 3);
    count = strplit.length;
    var res = '';
    for (i = 0; i < count; i++) {
        res += strplit[i] + ',';
    }
    res = res.substring(0, res.length - 1);
    res = strrev(res);
    $(id).val(res);
    return res
};

function Numbers(str) {
    return str.replace(/[^0-9]/gi, "");
}

function strrev(string) {
    string = string + '';
    var grapheme_extend = /(.)([\uDC00-\uDFFF\u0300-\u036F\u0483-\u0489\u0591-\u05BD\u05BF\u05C1\u05C2\u05C4\u05C5\u05C7\u0610-\u061A\u064B-\u065E\u0670\u06D6-\u06DC\u06DE-\u06E4\u06E7\u06E8\u06EA-\u06ED\u0711\u0730-\u074A\u07A6-\u07B0\u07EB-\u07F3\u0901-\u0903\u093C\u093E-\u094D\u0951-\u0954\u0962\u0963\u0981-\u0983\u09BC\u09BE-\u09C4\u09C7\u09C8\u09CB-\u09CD\u09D7\u09E2\u09E3\u0A01-\u0A03\u0A3C\u0A3E-\u0A42\u0A47\u0A48\u0A4B-\u0A4D\u0A51\u0A70\u0A71\u0A75\u0A81-\u0A83\u0ABC\u0ABE-\u0AC5\u0AC7-\u0AC9\u0ACB-\u0ACD\u0AE2\u0AE3\u0B01-\u0B03\u0B3C\u0B3E-\u0B44\u0B47\u0B48\u0B4B-\u0B4D\u0B56\u0B57\u0B62\u0B63\u0B82\u0BBE-\u0BC2\u0BC6-\u0BC8\u0BCA-\u0BCD\u0BD7\u0C01-\u0C03\u0C3E-\u0C44\u0C46-\u0C48\u0C4A-\u0C4D\u0C55\u0C56\u0C62\u0C63\u0C82\u0C83\u0CBC\u0CBE-\u0CC4\u0CC6-\u0CC8\u0CCA-\u0CCD\u0CD5\u0CD6\u0CE2\u0CE3\u0D02\u0D03\u0D3E-\u0D44\u0D46-\u0D48\u0D4A-\u0D4D\u0D57\u0D62\u0D63\u0D82\u0D83\u0DCA\u0DCF-\u0DD4\u0DD6\u0DD8-\u0DDF\u0DF2\u0DF3\u0E31\u0E34-\u0E3A\u0E47-\u0E4E\u0EB1\u0EB4-\u0EB9\u0EBB\u0EBC\u0EC8-\u0ECD\u0F18\u0F19\u0F35\u0F37\u0F39\u0F3E\u0F3F\u0F71-\u0F84\u0F86\u0F87\u0F90-\u0F97\u0F99-\u0FBC\u0FC6\u102B-\u103E\u1056-\u1059\u105E-\u1060\u1062-\u1064\u1067-\u106D\u1071-\u1074\u1082-\u108D\u108F\u135F\u1712-\u1714\u1732-\u1734\u1752\u1753\u1772\u1773\u17B6-\u17D3\u17DD\u180B-\u180D\u18A9\u1920-\u192B\u1930-\u193B\u19B0-\u19C0\u19C8\u19C9\u1A17-\u1A1B\u1B00-\u1B04\u1B34-\u1B44\u1B6B-\u1B73\u1B80-\u1B82\u1BA1-\u1BAA\u1C24-\u1C37\u1DC0-\u1DE6\u1DFE\u1DFF\u20D0-\u20F0\u2DE0-\u2DFF\u302A-\u302F\u3099\u309A\uA66F-\uA672\uA67C\uA67D\uA802\uA806\uA80B\uA823-\uA827\uA880\uA881\uA8B4-\uA8C4\uA926-\uA92D\uA947-\uA953\uAA29-\uAA36\uAA43\uAA4C\uAA4D\uFB1E\uFE00-\uFE0F\uFE20-\uFE26]+)/g;
    string = string.replace(grapheme_extend, '$2$1'); // Temporarily reverse
    return string.split('').reverse().join('');
};

function str_split(string, split_length) {
    if (split_length === null) {
        split_length = 1;
    }
    if (string === null || split_length < 1) {
        return false;
    }
    string += '';
    var chunks = [],
        pos = 0,
        len = string.length;
    while (pos < len) {
        chunks.push(string.slice(pos, pos += split_length));
    }
    return chunks;
};

function checkInputMoney(element) {
    let value = element.value.trim().replace(/\s+/g, '');
    value = value.replace(/[.,]/g, '');
    if (value.length > 1) {
        value = value.replace(/^0+/, '');
    }
    if (!value) {
        element.classList.add("is-invalid");
        element.classList.remove('is-valid');
        return;
    }
    const regex = /^\d+$/.test(value);
    if (!regex) {
        element.classList.add("is-invalid");
        element.classList.remove('is-valid');
        return;
    } else {
        element.classList.add('is-valid');
        element.classList.remove("is-invalid");
    }
    return value;
}