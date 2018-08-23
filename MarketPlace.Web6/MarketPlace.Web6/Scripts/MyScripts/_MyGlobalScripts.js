$(document).ready(function () {

    OnLoadModelSelectFirstInput();
    OnHoverClassImgThumbnailBackgroundYellow();
    AttachToolTips();

});

function PreviewImage(uploader, theImage)
{
    if(uploader && uploader.files && uploader.files[0])
    {
        var reader = new FileReader();
        {
            reader.onload = function (e) {
                $(theImage).attr('src', e.target.result);

            }

        }

        reader.readAsDataURL(uploader.files[0]);
        return false;
    }
}

function AttachToolTips() {
    $("[rel='tooltip']").tooltip();

}

function OnHoverClassImgThumbnailBackgroundYellow() {
    $('.backgroundYellowOnHover').hover(function () {
        $(this).css("background-color", "yellow");
        $(this).css("border-color", "blue");
        $(this).css("border-top-width", "5px;");

    }, function () {
        $(this).css("background-color", "white");
        $(this).css("border-color", "inherit");
        $(this).css("border-top-width", "inherit;");
    });

}

function OnLoadModelSelectFirstInput() {
    $('form:first *:input[type!=hidden]:first').focus();

}

function LogOut(sendingControl, dataUrl, helloLink) {

    $.ajax({
        type: 'POST',
        url: dataUrl,
        success: function () {
            helloLink.innerHTML = "Register";
            sendingControl.innerHTML = "Log in";
            $.notify("Logged Out", "success");
        }

    });

    
}