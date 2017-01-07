$print = {
    onlyPrint: function (strUrl, strId) {
        var $this = this;
        if (typeof strId == "undefined") strId = "print-button";
        var strActId = "#" + strId;
        if ($Common.checkDom(strActId)) {
            $this.printPageShow(strUrl);
            return false;
        }
    },

    multiPrint: function (strUrl) {
        var $this = this;
        $this.printPageShow(strUrl);
    },

    printPageShow: function (strUrl) {
        newwindow = window.open(strUrl, 'name', 'height=800,width=1000');
        if (window.focus) {
            newwindow.focus()
        }
    },

    printFullPageShow: function (strUrl) {
        var h = $(window).height();
        var w = $(window).width(); 
        newwindow = window.open(strUrl, 'name', 'height='+ h +',width=' + w);
        if (window.focus) {
            newwindow.focus()
        }
    },

    detailRecordManageRecords: function (strCat, strDate, strStatus, strTicket, intAll, strUrl) {
        if (typeof strCat == "undefined") strCat = "";
        if (typeof strDate == "undefined") strDate = "";
        if (typeof strStatus == "undefined") strStatus = "";
        if (typeof intAll == "undefined") intAll = "";
        if (typeof strTicket == "undefined") strTicket = "-1";
        var url = strUrl + '/?intCat=' + strCat + '&strDate=' + $Common.getTrim(strDate) + '&strStatus=' + $Common.getTrim(strStatus) + '&typeRecord=' + $Common.getTrim(strTicket) + '&isAll=' + intAll + '&orderBy=' + this.orderBy + '&order=' + this.order;
        this.printFullPageShow(url);
    },

    /*Search Item By Category*/
    printManageRecords: function (strUrl, strFrm, strCont) {
        if (typeof strFrm == "undefined") strFrm = "#lst-ctr-srch";
        if (typeof strCont == "undefined") strCont = "#lstContainer";
        var url = strUrl + '/?orderBy=' + this.orderBy + '&order=' + this.order;
        var $this = this;
        $(strFrm + " .txt-ctr").each(function () {
            var strName = $(this).attr("id");
            var strVal = $(this).val();
            url = url + "&" + strName + "=" + $Common.getTrim(strVal);
        });

        $(strFrm + " .slc-ctr").each(function () {
            var strName = $(this).attr("id");
            var strVal = $(this).val();
            url = url + "&" + strName + "=" + $Common.getTrim(strVal);
        });

        $(strFrm + " .chk-ctr").each(function () {
            var strName = $(this).attr("id");
            var strVal = $(this).val();
            url = url + "&" + strName + "=" + $Common.getTrim(strVal);
        });
        $this.printFullPageShow(url);
    },

    loadPrint: function () {
        window.print();
        setTimeout(function () { window.close(); }, 100);
    }
};