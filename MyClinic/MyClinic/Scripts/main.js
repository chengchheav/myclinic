$(function () {
    $common.initPaginator();
});

// TODO: Check exist the html element
$.fn.isExist = function () {
    if ($(this).length > 0)
        return true;
    return false;
}


var $common = {
    blnProcess : false,
    initPaginator: function () {
        if ($('#btnAdd').isExist())
            $common.initAddBotton();
        if ($('#cmbItemPerPage').isExist())
            $common.initItemPerPageSelection();
        //$(".ajax").colorbox();
        //$(".inline").colorbox({ inline: true, width: "40%" });
    },
    initAddBotton: function () {
        $("#btnAdd").click(function () {

        });
    },
    initItemPerPageSelection: function () {
        $("#cmbItemPerPage").change(function () {
            var url = $common.getAjindexUrl();
            //$common.loadingPage(url);
        });
    },
    loadingPage: function (url, container) {
        var $this = this;
        if($this.blnProcess === false){
            $.ajax({
                url: url,
                async: true,
                cache: false,
                type: "POST",
                dataType: "html",
                beforeSend: function () {
                    $Common.setShowActionOnPage(container);
                    $this.blnProcess = true;
                },
                success: function (responseText) {
                    $(container).html(responseText);
                    $Common.setHiddenActionOnPage(container);
                },
                complete: function() { 
                    $Common.setHiddenActionOnPage(container);
                    $this.blnProcess = false;
                },
                error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    $Common.setHiddenActionOnPage(container);
                    $this.blnProcess = false;
                },
            });
        }
    },
    loadingSearchPage: function (url, container, strBtn) {
        var $this = this;
        if (typeof strBtn == "undefined") strBtn = "div.editor #search-button";
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
    loadingPageNoWaitingSign: function (url, container) {
        $.ajax({
            url: url,
            async: true,
            cache: false,
            type: "POST",
            dataType: "html",
            beforeSend: function () {
                //$(container).html($common.getLoading());
            },
            success: function (responseText) {
                $(container).html(responseText);
            }
        });
    },
    submitPage: function (url, container, form_id) {
        var formData = $(form_id).serializeArray();
        $.ajax({
            url: url,
            async: true,
            cache: false,
            type: "POST",
            dataType: "html",
            data: formData,
            beforeSend: function () {
                //$(container).html($common.getLoading());
            },
            success: function (responseText) {
                $(container).html(responseText);
                if (responseText.indexOf('successfully') > -1) {
                    var timer = $.timer(function () {
                        timer.stop();
                        $common.closeColorboxDialog();
                        $common.searchClick();
                        $("#content_area").load(location.href + " #content_area");

                    });
                    timer.set({ time: 800, autostart: true });
                }
            }
        });
    },
    submitBusinessCycle: function (url, container, form_id) {
        var formData = $(form_id).serializeArray();
        $.ajax({
            url: url,
            async: true,
            cache: false,
            type: "POST",
            dataType: "html",
            data: formData,
            beforeSend: function () {
                //$(container).html($common.getLoading());
            },
            success: function (responseText) {
                $(container).html(responseText);
                if (responseText.indexOf('successfully') > -1) {
                    var timer = $.timer(function () {
                        timer.stop();
                        $common.closeColorboxDialog();
                        $("#content_area").load(location.href + " #content_area");

                    });
                    timer.set({ time: 800, autostart: true });
                }
            }
        });
    },
    submitNormalPage: function (url, container, form_id) {
        var formData = $(form_id).serializeArray();
        $.ajax({
            url: url,
            async: true,
            cache: false,
            type: "POST",
            dataType: "html",
            data: formData,
            beforeSend: function () {
                //$(container).html($common.getLoading());
            },
            success: function (responseText) {
                $(container).html(responseText);
                $common.toggleReload();
            }
        });
    },
    submitNormalPageCallingFunction: function (url, container, form_id, calling_function) {
        var formData = $(form_id).serializeArray();
        $.ajax({
            url: url,
            async: true,
            cache: false,
            type: "POST",
            dataType: "html",
            data: formData,
            beforeSend: function () {

            },
            success: function (responseText) {
                $(container).html(responseText);
                eval(calling_function);
            }
        });
    },
    submitPageNoContainer: function (url, form_id) {
        var formData = $(form_id).serializeArray();
        $.ajax({
            url: url,
            async: true,
            cache: false,
            type: "POST",
            dataType: "html",
            data: formData,
            beforeSend: function () {

            },
            success: function (responseText) {

            }
        });
    },
    submitPageCallingFunction: function (url, container, form_id, calling_function) {
        var formData = $(form_id).serializeArray();
        $.ajax({
            url: url,
            async: true,
            cache: false,
            type: "POST",
            dataType: "html",
            data: formData,
            beforeSend: function () {

            },
            success: function (responseText) {
                $(container).html(responseText);
                if (responseText.indexOf('successfully') > -1) {
                    var timer = $.timer(function () {
                        timer.stop();
                        $common.closeColorboxDialog();
                        eval(calling_function);
                    });
                    timer.set({ time: 800, autostart: true });
                }

            }
        });
    },
    getLoading: function () {
        var loading = '<div id="my_loading" class="my_loading"><img src="/Content/img/loading.gif"></div>';
        return loading;
    },
    getAjindexUrl: function () {
        return host + '/' + controller + '/Ajindex';
    },
    /*get URL For Search Pawn Name By Status*/
    getAjindexSearchNameUrl: function () {
        return host + '/' + controller + '/AjindexSearchName';
    },
    /*get URL For Search Item By Category*/
    getAjindexSearchItemByCategoryUrl: function () {
        return host + '/' + controller + '/AjindexSeachByCategory';
    },
    getCreateUrl: function () {
        return host + '/' + controller + '/Create';
    },
    pagingClick: function (pageNo, pageStatus,orderBy,order) {
        var pageSize = $('#cmbItemPerPage').val();
        var orderBy = $('#orderBy').val() == null ? orderBy : $('#orderBy').val();
        var order = $('#order').val() == null ? order : $('#order').val();
        var searchBy = $('#cmbSearchBy').val();
        var keywork = $('#searchKeyWord').val();
        var cycle = $('#cycleDate').val();
        var url = $common.getAjindexUrl() + '/?PageNo=' + pageNo +
        '&pageSize=' + pageSize +
        '&pageStatus=' + pageStatus +
        '&orderBy=' + orderBy +
        '&order=' + order +
        '&searchBy=' + searchBy +
        '&keyWord=' + keywork;

        var status = $('input:radio[name=status]:checked').val();
        if (status != null) {
            url += "&status=" + status;
        }
        url += "&cycle=" + cycle;
        $common.loadingPage(url, '#lstContainer');
    },
    /*pagingClickByStatus*/
    pagingClickByStatus: function (pageNo, pageStatus,orderBy,order) {
        var statusId = $("#cboStatus").val();
        var pageSize = $('#cmbItemPerPage').val();
        var orderBy = $('#orderBy').val() == null ? orderBy : $('#orderBy').val();
        var order = $('#order').val() == null ? order : $('#order').val();
        var searchBy = $('#cmbSearchBy').val();
        var keywork = $('#searchKeyWord').val();
        var cycle = $('#cycleDate').val();
        var url = $common.getAjindexSearchNameUrl() + '/?PageNo=' + pageNo +
        '&pageSize=' + pageSize +
        '&pageStatus=' + pageStatus +
        '&orderBy=' + orderBy +
        '&order=' + order +
        '&searchBy=' + searchBy +
        '&keyWord=' + keywork +
        '&statusId=' + statusId;

        var status = $('input:radio[name=status]:checked').val();
        if (status != null) {
            url += "&status=" + status;
        }
        url += "&cycle=" + cycle;
        $common.loadingPage(url, '#lstContainer');
    },
    /*pagingClickByCategory*/
    pagingClickByCategory: function (pageNo, pageStatus,orderBy,order) {
        var catId = $("#ItemCategory").val();
        var pageSize = $('#cmbItemPerPage').val();
        var orderBy = $('#orderBy').val() == null ? orderBy : $('#orderBy').val();
        var order = $('#order').val() == null ? order : $('#order').val();
        var searchBy = $('#cmbSearchBy').val();
        var keywork = $('#searchKeyWord').val();
        var cycle = $('#cycleDate').val();
        var url = $common.getAjindexSearchItemByCategoryUrl() + '/?PageNo=' + pageNo +
        '&pageSize=' + pageSize +
        '&pageStatus=' + pageStatus +
        '&orderBy=' + orderBy +
        '&order=' + order +
        '&searchBy=' + searchBy +
        '&keyWord=' + keywork +
        '&catId=' + catId;

        var status = $('input:radio[name=status]:checked').val();
        if (status != null) {
            url += "&status=" + status;
        }
        url += "&cycle=" + cycle;
        $common.loadingPage(url, '#lstContainer');
    },

    itemPerPageChange: function (orderBy,order) {
        var pageNo = 1;
        var pageStatus = 1;
        var pageSize = $('#cmbItemPerPage').val();
        var orderBy = orderBy;
        var order = order;
        var searchBy = $('#cmbSearchBy').val();
        var keywork = $('#searchKeyWord').val();
        var cycle = $('#cycleDate').val();
        var url = $common.getAjindexUrl() + '/?PageNo=' + pageNo +
        '&pageSize=' + pageSize +
        '&pageStatus=' + pageStatus +
        '&orderBy=' + orderBy +
        '&order=' + order +
        '&searchBy=' + searchBy +
        '&keyWord=' + keywork;

        var status = $('input:radio[name=status]:checked').val();
        if (status != null) {
            url += "&status=" + status;
        }
        url += "&cycle=" + cycle;
        $common.loadingPage(url, '#lstContainer');
    },

    /*ItemPerPageChangeBystatus*/
    itemPerPageChangeByStatus: function (orderBy,order) {
        var pageNo = 1;
        var pageStatus = 1;
        var pageSize = $('#cmbItemPerPage').val();
        var orderBy = orderBy;
        var order = order;
        var statusId = $("#cboStatus").val();
        var searchBy = $('#cmbSearchBy').val();
        var keywork = $('#searchKeyWord').val();
        var cycle = $('#cycleDate').val();
        var url = $common.getAjindexSearchNameUrl() + '/?PageNo=' + pageNo +
        '&pageSize=' + pageSize +
        '&pageStatus=' + pageStatus +
        '&orderBy=' + orderBy +
        '&order=' + order +
        '&searchBy=' + searchBy +
        '&keyWord=' + keywork +
        '&statusId=' + statusId;

        var status = $('input:radio[name=status]:checked').val();
        if (status != null) {
            url += "&status=" + status;
        }
        url += "&cycle=" + cycle;
        $common.loadingPage(url, '#lstContainer');
    },
    /*ItemPerPageChangeByCategory*/
    itemPerPageChangeByCategory: function (orderBy,order) {
        var pageNo = 1;
        var pageStatus = 1;
        var pageSize = $('#cmbItemPerPage').val();
        var orderBy = orderBy;
        var order = order;
        var catId = $("#ItemCategory").val();
        var searchBy = $('#cmbSearchBy').val();
        var keywork = $('#searchKeyWord').val();
        var cycle = $('#cycleDate').val();
        var url = $common.getAjindexSearchItemByCategoryUrl() + '/?PageNo=' + pageNo +
        '&pageSize=' + pageSize +
        '&pageStatus=' + pageStatus +
        '&orderBy=' + orderBy +
        '&order=' + order +
        '&searchBy=' + searchBy +
        '&keyWord=' + keywork +
        '&catId=' + catId;

        var status = $('input:radio[name=status]:checked').val();
        if (status != null) {
            url += "&status=" + status;
        }
        url += "&cycle=" + cycle;
        $common.loadingPage(url, '#lstContainer');
    },
    orderByClick: function (orderBy, order) {
        var pageNo = 1;
        var pageStatus = 1;
        var pageSize = $('#cmbItemPerPage').val();
        var orderBy = orderBy;
        var order = order;
        var searchBy = $('#cmbSearchBy').val();
        var keywork = $('#searchKeyWord').val();
        var cycle = $('#cycleDate').val();
        var url = $common.getAjindexUrl() + '/?PageNo=' + pageNo +
        '&pageSize=' + pageSize +
        '&pageStatus=' + pageStatus +
        '&orderBy=' + orderBy +
        '&order=' + order +
        '&searchBy=' + searchBy +
        '&keyWord=' + keywork;

        var status = $('input:radio[name=status]:checked').val();
        if (status != null) {
            url += "&status=" + status;
        }
        url += "&cycle=" + cycle;
        $common.loadingPage(url, '#lstContainer');
    },

    /*orderByClick_ByStatus*/
    orderByClick_ByStatus: function (orderBy, order) {
        var pageNo = 1;
        var pageStatus = 1;
        var pageSize = $('#cmbItemPerPage').val();
        var orderBy = orderBy;
        var order = order;
        var statusId = $("#cboStatus").val();
        var searchBy = $('#cmbSearchBy').val();
        var keywork = $('#searchKeyWord').val();
        var cycle = $('#cycleDate').val();
        var url = $common.getAjindexSearchNameUrl() + '/?PageNo=' + pageNo +
        '&pageSize=' + pageSize +
        '&pageStatus=' + pageStatus +
        '&orderBy=' + orderBy +
        '&order=' + order +
        '&searchBy=' + searchBy +
        '&keyWord=' + keywork +
        '&statusId=' + statusId;

        var status = $('input:radio[name=status]:checked').val();
        if (status != null) {
            url += "&status=" + status;
        }
        url += "&cycle=" + cycle;
        $common.loadingPage(url, '#lstContainer');
    },

    /*orderByClick_ByCategory*/
    orderByClick_ByCategory: function (orderBy, order) {
        var pageNo = 1;
        var pageStatus = 1;
        var pageSize = $('#cmbItemPerPage').val();
        var orderBy = orderBy;
        var order = order;
        var catId = $("#ItemCategory").val();
        var searchBy = $('#cmbSearchBy').val();
        var keywork = $('#searchKeyWord').val();
        var cycle = $('#cycleDate').val();
        var url = $common.getAjindexSearchItemByCategoryUrl() + '/?PageNo=' + pageNo +
        '&pageSize=' + pageSize +
        '&pageStatus=' + pageStatus +
        '&orderBy=' + orderBy +
        '&order=' + order +
        '&searchBy=' + searchBy +
        '&keyWord=' + keywork +
        '&catId=' + catId;

        var status = $('input:radio[name=status]:checked').val();
        if (status != null) {
            url += "&status=" + status;
        }
        url += "&cycle=" + cycle;
        $common.loadingPage(url, '#lstContainer');
    },
    searchClick: function () {
        var pageNo = 1;
        var pageStatus = 1;
        var pageSize = $('#cmbItemPerPage').val();
        var orderBy = '';
        var order = '';
        var searchBy = $('#cmbSearchBy').val();
        var keywork = $('#searchKeyWord').val();
        var cycle = $('#cycleDate').val();
        var url = $common.getAjindexUrl() + '/?PageNo=' + pageNo +
        '&pageSize=' + pageSize +
        '&pageStatus=' + pageStatus +
        '&orderBy=' + orderBy +
        '&order=' + order +
        '&searchBy=' + searchBy +
        '&keyWord=' + keywork;

        var status = $('input:radio[name=status]:checked').val();
        if (status != null) {
            url += "&status=" + status;
        }
        url += "&cycle=" + cycle;
        $common.loadingSearchPage(url, '#lstContainer');
    },
    /*Search Pawn By Status (ComboBox)*/
    searchPawnByStatus: function () {
        var pageNo = 1;
        var pageStatus = 1;
        var pageSize = $('#cmbItemPerPage').val();
        var orderBy = '';
        var order = '';
        var statusId = $("#cboStatus").val();
        var searchBy = $('#cmbSearchBy').val();
        var keywork = $('#searchKeyWord').val();
        var cycle = $('#cycleDate').val();
        var url = $common.getAjindexSearchNameUrl() + '/?PageNo=' + pageNo +
        '&pageSize=' + pageSize +
        '&pageStatus=' + pageStatus +
        '&orderBy=' + orderBy +
        '&order=' + order +
        '&searchBy=' + searchBy +
        '&keyWord=' + keywork +
        '&statusId=' + statusId;

        var status = $('input:radio[name=status]:checked').val();
        if (status != null) {
            url += "&status=" + status;
        }
        url += "&cycle=" + cycle;
        $common.loadingSearchPage(url, '#lstContainer');
    },

    /*Search Name by status when Click search button*/
    searchNameClick: function () {
        var pageNo = 1;
        var pageStatus = 1;
        var pageSize = $('#cmbItemPerPage').val();
        var orderBy = $('#cmbSearchBy').val();
        var order = 'asc';
        var statusId = $("#cboStatus").val();
        var searchBy = $('#cmbSearchBy').val();
        var keywork = $('#searchKeyWord').val();
        var cycle = $('#cycleDate').val();
        var url = $common.getAjindexSearchNameUrl() + '/?PageNo=' + pageNo +
        '&pageSize=' + pageSize +
        '&pageStatus=' + pageStatus +
        '&orderBy=' + orderBy +
        '&order=' + order +
        '&searchBy=' + searchBy +
        '&keyWord=' + keywork +
        '&statusId=' + statusId;

        var status = $('input:radio[name=status]:checked').val();
        if (status != null) {
            url += "&status=" + status;
        }
        url += "&cycle=" + cycle;
        $common.loadingSearchPage(url, '#lstContainer');
    },
    /*Search Item OrderBy ModifiedDate For Refresh Update Rate */
    SearchItemForRefreshRate: function () {
        var pageNo = 1;
        var pageStatus = 1;
        var pageSize = $('#cmbItemPerPage').val();
        var orderBy = $('#cmbSearchBy').val();
        var order = 'asc';
        var catId = $("#ItemCategory").val();
        var searchBy = $('#cmbSearchBy').val();
        var keywork = $('#searchKeyWord').val();
        var cycle = $('#cycleDate').val();
        var url = $common.getAjindexSearchItemByCategoryUrl() + '/?PageNo=' + pageNo +
        '&pageSize=' + pageSize +
        '&pageStatus=' + pageStatus +
        '&orderBy=ModifiedDate' +
        '&order=desc'+
        '&searchBy=' + searchBy +
        '&keyWord=' + keywork +
        '&catId=' + catId;

        var status = $('input:radio[name=status]:checked').val();
        if (status != null) {
            url += "&status=" + status;
        }
        url += "&cycle=" + cycle;
        $common.loadingSearchPage(url, '#lstContainer');
    },
    /*Search Item By Category*/
    SearchItemByCategory: function () {
        var pageNo = 1;
        var pageStatus = 1;
        var pageSize = $('#cmbItemPerPage').val();
        var orderBy = $('#cmbSearchBy').val();;
        var order = 'asc';
        var catId = $("#ItemCategory").val();
        var searchBy = $('#cmbSearchBy').val();
        var keywork = $('#searchKeyWord').val();
        var cycle = $('#cycleDate').val();
        var url = $common.getAjindexSearchItemByCategoryUrl() + '/?PageNo=' + pageNo +
        '&pageSize=' + pageSize +
        '&pageStatus=' + pageStatus +
        '&orderBy=' + orderBy +
        '&order=' + order +
        '&searchBy=' + searchBy +
        '&keyWord=' + keywork +
        '&catId=' + catId;

        var status = $('input:radio[name=status]:checked').val();
        if (status != null) {
            url += "&status=" + status;
        }
        url += "&cycle=" + cycle;
        $common.loadingSearchPage(url, '#lstContainer');
    },

    addClick: function () {
        var url = $common.getCreateUrl() + '/?';
        $common.openDialog('#mydialog');
        $common.loadingPage(url, '#mydialog');
    },
    openColorboxDialog: function () {
        window.location.href = '#inline_content';
    },
    closeColorboxDialog: function () {
        $.colorbox.close();
    },
    submit: function (url) {
        $common.submitPage(url, '#cboxLoadedContent', '#myform');
    },
    onFileBrowsingChange: function () {
        $('#uploadForm').submit();
        $('#imgUpload').val($("#_file").val());
    },
    requestUserRoleClick: function (leftItemId, container) {
        $('#LeftItemId').val(leftItemId);
        $("tr").removeClass("Highlight");
        $('#Highlight' + leftItemId).addClass("Highlight");
        var url = $common.getAjindexUrl() + '/' + leftItemId;
        $common.loadingPageNoWaitingSign(url, container);
    },
    requestPageRoleClick: function (leftItemId, container) {
        $('#LeftItemId').val(leftItemId);
        $("tr").removeClass("Highlight");
        $('#Highlight' + leftItemId).addClass("Highlight");
        var url = $common.getAjindexUrl() + '/' + leftItemId;
        $common.loadingPageNoWaitingSign(url, container);
    },
    onCheckboxClick: function (url, checkBoxId, itemId, formId) {
        var checkValue = $(checkBoxId + itemId).is(':checked')
        if (checkValue)
            $('#Action').val('Add');
        else
            $('#Action').val('Delete');
        $('#ItemId').val(itemId);
        $common.submitPageNoContainer(url, formId);
    },
    onNormalCheckboxClick: function (url, calling_function) {
        $.ajax({
            url: url,
            async: true,
            cache: false,
            type: "POST",
            dataType: "html",
            beforeSend: function () {

            },
            success: function (responseText) {
                eval(calling_function);
            }
        });
    },
    toggleReload: function () {
        $(".maintable a.expand").click(function (event) {
            $(event.target).parent().parent().next().find('td.innertable').toggle();

            if ($(event.target).text() == '+') {
                $(event.target).text('-');
            }
            else {
                $(event.target).text('+');
            }
            return false;
        });

        $(".maintable a.expand_all").click(function (event) {
            $('td.innertable').toggle();

            if ($(event.target).text() == '+') {
                $(event.target).text('-');
            }
            else {
                $(event.target).text('+');
            }
            return false;
        });
    },

    onSelectBoxChange: function (url, selectBoxId, container) {
        id = $(selectBoxId).val();
        if (id > 0) {
            url = url + '/' + id;
            $common.loadingPageNoWaitingSign(url, container);
        }
    },
    searchEnter: function (e) {
        if (typeof e == 'undefined' && window.event) { e = window.event; }
        if (e.keyCode == 13) {
            document.getElementById('search-button').onclick();
        }
    },
};

function menuString(myString) {
    if (myString == '')
        $('#menu li a#home').addClass('selected');
    if (myString == '/Product')
        $('#menu li a#product').addClass('selected');
    if (myString == '/Sale')
        $('#menu li a#sale').addClass('selected');
    if (myString == '/SaleDetail')
        $('#menu li a#sale').addClass('selected');
    if (myString == '/User')
        $('#menu li a#user').addClass('selected');
    if (myString == '/Customer')
        $('#menu li a#customer').addClass('selected');
    if (myString == '/ExchangeRate')
        $('#menu li a#exchangeRate').addClass('selected');
    if (myString == '/Configuration')
        $('#menu li a#setting').addClass('selected');
    if (myString == '/ProductCategory')
        $('#menu li a#setting').addClass('selected');
    if (myString == '/ProductType')
        $('#menu li a#setting').addClass('selected');
    if (myString == '/Currency')
        $('#menu li a#setting').addClass('selected');
    if (myString == '/SaleStock')
        $('#menu li a#stock').addClass('selected');
    if (myString == '/SaleStockOut')
        $('#menu li a#stock').addClass('selected');
    if (myString == '/WarehouseStock')
        $('#menu li a#stock').addClass('selected');
    if (myString == '/WarehouseStockout')
        $('#menu li a#stock').addClass('selected');
    if (myString == '/Product/Export')
        $('#menu li a#product').addClass('selected');
    if (myString == '/Product/Import')
        $('#menu li a#product').addClass('selected');
    if (myString == '/Product/Calculator')
        $('#menu li a#product').addClass('selected');
    if (myString == '/User/ChangePassword')
        $('#menu li a#setting').addClass('selected');
    if (myString.indexOf("BusinessCycle") > -1)
        $('#menu li a#businessCycle').addClass('selected');
    if (myString.indexOf("Role") > -1)
        $('#menu li a#setting').addClass('selected');
    if (myString.indexOf("Page") > -1)
        $('#menu li a#setting').addClass('selected');
    if (myString.indexOf("Reports") > -1)
        $('#menu li a#report').addClass('selected');
};