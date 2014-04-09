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

    $(".pane .btn-unapprove").click(function () {
        $(this).parents(".pane").animate({ backgroundColor: "#fff568" }, "fast")
        .animate({ backgroundColor: "#ffffff" }, "slow")
        .addClass("spam");
        return false;
    });

    $(".pane .btn-approve").click(function () {
        $(this).parents(".pane").animate({ backgroundColor: "#dafda5" }, "fast")
        .animate({ backgroundColor: "#ffffff" }, "slow")
        .removeClass("spam");
        return false;
    });

    $(".pane .btn-spam").click(function () {
        $(this).parents(".pane").animate({ backgroundColor: "#fbc7c7" }, "fast")
        .animate({ opacity: "hide" }, "slow");
        return false;
    });
});