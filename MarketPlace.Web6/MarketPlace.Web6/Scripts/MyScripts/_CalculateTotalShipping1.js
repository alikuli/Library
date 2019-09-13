function getTotalShipping()
{
    //var varQtyShipped = "Quantity_Ship"
    //var varShippedRs = "ShippedRs_Formatted"

    var varQtyOrdered = "Quantity_OrderStr"
    var varSalePrice = "SalePriceStr"
    var varPriceDiff = "Difference_Formatted"
    var varOriginalPrice = "OriginalPrice"
    var varTotal = "OrderedRs_Formatted"
    calculateShipping(varSalePrice, varQtyOrdered, varPriceDiff, varOriginalPrice, varTotal);
}

function calculateShipping(varSalePrice, varQtyOrdered, varPriceDiff, varOriginalPrice, varTotal)
{
    //var qtyShippedElem = document.getElementById(varQtyShipped);
    //var shippedRsElem = document.getElementById(varShippedRs);
    var totalElem = document.getElementById(varTotal);
    var salePriceElem = document.getElementById(varSalePrice);
    var qtyOrderedElem = document.getElementById(varQtyOrdered);
    var priceDiffElem = document.getElementById(varPriceDiff);
    var OrigPriceElem = document.getElementById(varOriginalPrice);

    //var qtyShippedValue = Number(qtyShippedElem.value);
    //var shippedRsValue = Number(shippedRsElem.value);
    var totalValue = Number(totalElem.value);
    var qtyOrderedValue = Number(qtyOrderedElem.value.replace(/,/g, ''));
    var salePriceValue = Number(salePriceElem.value.replace(/,/g,''));
    var origPriceValue = Number(OrigPriceElem.value);

    if (isNaN(totalValue))
        totalValue.value = 0;

    if (isNaN(qtyOrderedValue))
        qtyOrderedValue = 0;

    if (isNaN(salePriceValue))
    {
        salePriceValue = 0;
    }

    //if (isNaN(qtyShippedValue))
    //{
    //    qtyShippedValue = 0;
    //}



    if (isNaN(origPriceValue))
    {
        origPriceValue = 0;

    }

    var calcAmnt = qtyOrderedValue * salePriceValue
    totalElem.value = numberWithCommas(calcAmnt);


    priceDiffElem.value = salePriceValue - origPriceValue;


}

//https://stackoverflow.com/questions/2901102/how-to-print-a-number-with-commas-as-thousands-separators-in-javascript

function numberWithCommas(x) {
    var ans = x.toLocaleString();
    return ans;
}


