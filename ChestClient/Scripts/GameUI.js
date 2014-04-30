$.gameID = null;
$.cellFrom = null;
$.InWhom = null;
$.OppFrom = null;
$.OppTo = null;

$(document).ready(function () {
    setInterval(function () { pulseTimer(); }, 3000);
    function pulseTimer() {
        $.get("/Play/PulseRequest", {}, function (data) {
            if (data.Ret)
                alert(data.Ret);
            if (data.Messages.length > 0) {
                for (var s in data.Messages) {
                    if (data.Messages[s].Type == 4) {
                        FigurePosition();
                        $.OppFrom = data.Messages[s].Text.charAt(0) + data.Messages[s].Text.charAt(1);
                        $.OppTo = data.Messages[s].Text.charAt(3) + data.Messages[s].Text.charAt(4);
                    }
                    if (data.Messages[s].Type == 0) {
                        messagestabs = $("#messagesclass").html() + "<div class=\"pane alt\">" + data.Messages[s].Text + "</div>";
                        $("#messagesclass").html(messagestabs);
                    }
                    if (data.Messages[s].Type == 5) {
                        alert("Opponent surrendered.");
                        window.location.replace("/");
                    }
                    if (data.Messages[s].Type == 7) {
                        alert("Opponent accepted peace.");
                        window.location.replace("/");
                    }
                    if (data.Messages[s].Type == 8) {
                        alert("Opponent declined peace.");
                    }
                    isPeace = null;
                    if (data.Messages[s].Type == 6) {
                        $("#dialogPeaceConfirmation").dialog({
                            resizable: false,
                            width: 450,
                            modal: true,
                            open: function () {
                                $(".ui-widget-overlay").css('background', 'black');
                                $(".ui-dialog-titlebar-close").hide();
                            },
                            close: function () {
                                if (isPeace == true) {
                                    $.get("/Play/AcceptPeaceRequest?gameID=" + $.gameID);
                                    alert("You accepted peace.");
                                    window.location.replace("/");
                                }
                                if (isPeace == false) {
                                    $.get("/Play/DeclinePeaceRequest?gameID=" + $.gameID);
                                }
                            }
                        });
                        $('#buttonPeaceConfirm').button().click(function () {
                            isPeace = true;
                            $("#dialogPeaceConfirmation").dialog("close");
                        });
                        $('#buttonPeaceDecline').button().click(function () {
                            isPeace = false;
                            $("#dialogPeaceConfirmation").dialog("close");
                        });
                    }
                }
            }
        });
    }

    $("#board td[id]").click(function (eventObject) {
        cellClick(eventObject.currentTarget.id);
    });
    if (document.URL.indexOf("?gameID=") > 0) {
        $.gameID = document.URL.substr(document.URL.lastIndexOf("?gameID=") + 8, document.URL.length);
        FigurePosition();
    }
    else {
        $.get("/Game/StartFree", function (data) {
            if (data == null)
                alert("НЕ удалось создать игру");
            else {
                $.gameID = data;
                FigurePosition();
            }
        });
    }
    $('#surrenderButton').button().click(function () {
        $.get("/Play/SurrenderRequest?gameID=" + $.gameID);
        alert("You surrendered.");
        window.location.replace("/");
    });
    $('#peaceButton').button().click(function () {
        $.get("/Play/PeaceRequest?gameID=" + $.gameID);
        alert("Peace request sended to opponent.");
    });
});

function FigurePosition() {
    $.get("/Game/Status", { gameID: $.gameID }, function(data) {
        $("#board td[id]").html("");
        $("#board td[id]").removeClass("spinner");
        $("#board td[id]").removeClass("check");
        $("#board td[id]").removeClass("move");
        $("#board td[id]").removeClass("selectedFrom");
        $("#board td[id]").removeClass("selectedTo");
        ///Отображение хода противника стрелками
        /*
        if (data.DataMove.length > 0) {
            var from = data.DataMove[data.DataMove.length - 1].From;
            var to = data.DataMove[data.DataMove.length - 1].To;
            var hor = from.charCodeAt(0) - to.charCodeAt(0);
            var ver = from.charCodeAt(1) - to.charCodeAt(1);
            while (ver != 0 || hor != 0) {
                $("#" + String.fromCharCode(to.charCodeAt(0) + hor) + String.fromCharCode(to.charCodeAt(1) + ver)).addClass("spinner");
                if (hor > 0) {
                    hor--;
                }
                if (hor < 0) {
                    hor++;
                }
                if (ver > 0) {
                    ver--;
                }
                if (ver < 0) {
                    ver++;
                }
            }
        }
        */
        ///
        if (data.DataMove.length > 0) {
            $("#" + data.DataMove[data.DataMove.length - 1].From).addClass("selectedFrom");
            $("#" + data.DataMove[data.DataMove.length - 1].To).addClass("selectedTo");
        }
        for (var s in data.DataBoard) {
            if (data.DataTextStatus.indexOf("Check Black") > -1 && data.DataBoard[s] == "KB") {
                $("#" + s).addClass("check");
            }
            //if (data.DataTextStatus.indexOf("Now in progress")) {
            //    $("#" + s).addClass("move");
            //}
            if (data.DataTextStatus.indexOf("Check White") > -1 && data.DataBoard[s] == "KW") {
                $("#" + s).addClass("check");
            }
            $("#" + s).html("<img src = \"/Content/figure/" + data.DataBoard[s] + ".png\" width = 50 height = 50/>");
        }
        $("#playerwhite").html(data.DataWhitePlayer);
        $("#playerblack").html(data.DataBlackPlayer);
        if (data.DataTurn == 1) {
            $("#turn").html("<font size=\"3\" color=\"#330000\"> Черных </font>");
        } else {
            $("#turn").html("<font size=\"3\" color=\"#E9967A\"> Белых </font>");
        }
        var eatedwhites = "";
        for (i = 0; i < data.EatedWhites.length; i++) {
            eatedwhites += "<img src = \"/Content/figure/" + data.EatedWhites.charAt(i) + "W.png\" width = 24 height = 24/>";
        }
        var eatedblacks = "";
        for (var i = 0; i < data.EatedBlacks.length; i++) {
            eatedblacks += "<img src = \"/Content/figure/" + data.EatedBlacks.charAt(i) + "B.png\" width = 24 height = 24/>";
        }
        $("#whitemorgue").html(eatedwhites);
        $("#blackmorgue").html(eatedblacks);

        var move = "";
        var moves = [];
        for (var s2 in data.DataMove) {
            var x = Math.floor(s2 / 2);
            if (s2 % 2 == 0) {
                moves[x] = { move1: data.DataMove[s2] };
            } else {
                moves[x].move2 = data.DataMove[s2];
            }
        }
        for (var s3 in moves) {
            move = move + "<font size=\"3\" color=\"#330000\">" + (s3 * 1 + 1) + ". " + (move[s3].move1.Result3.indexOf("") == 0 ? (moves[s3].move1.DataMovedFigure + moves[s3].move1.From + moves[s3].move1.Result1 + moves[s3].move1.To + (moves[s3].move1.InWhom ? moves[s3].move1.InWhom + "  " : "") + moves[s3].move1.Result2) : move[s3].move1.Result3) + " &nbsp" +
                                                                                        (move[s3].move2.Result3.indexOf("") == 0 ? (moves[s3].move2 ? moves[s3].move2.DataMovedFigure + moves[s3].move2.From + moves[s3].move2.Result1 + moves[s3].move2.To + (moves[s3].move2.InWhom ? moves[s3].move2.InWhom + "  " : "") + moves[s3].move2.Result2 : "") : move[s3].move2.Result3) + "</font> </br>";
            $("#moveslist").html(move);
        }

        if (data.DataTextStatus.indexOf("Won by White") > -1) {
            $("#dialogWhiteWon").dialog({
                resizable: false,
                width: 450,
                closeOnEscape: true,
                modal: true,
                open: function () {
                    $(".ui-widget-overlay").css('background', 'black');
                },
            });
            $(".ui-widget-overlay").click(function () {
                $("#dialogWhiteWon").dialog("close");
            });
        }
        if (data.DataTextStatus.indexOf("Won by Black") > -1) {
            $("#dialogBlackWon").dialog({
                resizable: false,
                width: 450,
                closeOnEscape: true,
                modal: true,
                open: function () {
                    $(".ui-widget-overlay").css('background', 'black');
                },
            });
            $(".ui-widget-overlay").click(function () {
                $("#dialogBlackWon").dialog("close");
            });
        }
    });
}

function cellClick(cellId) {
    if ($("#" + cellId).hasClass("selected")) {
        $("#" + cellId).addClass("spinner");
        if (($.InWhom == null) && (cellId.charAt(1) == "8") && ($("#" + $.cellFrom + " img").attr("src").indexOf("PW.png") != -1)) {
            $("#dialogWhitePromotion").dialog({
                resizable: false,
                width: 450,
                closeOnEscape: true,
                modal: true,
                open: function () { $(".ui-dialog-titlebar-close").hide();
                    $(".ui-widget-overlay").css('background', 'black');
                },
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
            $(".ui-widget-overlay").click(function () {
                $.InWhom = null;
                $("#dialogWhitePromotion").dialog("close");
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
        else {
            if (($.InWhom == null) && (cellId.charAt(1) == "1") && ($("#" + $.cellFrom + " img").attr("src").indexOf("PB.png") != -1)) {
                $("#dialogBlackPromotion").dialog({
                    resizable: false,
                    width: 450,
                    closeOnEscape: true,
                    modal: true,
                    open: function () { $(".ui-dialog-titlebar-close").hide(); },
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
                $(".ui-widget-overlay").click(function () {
                    $.InWhom = null;
                    $("#dialogBlackPromotion").dialog("close");
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
            else {
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
        $("#board td[id]").removeClass("selectedFrom");
        $("#board td[id]").removeClass("selectedTo");
        $.OppFrom = null;
        $.OppTo = null;
    } else if ($("#" + cellId + " img").length) {
        $.cellFrom = cellId;
        $("#" + cellId).addClass("spinner");
        $.get("/Game/MoveVariants", { cell: cellId, gameID: $.gameID }, function (data) {
            $("#board td[id]").removeClass("selected");
            $("#board td[id]").removeClass("spinner");
            for (var s in data) {
                $("#" + data[s]).addClass("selected");
            }
        });
    }
}