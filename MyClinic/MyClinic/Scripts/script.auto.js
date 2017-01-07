$auto = {
    initSearch: function (strUrl, strName, strClss, strSet) {
        if (typeof strClss == "undefined") strClss = "#Medicine";
        if (typeof strSet == "undefined") strSet = "#MedicineId";
        if (typeof strName == "undefined") strName = "Name";
        var $this = this;
        if ($Common.checkDom(strClss)) {
            var options = {
                url: $Common.concatHostDomain(strUrl),
                getValue: strName,
                list: {
                    match: {
                        enabled: true
                    },
                    onSelectItemEvent: function () {
                        var id = $(strClss).getSelectedItemData().Id;
                        if ($Common.checkDom(strSet)) {
                            $(strSet).val(id);
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

    /*For Get List of Employee Using in Patien Add*/
    initSearchEmployee: function (strUrl, strName, strClss, strSet) {
        if (typeof strClss == "undefined") strClss = "#Diagnose_Name";
        if (typeof strSet == "undefined") strSet = "#DiagnoseBy";
        if (typeof strName == "undefined") strName = "Name";
        var $this = this;
        if ($Common.checkDom(strClss)) {
            var options = {
                url: $Common.concatHostDomain(strUrl),
                getValue: strName,
                list: {
                    match: {
                        enabled: true
                    },
                    onSelectItemEvent: function () {
                        var id = $(strClss).getSelectedItemData().Id;
                        if ($Common.checkDom(strSet)) {
                            $(strSet).val(id);
                        }
                    },
                    onKeyEnterEvent: function () {
                        var id = $(strClss).getSelectedItemData().Id;
                        if ($Common.checkDom(strSet)) {
                            $(strSet).val(id);
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

    /*For Get List of Disease Using in Diagnosis Add*/
    initSearchDisease: function (strUrl, strName, strClss, strSet) {
        if (typeof strClss == "undefined") strClss = "#Disease_Name";
        if (typeof strSet == "undefined") strSet = "#DiseaseId";
        if (typeof strName == "undefined") strName = "Name";
        var $this = this;
        if ($Common.checkDom(strClss)) {
            var options = {
                url: $Common.concatHostDomain(strUrl),
                getValue: strName,
                list: {
                    match: {
                        enabled: true
                    },
                    onSelectItemEvent: function () {
                        var id = $(strClss).getSelectedItemData().Id;
                        if ($Common.checkDom(strSet)) {
                            $(strSet).val(id);
                        }
                    },
                    onKeyEnterEvent: function () {
                        var id = $(strClss).getSelectedItemData().Id;
                        if ($Common.checkDom(strSet)) {
                            $(strSet).val(id);
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

    /*For Get List of Patient Using in Patien Add*/
    initSearchPatient: function (strUrl, strUrlDiagnosisCycle, strName, strClss, strSet) {
        if (typeof strClss == "undefined") strClss = "#Patient_Name";
        if (typeof strSet == "undefined") strSet = "#PatientId";
        if (typeof strName == "undefined") strName = "DisplayValue";
        var $this = this;
        if ($Common.checkDom(strClss)) {
            var options = {
                url: $Common.concatHostDomain(strUrl),
                getValue: strName,
                list: {
                    match: {
                        enabled: true
                    },
                    onSelectItemEvent: function () {
                        var id = $(strClss).getSelectedItemData().Id;
                        var name = $("#Patient_Name").getSelectedItemData().Name;
                        var sex = $("#Patient_Name").getSelectedItemData().Sex;
                        sex = sex == "M" ? $translator.translate('Male') : $translator.translate('Female');
                        var age = $("#Patient_Name").getSelectedItemData().Age;
                        if (typeof(age) != 'undefined') {
                            age = age + $translator.translate('year');
                        }
                        var tel = $("#Patient_Name").getSelectedItemData().Tel;

                        $("#PatientId").val(id);
                        $("#name").html(name);
                        $("#sex").html(sex);
                        $("#age").html(age);
                        $("#tel").html(tel);
                        $("#s_info").removeAttr("style");

                        if ($Common.checkDom(strSet)) {
                            $(strSet).val(id);
                        }
                        if ($(strSet).val() > 0) {
                            $.ajax({
                                url: $Common.concatHostDomain(strUrlDiagnosisCycle),
                                async: true,
                                cache: false,
                                type: "POST",
                                dataType: "json",
                                data: { id: $(strSet).val() },
                                error: function () {
                                    console.log("Error");
                                },
                                success: function (responseText) {
                                    $("#DiagnoseCycle").val(responseText.result);
                                    $("#DiagnoseCycleDis").val(responseText.result);

                                }
                            });
                        }
                    },
                    //Add by Mongkol
                    onKeyEnterEvent: function () {
                        var id = $(strClss).getSelectedItemData().Id;
                        var name = $("#Patient_Name").getSelectedItemData().Name;
                        var sex = $("#Patient_Name").getSelectedItemData().Sex;
                        sex = sex == "M" ? $translator.translate('Male') : $translator.translate('Female');
                        var age = $("#Patient_Name").getSelectedItemData().Age;
                        age = age + $translator.translate('year');
                        var tel = $("#Patient_Name").getSelectedItemData().Tel;

                        $("#PatientId").val(id);
                        $("#name").html(name);
                        $("#sex").html(sex);
                        $("#age").html(age);
                        $("#tel").html(tel);
                        $("#s_info").removeAttr("style");

                        if ($Common.checkDom(strSet)) {
                            $(strSet).val(id);
                        }
                        if ($(strSet).val() > 0) {
                            $.ajax({
                                url: $Common.concatHostDomain(strUrlDiagnosisCycle),
                                async: true,
                                cache: false,
                                type: "POST",
                                dataType: "json",
                                data: { id: $(strSet).val() },
                                error: function () {
                                    console.log("Error");
                                },
                                success: function (responseText) {
                                    $("#DiagnoseCycle").val(responseText.result);
                                    $("#DiagnoseCycleDis").val(responseText.result);

                                }
                            });
                        }
                    }
                    //Add by Mongkol
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
    }
};