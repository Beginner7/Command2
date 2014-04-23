$(document).ready(function () {
    var messagestabs = "";
    $(".btn-slide").click(function () {
        $("#panel").slideToggle("fast");
        $(this).toggleClass("active");
    });

    $(".pane:even").addClass("alt");

    $(".pane .btn-delete").click(function () {
        $(this).parents(".pane").animate({ backgroundColor: "#fbc7c7" }, "fast")
            .animate({ opacity: "hide" }, "slow");
        return false;
    });
    $("#SendMessageButton").click(function() {
        var text = $("#MessageArea").val();
        $("#MessageArea").val("");
        if (text.length == 0) {
            $("#MessageArea").animate({ borderColor: "red", top: 2 }, "slow")
                .animate({ borderColor: "#A9A9A9", top: 2}, "fast");
        } else {
            $.get("/Chat/SendMessage", { Message: text, GameID: $.gameID }, function (data) {
                messagestabs += "<div id=\"mymessage\" class=\"pane alt\"><p><h6>" + text + "</h6><a href=\"#\" class=\"btn-delete\">Delete</a>|</p></div>";                
                $("#messagesclass").html(messagestabs);
            });
        }
    });
});