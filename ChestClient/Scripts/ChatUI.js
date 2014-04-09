$(document).ready(function () {
    $(".btn-slide").click(function () {
        $("#panel").slideToggle("fast");
        $(this).toggleClass("active");
    });
});
$(document).ready(function () {

    $(".pane:even").addClass("alt");

    $(".pane .btn-delete").click(function () {
        alert("This comment will be deleted!");

        $(this).parents(".pane").animate({ backgroundColor: "#fbc7c7" }, "fast")
        .animate({ opacity: "hide" }, "slow");
        return false;
    });
});