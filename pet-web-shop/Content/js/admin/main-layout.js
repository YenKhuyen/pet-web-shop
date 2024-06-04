$(document).ready(function () {
    function activeNav() {
        const currentHref = window.location.pathname
        const currentNav = $('.nav-item.active');
        const newNav = $(`.nav-item:has(.nav-link[href="${currentHref.toLocaleLowerCase()}"])`);

        currentNav.removeClass("active");
        newNav.addClass("active");
    }

    activeNav();

    $(document).on("click", "#button-add-to-cart", function () {
        var id = $(this).data("id");
        var quantity = $(this).data("quantity");

        alert(id + " " + quantity);
    });

    function getUser() {
        $.ajax({
            url: "/Admin/LoginAdmin/CheckAuth",
            type: "GET",
            success: function (res) {
                if (res.isLoggedIn) {
                    $("#main-layout-user-name").html(res.userName);
                    $("#main-layout-user-image").attr("src", res.avatar);
                    $("#profile-dropdown-item").attr("href", `/thong-tin-ca-nhan`);
                } else {
                    window.location.href = "/dang-nhap";
                }
            }
        });
    }

    getUser();
});