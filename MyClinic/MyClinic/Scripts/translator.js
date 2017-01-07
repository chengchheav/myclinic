$translator = {

    translate: function (englishText, strLang) {
        if (typeof strLang == "undefined") strLang = lang;
        result = englishText;
        if (strLang == "km") {
            var scriptLen = $translator.script.length;
            for (i = 0; i < scriptLen; i++) {
                var scriptText = $translator.script[i].en;
                if (scriptText == englishText) {
                    result = $translator.script[i].km;
                    break;
                }
            }
        }
        return result;
    },

    script: [

        { en: "Edit", km: "កែប្រែ" },
        { en: "Remove", km: "ដកចេញ" },
        { en: "---Waiting---", km: "--- រង់ចាំ ---" },
        { en: "---Select One---", km: "--- ជ្រើសរើសមួយ---" },
        { en: "Male", km: "ប្រុស" },
        { en: "Female", km: "ស្រី" },
        { en: "year", km: "ឆ្នាំ" },
        { en: "Not Empty", km: "មិនអាចទទេ" },
        { en: "Please select the medicine again.", km: "សូមជ្រើសរើសព័ត៌មានថ្នាំម្តងទៀត។" },
        { en: "Please add the information of medicine for diagnosis.", km: "សូមបន្ថែមព័ត៌មានថ្នាំសំរាប់វេជ្ជបញ្ជា។" },
        { en: "Are you wish to delete diagnosis have patient name :", km: "តើអ្នកចង់លុបការធ្វើរោគវិនិច្ឆ័យមានអ្នកជំងឺឈ្មោះ :" },
        { en: "Please select the medicine again.", km: "សូមជ្រើសរើសព័ត៌មានថ្នាំម្តងទៀត។" },
        { en: "This Medicine not Valid.", km: "ឈ្មោះថ្នាំនេះមិនត្រឹមត្រូវទេ។" },
        { en: "Are you wish to delete this image ?", km: "តើអ្នកពិតជាចង់លុបរូបភាពនេះ​ ?" }
        
    ]

}

