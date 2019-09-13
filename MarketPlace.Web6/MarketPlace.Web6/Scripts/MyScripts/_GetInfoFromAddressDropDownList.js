

function updateAddressFields(dropDownListName, preNameOfFields, path) {
    var houseFieldName = preNameOfFields + "_HouseNo"
    var roadFieldName = preNameOfFields + "_Road"
    var address2FieldName = preNameOfFields + "_Address2"
    var townNameFieldName = preNameOfFields + "_TownName"
    var cityNameFieldName = preNameOfFields + "_CityName"
    var stateNameFieldName = preNameOfFields + "_StateName"
    var zipFieldName = preNameOfFields + "_Zip"
    var countryNameFieldName = preNameOfFields + "_CountryName"
    var phoneNameFieldName = preNameOfFields + "_Phone"
    var webNameFieldName = preNameOfFields + "_WebAddress"
    var emailNameFieldName = preNameOfFields + "_Email"
    var attentionFieldName = preNameOfFields + "_Attention"

    var houseElement = document.getElementById(houseFieldName)
    var roadElement = document.getElementById(roadFieldName)
    var address2Element = document.getElementById(address2FieldName)
    var townNameElement = document.getElementById(townNameFieldName)
    var cityNameElement = document.getElementById(cityNameFieldName)
    var stateNameElement = document.getElementById(stateNameFieldName)
    var zipElement = document.getElementById(zipFieldName)
    var countryNameElement = document.getElementById(countryNameFieldName)
    var phoneNameElement = document.getElementById(phoneNameFieldName)
    var webNameElement = document.getElementById(webNameFieldName)
    var emailNameElement = document.getElementById(emailNameFieldName)
    var attentionElement = document.getElementById(attentionFieldName)

    var selectedId =getSelectValueOf(dropDownListName)

    $.ajax({
        type: 'POST',
        dataType: 'json',
        cache: false,
        url: path + "?id=" + selectedId,
        contentType: "application/json",
        success: function (data, textStatus, jqXHR) {
            //Do Stuff 

            houseElement.value = data.HouseNo;
            roadElement.value = data.Road;
            address2Element.value = data.Address2;
            townNameElement.value = data.TownName;
            cityNameElement.value = data.CityName;
            stateNameElement.value = data.StateName;
            zipElement.value = data.Zip;
            countryNameElement.value = data.CountryName;
            phoneNameElement.value = data.Phone;
            webNameElement.value = data.WebAddress;
            emailNameElement.value = data.Email;
            attentionElement.value = data.Attention;
            

        },
        error: function (jqXHR, textStatus, errorThrown) {
            //Do Stuff or Nothing
            var error = textStatus
        }
    });

}



//function getSelectValueOf(dropdownlistName)
//{
//    var e = document.getElementById(dropdownlistName);
//    var thevalue = e.options[e.selectedIndex].value;
//    return thevalue
//}

//function getSelectTextOf(dropdownlistName) {
//    var e = document.getElementById(dropdownlistName);
//    var thetext = e.options[e.selectedIndex].text;
//    return thetext
//}
