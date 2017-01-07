/*
	Author: Mongkol
	Date:	22/07/2016
	Module:	Event Handler
*/
var $key = {
    initKeypress: function (elmId, valChangeElmId) {
        console.log(valChangeElmId);
        if (document.getElementById(elmId).addEventListener) {
            document.getElementById(elmId).addEventListener("keypress", keypress, false);
        }
        else if (document.getElementById(elmId).attachEvent) {
            document.getElementById(elmId).attachEvent("onkeypress", keypress);
        }
        else {
            document.getElementById(elmId).onkeypress = keypress;
        }

        function keypress(e) {
            
            if (!e) e = event;
            if (e.keyCode == 8 || e.keyCode == 46) {
                document.getElementById(valChangeElmId).value = 0;

            }
        }
    },
    /*User For Protect Medicine in Prescription Edit*/
    initKeypressForMedicine: function (elmId, valChangeElmId, valChangeElmName) {
        console.log(valChangeElmName);
        if (document.getElementById(elmId).addEventListener) {
            document.getElementById(elmId).addEventListener("keypress", keypress, false);
        }
        else if (document.getElementById(elmId).attachEvent) {
            document.getElementById(elmId).attachEvent("onkeypress", keypress);
        }
        else {
            document.getElementById(elmId).onkeypress = keypress;
        }

        function keypress(e) {
            console.log(valChangeElmId);
            if (!e) e = event;
            if (e.keyCode == 8 || e.keyCode == 46) {
                document.getElementById(valChangeElmId).value = 0;
                document.getElementById(valChangeElmName).value = '';
            }
        }
    }
}