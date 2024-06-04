$(document).ready(function () {
    CKEDITOR.replace("aboutDescription", {
        customConfig: "/content/ckeditor/config.js",
        extraAllowedContent: "span",
    });
});
