$popupMsg = {
    msgConfirmRenew : "",
    msgConfirmRedeem : "",
    msgConfirmVoid : "",
    msgConfirmClose : "",
    msgConfirmSoldOut : "",
    msgConfirmRenewWithRedeemSome : "",
    msgConfirmCancelRenew : "",
    msgConfirmCancelRedeem : "",
    msgConfirmCancelSolvedOut : "",
    msgConfirmCancelClosed : "",
    msgYes : "",
    msgNo : "",
    msgExpiredDate : "",
    msgInterestEarned : "",
    msgRedeemSomeAmount: "",
    msgSellAmount: "",
    msgReason: "",
    msgValidateReason: "",

}

$popup = {
        strWidth: "450",
        actionEdit      :0 ,
        actionDetail    :1,
        actionDelete     :2,
        actionEditPrescription    :3,
        actionPrint      :4,
        actionEditPatient     :5,
        actionPatientDetail    :6,
        actionPatientHistory    :7,
        actionPatientPrintHistory    :8,
        actionDeletePatient    :9,
        actionEditMedicine    :10,
        actionMedicineDetail    :11,
        actionDeleteMedicine    :12,
        actionEditEmployee    :13,
        actionEmployeeDetail    :14,
        actionDeleteEmployee    :15,
        actionEditDisease    :16,
        actionDiseaseDetail    :17,
        actionDeleteDisease    :18,
        actionEditUser    :19,
        actionUserDetail    :20,
        actionDeleteUser    :21,
        //actionEditPrescription    :22,
        actionPrescriptionDetail    :23,
        actionDeletePrescription    :24,
        actionDiagnosisPhoto    :25,
        
        /*For Set Text to Select and init popup*/
        initSetValueSelectAndPopUpAction: function (objThis, strClass) {
        var statusNumber = $(objThis).val();
        var text = $(objThis).find("option:first").val();
        $(objThis).val(text);
        var id = $(objThis).attr("data-value");
        var name = $(objThis).attr("data-name");

        var urlEdit = $Common.concatHostUrl('/Diagnosis/Edit/' + id);
        var urlEditPatient = $Common.concatHostUrl('/Patient/Edit/' + id);
        var urlDetail = $Common.concatHostUrl('/Diagnosis/Detail/' + id);
        var urlEditPrescription = $Common.concatHostUrl('/Prescription/Edit/' + id);
        var urlPrint = $Common.concatHostUrl('/Prescription/Print/' + id);
        var urlPatientDetail = $Common.concatHostUrl('/Patient/Detail/' + id);
        var urlPatientHistory = $Common.concatHostUrl('/PatientOperation/Report/' + id);
        var urlPatientPrintHistory = $Common.concatHostUrl('/PatientOperation/ReportPrint/' + id);
        var urlEditMedicine = $Common.concatHostUrl('/Medicine/Edit/' + id);
        var urlMedicineDetail = $Common.concatHostUrl('/Medicine/Detail/' + id);
        var urlEditEmployee = $Common.concatHostUrl('/Employee/Edit/' + id);
        var urlEmployeeDetail = $Common.concatHostUrl('/Employee/Detail/' + id);
        var urlEditDisease = $Common.concatHostUrl('/Disease/Edit/' + id);
        var urlDiseaseDetail = $Common.concatHostUrl('/Disease/Detail/' + id);
        var urlEditUser = $Common.concatHostUrl('/User/Edit/' + id);
        var urlUserDetail = $Common.concatHostUrl('/User/Detail/' + id);
        var urlDiagnosisPhoto = $Common.concatHostUrl('/Diagnosis/UploadPhotoViewer/' + id);
        //var urlEditPrescription = $Common.concatHostUrl('/Prescription/Edit/' + id);
        var urlPrescriptionDetail = $Common.concatHostUrl('/Prescription/Detail/' + id);




        if (statusNumber == $popup.actionEdit) {
            $Common.setRedirectPage(urlEdit);
        }
        else if (statusNumber == $popup.actionDetail) {
            $Common.setRedirectPage(urlDetail);
        }
        else if (statusNumber == $popup.actionDelete) {
            $dialogAlert.confirmDelete('','',$translator.translate('Are you wish to delete diagnosis have patient name :',lang) + name +'?','/en/Diagnosis/DeleteConfirmed/'+id);
        }
        else if (statusNumber == $popup.actionEditPrescription) {
           $Common.setRedirectPage(urlEditPrescription);
        }
        else if (statusNumber == $popup.actionPrint) {
            $print.multiPrint(urlPrint);
        }
        else if(statusNumber == $popup.actionEditPatient)
        {
            $Common.setRedirectPage(urlEditPatient);
        }
        else if (statusNumber == $popup.actionPatientDetail) {
            $Common.setRedirectPage(urlPatientDetail);
        }
        else if (statusNumber == $popup.actionPatientHistory) {
            $Common.setRedirectPage(urlPatientHistory);
        }
        else if (statusNumber == $popup.actionPatientPrintHistory) {
            $print.multiPrint(urlPatientPrintHistory);
        }
        else if (statusNumber == $popup.actionDeletePatient) {
            $dialogAlert.confirmDelete('','',$translator.translate('Are you wish to delete patient name :',lang) + name +'?',' Patient/DeleteConfirmed/'+id);
        }
        else if (statusNumber == $popup.actionEditMedicine) {
            $Common.setRedirectPage(urlEditMedicine);
        }
        else if (statusNumber == $popup.actionMedicineDetail) {
            $Common.setRedirectPage(urlMedicineDetail);
        }
        else if (statusNumber == $popup.actionDeleteMedicine) {
            $dialogAlert.confirmDelete('','',$translator.translate('Are you wish to delete medicine name :',lang) + name +'?',' Medicine/DeleteConfirmed/'+id);
        }
        else if (statusNumber == $popup.actionEditEmployee) {
            $Common.setRedirectPage(urlEditEmployee);
        }
        else if (statusNumber == $popup.actionEmployeeDetail) {
            $Common.setRedirectPage(urlEmployeeDetail);
        }
        else if (statusNumber == $popup.actionDeleteEmployee) {
            $dialogAlert.confirmDelete('','',$translator.translate('Are you wish to delete employee name :',lang) + name +'?',' Employee/DeleteConfirmed/'+id);
        }
        else if (statusNumber == $popup.actionEditDisease) {
            $Common.setRedirectPage(urlEditDisease);
        }
        else if (statusNumber == $popup.actionDiseaseDetail) {
            $Common.setRedirectPage(urlDiseaseDetail);
        }
        else if (statusNumber == $popup.actionDeleteDisease) {
            $dialogAlert.confirmDelete('','',$translator.translate('Are you wish to delete disease name :',lang) + name +'?',' Disease/DeleteConfirmed/'+id);
        }
        else if (statusNumber == $popup.actionEditUser) {
            $Common.setRedirectPage(urlEditUser);
        }
        else if (statusNumber == $popup.actionUserDetail) {
            $Common.setRedirectPage(urlUserDetail);
        }
        else if (statusNumber == $popup.actionDeleteUser) {
            $dialogAlert.confirmDelete('','',$translator.translate('Are you wish to delete user name :',lang) + name +'?',' User/DeleteConfirmed/'+id);
        }
        //else if (statusNumber == $popup.actionEditPrescription) {
        //    $Common.setRedirectPage(urlEditPrescription);
        //}
        else if (statusNumber == $popup.actionPrescriptionDetail) {
            $Common.setRedirectPage(urlPrescriptionDetail);
        }
        else if (statusNumber == $popup.actionDeletePrescription) {
            $dialogAlert.confirmDelete('','',$translator.translate('Are you wish to delete Prescription have patient name :',lang) + name +'?',' Prescription/DeleteConfirmed/'+id);
        }
        else if (statusNumber == $popup.actionDiagnosisPhoto) {
            $Common.setRedirectPage(urlDiagnosisPhoto);
        }
        else
            console.log("Status Not Defined");
        }
}