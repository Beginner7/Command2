$(document).ready(function () {
    FigurePosition();
});

function FigurePosition() {
    $.get("/Game/Status", function (data) {
        for (var s in data) {
            $("#" + s).html("<img src = \"/Content/figure/" + data[s] + ".png\" width = 40 height = 40/>");
        }
    });
}