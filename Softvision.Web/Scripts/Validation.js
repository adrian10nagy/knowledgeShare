
$(document).ready(function () {
    //register
    $("#btnUserRegister").on("click", function () {
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

        if (password.val() == ""){
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
        }


        //result
        if (errors.length > 0) {
            return false;
        }

        return true;
    });

    //Article
    $("#btnArticleCreate").on("click", function () {
        var errors = [];
        var title = $("#Title");
        var AnonymousName = $("#AnonymousName");
        var AnonymousEmail = $("#AnonymousEmail");
        var articleBody = $("#articleBody");
        
        title.css("border", '');
        AnonymousEmail.css("border", "");
        AnonymousName.css("border", "");
        articleBody.css("border", "");

        if (title.val() == "") {
            title.css("border", "2px solid red");
            errors.push("title");
        }

        if (AnonymousEmail.val() == "") {
            AnonymousEmail.css("border", "2px solid red");
            errors.push("AnonymousEmail");
        }

        if (AnonymousName.val() == "") {
            AnonymousName.css("border", "2px solid red");
            errors.push("AnonymousName");
        }

        if (articleBody.val() == "") {
            articleBody.css("border", "2px solid red");
            errors.push("articleBody");
        }
        //result
        if (errors.length > 0) {
            return false;
        }

        return true;
    });

    //Question
    $("#btnQuestionCreate").on("click", function () {
        var errors = [];
        var title = $("#Title");
        var body = $("#questionBody");
        var AnonymousName = $("#AnonymousName");
        var AnonymousEmail = $("#AnonymousEmail");

        title.css("border", '');
        body.css("border", '');
        AnonymousEmail.css("border", '');
        AnonymousName.css("border", '');

        if (title.val() == "") {
            title.css("border", "2px solid red");
            errors.push("title");
        }

        if (body.val() == "") {
            body.css("border", "2px solid red");
            errors.push("body");
        }

        if (AnonymousEmail.val() == "") {
            AnonymousEmail.css("border", "2px solid red");
            errors.push("AnonymousEmail");
        }

        if (AnonymousName.val() == "") {
            AnonymousName.css("border", "2px solid red");
            errors.push("AnonymousName");
        }

        //result
        if (errors.length > 0) {
            return false;
        }

        return true;
    });


});
