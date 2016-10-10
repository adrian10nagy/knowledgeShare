
$(document).ready(function () {
    $("#upVoteArticle").on("click", function () {
        var articleId = $("#idArticle").val();
        AjaxUpdateArticleVote(articleId, 1);
    });

    $("#downVoteArticle").on("click", function () {
        var articleId = $("#idArticle").val();
        AjaxUpdateArticleVote(articleId, -1);
    });
});

//updates MAIN page Article vote
function AjaxUpdateArticleVote(pArticleId, vote) {
    $.ajax({
        url: "/Article/AsyncUpdateArticleVote",
        type: "POST",
        data: { articleId: (pArticleId), typeId: (vote) },
        error: function (xhr) {
            alert("error processing request")
        },
        success: function (xhr) {
            if (xhr.inserted) {
                var voteScore = $("#voteScore").text();
                var newVote = parseInt(voteScore) + vote;
                updateArticleVotePV(newVote, pArticleId);
            }
        }
    })
}

//returns PV for Article VOTE
function updateArticleVotePV(pVotes, pArticleId) {
    var article = {
        Id: '',
        Vote: '',
    };

    article.Id = pArticleId;
    article.Vote = pVotes;

    $.ajax({
        url: "/Common/AsyncUpdateArticleVotePV",
        type: "GET",
        data: article,
        error: function (xhr) {
            alert("error processing request")
        }
    })
        .done(function (partialViewResult) {
            $("#votesPV").html(partialViewResult);
        });
}

//Article comment
function AjaxUpdateCommentVote(pCommentId, vote) {
    $.ajax({
        url: "/Article/AsyncUpdateCommentVote",
        type: "POST",
        data: { commentId: (pCommentId), typeId: (vote) },
        error: function (xhr) {
            alert("error")
        },
        success: function (xhr) {
            if (xhr.inserted) {
                debugger;
                var commentTagDown = document.getElementById("commentDown" + pCommentId);
                var commentTagUp = document.getElementById("commentUp" + pCommentId);
                var comemntVote = document.getElementById("commentVote" + pCommentId);

                var voteScore = comemntVote.innerHTML;
                var newVote = parseInt(voteScore) + vote;
                comemntVote.innerHTML = newVote;

                if (vote > 0) {
                    commentTagDown.className = commentTagDown.className.replace(/\bdownVoted\b/, '');
                    commentTagUp.className += " " + "upVoted";
                } else {
                    commentTagUp.className = commentTagUp.className.replace(/\bupVoted\b/, '');
                    commentTagDown.className += " " + "downVoted";
                }
            }
        }
    })
}