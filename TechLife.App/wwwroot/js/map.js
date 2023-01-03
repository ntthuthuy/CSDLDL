$(document).ready(function () {
    initMap();
});
function initMap() {
    var value = $(".position-mapp").val();
    var lt = ""; var lg = "";
    if (value != "") {
        var arr = value.split(',');
        lt = parseFloat(arr[0].trim());
        lg = parseFloat(arr[1].trim());
    }
    else {
        lt = parseFloat(16.471687); lg = parseFloat(107.594429);
    }
    const myLatlng = { lat: parseFloat(lt), lng: parseFloat(lg) };
    var map = new google.maps.Map(document.getElementById('mapgg'), {
        zoom: 16,
        center: myLatlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    });

    var marker = new google.maps.Marker({
        position: myLatlng,
        title: '@Html.Raw(Model.DuLieuDuLich.Ten)',
        map: map,
        draggable: true
    });
    marker.addListener("click", () => {
        updateMarkerPosition(marker.getPosition());
    });
    marker.addListener("drag", () => {
        updateMarkerPosition(marker.getPosition());
    });
}