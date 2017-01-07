$remark = {
    remarkUrl : "",
    remarkUrl: function(strVal){
        this.remarkUrl = strVal;
    },
    /*==========================Confirm Dayend Close ============================*/
    remarkRecord: function (strId, strMsg) { 
        var $this = this;
        var objDialog = $("#dialog-confirm").dialog({
            modal: true,
            title: "Remark for " + strMsg,
            closeOnEscape: false,
            resizable: false,
            autoOpen: true,
            width: $dialog.strWidth,
            buttons: {
                Yes: function () { 
                    var remark = $(".frm-rmk form #Remark").val();
                    $(this).dialog("close");
                    $.ajax({
                        url: $this.remarkUrl,
                        async: true,
                        cache: false,
                        type: "POST",
                        data: $(".frm-rmk form").serialize(),
                        dataType: "json",
                        error: function () {},
                        beforeSend: function () {},
                        success: function (responseText) {
                            if (responseText.result === "success") {
                                $dialog.initMessage();
                                $("#"+ strId).attr("title", remark);
                                $("#"+ strId).attr("data-original-title", remark);
                                $tooltip.initReContent("#"+ strId);
                            }else{
                                $dialog.initMessage("Your request is failure.");
                            }
                        }
                    });
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
                if (this.title == "") {
                    $(".ui-dialog-title").html("Remark for " + strMsg);
                }
            }
        });
        var htmlForm  = '<div class="frm-rmk">';
            htmlForm += '   <form method="post">';
            htmlForm += '       <input type="hidden" value="' + strId + '" name="Id" id="Id">';
            htmlForm += '       <div class="ctr">';
            htmlForm += '           <textarea style="resize: none; height: 200px; width: 100%;" id="Remark" name="Remark"></textarea>';
            htmlForm += '       </div>';
            htmlForm += '   </form>';
            htmlForm += '</div>';
        objDialog.html(htmlForm);
    },
};