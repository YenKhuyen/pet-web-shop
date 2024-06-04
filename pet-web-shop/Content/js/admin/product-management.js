$(document).ready(function () {
    var listImageURL = [];
    let checkedIndex = 1;
    const checkActionCreate = $("#cardProduct").data("action") == "create";
    CKEDITOR.replace("textDescription", {
        customConfig: "/content/ckeditor/config.js",
        extraAllowedContent: "span",
    });

    function BrowseServer() {
        var finder = new CKFinder();
        finder.selectActionFunction = function (fileUrl) {
            listImageURL.push(fileUrl);
            if (checkActionCreate) {
                addImageWhenCreate(listImageURL);
            } else {
                addImageWhenEdit(fileUrl);
            }
        }
        finder.popup();
    }

    $("#butonUPloadImage").on("click", function () {
        BrowseServer();
    })

    function addImageWhenCreate(listUrl) {
        var elTable = ""
        for (var i = 0; i < listUrl.length; i++) {
            elTable += `<tr id="rowId_${i + 1}">
                         <td class="text-center">${i + 1}</td>
                         <td class="text-center"><img width="100" src="${listUrl[i]} "/><input type="hidden" value="${listUrl[i]}" id="imagesInput" name="Images"/></td>
                         <td class="text-center"><input type="radio" value="${i + 1}" name="DefaulImage" id="defaultImage" ${i + 1 === checkedIndex || (checkedIndex === 0 && i + 1 === 1) ? "checked" : ""} /></td>
                         <td class="text-center"><a href="#" data-id="${i + 1}" class="btn btn-sm btn-outline-danger buton-delete-image">Remove</a></td>
                       </tr>`
        };

        $("#tableImage").html(elTable);
    }

    $(document).on("change", "#defaultImage", function () {
        if (checkActionCreate) {
            checkedIndex = $(this).val();
        }
    })

    $(document).on("click", ".buton-delete-image", function () {
        if (checkActionCreate) {
            deleteImageWhenCreate($(this).data("id"));
        } else {
            deleteImageWhenEdit($(this).data("id"));
        }
    })

    function deleteImageWhenCreate(id) {
        const rowEl = $(`#rowId_${id}`);
        const confirmCheck = confirm("Bạn có chắc chắn muốn xoá hình ảnh này không?");
        if (confirmCheck == true) {
            listImageURL.splice(parseInt(id) - 1, 1);
            rowEl.fadeTo("fast", 1, function () {
                rowEl.remove();
            })

            addImageWhenCreate(listImageURL);
        }
    }

    function deleteImageWhenEdit(id) {
        const product_id = $("#id").val();
        const confirmCheck = confirm("Bạn có chắc chắn muốn xoá hình ảnh này không?");
        if (confirmCheck == true) {
            $.ajax({
                url: "/admin/imagemanagement/delete",
                type: "POST",
                data: { id: id, product_id: product_id },
                success: function (res) {
                    if (res.success) {
                        renderListImage(res.new_list);
                    } else {
                        alert("Có lỗi xảy ra, không thể xoá hình ảnh!")
                    }
                }
            })
        }
    }

    $(document).on("click", "#defaultImage", function (e) {
        if (!checkActionCreate) {
            e.preventDefault();
            e.stopPropagation();
            const confirmCheck = confirm("Bạn có chắc muốn đặt hình ảnh này làm ảnh chính?");
            if (confirmCheck == true) {
                const product_id = $("#id").val();
                const id = $(this).val();
                $.ajax({
                    url: "/admin/imagemanagement/defaultimage",
                    type: "POST",
                    data: { id: id, product_id: product_id },
                    success: function (res) {
                        if (res.success) {
                            renderListImage(res.new_list);
                        } else {
                            alert("Có lỗi xảy ra, khôgn thể đặt làm hình ảnh chính!");
                        }
                    }
                });
            }
        }
    })

    function addImageWhenEdit(fileUrl) {
        if (!fileUrl) return;
        const product_id = $("#id").val();
        $.ajax({
            url: "/admin/imagemanagement/create",
            type: "POST",
            data: { product_id: product_id, url_image: fileUrl },
            success: function (res) {
                if (res.success) {
                    renderListImage(res.new_list);
                } else {
                    alert("có lỗi xảy ra!");
                }
            }
        });
    }

    function renderListImage(listImage) {
        var elTable = ""
        if (listImage.length == 0) {
            elTable += "<tr class='even'>< td colspan='4'>Không có bản ghi!</td> </tr >"
        }
        for (var i = 0; i < listImage.length; i++) {
            elTable += `<tr id="rowId_${listImage[i].id}">
                         <td class="text-center">${i + 1}</td>
                         <td class="text-center"><img width="100" src="${listImage[i].image} "/><input type="hidden" value="${listImage[i].image}" id="imagesInput" name="Images"/></td>
                         <td class="text-center"><input type="radio" value="${listImage[i].id}" name="DefaulImage" id="defaultImage" ${listImage[i].isDefault ? "checked" : ""} /></td>
                         <td class="text-center"><a href="#" data-id="${listImage[i].id}" class="btn btn-sm btn-outline-danger buton-delete-image">Remove</a></td>
                       </tr>`
        };

        $("#tableImage").html(elTable);
    }
});