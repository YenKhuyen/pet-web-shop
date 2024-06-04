function countCart() {
    $.ajax({
        url: "/Cart/CountCart",
        type: "GET",
        success: function (res) {
            if (res.success === true) {
                $("#count-cart").html(res.count);
            }
        }
    });
}

function getUser() {
    $.ajax({
        url: "/Admin/LoginAdmin/CheckAuth",
        type: "GET",
        success: function (res) {
            if (res.isLoggedIn) {
                $("#dropdown-user").html(`
                    <a href="/thong-tin-ca-nhan" class="dropdown-item text-one-line"><i class="bi bi-person-circle"></i> &nbsp; ${res.userName}</a>
                            <hr />
                            <a href="/don-mua" class="dropdown-item"><i class="bi bi-cart-check"></i> &nbsp; Đơn mua</a>
                            <a href="/logout" class="dropdown-item"><i class="bi bi-box-arrow-left"></i> &nbsp; Đăng xuất</a>
                `);

                $("#user-avatar").attr("src", res.avatar);

            } else {
                $("#dropdown-user").html(`
                    <a href="/dang-nhap" class="dropdown-item"><i class="bi bi-box-arrow-in-right"></i> &nbsp; Đăng nhập</a>
                `);
            }
        }
    });
}

$(document).ready(function () {
    countCart();
    getUser();
});