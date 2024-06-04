$(document).ready(function () {
    function initPagination() {
        const listPageItem = $('.pagination>li>a');
        for (var i = 0; i < listPageItem.length; i++) {
            $(listPageItem[i]).addClass('page-link');
        }
    }

    initPagination();

    $(document).on("click", "#button-cancel-order", function () {
        const id = $(this).data("id");
        const check = confirm("Bạn có muốn huỷ đơn hàng không?");
        const buttonEl = $(this);

        console.log("1243");

        if (check) {
            $.ajax({
                url: "/order/cancelorder",
                type: "POST",
                data: { id },
                success: function (res) {
                    if (res.success) {
                        var textStatus = $(`#order-${id} #order-status-text`);

                        textStatus.removeClass("text-primary");
                        textStatus.addClass("text-danger");
                        textStatus.html("Đơn hàng đã huỷ");
                        textStatus.addClass("text-danger");

                        var buttonParentEl = buttonEl.parent();
                        buttonParentEl.html(`<i><i class="bi bi-info-circle"></i>&nbsp;Đơn hàng đã được huỷ thành công!</i>`);
                        buttonParentEl.addClass("text-danger");
                        alert(res.msg);
                    } else {
                        alert(res.msg);
                    }
                },
            })
        }
    });
});