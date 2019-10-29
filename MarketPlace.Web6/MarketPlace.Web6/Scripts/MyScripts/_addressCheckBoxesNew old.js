//$(window).on("load", function () {

        //$(function ()
//{
//    $('#itemAnchor').on("click", function (e) {
//        //e.perventDefault();
        
//        var originalPath = $(this).attr("data-path");
//        var allData = getNamesOfCheckBoxes();
//        var theUrl = originalPath + "?MainLocationSelectorClass = " + allData;
//        $.ajax({
//            url: theUrl,
//            type: 'GET',
//            data: allData,
//            success: function (data)
//            {
//                console.log(data);
//            }
//        })
//    });
//})




//https://stackoverflow.com/questions/16091823/get-clicked-element-using-jquery-on-event
    //https://stackoverflow.com/questions/20962471/jquery-function-doesnt-work-after-ajax-call

    $("body").on("click", ".myItem", function (event) {
    //$(".myItem").click(function (event) {
        event.preventDefault();
        const clickedElement = $(event.target);
        const targetElement = clickedElement.closest(".myItem");
        var originalPath = $(targetElement).attr("data-path");
        var allData = getNamesOfCheckBoxes();
        var theUrl = originalPath;
        var dataAddy = JSON.stringify(allData);
        //var dataAddyFixed = "{MainLocationSelectorClass: " + dataAddy + "}";
        $.ajax({
            url: theUrl,
            type: 'GET',
            //processData: false,
            //data: dataAddyFixed,
            //contentType: "application/json; charset=utf-8",
            success: function (data) {
                //console.log(data);
                $('#ajaxResult').html(data);

            }
        })

        return false;
    });

        function getNamesOfCheckBoxes() {
            var checkBoxesArray = getCheckBoxes_Checked();
            var namesOfCheckBoxes = [];
            if (checkBoxesArray) {
                for (var i = 0; i < checkBoxesArray.length; i++) {
                    var name = $(checkBoxesArray[i]).attr("name");
                    namesOfCheckBoxes.push(name);
                }
            }

            return namesOfCheckBoxes;
        }


function anchorOnClick()
{

    //e.perventDefault();
    var originalPath = $('#itemAnchor').attr("data-path");
    var allData = getNamesOfCheckBoxes();
    var theUrl = originalPath + $.param(allData);
    var dataAddy = JSON.stringify(allData);
    var dataAddyFixed = "{MainLocationSelectorClass: " + dataAddy + "}";
    $.ajax({
        url: theUrl,
        type: 'GET',
        processData: false,
        data: dataAddyFixed,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //console.log(data);
            $('#ajaxResult').html(data);

        }
    })

    return false;
}

function getAllCheckedRecordsForPosting() {
    var lstCountries = getAllCountries_UniqueList();
    var mainRecordLst = [];
    if (lstCountries) {
        for (var i = 0; i < lstCountries.length; i++) {
            if (lstCountries[i] !== "")
            { }
            var Country =
                {
                    Name: lstCountries[i],
                    States: getStateList(lstCountries[i])
                };
            mainRecordLst.push(Country);
        }
    }

    var MainLocationSelectorClass =
        {
            Countries: mainRecordLst
        }

    for (var i = 0; i < MainLocationSelectorClass.Countries.length; i++) {
        console.log(MainLocationSelectorClass.Countries[i])
    }
    return MainLocationSelectorClass;
}

function getStateList(countryName) {
    var allStatesForCountry = getAllStatesForCountry_UniqueList(countryName);
    var lstStateRecs = [];
    if (allStatesForCountry) {
        for (var i = 0; i < allStatesForCountry.length; i++) {
            var State =
                {
                    Name: allStatesForCountry[i],
                    Cities: getCityList(countryName, allStatesForCountry[i])
                }
            lstStateRecs.push(State);
        }

    }
    return lstStateRecs;
}


function getCityList(countryName, stateName) {
    var allCitysForCountry = getAllCitiesForCountryState_UniqueList(countryName, stateName);
    var lstCityRecs = [];
    if (allCitysForCountry) {
        for (var i = 0; i < allCitysForCountry.length; i++) {
            var City =
                {
                    Name: allCitysForCountry[i],
                    Towns: getTownList(countryName, stateName, allCitysForCountry[i])
                }
            lstCityRecs.push(City);
        }

    }
    return lstCityRecs;
}


function getTownList(countryName, stateName, cityName) {
    var allTownsForCountry = getAllTownsForCountryAndStateAndCitiesNamed_UniqueList(countryName, stateName, cityName);
    var lstTownRecs = [];
    if (allTownsForCountry) {
        for (var i = 0; i < allTownsForCountry.length; i++) {
            var Town =
                {
                    Name: allTownsForCountry[i]
                }
            lstTownRecs.push(Town);
        }

    }
    return lstTownRecs;
}

function getAllCountries_UniqueList() {
    var allCheckBoxesCoordinates = getAllCheckBoxesCoordinates_Checked();
    var listOfCountries = [];
    for (var i = 0; i < allCheckBoxesCoordinates.length; i++) {
        listOfCountries.push(allCheckBoxesCoordinates[i].countryName)
    }

    var lst = unique(listOfCountries);
    return lst;
}


function getAllStatesForCountry_UniqueList(countryName) {
    var allStatesForCountry = getAllStatesCitiesTownsForCountryNamed(countryName);
    var lstOfStates = [];

    for (var i = 0; i < allStatesForCountry.length; i++) {
        lstOfStates.push(allStatesForCountry[i].stateName)
    }
    var lst = unique(lstOfStates);


    return lst;
}

function getAllCitiesForCountryState_UniqueList(countryName, stateName) {

    var allCitiesForCountryAndState = getAllStatesCitiesTownsForCountryNamed(countryName)
    var lstOfCities = [];

    for (var i = 0; i < allCitiesForCountryAndState.length; i++) {
        lstOfCities.push(allCitiesForCountryAndState[i].cityName)
    }

    var lst = unique(lstOfCities);
    return lst;
}

//gets unique list if towns for country, state, city
function getAllTownsForCountryAndStateAndCitiesNamed_UniqueList(countryName, stateName, cityName) {
    var allTownsForCountryAndStateAndCity = getAllTownsForCountryAndStateAndCitiesNamed(countryName, stateName, cityName)
    var lstOfTowns = [];

    for (var i = 0; i < allTownsForCountryAndStateAndCity.length; i++) {
        lstOfTowns.push(allTownsForCountryAndStateAndCity[i].townName)
    }

    var lst = unique(lstOfTowns);
    return lst;
}

function getAllStatesCitiesTownsForCountryNamed(countryName) {
    //get all coordinates where country, state, town are the same
    var allCheckBoxesCoordinates = getAllCheckBoxesCoordinates_All();
    var townParent = $.grep(allCheckBoxesCoordinates, function (element) {
        var condition =
            element.countryName === countryName;

        return condition;
    });
    return townParent;
}

function getAllCitiesTownsForCountryAndStateNamed(countryName, stateName) {
    //get all coordinates where country, state, town are the same
    var allCheckBoxesCoordinates = getAllCheckBoxesCoordinates_All();
    var townParent = $.grep(allCheckBoxesCoordinates, function (element) {
        var condition =
            element.countryName === countryName &&
            element.stateName === stateName;

        return condition;
    });
    return allStatesCitiesTownsFprCountry;
}

//this returns a list of town record
function getAllTownsForCountryAndStateAndCitiesNamed(countryName, stateName, cityName) {
    //get all coordinates where country, state, town are the same
    var allCheckBoxesCoordinates = getAllCheckBoxesCoordinates_All();
    var allTowns = $.grep(allCheckBoxesCoordinates, function (element) {
        var condition =
            element.countryName === countryName &&
            element.stateName === stateName &&
            element.cityName === cityName;
        return condition;
    });
    listOfTowns = [];
    if (allTowns) {
        for (var i = 0; i < allTowns.length; i++) {
            var Town =
                {
                    Name: allTowns[i].TownName,
                    IsSelected: true
                }
            listOfTowns.push(Town);
        }
    }
    return listOfTowns;
}

function getAllCitiesFor(countryName, stateName) {
    var townParent = $.grep(allCheckBoxesCoordinates, function (element) {
        var condition =
            element.countryName === countryName &&
            element.stateName === stateName;
        return condition;
    });
    listOfCities = [];
    if (allStatesCitiesTownsFprCountry) {
        for (var i = 0; i < length; i++) {
            var city =
               {
                   Name: cityName,
                   IsSelected: true,
                   Towns: getAllTownsForCountryAndStateAndCitiesNamed(countryName, stateName, cityName)
               }
            listOfCities.push(city);
        }
    }
    return listOfCities;



}


function unique(list) {
    var result = [];
    $.each(list, function (i, e) {

        if (e != null && e != "") {
            if ($.inArray(e, result) == -1) result.push(e);
        }
    });

    return result;
}










//-------------------------------------------------------------


////this gets all the children according to the level
function updateCheckBoxChildrenOf(checkBox, level) {
    var checkBoxCoordinate = getCheckBoxCoordinate(checkBox);
    var children = [];
    switch (level) {
        case "country":
            var children = getCountryChildren(checkBox, checkBoxCoordinate);
            break;
        case "state":
            var children = getStateChildren(checkBox, checkBoxCoordinate);
            if (checkBoxCoordinate.isSelected) {
                //select the parent parent country
                if (checkBoxCoordinate.isSelected) {
                    var stateParent = getStateParent(checkBox, checkBoxCoordinate)// select the parent state and parent country
                    $.merge(children, stateParent);
                }
            }
            if (checkBoxCoordinate.isSelected) {
                var stateParent = getCityParent(checkBox, checkBoxCoordinate)// select the parent state and parent country
                var stateGrandParent = getCityGrandParent(checkBox, checkBoxCoordinate)// select the parent state and parent country
                $.merge(stateParent, stateGrandParent);
                $.merge(children, stateParent);
            }

            break;
        case "city":
            var children = getCityChildren(checkBox, checkBoxCoordinate);
            if (checkBoxCoordinate.isSelected) {
                var cityParent = getCityParent(checkBox, checkBoxCoordinate)// select the parent state and parent country
                var cityGrandParent = getCityGrandParent(checkBox, checkBoxCoordinate)// select the parent state and parent country
                $.merge(cityParent, cityGrandParent);
                $.merge(children, cityParent);
            }
            break;

        case "town":
            if (checkBoxCoordinate.isSelected) {
                var townParent = getTownParent(checkBox, checkBoxCoordinate)// select the parent state and parent country
                var townGrandParent = getTownGrandParent(checkBox, checkBoxCoordinate)// select the parent state and parent country
                var townGreatGrandParent = getTownGreatGrandParent(checkBox, checkBoxCoordinate)// select the parent state and parent country
                $.merge(children, townParent);
                $.merge(children, townGrandParent);
                $.merge(children, townGreatGrandParent);
            }
            break;
        default:
            break;

    }

    updateChildrenStatusToParentStatus(children, checkBoxCoordinate.isSelected)
    return children;

}



var getCheckedBoxCoordinates = function () {
    var arrItem = [];
    var commaSeperatedId = "";
    var selectedPlaceArray = getCheckedBoxCoordinatesFor('.myAddressList:checkbox:checked');
    return selectedPlaceArray;
}


var getUnCheckedBoxesCoordinates = function () {
    var arrItem = [];
    var commaSeperatedId = "";
    var selectedPlaceArray = getCheckedBoxCoordinatesFor(getCheckBoxes_UnChecked());

    if (selectedPlaceArray) {
        for (var i = 0; i < selectedPlaceArray.length; i++) {
            console.log(selectedPlaceArray[i]);
        }
    }

    return selectedPlaceArray;
}



function getAllCheckBoxesCoordinates_All() {
    var allCheckBoxes = getCheckedBoxCoordinatesFor(getCheckBoxes_All());
    return allCheckBoxes;
}

function getAllCheckBoxesCoordinates_Checked() {
    var allCheckBoxes_Checked = getCheckedBoxCoordinatesFor(getCheckBoxes_Checked());
    return allCheckBoxes_Checked;
}
function getAllCheckBoxesCoordinates_UnChecked() {
    var allCheckBoxes_Checked = getCheckedBoxCoordinatesFor(getCheckBoxes_UnChecked());
    return allCheckBoxes_Checked;
}


function getCheckBoxesSelector_All() {
    return '.myAddressList:checkbox';
}

function getCheckBoxSelector_UnChecked() {
    return '.myAddressList:checkbox:not(:checked)';
}

function getCheckBoxSelector_Checked() {
    return '.myAddressList:checkbox:checked';
}

function getCheckBoxes_All() {
    return $(getCheckBoxesSelector_All());
}


function getCheckBoxes_Checked() {
    return $(getCheckBoxSelector_Checked());
}
function getCheckBoxes_UnChecked() {
    return $(getCheckBoxSelector_UnChecked());
}



//this gets the coordinate of a single complete record
function getCheckedBoxCoordinatesFor(checkBoxesArray) {
    var checkBoxCoordinateArray = [];
    $.each(checkBoxesArray, function (index, val) {
        var checkBoxCoordinate = getCheckBoxCoordinate(val);
        checkBoxCoordinateArray.push(checkBoxCoordinate);
    });
    return checkBoxCoordinateArray;
}



function getCheckBoxCoordinate(val) {
    var checkBoxId = $(val).attr("Id");
    var isSelected = val.checked;
    var fullName = $(val).attr("data-name");
    //there is a __IsSelected attached to the end.
    //we want to get rid of it
    var array = checkBoxId.split('__');
    var array_Name = fullName.split('__');
    //now just select the Id part
    //get rid of last entry
    array.pop();

    //now we have an array of name_id
    //except the first entry which is container_name_id

    //var arrayOfIds = [];

    ////the first entry is the MainLocationSelectorClass. Lets get rid of it
    //arrayOfIds.shift();

    //now we have the id to work with
    var arrayLength = array.length;
    var countryId = "0";
    var stateId = "0";
    var cityId = "0";
    var townId = "0";
    var countryName = "";
    var stateName = "";
    var cityName = "";
    var townName = "";

    switch (arrayLength) {
        case 1: //country
            countryId = getCountryId(array);
            countryName = getCountryName(array_Name)
            stateId = null;
            cityId = null;
            townId = null;
            break;
        case 2: //state
            countryId = getCountryId(array);
            countryName = getCountryName(array_Name)

            stateId = getStateId(array);
            stateName = getStateName(array_Name)

            cityId = null;
            townId = null;
            break;

        case 3: //city
            countryId = getCountryId(array);
            countryName = getCountryName(array_Name)

            stateId = getStateId(array);
            stateName = getStateName(array_Name)

            cityId = getCityId(array);
            cityName = getCityName(array_Name)

            townId = null;
            break;
        case 4://town
            countryId = getCountryId(array);
            countryName = getCountryName(array_Name)

            stateId = getStateId(array);
            stateName = getStateName(array_Name)

            cityId = getCityId(array);
            cityName = getCityName(array_Name)

            townId = getTownId(array);
            townName = getTownName(array_Name)

            break;
        default:
            break;
    }
    var selectedPlace = { countryId: countryId, countryName: countryName, stateId: stateId, stateName: stateName, cityId: cityId, cityName: cityName, townId: townId, townName: townName, isSelected: isSelected }

    return selectedPlace;

}




function getCountryName(array_Name) {
    var name = array_Name[0];
    return name;

}
function getStateName(array_Name) {
    var name = array_Name[1];
    return name;

}
function getCityName(array_Name) {
    var name = array_Name[2];
    return name;
}
function getTownName(array_Name) {
    var name = array_Name[3];
    return name;
}






function getCountryId(array) {
    var idArray = array[0].split('_');
    var id = idArray[2];
    return id.toString();
}





function getStateId(array) {
    var idArray = array[1].split('_');
    var id = idArray[1];
    return id.toString();
}
function getCityId(array) {
    var idArray = array[2].split('_');
    var id = idArray[1];
    return id.toString();
}

function getTownId(array) {
    var idArray = array[3].split('_');
    var id = idArray[1];
    return id.toString();
}



//this gets all the children according to the level
function getCheckBoxChildrenOf(checkBox, level) {
    var checkBoxCoordinate = getCheckBoxCoordinate(checkBox);
    var children = [];
    switch (level) {
        case "country":
            var children = getCountryChildren(checkBox, checkBoxCoordinate);
            break;
        case "state":
            var children = getStateChildren(checkBox, checkBoxCoordinate);
            if (checkBoxCoordinate.isSelected) {
                //select the parent parent country
                if (checkBoxCoordinate.isSelected) {
                    var stateParent = getStateParent(checkBox, checkBoxCoordinate)// select the parent state and parent country
                    $.merge(children, stateParent);
                }
            }
            if (checkBoxCoordinate.isSelected) {
                var stateParent = getCityParent(checkBox, checkBoxCoordinate)// select the parent state and parent country
                var stateGrandParent = getCityGrandParent(checkBox, checkBoxCoordinate)// select the parent state and parent country
                $.merge(stateParent, stateGrandParent);
                $.merge(children, stateParent);
            }

            break;
        case "city":
            var children = getCityChildren(checkBox, checkBoxCoordinate);
            if (checkBoxCoordinate.isSelected) {
                var cityParent = getCityParent(checkBox, checkBoxCoordinate)// select the parent state and parent country
                var cityGrandParent = getCityGrandParent(checkBox, checkBoxCoordinate)// select the parent state and parent country
                $.merge(cityParent, cityGrandParent);
                $.merge(children, cityParent);
            }
            break;

        case "town":
            if (checkBoxCoordinate.isSelected) {
                var townParent = getTownParent(checkBox, checkBoxCoordinate)// select the parent state and parent country
                var townGrandParent = getTownGrandParent(checkBox, checkBoxCoordinate)// select the parent state and parent country
                var townGreatGrandParent = getTownGreatGrandParent(checkBox, checkBoxCoordinate)// select the parent state and parent country
                $.merge(children, townParent);
                $.merge(children, townGrandParent);
                $.merge(children, townGreatGrandParent);
            }
            break;
        default:
            break;

    }

    updateChildrenStatusToParentStatus(children, checkBoxCoordinate.isSelected)
    return children;

}





function getTownParent(checkBox, coor) {
    //get all coordinates where country, state, town are the same
    var allCheckBoxesCoordinates = getAllCheckBoxesCoordinates_All();
    var townParent = $.grep(allCheckBoxesCoordinates, function (element) {
        var condition =
            element.countryId === coor.countryId &&
            element.stateId === coor.stateId &&
            element.cityId === coor.cityId &&
            element.townId === null;

        return condition;
    });
    return townParent;
}

function getTownGrandParent(checkBox, coor) {
    //get all coordinates where country, state, town are the same
    var allCheckBoxesCoordinates = getAllCheckBoxesCoordinates_All();
    var townGrandParent = $.grep(allCheckBoxesCoordinates, function (element) {
        var condition =
            element.countryId === coor.countryId &&
            element.stateId === coor.stateId &&
            element.cityId === null &&
            element.townId === null;

        return condition;
    });
    return townGrandParent
}

function getTownGreatGrandParent(checkBox, coor) {
    //get all coordinates where country, state, town are the same
    var allCheckBoxesCoordinates = getAllCheckBoxesCoordinates_All();
    var townGrandParent = $.grep(allCheckBoxesCoordinates, function (element) {
        var condition =
            element.countryId === coor.countryId &&
            element.stateId === null &&
            element.cityId === null &&
            element.townId === null;

        return condition;
    });
    return townGrandParent
}




function getStateParent(checkBox, coor) {
    //get all coordinates where country, state, city are the same
    var allCheckBoxesCoordinates = getAllCheckBoxesCoordinates_All();
    var stateParent = $.grep(allCheckBoxesCoordinates, function (element) {
        var condition =
            element.countryId === coor.countryId &&
            element.stateId === null &&
            element.cityId === null &&
            element.townId === null;

        return condition;
    });
    return stateParent;
}

function getCityParent(checkBox, coor) {
    //get all coordinates where country, state, city are the same
    var allCheckBoxesCoordinates = getAllCheckBoxesCoordinates_All();
    var cityParent = $.grep(allCheckBoxesCoordinates, function (element) {
        var condition =
            element.countryId === coor.countryId &&
            element.stateId === coor.stateId &&
            element.cityId === null &&
            element.townId === null;

        return condition;
    });
    return cityParent;
}

function getCityGrandParent(checkBox, coor) {
    //get all coordinates where country, state, city are the same
    var allCheckBoxesCoordinates = getAllCheckBoxesCoordinates_All();
    var cityGrandParent = $.grep(allCheckBoxesCoordinates, function (element) {
        var condition =
            element.countryId === coor.countryId &&
            element.stateId === null &&
            element.cityId === null &&
            element.townId === null;

        return condition;
    });
    return cityGrandParent
}

function getCityChildren(checkBox, coor) {
    //get all coordinates where country, state, city are the same
    var allCheckBoxesCoordinates = getAllCheckBoxesCoordinates_All();
    var citiesChildren = $.grep(allCheckBoxesCoordinates, function (element) {
        var condition =
            element.countryId === coor.countryId &&
            element.stateId === coor.stateId &&
            element.cityId === coor.cityId &&
            element.townId !== null;

        return condition;
    });

    return citiesChildren;
}

function getStateChildren(checkBox, coor) {
    //get all coordinates where country, state, city are the same
    var allCheckBoxesCoordinates = getAllCheckBoxesCoordinates_All();
    var children = $.grep(allCheckBoxesCoordinates, function (element) {
        var condition =
            element.countryId === coor.countryId &&
            element.stateId === coor.stateId;
        //element.cityId !== null &&
        //element.townId !== null;

        return condition;
    });

    return children;
}
function getCountryChildren(checkBox, coor) {
    //get all coordinates where country, state, city are the same
    var allCheckBoxesCoordinates = getAllCheckBoxesCoordinates_All();
    var children = $.grep(allCheckBoxesCoordinates, function (element) {
        var condition =
            element.countryId === coor.countryId;
        //element.stateId !== null &&
        //element.cityId !== null &&
        //element.townId !== null;

        return condition;
    });

    return children;
}

//This will update all the children to the status of the parent.
function updateChildrenStatusToParentStatus(childrenOfCheckBox, isSelected) {

    //the checkBox is the checkbox object coming in
    $.each(childrenOfCheckBox, function (index, coor) {
        //convert it back to an Id form
        var checkBoxId = convertToCheckBoxId(coor);
        var checkBox = $('#' + checkBoxId);

        if (checkBox.is(":checked")) {
            if (isSelected) {
                //dont do anything
            }
            else {
                //https://stackoverflow.com/questions/7389303/programmatically-uncheck-jquery-ui-checkbox-div
                $(checkBox).prop('checked', false);
            }

        }
        else {
            if (isSelected) {
                $(checkBox).prop('checked', true);
            }
            else {

                //dont do anything
            }

        }



    })
}



function convertToCheckBoxId(coor) {
    let country = coor.countryId;
    let state = coor.stateId;
    let city = coor.cityId;
    let town = coor.townId;

    let id = "";
    if (town) {
        id = "MainLocationSelectorClass_Countries_" + country + "__States_" + state + "__Cities_" + city + "__Towns_" + town + "__IsSelected";
    }
    else {
        if (city) {
            id = "MainLocationSelectorClass_Countries_" + country + "__States_" + state + "__Cities_" + city + "__IsSelected";
        }
        else {
            if (state) {
                id = "MainLocationSelectorClass_Countries_" + country + "__States_" + state + "__IsSelected";
            }
            else {
                id = "MainLocationSelectorClass_Countries_" + country + "__IsSelected";

            }

        }
    }

    return id;
}

//function getCheckedBoxCoordinates  () {
//    var arrItem = [];
//    var commaSeperatedId = "";
//    var selectedPlaceArray = getCheckedBoxCoordinatesFor('.myAddressList:checkbox:checked');
//    return selectedPlaceArray;
//}
//function getUnCheckedBoxesCoordinates () {
//    var arrItem = [];
//    var commaSeperatedId = "";
//    var selectedPlaceArray = getCheckedBoxCoordinatesFor(getCheckBoxes_UnChecked());

//    if (selectedPlaceArray) {
//        for (var i = 0; i < selectedPlaceArray.length; i++) {
//            console.log(selectedPlaceArray[i]);
//        }
//    }

//    return selectedPlaceArray;
//}

//function getAllCheckBoxesCoordinatesCoordinates () {
//    var arrItem = [];
//    var commaSeperatedId = "";
//    var selectedPlaceArray = getAllCheckBoxesCoordinates();
//    return selectedPlaceArray;
//}

//function getAllCheckBoxesCoordinates() {
//    var allCheckBoxes = getCheckedBoxCoordinatesFor(getCheckBoxes_All());
//    return allCheckBoxes;
//}

//function getAllCheckBoxesCoordinates_Checked() {
//    var allCheckBoxes_Checked = getCheckedBoxCoordinatesFor(getCheckBoxes_Checked());
//    return allCheckBoxes_Checked;
//}
//function getAllCheckBoxesCoordinates_UnChecked() {
//    var allCheckBoxes_Checked = getCheckedBoxCoordinatesFor(getCheckBoxes_UnChecked());
//    return allCheckBoxes_Checked;
//}


//function getCheckBoxesSelector_All() {
//    return '.myAddressList:checkbox';
//}

//function getCheckBoxSelector_UnChecked() {
//    return '.myAddressList:checkbox:not(:checked)';
//}

//function getCheckBoxSelector_Checked() {
//    return '.myAddressList:checkbox:checked';
//}

//function getCheckBoxes_All() {
//    return $(getCheckBoxesSelector_All());
//}
//function getCheckBoxes_Checked() {
//    return $(getCheckBoxSelector_Checked());
//}
//function getCheckBoxes_UnChecked() {
//    return $(getCheckBoxSelector_UnChecked());
//}
//function getCheckedBoxCoordinatesFor(checkBoxesArray) {
//    var checkBoxCoordinateArray = [];
//    $.each(checkBoxesArray, function (index, val) {
//        var checkBoxCoordinate = getCheckBoxCoordinate(val);
//        checkBoxCoordinateArray.push(checkBoxCoordinate);
//    });
//    return checkBoxCoordinateArray;
//}
//function getCheckBoxCoordinate(val) {
//    var checkBoxId = $(val).attr("Id");
//    var isSelected = val.checked;
//    var fullName =  $(val).attr("data-name");
//    //there is a __IsSelected attached to the end.
//    //we want to get rid of it
//    var array = checkBoxId.split('__');
//    var array_Name = fullName.split('__');
//    //now just select the Id part
//    //get rid of last entry
//    array.pop();

//    //now we have an array of name_id
//    //except the first entry which is container_name_id

//    //var arrayOfIds = [];

//    ////the first entry is the MainLocationSelectorClass. Lets get rid of it
//    //arrayOfIds.shift();

//    //now we have the id to work with
//    var arrayLength = array.length;
//    var countryId = "0";
//    var stateId = "0";
//    var cityId = "0";
//    var townId = "0";
//    var countryName = "";
//    var stateName = "";
//    var cityName = "";
//    var townName = "";

//    switch (arrayLength) {
//        case 1: //country
//            countryId = getCountryId(array);
//            countryName = getCountryName(array_Name)
//            stateId = null;
//            cityId = null;
//            townId = null;
//            break;
//        case 2: //state
//            countryId = getCountryId(array);
//            countryName = getCountryName(array_Name)

//            stateId = getStateId(array);
//            stateName = getStateName(array_Name)

//            cityId = null;
//            townId = null;
//            break;

//        case 3: //city
//            countryId = getCountryId(array);
//            countryName = getCountryName(array_Name)

//            stateId = getStateId(array);
//            stateName = getStateName(array_Name)

//            cityId = getCityId(array);
//            cityName = getCityName(array_Name)

//            townId = null;
//            break;
//        case 4://town
//            countryId = getCountryId(array);
//            countryName = getCountryName(array_Name)

//            stateId = getStateId(array);
//            stateName = getStateName(array_Name)

//            cityId = getCityId(array);
//            cityName = getCityName(array_Name)

//            townId = getTownId(array);
//            townName = getTownName(array_Name)

//            break;
//        default:
//            break;
//    }
//    var selectedPlace = { countryId: countryId, countryName: countryName, stateId: stateId, stateName:stateName, cityId: cityId, cityName: cityName, townId: townId, townName: townName, isSelected: isSelected }

//    return selectedPlace;

//}



//function getCountryName(array_Name) {
//    var name = array_Name[0];
//    return name;

//}
//function getStateName(array_Name) {
//    var name = array_Name[1];
//    return name;

//}
//function getCityName(array_Name) {
//    var name = array_Name[2];
//    return name;
//}
//function getTownName(array_Name) {
//    var name = array_Name[3];
//    return name;
//}






//function getCountryId(array) {
//    var idArray = array[0].split('_');
//    var id = idArray[2];
//    return id.toString();
//}





//function getStateId(array) {
//    var idArray = array[1].split('_');
//    var id = idArray[1];
//    return id.toString();
//}
//function getCityId(array) {
//    var idArray = array[2].split('_');
//    var id = idArray[1];
//    return id.toString();
//}

//function getTownId(array) {
//    var idArray = array[3].split('_');
//    var id = idArray[1];
//    return id.toString();
//}



//function getTownParent(checkBox, coor) {
//    //get all coordinates where country, state, town are the same
//    var allCheckBoxesCoordinates = getAllCheckBoxesCoordinatesCoordinates();
//    var townParent = $.grep(allCheckBoxesCoordinates, function (element) {
//        var condition =
//            element.countryId === coor.countryId &&
//            element.stateId === coor.stateId &&
//            element.cityId === coor.cityId &&
//            element.townId === null;

//        return condition;
//    });
//    return townParent;
//}

//function getTownGrandParent(checkBox, coor) {
//    //get all coordinates where country, state, town are the same
//    var allCheckBoxesCoordinates = getAllCheckBoxesCoordinatesCoordinates();
//    var townGrandParent = $.grep(allCheckBoxesCoordinates, function (element) {
//        var condition =
//            element.countryId === coor.countryId &&
//            element.stateId === coor.stateId &&
//            element.cityId === null &&
//            element.townId === null;

//        return condition;
//    });
//    return townGrandParent
//}

//function getTownGreatGrandParent(checkBox, coor) {
//    //get all coordinates where country, state, town are the same
//    var allCheckBoxesCoordinates = getAllCheckBoxesCoordinatesCoordinates();
//    var townGrandParent = $.grep(allCheckBoxesCoordinates, function (element) {
//        var condition =
//            element.countryId === coor.countryId &&
//            element.stateId === null &&
//            element.cityId === null &&
//            element.townId === null;

//        return condition;
//    });
//    return townGrandParent
//}




//function getStateParent(checkBox, coor) {
//    //get all coordinates where country, state, city are the same
//    var allCheckBoxesCoordinates = getAllCheckBoxesCoordinatesCoordinates();
//    var stateParent = $.grep(allCheckBoxesCoordinates, function (element) {
//        var condition =
//            element.countryId === coor.countryId &&
//            element.stateId === null &&
//            element.cityId === null &&
//            element.townId === null;

//        return condition;
//    });
//    return stateParent;
//}

//function getCityParent(checkBox, coor) {
//    //get all coordinates where country, state, city are the same
//    var allCheckBoxesCoordinates = getAllCheckBoxesCoordinatesCoordinates();
//    var cityParent = $.grep(allCheckBoxesCoordinates, function (element) {
//        var condition =
//            element.countryId === coor.countryId &&
//            element.stateId === coor.stateId &&
//            element.cityId === null &&
//            element.townId === null;

//        return condition;
//    });
//    return cityParent;
//}

//function getCityGrandParent(checkBox, coor) {
//    //get all coordinates where country, state, city are the same
//    var allCheckBoxesCoordinates = getAllCheckBoxesCoordinatesCoordinates();
//    var cityGrandParent = $.grep(allCheckBoxesCoordinates, function (element) {
//        var condition =
//            element.countryId === coor.countryId &&
//            element.stateId === null &&
//            element.cityId === null &&
//            element.townId === null;

//        return condition;
//    });
//    return cityGrandParent
//}

//function getCityChildren(checkBox, coor) {
//    //get all coordinates where country, state, city are the same
//    var allCheckBoxesCoordinates = getAllCheckBoxesCoordinatesCoordinates();
//    var citiesChildren = $.grep(allCheckBoxesCoordinates, function (element) {
//        var condition =
//            element.countryId === coor.countryId &&
//            element.stateId === coor.stateId &&
//            element.cityId === coor.cityId &&
//            element.townId !== null;

//        return condition;
//    });

//    return citiesChildren;
//}

//function getStateChildren(checkBox, coor) {
//    //get all coordinates where country, state, city are the same
//    var allCheckBoxesCoordinates = getAllCheckBoxesCoordinatesCoordinates();
//    var children = $.grep(allCheckBoxesCoordinates, function (element) {
//        var condition =
//            element.countryId === coor.countryId &&
//            element.stateId === coor.stateId;
//        //element.cityId !== null &&
//        //element.townId !== null;

//        return condition;
//    });

//    return children;
//}
//function getCountryChildren(checkBox, coor) {
//    //get all coordinates where country, state, city are the same
//    var allCheckBoxesCoordinates = getAllCheckBoxesCoordinatesCoordinates();
//    var children = $.grep(allCheckBoxesCoordinates, function (element) {
//        var condition =
//            element.countryId === coor.countryId;
//        //element.stateId !== null &&
//        //element.cityId !== null &&
//        //element.townId !== null;

//        return condition;
//    });

//    return children;
//}

////This will update all the children to the status of the parent.
//function updateChildrenStatusToParentStatus(childrenOfCheckBox, isSelected) {

//    //the checkBox is the checkbox object coming in
//    $.each(childrenOfCheckBox, function (index, coor) {
//        //convert it back to an Id form
//        var checkBoxId = convertToCheckBoxId(coor);
//        var checkBox = $('#' + checkBoxId);

//        if (checkBox.is(":checked")) {
//            if (isSelected) {
//                //dont do anything
//            }
//            else {
//                //https://stackoverflow.com/questions/7389303/programmatically-uncheck-jquery-ui-checkbox-div
//                $(checkBox).prop('checked', false);
//            }

//        }
//        else {
//            if (isSelected) {
//                $(checkBox).prop('checked', true);
//            }
//            else {

//                //dont do anything
//            }

//        }



//    })
//}



//function convertToCheckBoxId(coor) {
//    let country = coor.countryId;
//    let state = coor.stateId;
//    let city = coor.cityId;
//    let town = coor.townId;

//    let id = "";
//    if (town) {
//        id = "MainLocationSelectorClass_Countries_" + country + "__States_" + state + "__Cities_" + city + "__Towns_" + town + "__IsSelected";
//    }
//    else {
//        if (city) {
//            id = "MainLocationSelectorClass_Countries_" + country + "__States_" + state + "__Cities_" + city + "__IsSelected";
//        }
//        else {
//            if (state) {
//                id = "MainLocationSelectorClass_Countries_" + country + "__States_" + state + "__IsSelected";
//            }
//            else {
//                id = "MainLocationSelectorClass_Countries_" + country + "__IsSelected";

//            }

//        }
//    }

//    return id;
//}
//    });
