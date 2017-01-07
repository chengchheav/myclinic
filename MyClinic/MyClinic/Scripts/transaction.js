$transaction = {
    strActionId: 1,
    initBindEventChangeSetDataForSelect: function (strID, strSet, strUrl) {
        if (typeof strID == "undefined") strID = "#ItemCategory";
        if (typeof strSet == "undefined") strSet = "#Item";
        if (typeof strUrl == "undefined") strUrl = "";
        $(strID).bind("change", function () {
            var catId = $(strID).val();
            var url = $Common.concatHostUrl(strUrl + '/' + catId);
            $.ajax({
                url: url,
                async: true,
                cache: false,
                type: "POST",
                dataType: "json",
                error: function () {
                    console.log("Error");
                },
                beforeSend: function () {
                    $(strSet).html("<option value=''>" + $translator.translate('---Waiting---', lang) + "</option>");
                },
                success: function (responseText) {
                    var text = "";
                    if (responseText != "") {
                        text = "<option value=''>" + $translator.translate('---Select Item---', lang) + "</option>";
                        for (var i = 0; i < responseText.length; i++) {
                            text = text + "<option value='" + responseText[i].Id + "'>" + responseText[i].Name + "</option>"
                        }
                        $(strSet).html(text);
                        $("#InterestRate").val('');
                    }
                }
            });
        });
    },
    initSetInterestRateByItem: function (strID, strSet, strUrl) {
        $(strID).bind("change", function () {
            var itemId = $(strID).val();
            var url = $Common.concatHostUrl(strUrl + '/' + itemId);
            $.ajax({
                url: url,
                async: true,
                cache: false,
                type: "POST",
                dataType: "json",
                error: function () {
                    console.log("Error");
                },
                success: function (responseText) {
                    if (responseText != "") {
                        $(strSet).val(responseText.InterestRate);
                    }
                }
            });
        });
    },
    isLeapYear: function (year) {
        return ((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0);
    },
    initClickForChangeProfile: function () {

        $("#PhotoFileBase").change(function () {
            $transaction.readURL(this);
        });
        $("#imgPhoto").click(function () {
            $("#PhotoFileBase").trigger("click");
        });

    },
    readURL: function (input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                var Img = $('img').attr('src', e.target.result);
                $("div#imgPhoto").find("#image").css("display", "none");
                $("div#imgPhoto").html(Img);
                $("div#imgPhoto").find("img").css("width", "100%");
                $("div#imgPhoto").find("img").css("height", "100%");
                $("div#imgPhoto").find("img").css("object-fit", "cover");
            }
            reader.readAsDataURL(input.files[0]);
        }
    },
    initSearchPawnNameByStatus: function (strID) {
        if (typeof strID == "undefined") strID = "#cboStatus";
        $(strID).bind("change", function () {
            $common.searchPawnByStatus();
        });

    },
    initSearchItemByCategory: function (strID) {
        if (typeof strID == "undefined") strID = "#ItemCategory";
        $(strID).bind("change", function () {
            $common.SearchItemByCategory();
        });
    },
    setExpireDateByPawnTypeChange: function (statDate, endDate) {
        $("#PawnType").bind("change", function () {
            var id = $("#PawnType").val();
            var pDate = $(statDate).val();
            var url = $Common.concatHostUrl("/Pawn/CalculateDate/" + id);
            if (pDate.length > 0) {
                $.ajax({
                    url: url,
                    async: true,
                    cache: false,
                    type: "POST",
                    dataType: "json",
                    data: { pawnDate: pDate },

                    error: function () {
                        console.log("Error");
                    },
                    success: function (responseText) {
                        if (responseText != "") {
                            var pawnDate = $.datepicker.formatDate("dd/M/yy", new Date(responseText.PawnDate));
                            var expireDate = $.datepicker.formatDate("dd/M/yy", new Date(responseText.ExpireDate));
                            $(statDate).val(pawnDate);
                            $(endDate).val(expireDate);
                        }
                    }
                });
            }
        });
    },
    setExpireDateByPawnDate: function (startDate, endDate, matchDate) {
        if (typeof matchDate === "undefined") matchDate = "#FirstContractDate";
        $(startDate).datepicker({

            inline: true,
            minDate: new Date(2016, 1 - 1, 1),
            altField: '#PawnDate',
            altFormat: "dd/M/yy",
            dateFormat: "dd/M/yy",
            beforeShow: function () {
                $("#ui-datepicker-div").addClass("calendar");
            },
            onSelect: function () {
                var day1 = $(startDate).datepicker('getDate').getDate();
                var month1 = $(startDate).datepicker('getDate').getMonth() + 1;
                var year1 = $(startDate).datepicker('getDate').getFullYear();
                var pType = $("#PawnType").val();
                /*Get Expired Date For Weekly*/
                if (pType == 1) {
                    day1 = day1 + 7;

                    /*For Find Day per month*/
                    var daysInMonth;
                    if (month1 === 2) { // February
                        if ($transaction.isLeapYear(year1)) {
                            daysInMonth = 29;
                        } else {
                            daysInMonth = 28;
                        }
                    } else if (month1 === 4 || month1 === 6 || month1 === 9 || month1 === 11) {
                        daysInMonth = 30;
                    } else {
                        daysInMonth = 31;
                    }
                    if (day1 > daysInMonth) {
                        if (month1 == 12) {
                            month1 = 1;
                            year1 = year1 + 1;
                        }
                        else {
                            month1 = month1 + 1;
                        }
                        day1 = day1 - daysInMonth;
                    }

                }
                /*Get Expired Date For Monthly*/
                else {
                    month1 = month1 + 1;
                    /*For Find Day per month*/
                    var daysInNextMonth;
                    if (month1 === 2) { // February
                        if ($transaction.isLeapYear(year1)) {
                            daysInNextMonth = 29;
                        } else {
                            daysInNextMonth = 28;
                        }
                    } else if (month1 === 4 || month1 === 6 || month1 === 9 || month1 === 11) {
                        daysInNextMonth = 30;
                    } else {
                        daysInNextMonth = 31;
                    }
                    if (day1 > daysInNextMonth) {
                        console.log(day1 + ">" + daysInNextMonth);
                        day1 = day1 - 1;
                    }
                    if (month1 === 12) {
                        month1 = 1;
                        year1 = year1 + 1;
                    }
                }

                if (day1 < 10) {
                    day1 = '0' + day1;
                }
                if (month1 < 10) {
                    month1 = '0' + month1;
                }
                //console.log(day1);
                var fullDate = day1 + "/" + $transaction.convertMonthToText(month1) + "/" + year1;
                $(endDate).val(fullDate);
                if ($Common.checkDom(matchDate)) {
                    $(matchDate).val($(startDate).val());
                }
            }
        });
        if ($Common.checkDom(matchDate)) {
            $(matchDate).datepicker({
                dateFormat: "dd/M/yy"
            });
        }
        $(endDate).datepicker({
            dateFormat: "dd/M/yy"
        });

    },
    convertMonthToText: function (month) {
        var arrMonth = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
        return arrMonth[month - 1];
    },
    initChooseExpiredDatePopUp: function (statusId) {
        if (statusId == 2) {
            $("#txtExpiredDate").datepicker({
                dateFormat: "dd/M/yy"
            });
        }
    },
    initDatePicker: function (strId) {
        $(strId).datepicker({
            dateFormat: "dd/M/yy"
        });
    },
    initDatePickerAndSet: function (strId, strSet) {
        $(strId).datepicker({
            dateFormat: "dd/M/yy",
            onSelect: function () {
                var selected = $(this).val();
                $(strSet).val(selected);
            }
        });
    },
    initDatePickerForDateOfBirth: function (strId) {
        $(strId).datepicker({
            changeYear: true,
            yearRange: "1935:2000",
            dateFormat: "dd/M/yy"
        });
    },
    initDatePickerAndSetForDOB: function (strId, strSet) {
        var strUrl = $Common.concatHostUrl("/Patient/GetAge");
        $(strId).datepicker({
            changeYear: true,
            yearRange: "1935:2016",
            dateFormat: "dd/M/yy",
            onSelect: function () {
                var selected = $(this).val();
                $(strSet).val(selected);
                console.log(strId);
                console.log(strUrl);
                $transaction.initGetAgeByDateOfBirth(strId, strUrl);
            }
        });
    },

    initCalculateExpiredDateForRenew: function (strId, id) {
        var strUrl = $Common.concatHostUrl("/Pawn/CalculateDateForExpired/" + id);
        $.ajax({
            url: strUrl,
            async: true,
            cache: false,
            type: "POST",
            dataType: "json",
            error: function () {
                console.log("Unexcepted Error");
            },
            success: function (responseText) {
                var expireDate = $.datepicker.formatDate("dd/M/yy", new Date(responseText.ExpireDate));
                $(strId).val(expireDate);

            }
        });
    },
    initCalculateInterestEarned: function (strId, id) {
        var strUrl = $Common.concatHostUrl("/Pawn/CalculateInterestEarned/" + id);
        $.ajax({
            url: strUrl,
            async: true,
            cache: false,
            type: "POST",
            dataType: "json",
            error: function () {
                console.log("Unexcepted Error");
            },
            success: function (responseText) {
                $(strId).val(responseText.InterestEarned);
            }
        });
    },
    initDefaultPawnDate: function (strUrl) {
        $.ajax({
            url: strUrl,
            async: true,
            cache: false,
            type: "POST",
            dataType: "json",
            error: function () {
                console.log("Unexcepted Error");
            },
            success: function (responseText) {
                var pawnDate = $.datepicker.formatDate("dd/M/yy", new Date(responseText.PawnDate));
                var expireDate = $.datepicker.formatDate("dd/M/yy", new Date(responseText.ExpireDate));
                var firstContractDate = $.datepicker.formatDate("dd/M/yy", new Date(responseText.FirstContractDate));
                $("#PawnDate").val(pawnDate);
                $("#ExpireDate").val(expireDate);
                $("#FirstContractDate").val(firstContractDate);

            }
        });
    },
    initDefindActionId: function (strId) {
        $transaction.strActionId == strId;
    },
    initChangeClassMenu: function (oldClass, newClass) {
        if ($transaction.strActionId == 1) {
            $(document).find("." + oldClass).addClass(newClass);
            $(document).find("." + newClass).removeClass(oldClass);
        }
    },
    initSetWidth: function () {
        $(document).find('.easy-autocomplete').css('width', '276px');
    },
    initCheckAll: function (strId) {
        $(strId).change(function () {
            $("input:checkbox").prop('checked', $(this).prop("checked"));
            var n = $("input:checked").length;
            //console.log('All checkedbox'+n);
            if (n > 0) {
                $('#rate').removeAttr('style');
            }
            else {
                $('#rate').css('display', 'none');
            }
        });
    },
    initHideShowButtonInterrestRate: function () {
        $("input[type=checkbox]").click(function () {
            var n = $("input:checked").length;
            if (n > 0) {
                $('#rate').removeAttr('style');
            }
            else {
                $('#rate').css('display', 'none');
            }
        });

    },
    initGetIdItem: function () {
        $('#update-button').click(function () {
            var interestRate = 2;
            var id = [];
            var idList = '';
            $(':checkbox:checked').each(function (i) {
                id[i] = $(this).val();
                idList += $(this).val() + '|';
                alert($(this).val());
            });
            if (idList.length > 0) {
                idList = idList.replace('on|', '');
                idList = idList.substring(0, idList.length - 1);
            }
            //console.log(id);
            if (id.length > 0) {
                url = $Common.concatHostUrl("/Item/UpdateRate");
                $.ajax({
                    url: url,
                    async: true,
                    cache: false,
                    type: "POST",
                    dataType: "json",
                    data: { idList: idList, interestRate: interestRate },

                    error: function () {
                        console.log("Error");
                    },
                    success: function (responseText) {
                        console.log(responseText.result);
                    }
                });
            }
            //console.log(id);
        });
    },
    initShowActionCombo: function (strId) {
        $(strId).bind("change", function () {
            var id = $(this).attr("data-value");
            var ticketNumber = $(this).attr("data-ticket");
            var statusNumber = $(this).val();
            if (statusNumber == 0) {
                $Common.setRedirectPage('/Pawn/Edit/' + id);
            }
            else if (statusNumber == 1) {
                $Common.setRedirectPage('/Pawn/Detail/' + id);
            }
            else if (statusNumber == 2) {
                $dialogAlert.confirmUpdateSatetusForRenew('Record have been Renew', '', 'Are you sure to Renew ticket number ' + ticketNumber + '?', 'Pawn/UpdateStatusPawn/' + id, statusNumber, id, 'Renew');
            }
            else if (statusNumber == 3) {
                $dialogAlert.confirmUpdateSatetus('Record have been Redeem', '', 'Are you sure to Redeem ticket number ' + ticketNumber + '?', 'Pawn/UpdateStatusPawn/' + id, statusNumber, id);
            }
            else if (statusNumber == 4) {
                $dialogAlert.confirmUpdateSatetus('Record have been Void', '', 'Are you sure to Void ticket number ' + ticketNumber + ' ?', 'Pawn/UpdateStatusPawn/' + id, statusNumber, id);
            }
            else if (statusNumber == 5)
                $dialogAlert.confirmUpdateSatetusForRenew('Record have been RenewWithRedeemSome', '', 'Are you sure to RenewWithRedeemSome ticket number ' + ticketNumber + ' ?', 'Pawn/UpdateStatusPawn/' + id, '2', id, 'RenewWithRedeemSome');
            else
                console.log("Status Not Defind");
        });
    },
    initOnlyNumber: function (strId1) {
        $(strId1).numeric();
    },
    initCheckInterestBox: function (strId) {
        var Interrest = $(strId).val();
        if (Interrest == "" || typeof Interrest == "undefined") {
            $('#dialog-confirm').find('span').html($msg.ValidateInterestRate);
            return false;
        }
        else if (Interrest < 0 || Interrest > 99.9) {
            $('#dialog-confirm').find('span').html($msg.ValidateNumInterestRate);
            return false;
        }
        else {
            return true;
        }
    },
    initCheckSession: function () {
        var url = $Common.concatHostUrl("/Login/CheckSession");
        var session = 0;
        $.ajax({
            url: url,
            async: false,
            cache: false,
            type: "POST",
            dataType: "json",
            error: function () {
                console.log("Error");
            },
            success: function (responseText) {
                session = responseText;
            }
        });
        return session;
    },
    initGetDateOfBirthByAgeType: function (strId, strUrl) {
        $(strId).change(function () {
            ageType = $(strId).val();
            age = $("#Age").val();
            if (age != 0 && age < 120 && age != '') {
                $.ajax({
                    url: strUrl,
                    async: true,
                    cache: false,
                    type: "POST",
                    dataType: "json",
                    data: { strAge: age, ageType: ageType },
                    error: function () {
                        console.log("Error");
                    },
                    success: function (responseText) {
                        date = $.datepicker.formatDate("dd/M/yy", new Date(responseText.Dob));
                        $("#Dob").val(date);
                        $("#DateOfBirth").val(date);
                    }
                });
            }
            else {
                $("#Dob").val('');
                $("#DateOfBirth").val('1/1/1990 12:00:00 AM');
            }
        });
    },
    initGetDateOfBirthByAge: function (strId, strUrl) {
        $(strId).keyup(function () {
            age = $(strId).val();
            ageType = $("#ageType").val();
            if (age != 0 && age < 120 && age != '') {
                $.ajax({
                    url: strUrl,
                    async: true,
                    cache: false,
                    type: "POST",
                    dataType: "json",
                    data: { strAge: age, ageType: ageType },
                    error: function () {
                        console.log("Error");
                    },
                    success: function (responseText) {
                        date = $.datepicker.formatDate("dd/M/yy", new Date(responseText.Dob));
                        $("#Dob").val(date);
                        $("#DateOfBirth").val(date);
                    }
                });
            }
            else {
                $("#Dob").val('');
                $("#DateOfBirth").val('1/1/1990 12:00:00 AM');
            }
        });
    },
    initGetAgeByDateOfBirthWhenPressTextBox: function (strId, strUrl) {
        $(strId).keyup(function () {
            dob = $(strId).val();
            console.log(dob);
            if (dob != '') {
                $.ajax({
                    url: strUrl,
                    async: true,
                    cache: false,
                    type: "POST",
                    dataType: "json",
                    data: { dob: dob },
                    error: function () {
                        console.log("Error");
                    },
                    success: function (responseText) {
                        if(responseText.Age >0)
                        {
                            $("#Age").val(responseText.Age);
                            $("#ageType").val(responseText.Sex);
                        }
                        else{
                            $("#Age").val('');
                            $("#ageType").val('y');
                        }
                    }
                });
            }
        });
    },
    initGetAgeByDateOfBirth: function (strId, strUrl) {
        dob = $(strId).val();
        if (dob != '') {
            $.ajax({
                url: strUrl,
                async: true,
                cache: false,
                type: "POST",
                dataType: "json",
                data: { dob: dob },
                error: function () {
                    console.log("Error");
                },
                success: function (responseText) {
                    if(responseText.Age >0){
                        $("#Age").val(responseText.Age);
                        $("#ageType").val(responseText.ageType);
                    }else{
                            $("#Age").val('');
                            $("#ageType").val('y');
                        }
                }
            });
        }
    }
};