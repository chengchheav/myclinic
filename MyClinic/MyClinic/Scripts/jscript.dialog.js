var $dialog = {
    strContainer: ".tableList",
    strWidth: "450",
    setWidth: function (strVal) {
        $dialog.strWidth = strVal;
    },
    /*==========================Init Template Dialog==========================*/
    initTemplate: function () {
        var strConfirm = '<div id="dialog-confirm" title="Message"></div>';
        var strMessage = '<div id="dialog-message" title="Confirm"></div>';
        $("body").append(strConfirm + strMessage);
    },
    /*==========================Init Message Dialog==========================*/
    initMessage: function (strMsg) {
        if (typeof strTitle == "undefined") strTitle = "Message";
        if (typeof strMsg == "undefined") strMsg = "Your request is success.";
        var objDialog = $("#dialog-confirm").dialog({
            modal: true,
            title: strTitle,
            closeOnEscape: false,
            resizable: false,
            autoOpen: true,
            width: $dialog.strWidth,
            buttons: {
                Close: function () {
                    $(this).dialog("close");
                }
            },
            open: function (event, ui) {
                $(event.target).dialog('widget')
                    .css({ position: 'fixed' })
                    .position({ my: 'center', at: 'center', of: window });
                $(".ui-dialog-titlebar-close", ui.dialog || ui).hide();
            }
        });
        objDialog.html(strMsg);
    },
    /*==========================Init Confirm Dialog==========================*/
    /*Step Two*/
    initConfirm: function (strValue, strType, strUrl, strTitle, strMsg) {
        if (typeof strType == "undefined") strType = "delete";
        if (typeof strTitle == "undefined") strTitle = "Confirm";
        if (typeof strMsg == "undefined") strMsg = "Are you sure to do this request?";
        var objDialog = $("#dialog-confirm").dialog({
            modal: true,
            title: strTitle,
            closeOnEscape: false,
            resizable: false,
            autoOpen: true,
            width: $dialog.strWidth,
            buttons: {
                Yes: function () {
                    $dialog.transactionConfirm(strValue, strType, strUrl);
                    $(this).dialog("close");
                },
                No: function () {
                    $(this).dialog("close");
                }
            },
            open: function (event, ui) {
                $(event.target).dialog('widget')
                    .css({ position: 'fixed' })
                    .position({ my: 'center', at: 'center', of: window });
                $(".ui-dialog-titlebar-close", ui.dialog || ui).hide();
            }
        });
        objDialog.html(strMsg);
    },
    /*==========================Init Event For Delete Record==========================*/
    initBindEventDeleteRecord: function (strID, strContainer) {
        if (typeof strID == "undefined") strID = ".delete-action";
        if (typeof strContainer == "undefined") strContainer = ".tableList";
        strID = strContainer + " " + strID;
        var strType = "delete";
        if ($common.checkDom(strID)) {
            $(strID).each(function () {
                $(this).bind("click", function () {
                    var strValue = $(this).data("value");
                    var strUrl = $(this).data("url");
                    $dialog.strContainer = strContainer;
                    $dialog.initConfirm(strValue, strType, strUrl);
                    return false;
                });
            });
        }
    },
    /*=========For bind event for action on record===========*/
    /*Step One*/
    initBindEventRecord: function (strID, strContainer) {
        if (typeof strID == "undefined") strID = ".record-action";
        if (typeof strContainer == "undefined") strContainer = ".tableList";
        strID = strContainer + " " + strID;
        if ($common.checkDom(strID)) {
            $(strID).each(function () {
                $(this).bind("click", function () {
                    var strValue = $(this).data("value");
                    var strUrl = $(this).data("url");
                    var strTitle = $(this).data("title");
                    var strMsg = $(this).data("msg");
                    var strType = $(this).data("type");
                    $dialog.strContainer = strContainer;
                    $dialog.initConfirm(strValue, strType, strUrl, strTitle, strMsg);
                    return false;
                });
            });
        }
    },
    /*Step Three*/
    transactionConfirm: function (strValue, strType, strUrl) {
        if ($common.checkNetConnection()) {
            $.ajax({
                url: strUrl,
                async: true,
                cache: false,
                type: "POST",
                dataType: "json",
                data: {
                    value: strValue
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $common.setHiddenActionPage();
                    $dialog.initMessage("System have a unexpected Error, please refresh this page to do again.");
                },
                beforeSend: function () {
                    $common.setShowActionPage();
                },
                success: function (responseText) {
                    var strRecord = "#" + strValue;
                    var strMessageDetail = "Your request is fail.";
                    if (responseText.resultMsg == "succ") {
                        var strMessageDetail = "Your request is success.";
                        if (strType == "delete") {
                            $common.removeDom($dialog.strContainer + " " + strRecord);
                            if ($common.checkNotEmpty(responseText.resultDetail)) {
                                strMessageDetail = responseText.resultDetail;
                            }
                            $dialog.initMessage(strMessageDetail);
                        } else if (strType == "stop") {
                            $common.removeDom($dialog.strContainer + " " + strRecord);
                            $dialog.initMessage(responseText.resultDetail);
                            setTimeout(function () {
                                $common.setRefreshPage();
                            }, 2000);
                        } else if (strType == "enable") {
                            $common.removeDom($dialog.strContainer + " " + strRecord);
                            $dialog.initMessage(responseText.resultDetail);
                            setTimeout(function () {
                                $common.setRefreshPage();
                            }, 2000);
                        }
                    } else {
                        if ($common.checkNotEmpty(responseText.resultDetail)) {
                            strMessageDetail = responseText.resultDetail;
                        }
                        $dialog.initMessage(strMessageDetail);
                    }
                },
                complete: function () {
                    $common.setHiddenActionPage();
                }
            });
        }
    }
};



            