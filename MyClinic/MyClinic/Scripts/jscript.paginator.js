$(document).ready(function () {
    //$paging.pagingClick(1,0);
});

var $paging = {
    strForm: "#frmSearch",
    setPagingForm: function (strID) {
        if (typeof strID == "undefined") strID = "#frmSearch";
        this.strForm = strID;
        if ($('#pageSizePage').size() > 0) {
            var strPageSize = 10;
            if ($('#pageSize').size() > 0) {
                strPageSize = $('#pageSize').val();
            }
            $('#pageSizePage').val(strPageSize);
        }
    },
    pagingClick: function (pageNo, pageStatus) {
        $('#pageNo').val(pageNo);
        $paging.submitForm(this.strForm);
    },
    pageSizeChange: function () {
        $('#pageNo').val(1);
        if ($('#pageSizePage').size() > 0) {
            $('#pageSizePage').val($('#pageSize').val());
        }
        $paging.submitForm(this.strForm);
    },
    orderByClick: function (orderBy, order) {
        $('#pageNo').val(1);
        $paging.submitForm(this.strForm);
    },
    submitForm: function (form) {
        $(form).submit();
    }
};