

function copyFromAtoB(varA, varB) {
    moveValueFromAtoB(varA, varB, "HouseNo")
    moveValueFromAtoB(varA, varB, "Road")
    moveValueFromAtoB(varA, varB, "Address2")
    moveValueFromAtoB(varA, varB, "TownName")
    moveValueFromAtoB(varA, varB, "CityName")
    moveValueFromAtoB(varA, varB, "StateName")
    moveValueFromAtoB(varA, varB, "Zip")
    moveValueFromAtoB(varA, varB, "CountryName")
    moveValueFromAtoB(varA, varB, "Phone")
    moveValueFromAtoB(varA, varB, "WebAddress")
    moveValueFromAtoB(varA, varB, "Attention")
    return false;
}

function moveValueFromAtoB(varA,varB, variableName) {
    var idCountryA = varA + "_" + variableName
    var idCountryB = varB + "_" + variableName

    var fldA = document.getElementById(idCountryA);
    var fldB = document.getElementById(idCountryB);
    copyValueFromAToB(fldA, fldB)

}

function copyValueFromAToB(fldA, fldB) {
    fldB.value = fldA.value;
}


