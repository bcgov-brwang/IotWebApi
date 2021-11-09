$(document).ready(function () {
    var interval;
    var globalSampleTestUrl = "https://localhost:44389/api/todoitems";
    var queue = [];
    var globalElapsedTime;

    function getUrl() {
        return $.trim($("#txtAjayUrl").val());
    }

    $("#btnLaunchRequests").on("click", function () {
        queue = [];
        $("#divTimeElapsed").html("<i>calculating</i>");
        globalElapsedTime = window.performance.now();
        $("#divRequestStatus").show();
        $("#btnLaunchRequests").prop("disabled", true);
        var totalRequests = 50;
        var inputValue = $("#txtNumberOfConcurrentRequests").val();
        totalRequests = inputValue.trim();

        $("#divTotalNumberOfProcessed").html(totalRequests.toString());
        if (interval != null && interval != undefined) {
            clearInterval(interval);
        }
        interval = window.setInterval(function () {
            $("#divNumberOfCurrentRequests").text(queue.length);
        }, 500);

        for (let i = 1; i <= totalRequests; i++) {
            console.log("Loop No. " + i);
            if (i == totalRequests) {
                $.get(getUrl(), function (data) {
                    queue.push("1");
                    $("#btnLaunchRequests").prop("disabled", false);
                    $("#divNumberOfCurrentRequests").text(queue.length);
                    //clearInterval(interval);
                }).always(function () {
                    $("#btnLaunchRequests").prop("disabled", false);
                    globalElapsedTime = window.performance.now() - globalElapsedTime;
                    globalElapsedTime = Math.round((globalElapsedTime / 1000) * 100) / 100;
                    console.log("%cLast Result Processed in " + globalElapsedTime + " Seconds.", "color:green");
                    $("#divTimeElapsed").text(globalElapsedTime + " seconds");
                });
            }
            else {
                $.get(getUrl(), function (data) {
                    queue.push("1");
                });
            }
        }
    });



});

function getRecent() {
    $.ajax({

        //url: "https://localhost:44397/api/seismic/recent",
        url: "https://iotwebapi20211108143620.azurewebsites.net/api/seismic/recent",
        type: "GET",
        success: function (data) {
            alert("OK");
        }
    });
}