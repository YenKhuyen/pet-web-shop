$(document).ready(function () {
    $(document).on("click", "#btn-udpate-quantity", function () {
        const id = $(this).data("id");
        const quantity = $(`#quantity_${id}`).val();
        $.ajax({
            url: "/Cart/Edit",
            type: "POST",
            data: { id, quantity },
            success: function (res) {
                $("#total-price-cart").html(res.total + " VND");
                alert(res.msg);
            },
        });
    });

    $(document).on("click", "#btn-delete-cart", function () {
        const id = $(this).data("id");
        var check = confirm("Bạn có muốn xoá sản phẩm này khỏi giỏ hàng?");
        if (check)
            $.ajax({
                url: "/Cart/Delete",
                type: "POST",
                data: { id },
                success: function (res) {
                    let strEl = "";
                    const list = res.list_cart
                    for (var i = 0; i < list.length; i++) {
                        strEl += `<tr id="${list[i].id}">
                                <td>${i + 1}</td>
                                <td><img class="img-fluid mb-4" src="${list[i].image}" alt="" style="height: 50px; width: 50px; object-fit: contain;"></td>
                                <td>
                                    <b>
                                        ${list[i].title}
                                    </b>
                                </td>
                                <td>
                                    <div class="form-group">
                                        <input class="form-control" value="${list[i].quantity}" type="number" id="quantity_${list[i].id}" min="1" max="${list[i].product_quantity}" width="200" />
                                    </div>
                                </td>
                                <td>${list[i].price} VND</td>
                                <td>
                                    <a class="btn btn-sm btn-outline-primary" href="#" data-id="${list[i].id}" id="btn-udpate-quantity">Update</a>
                                    <a class="btn btn-sm btn-outline-danger" href="#" data-id="${list[i].id}" id="btn-delete-cart">Delete</a>
                                </td>
                            </tr>`;
                    };

                    $("#body-cart-table").html(strEl);
                    $("#total-price-cart").html(res.total + " VND");
                    countCart();
                    alert(res.msg);
                },
            });
    });
});