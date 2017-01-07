$datepicker = {
    minDate: new Date(2016, 1 - 1, 1),
    dateFormat: "dd/M/yy",
    numberOfMonths: 3,
    setDateFormat: function(strVal){
        this.dateFormat = strVal;
    },
    initDate: function(strClss){
        if (typeof strClss == "undefined") strClss = "#strDate";
        var $this = this;
        if ($Common.checkDom(strClss)) {
            $(strClss).datepicker({
                dateFormat: $this.dateFormat,
            });
        }
    },
    initRangeMonth: function (preMonth, curMonth) {
        if (typeof preMonth == "undefined") preMonth = "#preMonth";
        if (typeof curMonth == "undefined") curMonth = "#curMonth";
        var $this = this;
        if ($Common.checkDom(preMonth) && $Common.checkDom(curMonth)) {
            $(preMonth).datepicker({
                dateFormat: "yy.mm",
                changeMonth: true,
            });
            $(curMonth).datepicker({
                dateFormat: "yy.mm",
                changeMonth: true,
            });
        }
    },
    initRangeDate: function (strFrom, strTo) {
        if (typeof strFrom == "undefined") strFrom = "#startDate";
        if (typeof strTo == "undefined") strTo = "#endDate";
        var $this = this;
        if ($Common.checkDom(strFrom) && $Common.checkDom(strTo)) {
            $(strFrom).datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                dateFormat: $this.dateFormat,
                minDate: $this.minDate,
                numberOfMonths: $this.numberOfMonths,
                onClose: function (selectedDate) {
                    $(strTo).datepicker("option", "minDate", selectedDate);
                }
            });
            $(strTo).datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                dateFormat: $this.dateFormat,
                minDate: $this.minDate,
                numberOfMonths: $this.numberOfMonths,
                onClose: function (selectedDate) {
                    $(strFrom).datepicker("option", "maxDate", selectedDate);
                }
            });
        }
    }
};