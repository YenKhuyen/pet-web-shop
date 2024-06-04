$(document).ready(function () {
    $("body").on("click", "#btn-delete-product", function () {
        var id = $(this).data("id");
        var confirmCheck = confirm("Bạn có chắn chắn muốn xoá sản phẩm này không? Tất cả các hình ảnh thuộc sản phẩm này cũng sẽ bị xoá theo!");
        if (confirmCheck) {
            $.ajax({
                url: "/admin/productmanagement/delete",
                type: "POST",
                data: { id: id },
                success: function (res) {
                    if (res.success) {
                        $(`#product_${id}`).remove();
                    } else {
                        alert("Có lỗi xảy ra, không thể xoá sản phẩm này!")
                    }
                }
            })
        }
    });
});