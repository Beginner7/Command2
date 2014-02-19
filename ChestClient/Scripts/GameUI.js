$(document).ready(function () {
    $("#board td[id]").click(function (eventObject) {
        cellClick(eventObject.currentTarget.id);
        
    });
    FigurePosition();
});

function FigurePosition() {
    $.get("/Game/Status", function (data) {
        for (var s in data) {
            $("#" + s).html("<img src = \"/Content/figure/" + data[s] + ".png\" width = 40 height = 40/>");
        }
    });
}

function cellClick(cellId) {
    alert(cellId);
    //$.post("Account/Register", { Cell: "John" });
    //$.get("/Game/MoveVariants", function (cells) {
    //    for (var s in cells) {
    //        $("#" + s).html("<td bgcolor=\"123456\"> </td>");
    //    }
    //});
    
}