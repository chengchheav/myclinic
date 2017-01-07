$autoComplete = {
    /*For Search auto complete Name Using in Diagnosis Add*/
    initSearch: function (strId, strSetId, arrName) {
        $(strId).autocomplete({
            source: arrName,
            autoFocus: true,
            select: function (event, ui) {
                //console.log(ui.item.label);
                //console.log(ui.item.Id);
                $(strSetId).val(ui.item.Id);
            }
        });
    },
    /*For Get List of Patient Using in Patien Add*/
    initSearchPatient: function (strId, strSetId, arrName,strUrlDiagnosisCycle) {
        $(strId).autocomplete({
            source: arrName,
            autoFocus: true,
            select: function (event, ui) {
                var id = (ui.item.label).split("-");
                //alert( id);
                $(strSetId).val(id[0]);

                /*For Get DiagnosisCycle*/
                if ($(strSetId).val() > 0) {
                    $.ajax({
                        url: $Common.concatHostDomain(strUrlDiagnosisCycle),
                        async: true,
                        cache: false,
                        type: "POST",
                        dataType: "json",
                        data: { id: $(strSetId).val() },
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
        });
    },
    
};