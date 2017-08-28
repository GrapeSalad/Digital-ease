var arrayOfFileNames = [];//Stores the audio file names in an array for access later on
var num = 1;
function incrementFileNames() {//increments the file names
    if (arrayOfFileNames.length === 0) {
        arrayOfFileNames.push("speech0");
    }
    else {
        arrayOfFileNames.push("speech0 " + "(" + num.toString() + ")");
        num += 1;
    }
}

$().ready(function () {
    $("#save").click(function () {
        $("#viz").hide();//visualizer
        $("#controls").hide();//record/save
        $("#getAudioHelp").hide();//text at top
        $("#getTextHelp").show();//same as above
        $("#showRecording").show();//button to show recording area
        $("#getTextTime").show();//button to send speech-text
        incrementFileNames();
    });
    $("#showRecording").click(function () {
        $("#viz").show();
        $("#controls").show();
        $("#getAudioHelp").show();
        $("#getTextTime").hide();
        $("#getTextHelp").hide();
        $("#showRecording").hide();
    })
    $("#getTextTime").click(function () {
        $("#loadingGif").show();
        debugger;
        $.ajax({
            type: 'GET',
            dataType: 'html',
            data: { fileName: arrayOfFileNames[arrayOfFileNames.length - 1].toString() },//pulls most recent audio file
            url: '/TextOutput',
            success: function (result) {
                $("#loadingGif").hide();
                var shown = $(result).find(".toBeShown").html();
                $("#textOutputResult").html(shown);
            }
        });
    });
});