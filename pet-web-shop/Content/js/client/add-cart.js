$(document).ready(function () {
    $(document).on("click", "#button-add-to-cart", function () {
        var id = $(this).data("id");
        var quantity = $(this).data("quantity");

        $.ajax({
            url: "/Cart/Create",
            type: "POST",
            data: { product_id: id, quantity: quantity },
            success: function (res) {
                if (res.success) {
                    countCart();
                    alert(res.msg);
                } else {
                    if (res.msg) {
                        alert(res.msg);
                    } else {
                        window.location.href = "/dang-nhap";
                    }
                }
            }
        });
    });
});