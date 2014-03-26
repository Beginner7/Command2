$(document).ready(function () {
    setInterval(function () { pulseTimer(); }, 1000);
    function pulseTimer() {
        $.get("/Play/PulseRequest", {}, function (data) {
            if (data.Ret)
                alert(data.Ret);
            if (data.Messages.length > 0) {
                for (var s in data.Messages) {
                    alert(data.Messages[s].Type);
                }
            }
        });
    }
    $.get("/Play/PlayRequest", {}, function (data) {
        if (data)
            alert(data);
    });
    $("#dialogWaitingOpponent").dialog({
        resizable: false,
        width: 350,
        closeOnEscape: true,
    });

    $('#buttonCancel').button().click(function () {
        $.get("/Play/StopRequest", {}, function (data) {
            if (data)
                alert(data);
        });
        window.location.replace("/");
    });
});