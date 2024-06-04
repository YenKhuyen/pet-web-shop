$(document).ready(function () {
    function readURL(input, preview, targetInputUpload) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                preview.attr('src', e.target.result);
                targetInputUpload.val(e.target.result.split("base64,")[1])
            }

            reader.readAsDataURL(input.files[0]);
        }
    }

    $("#inputUploadImage").change(function () {
        readURL(this, $('#imagePreview'), $("#avatar"));
    });

    $("#imagePreview").on("click", function () {
        $("#inputUploadImage").click();
    })
});