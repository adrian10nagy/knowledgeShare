
$(document).ready(function () {

    $('#editorTexarea').keyup(function (e) {
        var currentPosition = this.selectionStart;
        var editorTextarea = $('#editorTexarea');
        editorAddField(editorTextarea);
    });

    $('#editorTexarea').keydown(function (e) {
        var code = e.keyCode || e.which;
        var keycode = (e.keyCode ? e.keyCode : e.which);

        if (keycode == 9) {
            e.preventDefault();
            var start = $(this).get(0).selectionStart;
            var end = $(this).get(0).selectionEnd;

            // set textarea value to: text before caret + tab + text after caret
            $(this).val($(this).val().substring(0, start)
						+ "\t"
						+ $(this).val().substring(end));

            // put caret at right position again
            $(this).get(0).selectionStart =
			$(this).get(0).selectionEnd = start + 1;
        }

        var editorTextarea = $('#editorTexarea');

        editorAddField(editorTextarea);
    });

});


function editorAddField(editorTextarea) {
    var editorTextareaText = editorTextarea.val().replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot;');;

    var previewDiv = $('#previewDiv');

    $.ajax({
        url: "/Common/AsyncUpdateTrueEditorView",
        type: "GET",
        data: { parseText: (editorTextareaText) }
    })
		 .done(function (partialViewResult) {
		     previewDiv.html(partialViewResult);
		 });
}
