var articleFieldNr = 0;

$(document).ready(function () {

    $("#btnAddComment").on("click", function () {
        addArticleComment();
    });

});

function addArticleComment() {
    $("#commentBodyText").css("border", '');
    $("#AnonymousName").css("border", '');
    $("#AnonymousEmail").css("border", '');

    var currentdate = new Date();

        if ($("#commentBodyText").val() == "") {
        $("#commentBodyText").css("border", "2px solid red");
        return false;
    } 

    var comment = {
        IdArticle: '',
        TextField: '',
        AnonymousName: '',
        AnonymousEmail: '',
    };

    comment.IdArticle = $("#idArticle").val();
    comment.AnonymousName = $("#AnonymousName").val()+" ";
    comment.AnonymousEmail = $("#AnonymousEmail").val()+" ";
    comment.TextField = $("#commentBodyText").val();

    $.ajax({
        type: "POST",
        url: "/Article/InsertComment",
        data: comment,
        success: function (data) {
            if (data.annonymousCredentials == false) {
                $("#AnonymousName").css("border", "2px solid red");
                $("#AnonymousEmail").css("border", "2px solid red");
            } else {
                updateArticleComments(comment.IdArticle);
                $("#commentBodyText").val('');
                if (data.newUser) {
                    location.reload();
                }
            }
        },
        error: function (xhr) {
            alert("server error occured while inserting comment.");
        }
    });
}

function updateArticleComments(articleId) {
    $.ajax({
        url: "/Article/AsyncUpdateComments",
        type: "GET",
        data: { articleId: (articleId) },
        error: function (xhr) {
            alert("server error occred when updating comments")
        }
    })
    .done(function (partialViewResult) {
        $("#allArticleComments").html(partialViewResult);
    });
}

function updateArticles() {
    $.ajax({
        url: "/Article/AsyncUpdateArticles",
        type: "GET",
        data: {}
    })
        .done(function (partialViewResult) {
            $("#allArticles").html(partialViewResult);
        });
}

function insertText(text) {
    var textarea = document.getElementById("editorTexarea");
    var val = textarea.value;
    var start = textarea.selectionStart, end = textarea.selectionEnd;

    if (start == end) {
        textarea.value = val.slice(0, start) + text + val.slice(end);
        textarea.focus();
        var caretPos = start + text.length;
        textarea.setSelectionRange(start + 2, caretPos - 2);
    } else {
        var selectedtext = val.slice(start, end);
        var textToAdd = text.slice(0, 2);
        var caretPos = start + selectedtext.length;

        textarea.value = val.slice(0, start) + textToAdd + selectedtext + textToAdd + val.slice(end);
        textarea.focus();
        textarea.setSelectionRange(start + 2, caretPos + 2);
    }

    var editorTextarea = $('#editorTexarea');
    editorAddField(editorTextarea);

}
