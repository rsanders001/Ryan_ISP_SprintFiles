$(document).ready(function () {
    $("#EditInfo").submit(function (e) {
        var errors = validateForm();
        if (errors == '') {
            return true;
        } else {

            e.preventDefault();
            $('#JSErrors').html(errors);
            alert(errors);
            return false;
        }
    });
    function validateForm() {
        var errorFields = new Array();
        var date = new Date();
        var month = date.getMonth() + 1;
        var day = date.getDate();
        var year = date.getFullYear();
        //Check required fields have something in them
        if ($('#FirstName').val() == '') {
            errorFields.push('First Name cannot be empty');
        }
        if ($('#LastName').val() == '') {
            errorFields.push('Last Name cannot be empty');
        }
        
        if ($('#Email').val() == '') {
            errorFields.push('Email cannot be empty');
        }
        if ($('#Phone').val() == '') {
            errorFields.push('Phone cannot be empty');
        }
        if ($('#ExpectedGradYear').val() < year) {
            errorFields.push('Expected Graduation Year cannot be earlier then this year');
        }
        if ($('#ExpectedGradYear').val() > (year + 4)) {
            errorFields.push('Expected Graduation Year cannot be more then 4 years');
        }
    return errorFields.toString();
    }   
});