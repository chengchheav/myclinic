$setting = {
    urlSubmit: "",
    initSetting: function (strUrl) {
        var $this = this;
        $this.urlSubmit = strUrl;
        var strTxt = ".txt-ctr";
        var KeyType = "1";
        if ($Common.checkDom(strTxt)) {
            $(strTxt).each(function () {
                $(this).bind("blur", function () {
                    var KeyValue = $(this).val();
                    var Id = $(this).data("value");
                    $this.setSubmit(Id, KeyValue, KeyType);
                });
            });
        }

        var strTxt = ".chk-ctr";
            KeyType = "2";
        if ($Common.checkDom(strTxt)) {
            $(strTxt).each(function () {
                $(this).bind("change", function () {
                    var KeyValue = 0;
                    if ($(this).is(':checked')) {
                        KeyValue = $(this).val();
                    }
                    var Id = $(this).data("value");
                    $this.setSubmit(Id, KeyValue, KeyType);
                });
            });
        }
    },

    setSubmit: function (Id, KeyValue, KeyType) {
        var $this = this;
        $.ajax({
            url: $this.urlSubmit,
            async: true,
            cache: false,
            type: "POST",
            data: {
                Id: Id,
                KeyValue: KeyValue,
                KeyType: KeyType,
            },
            dataType: "json",
            error: function () { },
            beforeSend: function () { },
            success: function (responseText) {
                if (responseText.result === "success") {
                    console.log("success");
                } else {
                    console.log("failure");
                }
            }
        });
    }
};

