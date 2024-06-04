$(document).ready(function () {

    $(document).on("click", ".side_view_container:not(.active)", function () {
        const currentActive = $(".side_view_container.active");
        currentActive.removeClass("active");
        $(this).addClass("active");

        const mainView = $("#main-image-view");
        mainView.attr("src", $(this).children().attr("src"));
    })


    $(document).on("click", "#add-quantity-product, #minus-quantity-product", function () {
        var quantity = $("#quantity_value");
        var checkAdd = $(this).hasClass("plus");
        var buttonAdd = $("#button-add-to-cart");
        var quantityCurrent = $("#quantity_current").data("quantity");
        if (checkAdd) {
            var currValue = parseInt(quantity.val());
            var value = currValue < parseInt(quantityCurrent) ? currValue + 1 : currValue;
            quantity.val(value);
            buttonAdd.data("quantity", value);
        } else {
            var currValue = parseInt(quantity.val());
            var value = currValue > 1 ? currValue - 1 : currValue;
            quantity.val(value);
            buttonAdd.data("quantity", value);
        }
    });

    $(document).on("change", "#quantity_value", function (e) {
        var quantityCurrent = $("#quantity_current").data("quantity");
        if (isNaN($(this).val()) || $(this).val() <= 0) {
            $(this).val(1);
        }

        if (parseInt($(this).val()) > parseInt(quantityCurrent)) {
            $(this).val(quantityCurrent);
        }
    });
});