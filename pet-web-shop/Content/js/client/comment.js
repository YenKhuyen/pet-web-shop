$(document).ready(function () {
    function scrollIntoView() {
        const currentHref = window.location.search;

        if (currentHref.includes("?page=")) {
            const commentContainer = $("#comment-container");
            if (commentContainer.length)
                $('html, body').animate({
                    scrollTop: commentContainer.offset().top
                }, 500);
        }
    }

    scrollIntoView();

    $(".comment").each(function () {
        var comment = $(this).find(".comment-text");
        var lineHeight = parseFloat(comment.css('line-height'));
        var maxHeight = lineHeight * 5 - 1;

        if (comment.height() > maxHeight) {
            $(this).find(".expand-button").show();
        }
    });

    $(".expand-button").click(function () {
        var comment = $(this).prev(".comment-text");
        comment.toggleClass("expanded");
        $(this).text(comment.hasClass("expanded") ? "Ẩn bớt..." : "Đọc thêm...");
    });
});