$(document).ready(function () {
    $('#myModal').on('show.bs.modal', function (e) {

        var category = $(e.relatedTarget).data('category');
        var categoryId = $(e.relatedTarget).data('category-id');
        $(this).find('#categoryName').text(category);
        $(this).find('#id').val(categoryId);
    })
})

