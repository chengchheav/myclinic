$manage = {
    pageNo : 1,
    pageStatus : 1,
    pageSize : 10,
    orderBy : "",
    order: "",
    blnProcess : false,

    strDetailUrl: "",

    /*Search Item By Category*/
    setDetailRecordUrl: function(strUrl){
        this.strDetailUrl = strUrl;
    },
    detailRecordByFullUrl: function (strUrl, newTab) {
    if (typeof newTab == "undefined") newTab = 0;
        if(newTab == 1){
            $Common.setRedirectNewTab(strUrl);
        }else{
            $Common.setRedirectPage(strUrl);
        }
    },
    detailRecordManageRecords: function (strCat, strDate, strStatus, strTicket, intAll, strUrl, newTab) {
        if (typeof strCat == "undefined") strCat = "";
        if (typeof strDate == "undefined") strDate = "";
        if (typeof strStatus == "undefined") strStatus = "";
        if (typeof intAll == "undefined") intAll = "";
        if (typeof strTicket == "undefined") strTicket = "-1";
        if (typeof strUrl == "undefined") strUrl = this.strDetailUrl;
        if (typeof newTab == "undefined") newTab = "1";
        var url         = strUrl + '/?intCat=' + strCat + '&strDate=' + $Common.getTrim(strDate) + '&strStatus=' + $Common.getTrim(strStatus) + '&typeRecord=' + $Common.getTrim(strTicket) + '&isAll=' + intAll + '&orderBy=' + this.orderBy + '&order=' + this.order;
        if(newTab == 1){
            $Common.setRedirectNewTab(url);
        }else{
            $Common.setRedirectPage(url);
        }
    },

    /*Search Item By Category*/
    printManageRecords: function (strUrl, strFrm, strCont) {
        if (typeof strFrm == "undefined") strFrm = "#lst-ctr-srch";
        if (typeof strCont == "undefined") strCont = "#lstContainer";
        var url         = strUrl + '/?orderBy=' + this.orderBy + '&order=' + this.order;
        $(strFrm + " .txt-ctr").each(function(){
            var strName = $(this).attr("id");
            var strVal = $(this).val();
            url = url + "&" + strName + "=" + $Common.getTrim(strVal);
        });

        $(strFrm + " .slc-ctr").each(function(){
            var strName = $(this).attr("id");
            var strVal = $(this).val();
            url = url + "&" + strName + "=" + $Common.getTrim(strVal);
        });

        $(strFrm + " .chk-ctr").each(function(){
            var strName = $(this).attr("id");
            var strVal = $(this).val();
            url = url + "&" + strName + "=" + $Common.getTrim(strVal);
        });
        $Common.setRedirectNewTab(url);
    },

    /*Search Item By Category*/
    printManageRecordsPaging: function (strUrl, strFrm, strCont) {
        if (typeof strFrm == "undefined") strFrm = "#lst-ctr-srch";
        if (typeof strCont == "undefined") strCont = "#lstContainer";
        var url         = strUrl + '/?PageNo=' + this.pageNo + '&pageSize=' + this.pageSize + '&pageStatus=' + this.pageStatus + '&orderBy=' + this.orderBy + '&order=' + this.order;
        $(strFrm + " .txt-ctr").each(function(){
            var strName = $(this).attr("id");
            var strVal = $(this).val();
            url = url + "&" + strName + "=" + $Common.getTrim(strVal);
        });

        $(strFrm + " .slc-ctr").each(function(){
            var strName = $(this).attr("id");
            var strVal = $(this).val();
            url = url + "&" + strName + "=" + $Common.getTrim(strVal);
        });

        $(strFrm + " .chk-ctr").each(function(){
            var strName = $(this).attr("id");
            var strVal = $(this).val();
            url = url + "&" + strName + "=" + $Common.getTrim(strVal);
        });
        $Common.setRedirectNewTab(url);
    },

    /*Search Item By Category*/
    SearchManageRecords: function (strFrm, strCont) {
        if (typeof strFrm == "undefined") strFrm = "#lst-ctr-srch";
        if (typeof strCont == "undefined") strCont = "#lstContainer";
        var pageNo      = 1;
        var pageStatus  = 1;
        var pageSize    = $('#cmbItemPerPage').val();
        var orderBy     = '';
        var order       = '';
        var url         = $(strFrm).data("action") + '/?PageNo=' + pageNo + '&pageSize=' + pageSize + '&pageStatus=' + pageStatus + '&orderBy=' + orderBy + '&order=' + order;
        $(strFrm + " .txt-ctr").each(function(){
            var strName = $(this).attr("id");
            var strVal = $(this).val();
            url = url + "&" + strName + "=" + $Common.getTrim(strVal);
        });

        $(strFrm + " .slc-ctr").each(function(){
            var strName = $(this).attr("id");
            var strVal = $(this).val();
            url = url + "&" + strName + "=" + $Common.getTrim(strVal);
        });

        $(strFrm + " .chk-ctr").each(function(){
            var strName = $(this).attr("id");
            var strVal = $(this).val();
            url = url + "&" + strName + "=" + $Common.getTrim(strVal);
        });
        var strBtn = strFrm + " #search-button";
        this.loadingPage(url, strCont, strBtn);
        this.pageNo = pageNo;
        this.pageStatus = pageStatus;
        this.pageSize = pageSize;
        this.orderBy = orderBy;
        this.order = order;
    },

    /*pagingClickByStatus*/
    pagingManageRecords: function (pageNo, pageStatus,orderBy,order, strFrm, strCont) {
        if (typeof strFrm == "undefined") strFrm = "#lst-ctr-srch";
        if (typeof strCont == "undefined") strCont = "#lstContainer";
        var pageSize = $('#cmbItemPerPage').val();
        //var orderBy = $('#orderBy').val() == null ? '' : $('#orderBy').val();
        //var order = $('#order').val() == null ? '' : $('#order').val();
        var orderBy = orderBy;
        var order = order;
        var url = $(strFrm).data("action") + '/?PageNo=' + pageNo + '&pageSize=' + pageSize + '&pageStatus=' + pageStatus + '&orderBy=' + orderBy + '&order=' + order;
        $(strFrm + " .txt-ctr").each(function(){
            var strName = $(this).attr("id");
            var strVal = $(this).val();
            url = url + "&" + strName + "=" + $Common.getTrim(strVal);
        });

        $(strFrm + " .slc-ctr").each(function(){
            var strName = $(this).attr("id");
            var strVal = $(this).val();
            url = url + "&" + strName + "=" + $Common.getTrim(strVal);
        });

        $(strFrm + " .chk-ctr").each(function(){
            var strName = $(this).attr("id");
            var strVal = $(this).val();
            url = url + "&" + strName + "=" + $Common.getTrim(strVal);
        });
        $common.loadingPage(url, strCont);
        $("#pageNo").val(pageNo);
        $("#pageStatus").val(pageStatus);
        this.pageNo = pageNo;
        this.pageStatus = pageStatus;
        this.pageSize = pageSize;
        this.orderBy = orderBy;
        this.order = order;
    },

    orderManageRecords: function (orderBy, order, strFrm, strCont) {
        if (typeof strFrm == "undefined") strFrm = "#lst-ctr-srch";
        if (typeof strCont == "undefined") strCont = "#lstContainer";
        var pageNo = 1;
        var pageStatus = 1;
        var pageSize = $('#cmbItemPerPage').val();
        var orderBy = orderBy;
        var order = order;
        var url = $(strFrm).data("action") + '/?PageNo=' + pageNo + '&pageSize=' + pageSize + '&pageStatus=' + pageStatus + '&orderBy=' + orderBy + '&order=' + order;
        $(strFrm + " .txt-ctr").each(function(){
            var strName = $(this).attr("id");
            var strVal = $(this).val();
            url = url + "&" + strName + "=" + $Common.getTrim(strVal);
        });

        $(strFrm + " .slc-ctr").each(function(){
            var strName = $(this).attr("id");
            var strVal = $(this).val();
            url = url + "&" + strName + "=" + $Common.getTrim(strVal);
        });

        $(strFrm + " .chk-ctr").each(function(){
            var strName = $(this).attr("id");
            var strVal = $(this).val();
            url = url + "&" + strName + "=" + $Common.getTrim(strVal);
        });
        $common.loadingPage(url, strCont);
        this.pageNo = pageNo;
        this.pageStatus = pageStatus;
        this.pageSize = pageSize;
        this.orderBy = orderBy;
        this.order = order;
    },

    orderManageRecordsWithLink: function (strUrl, orderBy, order, strCont) {
        if (typeof strCont == "undefined") strCont = "#lstContainer";
        $common.loadingPage(strUrl, strCont);
        this.orderBy = orderBy;
        this.order = order; 
    },

    loadingPage: function (url, container, strBtn) {
        var $this = this;
        if($this.blnProcess === false){
            var titleSearch = $(strBtn).val();
            $.ajax({
                url: url,
                async: true,
                cache: false,
                type: "POST",
                dataType: "html",
                beforeSend: function () {
                    $Common.setShowActionOnPage(container);
                    $this.blnProcess = true;
                    $(strBtn).val($msg.waiting);
                },
                success: function (responseText) {
                    $(container).html(responseText);
                    $Common.setHiddenActionOnPage(container);
                },
                complete: function() { 
                    $Common.setHiddenActionOnPage(container);
                    $this.blnProcess = false;
                    $(strBtn).val(titleSearch);
                },
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    $Common.setHiddenActionOnPage(container);
                    $this.blnProcess = false;
                    $(strBtn).val(titleSearch);
                },
            });
        }
    },
};