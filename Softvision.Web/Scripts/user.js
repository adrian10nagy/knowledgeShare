$(document).ready(function () {
    $("#btnUserLogin").on("click", function () {
        var userEmail = $("#loginUserEmail");
        var userPass = $("#loginUserPassword");

        LoginUser(userEmail, userPass);
    });

    
    $("#btnUserRegister").on("click", function () {
        return RegisterUser();
    });

});

function RegisterUser() {

    var errors = [];
    var email = $("#registerUserEmail");
    var password = $("#registerUserPassword");
    var passwordVerify = $("#registerUserPasswordVerify");

    email.css("border", '');
    password.css("border", '');
    passwordVerify.css("border", '');

    if (email.val() == "") {
        email.css("border", "2px solid red");
        errors.push("email");
    }

    if (password.val() == "") {
        password.css("border", "2px solid red");
        errors.push("password");
    }
    if (passwordVerify.val() == "") {
        passwordVerify.css("border", "2px solid red");
        errors.push("passwordVerify");

    }

    if (password.val() != passwordVerify.val()) {
        password.css("border", "2px solid red");
        passwordVerify.css("border", "2px solid red");
        errors.push("passwordVerifyDiffer");
    }


    //result
    if (errors.length > 0) {
        return false;
    }

    return true;

}

function LoginUser(pUserEmail, pUserPass) {
    pUserEmail.css("border", '');
    pUserPass.css("border", '');

    $.ajax({
        url: "/User/AsyncUserLogin",
        type: "POST",
        data: { userEmail: pUserEmail.val(), userPass: pUserPass.val() },
        error: function (xhr) {
            alert("server error while processing the request. Please return shortly")
        },
        success: function (xhr) {
            if (xhr.success == true) {
                window.location.href = "/Home/Index";
            } else {
                pUserEmail.css("border", "2px solid red");
                pUserPass.css("border", "2px solid red");
            }
        }
    })

    .done(function (partialViewResult) {
    });
}