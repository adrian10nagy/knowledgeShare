
function updateSubCategory(pCategoryID) {

    $.ajax({
        url: "/Common/AsyncUpdateSubCategory",
        type: "GET",
        data: { categoryId: (pCategoryID) },
        error: function (xhr) {
            alert("server error :(")
        },
        success: function (xhr) {
        }
    })
     .done(function (partialViewResult) {
         $("#subCategoryDiv").html(partialViewResult);
     });
}
