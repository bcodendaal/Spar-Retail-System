function IsError(object) {
    object.addClass('validation-error');
    object.focus(function () { object.removeClass('validation-error') });
    object.change(function () { object.removeClass('validation-error') });
}

function ValidateElement(obj) {
    var retval = true;

    var value = obj.val();
    var validationType = obj.data("validationtype");

    if (validationType == "number")
        if (value == null || value == '' || value == 'undefined' || isNaN(value) || value == 0) {
            IsError(obj);
            return false;
        }

    return retval;
}