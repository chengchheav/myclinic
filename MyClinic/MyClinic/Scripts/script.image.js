$image = {
    urlSubmit: "",
    strWidth: "450",
    strDefaultImage: "",
    readLogo: function (input, strClss) {
        var KeyType = 3;
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                var KeyValue = e.target.result;
                var Id = $("." + strClss).data("value");
                $("." + strClss).find("img").attr('src', KeyValue);
                $("." + strClss + " input[type=hidden]").val(KeyValue);
                $setting.setSubmit(Id, KeyValue, KeyType);
            }
            reader.readAsDataURL(input.files[0]);
        }
    },
    initLogo: function (strUrl, strClss) {
        if (typeof strClss == "undefined") strClss = "ImageStream";
        var $this = this;
        $("." + strClss + " input[type=file]").change(function () {
            $this.readLogo(this, strClss);
        });
        $("." + strClss + " #trigger").click(function () {
            $("." + strClss + " input[type=file]").trigger("click");
        });
    },

    setDefaultImg: function (image) {
        image.onerror = "";
        image.src = $image.strDefaultImage;
        return true;
    },

    readUpload: function (input, strClss) {
        //console.log(input);
        //console.log(input.files[0]);
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                var strSource = e.target.result;
                $("div#imgPhoto").find("img").attr('src', e.target.result);
                $("div#imgPhoto").find("img").css("width", "100%");
                $("div#imgPhoto").find("img").css("height", "100%");
                $("div#imgPhoto").find("img").css("object-fit", "cover");
                $("#ImageStream").val(strSource);
            }
            reader.readAsDataURL(input.files[0]);
        }
    },
    initUpload: function (strClss) {
        if (typeof strClss == "undefined") strClss = "ImageStream";
        var $this = this;
        $("#PhotoFileBase").change(function () {
            $this.readUpload(this);
        });
        $("#imgPhoto").click(function () {
            $("#PhotoFileBase").trigger("click");
        });
    },
    initRemoveImage: function (isRemoveCreatePhoto, fileName) {
        var url = host + '/PhotoUpload/DeleteCreatePhoto';
        if (isRemoveCreatePhoto == 0) {
            url = host + '/PhotoUpload/DeleteEditPhoto';
        }
        var data = new FormData();
        data.append("fileName", fileName);
        $.ajax({
            type: "POST",
            url: url,
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                $image.imgRenderer(result, isRemoveCreatePhoto);
            },
            error: function (xhr, status, p3, p4) {
                var err = "Error " + " " + status + " " + p3 + " " + p4;
                if (xhr.responseText && xhr.responseText[0] == "{")
                    err = JSON.parse(xhr.responseText).Message;
                console.log(err);
            }
        });
    },
    imgRenderer: function (imgList, isRemoveCreatePhoto) {
        var imgBox = '';
        if (imgList != '') {
            var arrImg = imgList.split("|");
            var msgText = $translator.translate("Are you wish to delete this image ?", lang);
            for (i = 0; i < arrImg.length; i++) {
                var imgUrl = arrImg[i];
                if (isRemoveCreatePhoto == 0) {
                    imgBox += '<div id="imgPhoto" class="bx-pt" style="margin:20px;"><span onclick="$image.confirmDeleteImage(\'\',\'\',\'' + msgText + '\',' + isRemoveCreatePhoto + ',\'' + imgUrl + '\');"></span><img src="' + imgUrl + '" style="width:100%;height:100%;object-fit:cover;"  /></div>';
                }
                else {
                    imgBox += '<div id="imgPhoto" class="bx-pt" style="margin:20px;"><span onclick="$image.initRemoveImage(' + isRemoveCreatePhoto + ',\'' + imgUrl + '\');"></span><img src="' + imgUrl + '" style="width:100%;height:100%;object-fit:cover;"  /></div>';
                }
            }
        }
        $('#dis-images').html(imgBox);
    },


    /*========================== Confirm Delete Dialog ==========================*/
    confirmDeleteImage: function (strType, strTitle, strMsg, isRemoveCreatePhoto, fileName) {
        if (typeof strType == "undefined") strType = "delete";
        if (typeof strTitle == "undefined") strTitle = "Message";
        if (typeof strMsg == "undefined") strMsg = "Are you sure to delete this record?";
        var objDialog = $("#dialog-confirm").dialog({
            modal: true,
            title: strTitle,
            closeOnEscape: false,
            resizable: false,
            autoOpen: true,
            width: $image.strWidth,
            buttons: {
                Yes: function () {
                    $image.initRemoveImage(isRemoveCreatePhoto, fileName);
                    $(this).dialog("close");
                },
                No: function () {
                    $(this).dialog("close");
                }
            },
            open: function (event, ui) {
                $('.ui-dialog-buttonset button:first-child').html($msg.yes);
                $('.ui-dialog-buttonset button:last-child').html($msg.no);
                $(event.target).dialog('widget')
                    .css({ position: 'fixed' })
                    .position({ my: 'center', at: 'center', of: window });
                $(".ui-dialog-titlebar-close", ui.dialog || ui).hide();
                if (this.title == "") {
                    $(".ui-dialog-title").html($msg.message);
                }
            }
        });
        objDialog.html(strMsg);
    }

};

