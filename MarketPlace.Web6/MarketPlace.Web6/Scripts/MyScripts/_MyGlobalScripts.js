$(document).ready(function () {

    //console.log('Global');
    OnLoadModelSelectFirstInput();
    OnHoverClassImgThumbnailBackgroundYellow();
    initializeToolTipsBootStrap4();
    onClick();

});


function initializeToolTipsBootStrap4()
{
    console.log('initializeToolTipsBootStrap4');
    $('[data-toggle="tooltip"]').tooltip();
}

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
         

function mouseOver() {
    console.log("Mouse Over sensed");
}

function onClick() {
    console.log("Clicked!");
}

function AttachToolTipsBootStrap3() {
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


//This is taken from JqTest8.
//we need to wire the form as well as follows...
//

//@using (Html.BeginForm("AddOrEdit", "Employees", new { isedit = @ViewBag.IsEdit }, FormMethod.Post, 
//    new
//    {
//        enctype = "multipart/form-data",
//        onsubmit = "return AjaxPost(this);",
//        data_resetUrl = @Url.Action("AddOrEdit", "Employees", new { id = 0 })
//    }))





function AjaxPost(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        var ajaxConfig = {
            type: 'POST',
            data: new FormData(form),
            url: form.action,
            success: function (response) {
                $('#viewAll').html(response);
                refreshAddOrEdit($(form).attr('data-resetUrl'), true);
            }
        };

        if ($(form).attr('enctype') === 'multipart/form-data') {
            ajaxConfig["contentType"] = false;
            ajaxConfig["processData"] = false;
        }

        $.ajax(ajaxConfig);
    }
    return false;
}
