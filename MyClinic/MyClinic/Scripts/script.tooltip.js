$tooltip = {
    initContent: function (strClss) {
        if (typeof strClss == "undefined") strClss = "tlt";
        var $this = this;
        var strClass = "." + strClss;
        if ($Common.checkDom(strClass)) {
            $(strClass).tooltip({
                container: 'body',
                content: function () {
                    var element = $(this);
                    return "<div style='width: 100%;'>" + element.attr("title") + "</div>";
                }
            });
            $(strClass).removeClass(strClss);
        }
    },
    initReContent: function (strId) {
        var $this = this;
        if ($Common.checkDom(strId)) {
            $(strId).tooltip({
                container: 'body',
                content: function () {
                    var element = $(this);
                    return "<div style='width: 100%;'>" + element.attr("title") + "</div>";
                }
            });
        }
    }
};