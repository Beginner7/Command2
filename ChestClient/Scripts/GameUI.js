﻿$.gameID = null;
$.cellFrom = null;
$.InWhom = null;

$(document).ready(function () {
    var window;
    $("#board td[id]").click(function (eventObject) {
        cellClick(eventObject.currentTarget.id);
        
    });
    $.get("/Game/StartFree", function (data) {
        if (data == null)
            alert("НЕ удалось создать игру");
        else {
            $.gameID = data;
            FigurePosition();
        }
    });
});

function FigurePosition() {
    $.get("/Game/Status", { gameID: $.gameID }, function (data) {
        $("#board td[id]").html("");
        for (var s in data) {
            $("#" + s).html("<img src = \"/Content/figure/" + data[s] + ".png\" width = 40 height = 40/>");
        }
    });
}

function cellClick(cellId) {
    if ($("#" + cellId).hasClass("selected")) {
        if (($.InWhom == null) && (cellId.charAt(1) == "8") && ($("#" + $.cellFrom + " img").attr("src").indexOf("PW.png") != -1)) {
            $("#dialogWhitePromotion").dialog();
            $.InWhom = "Q";
        }
        if (($.InWhom == null) && (cellId.charAt(1) == "1") && ($("#" + $.cellFrom + " img").attr("src").indexOf("PB.png") != -1)) {
            $("#dialogBlackPromotion").dialog();
            $.InWhom = "Q";
        }

        $.get("/Game/DoMove", { From: $.cellFrom, To: cellId, gameID: $.gameID, InWhom: $.InWhom}, function (data) {
            if (data)
                alert(data);
            else {
                $("#board td[id]").removeClass("selected");
                FigurePosition();
            }

        });
    } else if ($("#" + cellId + " img").length) {
        $.cellFrom = cellId;
        $.get("/Game/MoveVariants", { cell: cellId, gameID: $.gameID }, function(data) {
            $("#board td[id]").removeClass("selected");
            for (var s in data) {
                $("#" + data[s]).addClass("selected");
            }
        });
    }
}