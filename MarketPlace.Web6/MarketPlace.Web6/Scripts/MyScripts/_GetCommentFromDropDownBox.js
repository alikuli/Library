

//Used in EncashmentRequest.cshtml
//and PaymentMethodsController
function getCommentOfPaymentMethod(dropdownListName, path, divToChangeId, commentId) {
    var selectedValue = getSelectValueOf(dropdownListName);
    var divToChange_Html = document.getElementById(divToChangeId);
    var divComment = document.getElementById(commentId);

    $.ajax({
        type: 'GET',
        dataType: 'json',
        cache: false,
        url: path + "?id=" + selectedValue,
        contentType: "application/json",
        success: function (data, textStatus, jqXHR) {
            //Do Stuff 
            divToChange_Html.innerText = data.message;
            divComment.setAttribute("placeholder", data.message); 


        },
        error: function (jqXHR, textStatus, errorThrown) {
            //Do Stuff or Nothing
            var error = textStatus
        }
    });
}

