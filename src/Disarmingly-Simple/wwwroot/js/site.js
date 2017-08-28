var arrayOfFileNames = [];//Stores the audio file names in an array for access later on
var num = 1;
function incrementFileNames() {//increments the file names
    if (arrayOfFileNames.length === 0) {
        arrayOfFileNames.push("speech0");
    }
    else {
        arrayOfFileNames.push("speech0 " + "(" + num.toString() + ")");
    }
    num += 1;
}

$().ready(function () {
    $("#save").click(function () {
        $("#viz").hide();
        $("#controls").hide();
        $("#getAudioHelp").hide();
        $("#getTextHelp").show();
        incrementFileNames();
    })
    $("#getTextTime").click(function () {
        $("#loadingGif").show();
        $.ajax({
            type: 'GET',
            dataType: 'html',
            data: { fileName: arrayOfFileNames[arrayOfFileNames.length-1].toString() },//pulls most recent audio file
            url: '/TextOutput',
            success: function (result) {
                $("#loadingGif").hide();
                var shown = $(result).find(".toBeShown").html();
                $("#textOutputResult").html(shown);
            }
        })
    })
})