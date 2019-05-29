//https://stackoverflow.com/questions/35336833/jquery-rotate-an-image-and-save-them-with-same-name-file-overwrite

$(document).ready(function () {
    //  Initialize image and canvas
    var previous = 0;
    img = document.getElementById('image');
    canvas = document.getElementById('canvas');

    if (!canvas || !canvas.getContext) {
        if(canvas.parentNode)
            canvas.parentNode.removeChild(canvas);
    }
    else {
        img.style.position = 'absolute';
        img.style.visibility = 'hidden';
    }
    rotateImage(0);

    //  Handle clicks for control links
    $('#resetImage').click(function () {
        rotateImage(0);
    });

    $('#saveImage').click(function () {
        var downloadBtn = document.getElementById("saveImage");
        var theImageData = downloadBtn.getAttribute("href");
        var urlToFile = downloadBtn.getAttribute("data-urlToFile")
        var pictureName = document.getElementById("Name").value;
        var pictureRelativeAddress = document.getElementById("RelativeWebsitePath").value;
        var pictureNameWithPath = pictureRelativeAddress + '\\' + pictureName;
        var id = document.getElementById("Id");
        SavePicture(theImageData, id, urlToFile);
    });

    $('#rotate90').click(function () {
        rotateImage(90);
    });
    $('#rotate90Right').click(function () {
        previous += 90;
        if (previous > 270)
            previous = 0;

        rotateImage(previous);
    });

    $('#rotate90Left').click(function () {
        previous -= 90;
        if (previous < 0)
            previous = 270;

        rotateImage(previous);
    });

    $('#rotate180').click(function () { rotateImage(180); });
    $('#rotate270').click(function () { rotateImage(270); });
});

function rotateImage(degree) {
    if (document.getElementById('canvas')) {
        var cContext = canvas.getContext('2d');
        var cw = img.width, ch = img.height, cx = 0, cy = 0;

        //   Calculate new canvas size and x/y coorditates for image
        switch (degree) {
            case 90:
                cw = img.height;
                ch = img.width;
                cy = img.height * (-1);
                break;
            case 180:
                cx = img.width * (-1);
                cy = img.height * (-1);
                break;
            case 270:
                cw = img.height;
                ch = img.width;
                cx = img.width * (-1);
                break;
        }

        //  Rotate image            
        canvas.setAttribute('width', cw);
        canvas.setAttribute('height', ch);
        cContext.rotate(degree * Math.PI / 180);
        cContext.drawImage(img, cx, cy);
        $('#saveImage').attr('href', canvas.toDataURL())
    } else {
        //  Use DXImageTransform.Microsoft.BasicImage filter for MSIE
        switch (degree) {
            case 0: image.style.filter = 'progid:DXImageTransform.Microsoft.BasicImage(rotation=0)'; break;
            case 90: image.style.filter = 'progid:DXImageTransform.Microsoft.BasicImage(rotation=1)'; break;
            case 180: image.style.filter = 'progid:DXImageTransform.Microsoft.BasicImage(rotation=2)'; break;
            case 270: image.style.filter = 'progid:DXImageTransform.Microsoft.BasicImage(rotation=3)'; break;
        }
    }
}

////https://stackoverflow.com/questions/34039673/how-to-print-or-save-the-online-edited-image-with-javascript-jquery
//function dataURItoBlob(dataURI) {
//    // convert base64/URLEncoded data component to raw binary data held in a string
//    var byteString;
//    if (dataURI.split(',')[0].indexOf('base64') >= 0)
//        byteString = atob(dataURI.split(',')[1]);
//    else
//        byteString = unescape(dataURI.split(',')[1]);

//    // separate out the mime component
//    var mimeString = dataURI.split(',')[0].split(':')[1].split(';')[0];

//    // write the bytes of the string to a typed array
//    var ia = new Uint8Array(byteString.length);
//    for (var i = 0; i < byteString.length; i++) {
//        ia[i] = byteString.charCodeAt(i);
//    }

//    return new Blob([ia], { type: mimeString });
//}


function SavePicture(dataIn, idIn, urlToFile) {
    //In HTML5 you can do like this:
    var fixedImage = dataIn.replace('data:image/png;base64,', '');
    var data = JSON.stringify({ imageFile: fixedImage, id: idIn.value });

//    Then send it with ajax like this:

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: urlToFile,
        data: data,
        cache: false,
        success: function(result){
            //check success
            console.log("Made it");
        },
    });
}

////In HTML5 you can do like this:

////var blob = dataURItoBlob(data);
////var fd = new FormData();
////fd.append("imageFile", blob);
////Then send it with ajax like this:

////$.ajax({
////    type: "POST",
////    url:"url to file",
////    data: fd,
////    success: function(result){
////        //check success
////    };
//});