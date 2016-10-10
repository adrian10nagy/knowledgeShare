function updateAdminQuestions() {
    $.ajax({
        url: "/Admin/AsyncUpdateQuestions",
        type: "GET",
        data: {},
        error: function (xhr) {
            alert("error updating q a")
        },
        success: function (xhr) {
        }
    })
        .done(function (partialViewResult) {
            $("#allAdminQuestions").html(partialViewResult);
        });
}

function removeQuestion(questionId) {
    alert(questionId);
    AjaxUpdateQuestionRemove(questionId);
}

function AjaxUpdateQuestionRemove(pQuestionId) {
    $.ajax({
        url: "/Question/AsyncUpdateQuestionRemove",
        type: "POST",
        data: { QuestionId: (pQuestionId) },
        error: function (xhr) {
            alert("error")
        },
        success: function (xhr) {
        }
    })
    .done(function () {
        updateAdminQuestions();
    });
}


//Update Questions- Answers- Admin
function updateQuestionsAnswersAdmin(answerId) {
    $.ajax({
        url: "/Admin/AsyncUpdateAnswersAdmin",
        type: "GET",
        data: { answerId: (answerId) },
        error: function (xhr) {
            alert("error updateQuestionsAnswersAdmin")
        },
        success: function (xhr) {
        }
    })
    .done(function (partialViewResult) {
        $("#allQuestionAnswers").html(partialViewResult);
    });
}

//popup for Answer REMOVE- popup
function RemoveAnswer(pAnswerId, pAnswerVote) {
    $("#removeAnswerForm").dialog({
        autoOpen: true,
        position: { my: "center", at: "top+450", of: window },
        width: 700,
        resizable: false,
        title: "Remove answer with: " + pAnswerVote + " votes",
        modal: true,
        buttons: {
            "Remove Answer": function () {
                AjaxRemoveAnswer(pAnswerId);
                $(this).dialog("close");
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });
}

//Article answer REMOVE- Admin
function AjaxRemoveAnswer(pAnswerId) {
    $.ajax({
        url: "/Admin/AsyncAnswerRemove",
        type: "POST",
        data: { answerId: (pAnswerId) },
        error: function (xhr) {
            alert("error AjaxRemoveAnswer")
        },
        success: function (xhr) {
            if (xhr.removed) {
                updateQuestionsAnswersAdmin(xhr.questionId)
            }
        }
    })
}