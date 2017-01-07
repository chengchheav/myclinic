$managelog = {
    pageNo : 1,
    pageStatus : 1,
    pageSize : 10,
    orderBy : "",
    order: "",

    strDetailUrl: "",

    /*Search Item By Category*/
    setDetailRecordUrl: function(strUrl){
        this.strDetailUrl = strUrl;
    },
    detailRecordManageRecords: function (strCat, strDate, strStatus, intAll, strUrl, newTab) {
        if (typeof strCat == "undefined") strCat = "";
        if (typeof strDate == "undefined") strDate = "";
        if (typeof strStatus == "undefined") strStatus = "";
        if (typeof intAll == "undefined") intAll = "";
        if (typeof strUrl == "undefined") strUrl = this.strDetailUrl;
        if (typeof newTab == "undefined") newTab = "1";
        var url         = strUrl + '/?intCat=' + strCat + '&strDate=' + $Common.getTrim(strDate) + '&strStatus=' + $Common.getTrim(strStatus) + '&isAll=' + intAll + '&orderBy=' + this.orderBy + '&order=' + this.order;
        if(newTab == 1){
            $Common.setRedirectNewTab(url);
        }else{
            $Common.setRedirectPage(url);
        }
    },
    /*Search Log By Date*/
    SearchLogByDate: function (strFrm, strCont) {
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
        $common.loadingSearchPage(url, strCont);
        this.pageNo = pageNo;
        this.pageStatus = pageStatus;
        this.pageSize = pageSize;
        this.orderBy = orderBy;
        this.order = order;
    },

    /*paging Search by Date*/
    pagingManageRecords: function (pageNo, pageStatus,orderBy,order, strFrm, strCont) {
        if (typeof strFrm == "undefined") strFrm = "#lst-ctr-srch";
        if (typeof strCont == "undefined") strCont = "#lstContainer";
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
        $("#pageNo").val(pageNo);
        $("#pageStatus").val(pageStatus);
        this.pageNo = pageNo;
        this.pageStatus = pageStatus;
        this.pageSize = pageSize;
        this.orderBy = orderBy;
        this.order = order;
    },

    /*For Item Per Page Change Log By Date */
    itemPerPageChangeLogByDate: function () {
        var pageNo = 1;
        var pageStatus = 1;
        if (typeof strFrm == "undefined") strFrm = "#lst-ctr-srch";
        if (typeof strCont == "undefined") strCont = "#lstContainer";
        var pageSize = $('#cmbItemPerPage').val();
        var orderBy = $('#orderBy').val() == null ? '' : $('#orderBy').val();
        var order = $('#order').val() == null ? '' : $('#order').val();
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

    /*For Order By Click  Log By Date */
    orderByClickLogByDate: function (orderBy, order) {
        var pageNo = 1;
        var pageStatus = 1;
        if (typeof strFrm == "undefined") strFrm = "#lst-ctr-srch";
        if (typeof strCont == "undefined") strCont = "#lstContainer";
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

    /*Search In Log User By Date*/
    SearchInLogUserByDate: function (strFrm, strCont) {
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
        $common.loadingSearchPage(url, strCont);
        this.pageNo = pageNo;
        this.pageStatus = pageStatus;
        this.pageSize = pageSize;
        this.orderBy = orderBy;
        this.order = order;
    },
};