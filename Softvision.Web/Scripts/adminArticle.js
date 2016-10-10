function updateAdminArticles() {
    $.ajax({
        url: "/Admin/AsyncUpdateArticles",
        type: "GET",
        data: {},
        error: function (xhr) {
            alert("error updating q a")
        },
        success: function (xhr) {
        }
    })
        .done(function (partialViewResult) {
            $("#allAdminArticles").html(partialViewResult);
        });
}

function removeArticle(articleId) {
    alert(articleId);
    AjaxUpdateArticleRemove(articleId);
}

function AjaxUpdateArticleRemove(pArticleId) {
    $.ajax({
        url: "/Article/AsyncUpdateArticleRemove",
        type: "POST",
        data: { ArticleId: (pArticleId) },
        error: function (xhr) {
            alert("error")
        },
        success: function (xhr) {
            alert("successfuly deleted article"+ pArticleId)
        }
    })
    .done(function () {
        updateAdminArticles();
    });
}

//Update Article Comments - Admin
function updateArticleCommentsAdmin(articleId) {
    $.ajax({
        url: "/Admin/AsyncUpdateCommentsAdmin",
        type: "GET",
        data: { articleId: (articleId) },
        error: function (xhr) {
            alert("error updateArticleCommentsAdmin")
        },
        success: function (xhr) {
        }
    })
    .done(function (partialViewResult) {
        $("#allArticleComments").html(partialViewResult);
    });
}

//popup for Article REMOVE- popup
function RemoveComment(pCommentId, pCommentField) {
    $("#removeArticleCommentForm").dialog({
        autoOpen: true,
        position: { my: "center", at: "top+450", of: window },
        width: 700,
        resizable: false,
        title: "Remove comment: "+ pCommentField,
        modal: true,
        buttons: {
            "Remove Comment": function () {
                AjaxRemoveComment(pCommentId);
                $(this).dialog("close");
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });
}

//Article comment remove- Admin
function AjaxRemoveComment(pCommentId) {
    $.ajax({
        url: "/Admin/AsyncArticleCommentRemove",
        type: "POST",
        data: { commentId: (pCommentId) },
        error: function (xhr) {
            alert("error AjaxRemoveComment")
        },
        success: function (xhr) {
            if (xhr.removed) {
                updateArticleCommentsAdmin(xhr.articleId)
            }
        }
    })
}