let marker = null;

$(document).ready(function () {
    var bounds = L.latLngBounds(L.latLng(6.535372089237187, 92.03704517352661), L.latLng(26.8178652420686, 110.13638556709503));

    var map = L.map('map', {
        center: [16.469237420720585, 107.57805064644816],
        zoom: 15,
        minZoom: 10,
        maxBounds: bounds
    });

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(map);

    function addMarker(lat, lng) {
        if (marker != null) {
            map.removeLayer(marker);
        }

        if (lat != null && lat != "" && lat != 0 && lng != null && lng != "" && lng != 0) {
            if (($('#map').hasClass('d-none'))) {
                $('#map').removeClass('d-none')
            }

            marker = new L.circleMarker([lat, lng], {
                radius: 7,
                color: '#ffffff',
                weight: 1,
                fillColor: '#dc3545',
                fillOpacity: 1
            });

            map.addLayer(marker);

            map.flyTo([lat, lng], 17);
        } else {
            if (!($('#map').hasClass('d-none'))) {
                $('#map').addClass('d-none')
            }
        }
    }

    let pos = $('#position-mapp').val();
    if (pos != null && pos != "") {
        let latlng = pos.split(",")
        let lat = latlng[0].replace(/\s/g, '');
        let lng = latlng[1].replace(/\s/g, '');
        $('#toaDoX').val(lat);
        $('#toaDoY').val(lng);
        addMarker(lat, lng);
    }

    function addMarkerToMap() {
        let posX = $('#position-mappx').val();
        let posY = $('#position-mappy').val();
        if (posX != null && posX != "" && posY != null && posY != "") {
            addMarker(posX, posY);
        }
    }

    addMarkerToMap();

    $('body').on('show.bs.tab', function (e) {
        setTimeout(function () { map.invalidateSize() }, 500);
    })

    $('body').on('click', '#update-map', function (e) {
        addMarkerToMap();
    })
});