$.gameID = null;
$.cellFrom = null;

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
            $("#" + s).html("<img src = \"/Content/figure/" + data[s] + ".png\" width = 40 height = 40/>");
        }
        for (var s in data.DataStatus)
        {
            
        }
    });
}

function cellClick(cellId) {
    if ($("#" + cellId).hasClass("selected")) {
        $.get("/Game/DoMove", { From: $.cellFrom, To: cellId, gameID: $.gameID }, function (data) {

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