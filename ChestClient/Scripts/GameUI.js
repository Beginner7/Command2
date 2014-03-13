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
            $("#" + s).html("<img src = \"/Content/figure/" + data.DataBoard[s] + ".png\" width = 50 height = 50/>");
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
            $("#dialogWhitePromotion").dialog({
                resizable: false,
                width: 450,
                closeOnEscape: true,
                modal: true,
                close: function () {
                    $("#textArea").val("");
                    $.get("/Game/DoMove", { From: $.cellFrom, To: cellId, gameID: $.gameID, InWhom: $.InWhom }, function (data) {
                        if (data)
                            alert(data);
                        else {
                            $("#board td[id]").removeClass("selected");
                            FigurePosition();
                        }
                    });
                    $.InWhom = null;
                }
            });
            $('#whiteQueenButton').button({
                text: false,
                icons: {
                    primary: 'white-queen-icon'
                }
            }).click(function () {
                $.InWhom = "Q";
                $("#dialogWhitePromotion").dialog("close");
            });
            $('#whiteRookButton').button({
                text: false,
                icons: {
                    primary: 'white-rook-icon'
                }
            }).click(function () {
                $.InWhom = "R";
                $("#dialogWhitePromotion").dialog("close");
            });
            $('#whiteBishopButton').button({
                text: false,
                icons: {
                    primary: 'white-bishop-icon'
                }
            }).click(function () {
                $.InWhom = "B";
                $("#dialogWhitePromotion").dialog("close");
            });
            $('#whiteKnightButton').button({
                text: false,
                icons: {
                    primary: 'white-knight-icon'
                }
            }).click(function () {
                $.InWhom = "N";
                $("#dialogWhitePromotion").dialog("close");
            });
        }
        else
        {
            if (($.InWhom == null) && (cellId.charAt(1) == "1") && ($("#" + $.cellFrom + " img").attr("src").indexOf("PB.png") != -1)) {
                $("#dialogBlackPromotion").dialog({
                    resizable: false,
                    width: 450,
                    closeOnEscape: true,
                    modal: true,
                    close: function () {
                        $("#textArea").val("");
                        $.get("/Game/DoMove", { From: $.cellFrom, To: cellId, gameID: $.gameID, InWhom: $.InWhom }, function (data) {
                            if (data)
                                alert(data);
                            else {
                                $("#board td[id]").removeClass("selected");
                                FigurePosition();
                            }
                        });
                        $.InWhom = null;
                    }
                });
                $('#blackQueenButton').button({
                    text: false,
                    icons: {
                        primary: 'black-queen-icon'
                    }
                }).click(function () {
                    $.InWhom = "Q";
                    $("#dialogBlackPromotion").dialog("close");
                });
                $('#blackRookButton').button({
                    text: false,
                    icons: {
                        primary: 'black-rook-icon'
                    }
                }).click(function () {
                    $.InWhom = "R";
                    $("#dialogBlackPromotion").dialog("close");
                });
                $('#blackBishopButton').button({
                    text: false,
                    icons: {
                        primary: 'black-bishop-icon'
                    }
                }).click(function () {
                    $.InWhom = "B";
                    $("#dialogBlackPromotion").dialog("close");
                });
                $('#blackKnightButton').button({
                    text: false,
                    icons: {
                        primary: 'black-knight-icon'
                    }
                }).click(function () {
                    $.InWhom = "N";
                    $("#dialogBlackPromotion").dialog("close");
                });
            }
            else
            {
                $.get("/Game/DoMove", { From: $.cellFrom, To: cellId, gameID: $.gameID }, function (data) {
                    if (data)
                        alert(data);
                    else {
                        $("#board td[id]").removeClass("selected");
                        FigurePosition();
                    }
                });
            }
        }
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