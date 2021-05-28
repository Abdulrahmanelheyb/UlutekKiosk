$(document).ready(function () {

    var url = window.location.pathname;
    url = url.replace("/", "");
    $('a').removeClass("active");
    if (url === "") {
        $('a[href="/"]').addClass("active");
    }
    
    $('a[href*="' + url + '"]').addClass("active");

});