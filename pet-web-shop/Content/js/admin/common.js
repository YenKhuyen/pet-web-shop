$(document).ready(function () {
    function initPagination() {
        const listPageItem = $('.pagination>li>a');
        for (var i = 0; i < listPageItem.length; i++) {
            $(listPageItem[i]).addClass('page-link');
        }
    }

    initPagination();
});