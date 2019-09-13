//https://stackoverflow.com/questions/33947882/pass-model-to-controller-using-jquery-ajax


function postAddress(addressName, theUrl) {
    var valAddressOwnerId = document.getElementById("CustomerId").value;
    var valHouseNo =   myfunction(addressName, "HouseNo")
    var valRoadNo = myfunction(addressName, "Road")
    var valAddress2 = myfunction(addressName, "Address2")
    var valTownName = myfunction(addressName, "TownName")
    var valCityName = myfunction(addressName, "CityName")
    var valStateName = myfunction(addressName, "StateName")
    var valZip = myfunction(addressName, "Zip")
    var valCountryName = myfunction(addressName, "CountryName")
    var valPhone = myfunction(addressName, "Phone")

    var viewModel = {
        id: valAddressOwnerId,
        HouseNo: valHouseNo,
        Road: valRoadNo,
        Address2: valAddress2,
        TownName: valTownName,
        CityName: valCityName,
        StateName: valStateName,
        Zip: valZip,
        CountryName: valCountryName,
        Phone: valPhone,
    }

    var theUrlFixed = theUrl
    var stringifiedData = JSON.stringify(viewModel)

    $.ajax({
        type: 'POST',
        dataType: 'json',
        data: JSON.stringify(viewModel),
        cache: false,
        url: theUrlFixed,
        contentType: "application/json",
        success: function (data, textStatus, jqXHR) {
            //Do Stuff 
            
            $.notify(data.message, "success");

        },
        error: function (jqXHR, textStatus, errorThrown) {
            //Do Stuff or Nothing
            var error = textStatus
        }
    });
    return false;
}

function myfunction(varA, variableName) {
    var idCountryA = varA + "_" + variableName

    var fldA = document.getElementById(idCountryA);
    return fldA.value;
}



