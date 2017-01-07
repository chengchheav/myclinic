$arrElement = {
    Code        : "",
    Id          : 0,
    MedicineId  : 0,
    Medicine    : "",
    Qty         : "",
    MedicineTypeId     : "",
    MedicineType       : "",
    Morning     : "",
    Noon        : "",
    Night       : "",
    Remark      : "",
    tabIndex1   : 0,
    tabIndex2   : 0,
    tabIndex3   : 0,
    tabIndex4   : 0,
    tabIndex5   : 0,
    tabIndex6   : 0,
    tabIndex7   : 0,
}

$pres = {
    intIndex: 0,
    lastTabindex: 0,
    strAutoUrl : "",
    blnDetectPage : true,
    blnError : false,

    strContainer: "#tbl-pres tbody",
    intiTriggerAddMedicine:function(strId){
        $(strId).trigger( "click" );
    },
    init: function (strUrl, intIndex) {
        if (typeof intIndex == "undefined") intIndex = 0;
        this.disableDetectPage();
        this.intIndex = intIndex;
        this.strAutoUrl = strUrl;
    },
    orderRow: function(){
        var intIndex = 1;
        if ($(this.strContainer + " tr.rcd-dta").size() > 0) {
            $(this.strContainer + " tr.rcd-dta").each(function(){
                $(this).find("div.no").html(intIndex);
                intIndex = intIndex + 1;
            });
        }
    },
    checkValidForm: function(){
    console.log(1);
        var blnReturn = true;
        var totalRecord = $(this.strContainer + " tr.rcd-dta").size();
        ///console.log("MedicineId :" + $("#MedicineId").val());
        
        $("input[name='MedicineId']").each(function(){
                
            var fistParent = $(this).closest('tr');
            var strClass = fistParent.attr('id');
            var parent = $(this).closest("div");
            parent.addClass(strClass);

            var strId = "#"+strClass;
            var disName = $(strId).find("input[name='Medicine']").val();
            var realName = $(strId).find("input[name='MedicineName']").val();

            console.log($(this).val());
            /*Check if display Name of Medicine == Real Name in hidden control*/
            if(disName == realName){
                if($(this).val() == 0 )
                {
                    blnReturn = false;
                    console.log(strClass);
                    if($('.'+strClass).find("span").hasClass("error") == false)//Check if class error not have
                    {
                        $('.'+strClass).css('margin-top','20px');
                        $( '<span class="error">'+ $translator.translate("This Medicine not Valid.",lang)+'</span>' ).appendTo( $("."+strClass) );
                    }
                }
                else
                {
                    
                    if($('.'+strClass).find("span").hasClass("error") == true)//Check if class error  have
                    {
                        $('.'+strClass).removeAttr("style");
                        $('.'+strClass).find(".error").remove();
                        $('div').removeClass(strClass);
                    }
                }
            }
            else{
                blnReturn = false;
                if($('.'+strClass).find("span").hasClass("error") == false)//Check if class error not have
                {
                    $('.'+strClass).css('margin-top','20px');
                    $( '<span class="error">'+ $translator.translate("This Medicine not Valid.",lang)+'</span>' ).appendTo( $("."+strClass) );
                }
            }
        });
        return blnReturn;
    },
    initGetMedicine: function (strClss, strSet, strAnother,strSetName, strName) {
        if (typeof strName == "undefined") strName = "Name";
        var $this = this;
        console.log(strClss+"="+strSet+"="+ strAnother+"="+strSetName);
        if ($Common.checkDom(strClss)) {
            var options = {
                url: $Common.concatHostDomain(this.strAutoUrl),
                getValue: strName,
                list: {
                    match: {
                        enabled: true
                    },
                    onSelectItemEvent: function () {

                        if ($Common.checkDom(strSet)) {
                            var id = $(strClss).getSelectedItemData().Id;
                            var name = $(strClss).getSelectedItemData().Name;
                            $(strSet).val(id);

                            $(strSetName).val(name);
                            console.log("strSet = " + strSet)
                            console.log("strSetName = " + strSetName)
                        }

                        if ($Common.checkDom(strAnother)) {
                            /*var name = $(strClss).getSelectedItemData().MedicineType;
                            $(strAnother).val(name);*/
                        }
                    }
                },
                theme: "square",
                ajaxSettings: {
                    dataType: "json",
                    method: "POST",
                    data: {
                        dataType: "json"
                    }
                },
                preparePostData: function (data) {
                    data.phrase = $(strClss).val();
                    return data;
                }
            };
            $(strClss).easyAutocomplete(options);
        }
    },
    /*For add the prescription into list*/
    addPres: function (strNote) {
        $arrElement.tabIndex1 = this.lastTabindex + 1;
        $arrElement.tabIndex2 = this.lastTabindex + 2;
        $arrElement.tabIndex3 = this.lastTabindex + 3;
        $arrElement.tabIndex4 = this.lastTabindex + 4;
        $arrElement.tabIndex5 = this.lastTabindex + 5;
        $arrElement.tabIndex6 = this.lastTabindex + 6;
        $arrElement.tabIndex7 = this.lastTabindex + 7;
        this.lastTabindex = $arrElement.tabIndex7;
        
        //console.log(this.lastTabindex);
        $arrElement.Code = this.intIndex + 1;
        var selectId = 'selectId'+$arrElement.Code
        var classSelect = '.'+selectId +' select';
        var strRowFrm = 'row' + $arrElement.Code;
        var strRow = 'med' + $arrElement.Code;
        var strRowId = 'medId' + $arrElement.Code;
        var strRowName = 'medName' + $arrElement.Code;
        var strHtml = '<tr id="' + strRowFrm + '" class="rcd-dta bdr">';
            strHtml += '    <td><div class="vle-bx no" style="text-align:center;">' + $arrElement.Code + '</div></td>';
            strHtml += '    <td><div class="vle-bx"><input id = "'+strRow +'" name="Medicine" type="text" value="' + $arrElement.Medicine + '" tabindex="'+$arrElement.tabIndex1+'"><input name="MedicineId" id="'+strRowId+'" type="hidden" value="' + $arrElement.MedicineId + '"><input type="hidden" name="MedicineName" id="'+strRowName+'" /><input name="Id" type="hidden" value="' + $arrElement.Id + '"></div></td>';
            strHtml += '    <td><div class="vle-bx"><input style="text-align:center;" name="Qty" type="text" value="' + $arrElement.Qty + '" tabindex="'+$arrElement.tabIndex2+'"></div></td>';
            //strHtml += '    <td><div class="vle-bx"><input name="MedicineType" type="text" value="' + $arrElement.MedicineType + '"><input name="MedicineTypeId" type="hidden" value="' + $arrElement.MedicineTypeId + '"></div></td>';
            //strHtml += '    <td><div class="vle-bx">'+medicineTypeSelectBox+'</div></td>';
            strHtml += '    <td><div class="vle-bx '+selectId+'">'+usageSelectBox+'</div></td>';
            strHtml += '    <td><div class="vle-bx"><input style="text-align:center;" name="Morning" type="text" value="' + $arrElement.Morning + '" tabindex="'+$arrElement.tabIndex4+'"></div></td>';
            strHtml += '    <td><div class="vle-bx"><input style="text-align:center;" name="Noon" type="text" value="' + $arrElement.Noon + '" tabindex="'+$arrElement.tabIndex5+'"></div></td>';
            strHtml += '    <td><div class="vle-bx"><input style="text-align:center;" name="Night" type="text" value="' + $arrElement.Night + '" tabindex="'+$arrElement.tabIndex6+'"></div></td>';
            strHtml += '    <td><div class="vle-bx"><input name="Remark" type="text" value="' + $arrElement.Remark + '" tabindex="'+$arrElement.tabIndex7+'"></div></td>';
            strHtml += '    <td class="last act">';
            strHtml += '        <a href="#" class="btn" onclick="$pres.confirmDelete(\'' + $arrElement.Code + '\', \'' + $translator.translate("Are you sure to delete this ") +  $arrElement.Medicine + '?' + '\');">-</a>';
            strHtml += '     </td>';
            strHtml += '</tr>';
        $(this.strContainer).append(strHtml);
        this.orderRow();
        this.intIndex = this.intIndex + 1;
        var strClss = "#" + strRowFrm + " input[name='Medicine']";
        var strSet  = "#" + strRowFrm + " input[name='MedicineId']";
        var strAnother  = "#" + strRowFrm + " input[name='MedicineType']";
        var strSetName  = "#" + strRowFrm + " input[name='MedicineName']";
        this.initGetMedicine(strClss, strSet, strAnother,strSetName);
        //set TabIndex to select Box
        $(document).find(classSelect).attr('tabindex',$arrElement.tabIndex3);
        
       if(this.lastTabindex == 7){
            $('[tabindex=' + $arrElement.tabIndex1+ ']').focus();
       }
       else {
            if(strNote == 'Tab'){
                $('[tabindex=' + ($arrElement.tabIndex1 - 1)+ ']').focus();
            }
            else{
                $('[tabindex=' + $arrElement.tabIndex1+ ']').focus();
            }
        }
        
        //$key.initKeypress(strRow, strRowId);
        $key.initKeypressForMedicine(strRow, strRowId,strSetName);
        

        /*Add New Row Of Midecine When Click Tab and cursor at the last of input field*/
        $("input[name='Remark']").keydown(function (e) {
            var Value = $(this).attr('tabindex');
            //alert(Value);
            //console.log(Value);
            //console.log($pres.lastTabindex);
            if(Value == $pres.lastTabindex)
            {
                if (e.which == 9)
                    $pres.addPres("Tab");
            }
        });
        return false;
    },
    clearError: function(){
        if($("#tbl-pres tfoot .error").size() > 0){
            $("#tbl-pres tfoot .error").each(function(){
                $(this).remove();
            });
        }
    },
    
    /*For confirm delet data*/
    confirmClear: function (strMsg) {
        var $this = this;
        if ($(this.strContainer + " tr.rcd-dta").size() > 0) {
            var objDialog = $("#dialog-confirm").dialog({
                modal: true,
                title: "Message",
                closeOnEscape: false,
                resizable: false,
                autoOpen: true,
                width: $dialogAlert.strWidth,
                buttons: {
                    Yes: function () {
                        $this.clearData();
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
    },
    clearData: function () {
        $(this.strContainer + " tr.rcd-dta").each(function () {
            $(this).remove();
        });
        this.arrPres = [];
    },
    setData: function () {
        $("#Id").val($arrElement.Id);
        $("#Medicine").val($arrElement.Medicine);
        $("#MedicineId").val($arrElement.MedicineId);
        $("#Qty").val($arrElement.Qty);
        $("#MedicineType").val($arrElement.MedicineTypeId);
        $("#Morning").val($arrElement.Morning);
        $("#Noon").val($arrElement.Noon);
        $("#Night").val($arrElement.Night);
        $("#Remark").val($arrElement.Remark);
    },
    /*For delete the prescription from list*/
    deletePres: function (strCode) {
        var rawId = "#row" + strCode;
        if ($(rawId).size() > 0) {
            $(rawId).remove();
            this.orderRow();
        }
    },
    /*For confirm delet data*/
    confirmDelete: function (strCode, strMsg) {
        var $this = this;
        var objDialog = $("#dialog-confirm").dialog({
            modal: true,
            title: "Message",
            closeOnEscape: false,
            resizable: false,
            autoOpen: true,
            width: $dialogAlert.strWidth,
            buttons: {
                Yes: function () {
                    $this.deletePres(strCode);
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
    },
    getValue: function(strId){
        var strReturn = "";
        if($(strId).size() > 0){
            strReturn = $(strId).val();
        }
        return strReturn;
    },
    /*For prepare the prescription to submit to server*/
    preparePres: function () {
        this.enableDetectPage();
    },
    disableDetectPage: function(){
        window.onbeforeunload = null; 
        this.blnDetectPage = false;
    },
    enableDetectPage: function(){ 
        var $this = this;
        window.addEventListener("beforeunload", function (e) { 
            if($this.blnDetectPage === true){
                var confirmationMessage = 'Do you want to leave this page? '
                                        + 'If you did it, your transaction will be lost.';

                (e || window.event).returnValue = confirmationMessage; //Gecko + IE
                return confirmationMessage; //Gecko + Webkit, Safari, Chrome etc.
            }else{
                return null;
            }
        });
    },
};

