$image = {
    urlSubmit: "",
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
    initUploadMultiple: function (strClss) {
        if (typeof strClss == "undefined") strClss = "ImageStream";
        var $this = this;
        $("#PhotoFileBase").change(function () {
            $this.readUpload(this);
        });
        $("#button-upload").click(function () {
            $("#PhotoFileBase").trigger("click");
        });
    }
};

