$().ready(function () {
    $("#save").click(function () {
        $("#viz").hide();
        $("#controls").hide();
        $("#getAudioHelp").hide();
        $("#getTextHelp").show();
    })
    $("#getTextTime").click(function () {
        $.ajax({
            type: 'GET',
            dataType: 'html',
            data: { fileName: "myRecording02" },
            url: '/TextOutput',
            success: function (result) {
                var shown = $(result).find(".toBeShown").html();
                $("#textOutputResult").html(shown);
            }
        })
    })
})