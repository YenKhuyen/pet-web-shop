$(document).ready(function () {
    $(".comment").each(function () {
        var comment = $(this).find(".comment-text");
        var lineHeight = parseFloat(comment.css('line-height'));
        var maxHeight = lineHeight * 5 - 1;

        if (comment.height() > maxHeight) {
            $(this).find(".expand-button").show();
        }
    });

    $(".expand-button").click(function () {
        var comment = $(this).prev(".comment-text");
        comment.toggleClass("expanded");
        $(this).text(comment.hasClass("expanded") ? "Ẩn bớt..." : "Đọc thêm...");
    });


    $(document).on("click", "#button-delete-comment", function () {
        const id = $(this).data("id");
        console.log(id);

        var confirmCheck = confirm("Bạn có chắc chắn muốn xoá bình luận này không?")
        if (confirmCheck) {
            $.ajax({
                url: "/admin/postmanagement/removecomment",
                type: "POST",
                data: { id: id },
                success: function (res) {
                    if (res.success) {
                        $(`#comment_` + id).remove();
                        alert(res.msg);
                    } else {
                        alert(res.msg);
                    }
                }
            })
        }
    })
})