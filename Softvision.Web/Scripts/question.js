
$(document).ready(function () {

    $("#btnAddQuestion").on("click", function () {
        addQuestionAnswer();
    });

});

function AjaxUpdateArticleAnswersVote(pAnswerId, vote) {
    $.ajax({
        url: "/Article/AsyncUpdateArticleAnswerVote",
        type: "POST",
        data: { articleId: (pAnswerId), typeId: (vote) },
        error: function (xhr) {
            alert("server error occured when updating answers")
        },
        success: function (xhr) {
        }
    })
}

function updateQuestionAnswers(questionId) {
    $.ajax({
        url: "/Question/AsyncUpdateAnswers",
        type: "GET",
        data: { questionId: (questionId) }
    })
        .done(function (partialViewResult) {
            $("#allQuestionAnswers").html(partialViewResult);
        });
}

function addQuestionAnswer() {
    $("#answerAnonymousName").css("border", '');
    $("#answerAnonymousEmail").css("border", '');
    $("#editorTexarea").css("border", '');

    var currentdate = new Date();
      if ($("#editorTexarea").val() == "") {
        $("#editorTexarea").css("border", "2px solid red");
        return false;
    }

    var answer = {
        IdQuestion: '',
        Body: '',
        AnonymousName: '',
        AnonymousEmail: '',
    };

    answer.IdQuestion = $("#idQuestion").val();
    answer.AnonymousName = $("#answerAnonymousName").val();
    answer.AnonymousEmail = $("#answerAnonymousEmail").val();
    answer.Body = $("#editorTexarea").val();;

    $.ajax({
        type: "POST",
        url: "/Question/InsertAnswer",
        data: answer,
        success: function (data) {
            if (data.annonymousCredentials == false) {
                $("#answerAnonymousName").css("border", "2px solid red");
                $("#answerAnonymousEmail").css("border", "2px solid red");
            } else {
                updateQuestionAnswers(answer.IdQuestion);
                $("#editorTexarea").val('');
                if (data.newUser) {
                    location.reload();
                }
            }
        }
    });
}

function updateQuestions() {
    $.ajax({
        url: "/Question/AsyncUpdateQuestions",
        type: "GET",
        data: { },
        error: function (xhr) {
            alert("server error occuread when updating answers")
        }
    })
        .done(function (partialViewResult) {
            $("#allQuestions").html(partialViewResult);
        });
}


//apelata din cshtml(AnswerPartialView) onclick=fc()
function UpdateAnswerStatus(pAnswerId, pStatusId, pQuestionId) {
	$.ajax({
		url: "/Question/AsyncUpdateAnswerStatus",
		type: "POST",
		data: { answerId: (pAnswerId), statusId: (pStatusId), questionId: (pQuestionId)
        },
		error: function (xhr) {
			alert("error")
		},
		success: function (xhr) {
		    if (xhr.success) {
			    updateQuestionAnswers(pQuestionId);
			} else {
                ///log error
			}
		}
	})
}