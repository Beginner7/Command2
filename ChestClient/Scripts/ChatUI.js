$(document).ready(function () {
    $(".btn-slide").click(function () {
        $("#panel").slideToggle("fast");
        $(this).toggleClass("active");
    });
});