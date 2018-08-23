$(document).ready(function () {
});




//Convention
//Like Button Id = like123
//Unlikw Button Id = unlike123
//Like Count Button Id = like123count
//Unlike count button Id = unlike123count


function Like(thisButton)
{

    var thisButtonId = '#' + $(thisButton).attr('id');
    var otherButtonId = "";

    var otherButtonCountId = "";
    var thisbuttonCountId = "";

    var thisButtonUrl = "";
    var otherButtonUrl = "";

    var currBtnId = $(thisButton).attr("id");
    if (IsLike(currBtnId))
    {
        //if (currBtnId.indexOf("un") == -1) {
        //thisButton is a like button
        otherButtonId = '#un' + $(thisButton).attr("id");
    }
    else
    {
        //thisButton is an unlike button
        otherButtonId = '#' + currBtnId.slice(2)

    }

    otherButtonCountId = otherButtonId + "count";
    thisbuttonCountId = thisButtonId + "count";

    //get the buttons
    var thisButtonCount = $(thisbuttonCountId);

    var otherButton = $(otherButtonId);
    var otherButtonCount = $(otherButtonCountId);

    //get the comment for the like/unlike
    var theComment = $('#LikeUnlikeModalForEntryId').val();
    //get the Urls
    thisButtonUrl = $(thisButton).attr('data-url');
    otherButtonUrl = $(otherButton).attr('data-url');

    if (theComment && theComment.length !== 0)
    {
        commentUrlAddition = '&comment=' + theComment;
        thisButtonUrl = thisButtonUrl + commentUrlAddition;
        otherButtonUrl = otherButtonUrl + commentUrlAddition;
    }
    else
    {
        alert("Comment is empty. Nothing Posted");
        return false;
    }




    $.ajax({
        type: 'POST',
        url: thisButtonUrl,
        success: function (response) {
            if (response.success) {

                //enables/disables buttons
                $(thisButtonId).addClass("disabled");
                $(otherButtonId).removeClass('disabled');

                //the controller sends back the data correctly
                thisButtonCount.html(response.thisButtonCount);
                otherButtonCount.html(response.otherButtonCount);
                $.notify(response.message, "success");
            }
            else {
                $.notify(response.message, "error");
            }
        }
    });
}

//------------------------------------------------------



function EditlikeUnlikeModal(sender,isLike) {

    var modalBody = document.getElementById("myModalbodyNewid");
    var senderId = $(sender).attr('id');
    var dataUrl = $(sender).attr('data-url');
    var nameOfProduct = $(sender).attr('data-nameofproduct');
    var spanInModalHeading = document.getElementById("myModalHeading.span");
    var modalCloseBtn = document.getElementById("modalNewCloseBtnId");
    $(modalCloseBtn).attr('onclick', "Like(document.getElementById('" + senderId + "'));");

    if (IsLike(senderId))
    {
        spanInModalHeading.className = 'text-success';
        spanInModalHeading.innerHTML = " Please tell us why you like " + nameOfProduct;

    }
    else
    {
        spanInModalHeading.className = 'text-danger';
        spanInModalHeading.innerHTML = " Please tell us why you dont like " + nameOfProduct;

    }

    //remove all previous elements
    clearAllModalHtml(modalBody);
    EditlikeUnlikeModalHTML(modalBody);


}//end function


function EditlikeUnlikeModalHTML(modalBody) {
    
    //build the inner data
    let rowDiv = document.createElement('div');
    rowDiv.className = 'row';

    let formGroupDiv = document.createElement('div');
    formGroupDiv.className = 'form-group';



    let labelDiv = document.createElement('span');
    labelDiv.className = 'control-label col-md-2';
    labelDiv.innerHTML = "Why?";


    let inputElement = document.createElement('textarea');
    inputElement.className = 'form-control';
    inputElement.type = 'text';
    inputElement.id = 'LikeUnlikeModalForEntryId';

    let ctrlDiv = document.createElement('span');
    ctrlDiv.className = 'col-md-10';
    ctrlDiv.appendChild(inputElement);

    rowDiv.appendChild(labelDiv);
    rowDiv.appendChild(ctrlDiv);

    formGroupDiv.appendChild(rowDiv);
    modalBody.appendChild(formGroupDiv);

    FocusOnCommentFieldInLikeUnlikeModal();
    ShowSaveBtnInLikeUnlike();

}

//------------------------------------------------------



function ShowPplWhoLikeUnlikeThis(sender) {

    var modalBody = document.getElementById("myModalbodyNewid");
    var dataUrl = $(sender).attr('data-url');
    var senderId = $(sender).attr('id');
    var nameOfProduct = $(sender).attr('data-nameofproduct');
    var spanInModalHeading = document.getElementById("myModalHeading.span");

    if (IsLike(senderId))
    {
        spanInModalHeading.className = 'text-success far fa-smile';
        spanInModalHeading.innerHTML = " Happy with " + nameOfProduct;
    }
    else
    {
        spanInModalHeading.className = 'text-danger far fa-frown-open';
        spanInModalHeading.innerHTML = " Unhappy with " + nameOfProduct;

    }
    //ShowSaveBtnInLikeUnlike('Close');
    //remove all previous elements
    clearAllModalHtml(modalBody);

    $.ajax({
        type: 'GET',
        cache: false,
        url: dataUrl,
        success: function (response) 
        {

            var usersLst = response.likedUserLst;

            if (!IsLike(senderId))
                usersLst = response.unlikedUsersLst;

            makeModalToShowUsers(usersLst, modalBody);


        }, //end success
        error: function (jq, status, message) {
            alert('A jQuery error has occurred. Status: ' + status + ' - Message: ' + message);
        }//end error
    }); //end ajax
}//end function


//This will make the modal. The modalBody is the body in which we want the Html to show.
//the un
function makeModalToShowUsers(usersLst, modalBody) {
    var len = usersLst.length;
    //build the inner data
    var counter = 0;
    for (var item in usersLst.Data) {
        currData = usersLst.Data[item];
        var theImage = "/Content/MyImages/logo.jpg";
        makeModalToShowUsersHtml(modalBody, currData, theImage, counter);
        counter++;

    }; //End for

    //$("#modalNewCloseBtnId").innerHTML = "Close";


}

function makeModalToShowUsersHtml(modalBody, currData, theImage, counter) {
    //create
    let div = document.createElement('div');
    div.className = 'row';

    let spanImage = document.createElement('span');
    spanImage.className = 'col-md-2';

    let spanNameLink = document.createElement('span');
    spanNameLink.className = 'col-md-2';

    let spanComment = document.createElement('span');
    spanComment.className = 'col-md-8';
    spanComment.className = 'text-left';
    spanComment.innerHTML = currData.UserComment;


    let anchor = document.createElement('a');
    anchor.href = currData.UserAddressFixed;
    anchor.textContent = currData.Name + ": ";

    let image = document.createElement('img');
    image.src = theImage;
    image.height = '75';
    image.width = '75';
    var theId = 'image' + counter;
    image.id = theId;
    image.style.margin = '3';
    image.className = 'img-thumbnail';

    

    //append
    spanImage.appendChild(image);
    spanNameLink.appendChild(anchor);
    

    div.appendChild(spanImage);
    div.appendChild(spanNameLink);
    div.appendChild(spanComment);
    modalBody.appendChild(div);
    HideSaveBtnInLikeUnlike();
}




//------------------------------------------------------

function Bid() {
    alert("Bidding Not implemented");
}

function ShoppingCart() {
    alert("Shopping Cart Not implemented");

}
//------------------------------------------------------

//Looks at the id and decides if it is a like button or not.

function IsLike(thisButton) {
    var currBtnId = $('#' + thisButton).attr("id");
    if (currBtnId.indexOf("unlike") == -1) {
        //thisButton is a like button
        return true;
    }
    else {
        //thisButton is an unlike button
        return false;

    }

}

function LoadDataUrlInComment(sender, commentSaveBtn)
{
    var dataToSave = $(sender).attr('data-url');
    $(commentSaveBtn).attr('data-url', dataToSave);

}

function FocusOnCommentFieldInLikeUnlikeModal() {
    $('#mymodalnew').on('shown.bs.modal', function () {
        $('#LikeUnlikeModalForEntryId').trigger('focus');
    });

    //$('#myModalbodyNewid').on("shown.bs.modal", function () {
    //    $(this).find(".form-control:first").focus();
    //});
    //document.getElementById("LikeUnlikeModalForEntryId").focus();
}

function ShowSaveBtnInLikeUnlike() {
    $("#modalNewCloseBtnId").removeClass('invisible');
}

function HideSaveBtnInLikeUnlike() {
    $("#modalNewCloseBtnId").addClass('invisible');
}

//This deletes all the previous HTML. If we dont do this then the old HTML from the previous Modal will show.
function clearAllModalHtml(modalBody) {
    while (modalBody.firstChild) {
        modalBody.removeChild(modalBody.firstChild);
    }
}




//------------------------------------------------------




function AddComment(saveBtn, theModal, commentCtrl) {
    //get the data url
    var dataUrl = $(saveBtn).attr('data-url');
    var commentData = $(commentCtrl).val();
    dataUrl = dataUrl + '&comment=' + commentData;
    $(theModal).modal('hide')
    $(commentCtrl).val("");
    if (dataUrl) {
        $.ajax({
            type: 'POST',
            url: dataUrl,
            success: function (response) {
                if (response.success) {
                    console.log(response.success);
                    $.notify(response.message, "success");
                }
                else {
                    console.log(response.success);
                    $.notify(response.message, "error");

                }
            }
        });
    }
    return false;

}

