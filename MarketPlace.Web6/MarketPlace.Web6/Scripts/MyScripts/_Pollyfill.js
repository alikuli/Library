//I was getting an error
//Object doesn't support property or method 'assign'
//from strackflow...
//https://stackoverflow.com/questions/35215360/getting-error-object-doesnt-support-property-or-method-assign
//actual site with the code
//https://jsbin.com/pimixel/edit?html,js,output


//////////////////////////////////////////
// Polyfill Removes error: Object doesn't support property or method 'assign'
//////////////////////////////////////////
if (typeof Object.assign != 'function') {
    Object.assign = function (target) {
        'use strict';
        if (target == null) {
            throw new TypeError('Cannot convert undefined or null to object');
        }

        target = Object(target);
        for (var index = 1; index < arguments.length; index++) {
            var source = arguments[index];
            if (source != null) {
                for (var key in source) {
                    if (Object.prototype.hasOwnProperty.call(source, key)) {
                        target[key] = source[key];
                    }
                }
            }
        }
        return target;
    };
}