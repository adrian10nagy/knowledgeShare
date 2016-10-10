$(document).ready(function () {

    $("#downVoteQuestion").on("click", function () {
        var questionId = $("#idQuestion").val();
        AjaxUpdateQuestionVote(questionId, -1);
    });

    $("#upVoteQuestion").on("click", function () {
        var questionId = $("#idQuestion").val();
        AjaxUpdateQuestionVote(questionId, 1);
    });

});

//after click on div
function AjaxUpdateQuestionVote(pQuestionId, vote) {
    $.ajax({
        url: "/Question/AsyncUpdateQuestionVote",
        type: "POST",
        data: { questionId: (pQuestionId), typeId: (vote) },
        error: function (xhr) {
            alert("error processing request")
        },
        success: function (xhr) {
            if (xhr.inserted) {
                var voteScore = $("#voteScore").text();
                var newVote = parseInt(voteScore) + vote;
                updateQuestionVotePV(newVote, pQuestionId);
            }
        }
    })
}

//onload & after Question Vote update
function updateQuestionVotePV(pVotes, pQuestionId) {
    var question = {
        Id: '',
        Vote: '',
    };

    question.Id = pQuestionId;
    question.Vote = pVotes;

    $.ajax({
        url: "/Common/AsyncUpdateQuestionVotePV",
        type: "GET",
        data: question,
        error: function (xhr) {
            alert("error processing request")
        },
        success: function (xhr) {
        }
    })
        .done(function (partialViewResult) {
            $("#votesPV").html(partialViewResult);
        });
}

//apelata din cshtml(AnswerPartialView) onclick=fc()
function AjaxUpdateAnswerVote(pAnswerId, vote) {
    $.ajax({
        url: "/Question/AsyncUpdateAnswerVote",
        type: "POST",
        data: { answerId: (pAnswerId), typeId: (vote) },
        error: function (xhr) {
            alert("error")
        },
        success: function (xhr) {
            if (xhr.inserted) {
                var answerTagDown = document.getElementById("answerDown" + pAnswerId);
                var answerTagUp = document.getElementById("answerUp" + pAnswerId);
                var answerVote = document.getElementById("answerVote" + pAnswerId);

                var voteScore = answerVote.innerHTML;
                var newVote = parseInt(voteScore) + vote;
                answerVote.innerHTML = newVote;

                if (vote > 0) {
                    answerTagDown.className = answerTagDown.className.replace(/\bdownVoted\b/, '');
                    answerTagUp.className += " " + "upVoted";
                } else {
                    answerTagUp.className = answerTagUp.className.replace(/\bupVoted\b/, '');
                    answerTagDown.className += " " + "downVoted";
                }
            }
        }
    })
}
