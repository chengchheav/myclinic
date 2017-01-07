/*For store the function all together page
* Published Date 22-10-2015
* Developed Team RoogRog.com*/
var $Common = {
    strBoxContainerID: '',
    strFormValidate: '',
    strErrorBrd: '',
    strDIVReplace: '',
    strClassLazy: 'lazy',
    strClassTransparent: 'transparent',
    strHaveAlert: 0,
    strEnterSubmit: 0,
    intTabIndex: 0,
    intErrorForm: 0,
    intHaveErrorMSG: 0,
    intHaveErrorCLS: 0,
    intTotalControl: 0,
    intStartValidate: 1,
    objMsgTimer: null,
    strDefaultImage: "",

    initMouseHoveNavigation: function (strClass) {
        if (typeof strClass === "undefined") strClass = "#navigation ul li .sub";
        $(strClass).mouseover(function () {
            $(this).addClass("block");
        }).mouseout(function () {
            $(this).removeClass("block");
        });
    },

    /*For show action on buttom page*/
    setShowActionInButtom: function (strContainer) {
        if ($(strContainer + ' #image-loading').size() > 0) {
            $(strContainer + ' #image-loading').show();
        }
    },
    setTitleMessageBox: function (strID, strVal) {
        $(strID).html("<p>" + strVal + "</p>");
    },
    /*For hide action on buttom page*/
    setHiddenActionInButtom: function (strContainer) {
        if ($(strContainer + ' #image-loading').size() > 0) {
            $(strContainer + ' #image-loading').hide();
        }
    },
    /*Handle the onError event for the image to reassign its source*/
    setDefaultImg: function (image) {
        image.onerror = "";
        image.src = $Common.strDefaultImage;
        return true;
    },
    /*For bind lazy load*/
    bindLazyLoadImage: function (strID) {
        var $q = function (q, res) {
            if (document.querySelectorAll) {
                res = document.querySelectorAll(q);
            } else {
                var d = document
                        , a = d.styleSheets[0] || d.createStyleSheet();
                a.addRule(q, 'f:b');
                for (var l = d.all, b = 0, c = [], f = l.length; b < f; b++) {
                    l[b].currentStyle.f && c.push(l[b]);
                }

                a.removeRule(0);
                res = c;
            }
            return res;
        }
        , addEventListener = function (evt, fn) {
            window.addEventListener
                    ? this.addEventListener(evt, fn, false)
                    : (window.attachEvent)
                    ? this.attachEvent('on' + evt, fn)
                    : this['on' + evt] = fn;
        }
        , _has = function (obj, key) {
            return Object.prototype.hasOwnProperty.call(obj, key);
        };

        function loadImage(el, fn) {
            var img = new Image();
            var src = el.getAttribute('data-src');
            var cls = el.getAttribute('class');
            img.onload = function () {
                if (!!el.parent) {
                    el.parent.replaceChild(img, el)
                } else {
                    el.src = src;
                    el.classList.remove(strID);
                }
                fn ? fn() : null;
            }
            img.src = src;
        }

        function elementInViewport(el) {
            var rect = el.getBoundingClientRect()
            return (rect.top >= 0 && rect.left >= 0 && rect.top <= (window.innerHeight || document.documentElement.clientHeight))
        }
        var images = new Array()
                , query = $q('img.' + strID)
                , processScroll = function () {
                    for (var i = 0; i < images.length; i++) {
                        if (elementInViewport(images[i])) {
                            loadImage(images[i], function () {
                                images.splice(i, i);
                            });
                        }
                    }
                    ;
                }
        ;
        // Array.prototype.slice.call is not callable under our lovely IE8 
        for (var i = 0; i < query.length; i++) {
            images.push(query[i]);
        }
        ;
        processScroll();
        addEventListener('scroll', processScroll);
    },
    initNavigation: function (strId) {
        if (typeof strId === "undefined") strId = "#nav-menu";
        var postScroll = 0;
        if (this.checkDom(strId)) {
            $(strId).bind("click", function () {
                if ($('.drop-menu:visible').length) {
                    $("#main").removeClass("open-nav");
                    $RoogRogScroll.initScrollPositionPage(postScroll);
                    $('.drop-menu').hide("fast", function () {
                        $("#active-menu").hide();
                    });
                    $Common.setScrollPage();
                } else {
                    postScroll = $RoogRogScroll.intPositionScroll;
                    $('.drop-menu').show("fast", function () {
                        $("#active-menu").show();
                        $("#main").addClass("open-nav");
                    });
                    $Common.setUnscrollPage();
                }
            });
        }

        var strLink = $(".drop-menu").data("href");
        $('.drop-menu ul>li').each(function () {
            $(this).bind("click", function () {
                var strKey = $(this).data("value");
                var strRedirect = strLink + "/" + strKey;
                $Common.setRedirectPage(strRedirect);
            });
        });

    },
    getClearSpace: function (strVal) {
        return strVal.replace(/\s+/g, '');
    },
    /*For protect no space when input*/
    initProtectSpaceKey: function (strId) {
        if (this.checkDom(strId)) {
            $(strId).keyup(function (e) {
                if (e.keyCode == 32) {
                    var strVal = $(this).val();
                    $(this).val($Common.getClearSpace(strVal));
                }
            });

            $(strId).bind('paste change', function () {
                var strVal = $(this).val();
                $(this).val($Common.getClearSpace(strVal));
            });
        }
    },
    /*For set class zoom for page*/
    setZoomPage: function (strVal, strID) {
        if (typeof strID === "undefined") strID = "#zoom-pge";
        if (strVal === "small") {
            if ($(strID).hasClass('zoom-big')) {
                $(strID).removeClass('zoom-big');
            }
            $(strID).addClass('zoom-small');
        } else if (strVal === "big") {
            if ($(strID).hasClass('zoom-small')) {
                $(strID).removeClass('zoom-small');
            }
            $(strID).addClass('zoom-big');
        }
    },
    /*For init the facebook share
    * strClass is class name not dot*/
    initFacebookShare: function (strClass) {
        if (typeof strClass === "undefined") strClass = "social-share";
        var classItem = "." + strClass;
        if (this.checkDom(classItem)) {
            $(classItem).each(function () {
                $(this).bind("click", function () {
                    var strShareLink = $(this).data("share");
                    window.open(strShareLink, 'sharer', 'toolbar=0,status=0,width=548,height=325');
                });
                $(this).removeClass(strClass);
            });
        }
    },
    /*For share the link by facebook
    * strClass is class name not dot*/
    initFBShareFull: function (strShareLink) {
        if (this.checkNotEmpty(strShareLink)) {
            window.open(strShareLink, 'sharer', 'toolbar=0,status=0,width=548,height=325');
        }
    },
    /*For init the link page
    * strClass is class name not dot*/
    initHrefActionBox: function (strClass) {
        if (typeof strClass === "undefined") strClass = "event-href";
        var classItem = "." + strClass;
        if (this.checkDom(classItem)) {
            $(classItem).each(function () {
                $(this).bind("click", function () {
                    var strLink = $(this).data("href");
                    $Common.setRedirectPage(strLink);
                });
                $(this).removeClass(strClass);
            });
        }
    },

    /*For init the menu
    * strClass is class name not dot*/
    initManuSwitchHeader: function (strClass) {
        if (typeof strClass === "undefined") strClass = "#menu-switch .inner";
        if (this.checkDom(strClass)) {
            $(strClass).each(function () {
                $(this).bind("click", function () {
                    var strLink = $(this).children("a").attr("href");
                    $Common.setRedirectPage(strLink);
                });
            });
        }
    },
    /*For init the tooltip for tag*/
    initJqueryTooltip: function () {
        if (typeof $('.ttp-top').tooltip === "function") {
            $('.ttp-top').tooltip({
                tooltipClass: "top",
                content: function () {
                    var element = $(this);
                    return element.attr("title");
                },
                position: {
                    my: "center bottom-20",
                    at: "center top",
                    using: function (position, feedback) {
                        $(this).css(position);
                        $("<div>")
                                .addClass("arrow")
                                .addClass(feedback.vertical)
                                .addClass(feedback.horizontal)
                                .appendTo(this);
                    }
                }
            });

            $('.ttp-right').tooltip({
                tooltipClass: "right",
                content: function () {
                    var element = $(this);
                    return element.attr("title");
                },
                position: {
                    my: 'left center',
                    at: 'right+10 center',
                    using: function (position, feedback) {
                        $(this).css(position);
                        $("<div>")
                                .addClass("arrow")
                                .addClass(feedback.vertical)
                                .addClass(feedback.horizontal)
                                .appendTo(this);
                    }
                }
            });

            $('.ttp-bottom').tooltip({
                tooltipClass: "bottom",
                content: function () {
                    var element = $(this);
                    return element.attr("title");
                },
                position: {
                    my: 'center top',
                    at: 'center bottom+10',
                    using: function (position, feedback) {
                        $(this).css(position);
                        $("<div>")
                                .addClass("arrow")
                                .addClass(feedback.vertical)
                                .addClass(feedback.horizontal)
                                .appendTo(this);
                    }
                }
            });

            $('.ttp-left').tooltip({
                tooltipClass: "left",
                content: function () {
                    var element = $(this);
                    return element.attr("title");
                },
                position: {
                    my: "right top",
                    at: "left-10 top-5",
                    using: function (position, feedback) {
                        $(this).css(position);
                        $("<div>")
                                .addClass("arrow")
                                .addClass(feedback.vertical)
                                .addClass(feedback.horizontal)
                                .appendTo(this);
                    }
                }
            });
        }
    },
    /*For resize topup box when screen change*/
    setResizeTopupBox: function () {
        if ($('#cboxOverlay').is(':visible')) {
            $.colorbox.resize({ width: '100%', height: '100%' });
        }
    },
    /*For init navigation page*/
    initNavigationTemplate: function () {
        $('nav#menu').mmenu();
    },

    /*For show dom when page load complete
    * strID is id or class of dom*/
    initShowDomLoadPageComplete: function (strID) {
        if (this.checkNotEmpty(strID)) {
            $(window).load(function () {
                if ($Common.checkDom(strID)) {
                    $(strID).show();
                }
            });
        }
    },
    clearContainDOM: function (strID) {
        if ($(strID).size() > 0) {
            $(strID).html("");
        }
    },
    getClearSpace: function (strVal) {
        return strVal.replace(/\s+/g, '');
    },
    /*For protect no space when input*/
    initProtectSpaceKey: function (strId) {
        if (this.checkDom(strId)) {
            $(strId).keyup(function (e) {
                if (e.keyCode == 32) {
                    var strVal = $(this).val();
                    $(this).val($Common.getClearSpace(strVal));
                }
            });

            $(strId).bind('paste change', function () {
                var strVal = $(this).val();
                $(this).val($Common.getClearSpace(strVal));
            });
        }
    },
    removeDOM: function (strID) {
        if ($(strID).size() > 0) {
            $(strID).remove();
        }
    },
    addDOM: function (strID, strHTML) {
        if ($(strID).size() > 0) {
            $(strID).html(strHTML);
        }
    },
    appendDOM: function (strID, strIDDom, strHTML) {
        if ($(strID).size() > 0) {
            if ($(strIDDom).size() > 0) {
                $(strIDDom).remove();
            }
            $(strID).append(strHTML);
        }
    },
    setHtml: function (strID, strValue) {
        if (this.checkNotEmpty(strValue)) {
            $(strID).html(strValue);
        }
    },
    setHtmlAppend: function (strID, strValue) {
        if (this.checkNotEmpty(strValue)) {
            $(strID).append(strValue);
        }
    },
    setHtmlPrepend: function (strID, strValue) {
        if (this.checkNotEmpty(strValue)) {
            $(strID).prepend(strValue);
        }
    },
    setTooltipMessage: function (strClass) {
        if (typeof strClass == "undefined") strClass = ".tooltip-msg";
        if (this.checkDom(strClass)) {
            $(strClass).tooltip({
                show: {
                    effect: "slideDown",
                    delay: 250
                }
            });
        }
    },
    getFullDateTime: function () {
        var currentdate = new Date();
        var intDay = currentdate.getDate();
        var intMonth = currentdate.getMonth() + 1;
        var intYear = currentdate.getFullYear();
        var intHours = currentdate.getHours();
        var intMinute = currentdate.getMinutes();
        var intSecond = currentdate.getSeconds();
        var datetime = "Last update: " + (intDay < 10 ? "0" : "") + intDay + "/"
                + (intMonth < 10 ? "0" : "") + intMonth + "/"
                + currentdate.getFullYear() + " "
                + (intHours < 10 ? "0" : "") + intHours + ":"
                + (intMinute < 10 ? "0" : "") + intMinute + ":"
                + (intSecond < 10 ? "0" : "") + intSecond;

        return datetime;
    },
    setRemoveScriptJavascriptBySearchContent: function (strSearch) {
        if (typeof strSearch == "undefined") strSearch = "frmFormValidator";
        if ($("script").size() > 0) {
            $("script").each(function () {
                if (this.innerHTML.length > 0) {
                    var googleScriptRegExp = new RegExp(strSearch);
                    if (this.innerHTML.match(googleScriptRegExp) && this.innerHTML.indexOf("innerHTML") == -1)
                        $(this).remove();
                }
            });
        }
    },
    /*For set content have new record*/
    setTotalNewResult: function ($strTotal) {
        this.setHtml(".scrollToTop #total-result", $strTotal);
    },
    setAddClassNew: function () {
        if (!$('body').hasClass('have-new')) {
            $('body').addClass('have-new');
        }
    },
    setRemoveClassNew: function () {
        if ($('body').hasClass('have-new')) {
            $('body').removeClass('have-new');
        }
    },
    setShowActionOnPage: function (strID) {
        var strContainer = strID;
        if ($Common.checkDom(strID + ' #image-loading')) {
            $(strID + ' #image-loading').show();
        } else {
            $(strID).append('<div class="image-loading" id="image-loading" style="display:block;"></div>');
        }
        if ($(strID + ' .trans-box').size() > 0) {
            strContainer = strID + ' .trans-box';
        }
        if (!$(strContainer).hasClass('transparent')) {
            $(strContainer).addClass('transparent');
        }
    },
    setHiddenActionOnPage: function (strID) {
        var strContainer = strID;
        if ($Common.checkDom(strID + ' #image-loading')) {
            $(strID + ' #image-loading').remove();
        }
        if ($(strID + ' .trans-box').size() > 0) {
            strContainer = strID + ' .trans-box';
        }
        if ($(strContainer).hasClass('transparent'))
            $(strContainer).removeClass('transparent');
    },
    setShowActionInPage: function (strID) {
        var strContainer = strID;
        if ($(strID + ' .sub-loading').size() < 1) {
            $(strID).append('<div class="sub-loading" id="sub-loading"></div>');
        }
        if ($(strID + ' div.trans-in').size() > 0) {
            strContainer = strID + ' div.trans-in';
        }
        if (!$(strContainer).hasClass('transparent')) {
            $(strContainer).addClass('transparent');
        }
    },
    setHiddenActionInPage: function (strID) {
        var strContainer = strID;
        if ($(strID + ' .sub-loading').size() > 0) {
            $(strID + ' .sub-loading').remove();
        }
        if ($(strID + ' div.trans-in').size() > 0) {
            strContainer = strID + ' div.trans-in';
        }
        if ($(strContainer).hasClass('transparent'))
            $(strContainer).removeClass('transparent');
    },
    /*For replace the special charactor*/
    replaceEscapeCharacter: function (strVal) {
        return strVal.replace(/[-[\]{}()*+?.,\\^$|#\s]/g, "\\$&");
    },
    setShowActionInFrame: function (strID) {
        var strContainer = strID;
        if ($(strID + ' .sub-loading').size() < 1) {
            $(strID).append('<div class="sub-loading" id="sub-loading"></div>');
        }
    },
    setHiddenActionInFrame: function (strID) {
        var strContainer = strID;
        if ($(strID + ' .sub-loading').size() > 0) {
            $(strID + ' .sub-loading').remove();
        }
    },
    setRedirectPage: function (strURL) {
        if (this.getTrim(strURL) != "") {
            window.location.href = strURL;
        }
    },
    /*For redirect with new tab*/
    setRedirectNewTab: function (strURL) {
        if (this.getTrim(strURL) !== "") {
            window.open(strURL, '_blank');
        }
    },
    setRefreshPage: function () {
        location.reload();
    },
    /*For convert string to integer*/
    convertStringToInteger: function (strVal) {
        return parseInt(strVal);
    },
    /*For get value from global parameter with switch language
    *strVal is key of array*/
    getMessage: function (strVal) {
        if ((typeof strMessage !== "undefined" && strMessage !== null) && (strVal !== null && $.trim(strVal) != "")) {
            return this.getTrim(strMessage[strVal][this.getLanguageURL()]);
        } else {
            return "";
        }
    },
    /*For get value from globle paramenter
    *strVal is key of array*/
    getMsgValue: function (strVal) {
        if ((typeof strMessage !== "undefined" && strMessage !== null) && (strVal !== null && $.trim(strVal) != "")) {
            return this.getTrim(strMessage[strVal]);
        } else {
            return "";
        }
    },
    getKeyPge: function (strVal) {
        if ((typeof $RoogRogPage !== "undefined")) {
            if ((typeof $RoogRogPage.jsnMsg !== "undefined" && $RoogRogPage.jsnMsg !== null) && (strVal !== null && $.trim(strVal) != "")) {
                return this.getTrim($RoogRogPage.jsnMsg[strVal]);
            }
        } else {
            return "";
        }
    },

    getMsgPge: function (strVal) {
        if ((typeof $RoogRogPage !== "undefined")) {
            if ((typeof $RoogRogPage.jsnMsg !== "undefined" && $RoogRogPage.jsnMsg !== null) && (strVal !== null && $.trim(strVal) != "")) {
                return this.getTrim($RoogRogPage.jsnMsg[strVal][this.getLanguageURL()]);
            }
        } else {
            return "";
        }
    },
    /*For create full url with ssl*/
    getFullUrlSSL: function (strPath) {
        var strReturn = strPath;
        var strHost = window.location.hostname;
        if (this.checkNotEmpty(strPath)) {
            strReturn = "https://" + strHost + strPath;
        }
        return strReturn;
    },
    /*For create full url with not ssl*/
    getFullUrlNotSSL: function (strPath) {
        var strReturn = strPath;
        var strHost = window.location.hostname;
        if (this.checkNotEmpty(strPath)) {
            strReturn = "http://" + strHost + strPath;
        }
        return strReturn;
    },
    getConvertLowercase: function (strVal) {
        strReturn = "";
        strVal = $.trim(strVal);
        if (strVal != "") {
            strReturn = strVal.toLowerCase();
        }
        return strReturn;
    },
    getPrefixZero: function (strVal) {
        strReturn = strVal;
        if (strVal < 10) {
            strReturn = '0' + strVal;
        }
        return strReturn;
    },
    /*For formate the currency*/
    getFormateCurrency: function (strVal, intTotal, strSign) {
        if (typeof intTotal === "undefined") intTotal = 2;
        if (typeof strSign === "undefined") strSign = "";
        var strReturn = "0.00";
        if (this.checkNotEmpty(strVal)) {
            var num = parseFloat(strVal);
            strReturn = num.toFixed(intTotal);
        }
        return strSign + strReturn;
    },
    getPrefixDay: function (strVal) {
        strReturn = strVal;
        var suffixes = ["th", "st", "nd", "rd", "th", "th", "th", "th", "th", "th", "th", "th", "th", "th", "th", "th", "th", "th", "th", "th", "th", "st", "nd", "rd", "th", "th", "th", "th", "th", "th", "th", "st"];
        if (strVal >= 1 && strVal <= 31) {
            strReturn = strReturn + suffixes[strVal];
        }
        return strReturn;
    },
    getDomainURL: function (strURL) {
        return strURL.split('/')[2];
    },
    getLanguageURL: function () {
        var strLan = "en";
        if (window.location.href.indexOf('km') > -1) {
            strLan = "km";
        };
        return strLan;
    },
    getCountDOM: function (strDom) {
        return $(strDom).size();
    },
    getTrim: function (strVal) {
        return $.trim(strVal);
    },
    getValue: function (strId) {
        var strReturn = "";
        if (this.checkDom(strId)) {
            strReturn = $(strId).val();
        }
        return strReturn;
    },
    getFullURL: function () {
        return window.location.href;
    },
    getLowercase: function (strVal) {
        if (typeof strVal == "undefined") strVal = "";
        return this.getTrim(strVal.toLowerCase());
    },
    setDisableButtomControl: function (idDom) {
        $(idDom).undelegate('click');
        $(idDom).attr('disabled', 'disabled');

    },
    setHiddenAllChildElement: function (idDom) {
        if ($(idDom + ' div').size() > 1) {
            $(idDom + ' div').each(function () {
                $(this).hide();
            });
        }
    },
    setAnableButtomControl: function (idDom) {
        $(idDom).removeAttr('disabled');
        $(idDom).delegate("click");
    },
    setCheckValidateControl: function (idDom, strVal) {
        $(idDom).attr('rel', strVal);
    },
    setEorrorBorder: function (intVal) {
        strErrorBrd = intVal;
    },
    setReplaceDOMDiv: function (idDom, intAlert) {
        strDIVReplace = idDom;
        strHaveAlert = intAlert
    },
    setPropertyFormSubmit: function (strPlace, intAlert) {
        strDIVReplace = strPlace;
        strHaveAlert = intAlert;
    },
    setAddClassControl: function (idDom) {
        if (strErrorBrd == 0) {
            idDom.addClass(function (index) {
                var cls = idDom.attr('class');
                var pattern = /error/;
                if (!pattern.test(cls)) {
                    $Common.intErrorForm = 1;
                    $Common.intHaveErrorCLS = 1;
                    return "shadow-error";
                }
            });
        }
        idDom.focus();
    },
    setAddErrorControl: function (strID) {
        $(strID).addClass(function (index) {
            var cls = $(this).attr('class');
            var pattern = /error/;
            if (!pattern.test(cls)) {
                return "error";
            }
        });
    },
    setAddClassIgnoreControl: function (idDom) {
        idDom.addClass(function (index) {
            var cls = idDom.attr('class');
            var pattern = /ignore/;
            if (!pattern.test(cls)) {
                return "ignore";
            }
        });
    },
    setRemoveClassIgnoreControl: function (idDom) {
        idDom.removeClass('ignore');
    },
    setAppendClass: function (strID, strClass) {
        if ($(strID).size() > 0) {
            $(strID).addClass(strClass);
        }
    },
    setProtectSlashPath: function (strVal) {
        /*if(strVal != ''){
        strVal = strVal.replace(/\\/g, '/');
        }*/
        return strVal;
    },
    setProtectSlashURL: function (strVal) {
        return strVal;
    },
    //For set the value for controller 
    setValueController: function (strID, strVal) {
        if (this.checkDom(strID)) {
            $(strID).val(this.getTrim(strVal));
        }
    },
    setAddClassBackGroundControl: function (idDom, strColor) {
        if (idDom != '') {
            idDom.css('background-color', strColor);
        }
    },
    setRemoveClassControl: function (idDom) {
        $(idDom).removeClass('shadow-error');
        this.intHaveErrorCLS = 0;
    },
    setAddMSGForControl: function (idDom, strMSG) {
        var $this = this;
        if ($.trim($(idDom).attr('data-type')) != 'group') {
            $(idDom).parent().contents().filter(function () { return this.nodeType != 1; }).remove();
            //var arrStrings = $( idDom ).parent().html().split("<span></span>");
            $this.setRemoveMSGForControl(idDom);
            if ($(idDom).parent().find("div.msg").length == 0) {
                $(idDom).parent().append("<div class='msg'>" + strMSG + "</div>");
            } else {
                this.setRemoveMSGForControl(idDom);
                $(idDom).parent().append("<div class='msg'>" + strMSG + "</div>");
            }
        } else {
            if ($(idDom).parents('.box-grp').children('div.msg').length == 0) {
                $(idDom).parents('.box-grp').append("<div class='msg'>" + strMSG + "</div>");
            }
        }
        this.intErrorForm = 1;

    },
    setRemoveMSGForControl: function (idDom) {
        var $this = this;
        var intHave = 0;
        if ($.trim($(idDom).attr('data-type')) != 'group') {
            $(idDom).parent().contents().filter(function () { return this.nodeType != 1; }).remove();
            if ($(idDom).parent().find("div.msg").length > 0) {
                $(idDom).parent().find("div.msg").remove();
            }
        } else {
            $(idDom).parents('.box-grp').find('.ctr').each(function () {
                if ($.trim($(this).val()) == '') {
                    intHave = 1;
                }
            });
            if (intHave == 0) {
                $(idDom).parents('.box-grp').find('div.msg').each(function () {
                    $(this).remove();
                });
            }
        }
    },
    checkFunction: function () {
        var strReturn = false;
        if (typeof setTransactionFunctions !== 'undefined' && $.isFunction(setTransactionFunctions)) {
            strReturn = true;
        }
        return strReturn;
    },
    checkDom: function (strID) {
        var strReturn = false;
        if ($(strID).size() > 0) {
            strReturn = true;
        }
        return strReturn;
    },
    checkNotEmpty: function (strValue) {
        var strReturn = false;
        if (this.getTrim(strValue) !== "") {
            strReturn = true;
        }
        return strReturn;
    },
    /*For check internet connection*/
    checkNetConnection: function () {
        var strLang = this.getLanguageURL();
        // Handle IE and more capable browsers
        var xhr = new (window.ActiveXObject || XMLHttpRequest)("Microsoft.XMLHTTP");
        var status;
        // Open new request as a HEAD to the root hostname with a random param to bust the cache
        xhr.open("HEAD", "//" + window.location.hostname + "/" + strLang + "?rand=" + Math.floor((1 + Math.random()) * 0x10000), false);
        // Issue request and handle response
        try {
            xhr.send();
            return (xhr.status >= 200 && xhr.status < 300 || xhr.status === 304);
        } catch (error) {
            return false;
        }
    },
    checkExistImageUrl: function (strURL, strCallback) {
        var img = new Image();
        img.onload = function () {
            strCallback(true);
        };
        img.onerror = function () {
            strCallback(false);
        };
        img.src = strURL;
    },
    bindEventcheckExistImageUrl: function (strClass) {
        if (typeof strClass === "undefined") strClass = "img.main-img";
        var $this = this;
        $(strClass).each(function () {
            $this.checkExistImageUrl($(this).attr('src'), function (strVal) {
                if (strVal == false) {
                    $(this).attr('src', $this.getMessage('default-image-news'));
                }
            });
        });
    },
    checkEmpty: function (strValue) {
        var strReturn = false;
        if (this.getTrim(strValue) === "") {
            strReturn = true;
        }
        return strReturn;
    },
    setUnscrollPage: function () {
        $('body').addClass('stop-scrolling');
        $('body').bind('touchmove', function (e) { e.preventDefault(); });
        $('body').unbind('touchmove');
    },
    setScrollPage: function () {
        $('body').removeClass('stop-scrolling');
        $('body').bind('touchmove', function () { return true; });
        $('body').bind('touchmove');
    },
    //For check json formate
    checkJSONFormat: function (strVal) {
        try {
            //JSON.parse(strVal);
            jQuery.parseJSON(strVal);
        } catch (e) {
            return false;
        }
        return true;
    },
    checkProperyObject: function (strKey, objValue) {
        strReturn = false;
        if (objValue.hasOwnProperty(strKey)) {
            strReturn = true;
        }
        return strReturn;
    },
    setDefaultMainImage: function (strVal) {
        var strLang = this.getLanguageURL();
        if ($(strVal).size() > 0) {
            $(strVal).attr('src', this.getMessage('default-main-image'));
        }
    },
    checkHaveErrorMSGForControl: function (idDom) {

        var $this = this;
        if ($.trim($(idDom).attr('data-type')) != 'group') {
            if ($(idDom).parent().contents().filter(function () { return this.nodeType != 1; })) {
                this.intHaveErrorMSG = 1;
            }
            if ($(idDom).parent().find("div.msg").length > 0) {
                this.intHaveErrorMSG = 1;
            }
        } else {
            if ($(idDom).parents('.box-grp').children('div.msg').length > 0) {
                this.intHaveErrorMSG = 1;
            }
        }
    },
    setLinkRedirect: function (strClass) {
        if (typeof strClass === 'undefined') strClass = ".link-page";
        if (this.checkDom(strClass)) {
            $(strClass).bind("click", function () {
                var strLink = $(this).data('url');
                $Common.setRedirectPage(strLink);
            });
        }
    },
    checkHaveErrorCLSForControl: function (idDom) {
        var $strRet = false;
        idDom.addClass(function (index) {
            var cls = idDom.attr('class');
            var pattern = /shadow-error/;
            if (pattern.test(cls)) {
                $Common.intHaveErrorCLS = 1;
                $strRet = true;
            }
        });
        return $strRet;
    },
    /*For protect redirect when click on href that contain the # sign*/
    initProtectHref: function () {
        $('a[href=\"#\"]').click(function (event) {
            event.preventDefault();
        });
    },
    resetMenuUnactive: function (strID, strCSS) {
        $(strID + ' ul li').each(function () {
            if ($(this).hasClass('active')) {
                $(this).removeClass('active');
            }
        });
    },
    /*For append script into body*/
    appendScript: function (strVal) {
        if (this.checkNotEmpty(strVal)) {
            var s = document.createElement("script");
            s.type = "text/javascript";
            s.src = strVal;
            $("body").append(s);
        }
    },
    bindEventProtectNumberForController: function (strID) {
        if ($(strID).size() > 0) {
            $(strID).bind('keydown', function (event) {
                event = event || window.event;
                var strReturn = false;
                var strKeyCode = event.which || event.keyCode;
                if ((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 96 && event.keyCode <= 105)) {
                    strReturn = true;
                }
                if (strKeyCode == 13 || strKeyCode == 8 || strKeyCode == 46 || strKeyCode == 9 || strKeyCode == 188 || strKeyCode == 190) {
                    strReturn = true;
                }
                return strReturn;
            });
        }
    },
    concatHostUrl: function(url){
        return host + url;
    },
    concatHostDomain: function (url) {
        return hostNoLang + url;
    },
    generateJsonListImageFromSplitString: function (strListImage, strSplit) {
        if (typeof (strSplit) === 'undefined') strSplit = "|";
        var objMeta = { metadata: [] };
        if (this.getTrim(strListImage) !== '') {
            var objImage = strListImage.split(strSplit);
            var intAllow = objImage.length;
            for (i = 0; i < intAllow; i++) {
                objMeta.metadata.push({
                    "file_name": objImage[i],
                    "file_type": "image/jpeg"
                });
            }
        }
        return JSON.stringify(objMeta);
    }
}


