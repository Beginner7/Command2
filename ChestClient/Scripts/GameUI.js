$.gameID = null;
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
        for (var s in data.DataBoard) {
            $("#" + s).html("<img src = \"/Content/figure/" + data.DataBoard[s] + ".png\" width = 40 height = 40/>");
        }
        $("#playerwhite").html(data.DataWhitePlayer);
        $("#playerblack").html(data.DataBlackPlayer);
        if (data.DataTurn == 1) {
            $("#turn").html("<font size=\"5\" color=\"#330000\"> Черных </font>");
        } else {
            $("#turn").html("<font size=\"5\" color=\"ccff99\"> Белых </font>");
        }
        var move = "";
        if (data.DataTurn == 1) {
            for (var s2 in data.DataMoves) {
                move = move + "<font size=\"5\" color=\"#330000\">" + data.DataMoves[s2].From + "  " + data.DataMoves[s2].To + "</font> <br>";
                $("#moveslist").html(move);
            }
        } else {
            for (var s2 in data.DataMoves) {
                move = move + "<font size=\"5\" color=\"#ccff99\">" + data.DataMoves[s2].From + "  " + data.DataMoves[s2].To + "</font> <br>";
                $("#moveslist").html(move);
            }
        }
        $("#gamestatus").html(data.DataStatus);
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