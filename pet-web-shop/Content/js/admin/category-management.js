$(document).ready(function () {
    $("body").on("click", "#btn-delete-cate", function () {
        var id = $(this).data("id");
        var confirmCheck = confirm("Bạn có chắc chắn muốn xoá danh mục này không? Tất cả các sản phẩm thuộc danh mục này cũng sẽ bị xoá theo!")
        if (confirmCheck) {
            $.ajax({
                url: "/admin/categorymanagement/delete",
                type: "POST",
                data: { id: id },
                success: function (res) {
                    if (res.success) {
                        $(`#cate_` + id).remove();
                    } else {
                        alert("Có lỗi xảy ra! Không thể xoá danh mục này!")
                    }
                }
            })
        }
    });
});