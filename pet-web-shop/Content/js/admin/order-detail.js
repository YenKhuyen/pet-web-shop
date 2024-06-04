$(document).ready(function () {
    $(document).on("change", '[name="order-status"]', function () {
        $("#input-status-value").val($(this).val());
    });
});
