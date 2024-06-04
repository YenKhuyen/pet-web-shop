$(document).ready(function () {
    $(document).on("click", "#button-delete-post", function () {
        const id = $(this).data("id");

        var confirmCheck = confirm("Bạn có chắc chắn muốn xoá bài viết này không?")
        if (confirmCheck) {
            $.ajax({
                url: "/admin/postmanagement/delete",
                type: "POST",
                data: { id: id },
                success: function (res) {
                    if (res.success) {
                        $(`#post_` + id).remove();
                        alert(res.msg);
                    } else {
                        alert(res.msg);
                    }
                }
            })
        }
    });
});