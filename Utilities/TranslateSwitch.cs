///////////////////////////////////////////////////////////////////////////////
// SAMPLE: Symmetric key encryption and decryption using Rijndael algorithm.
// 
// To run this sample, create a new Visual C# project using the Console
// Application template and replace the contents of the Class1.cs file with
// the code below.
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// 
// Copyright (C) 2002 Obviex(TM). All rights reserved.
// 
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Utilities
{
    public class TranslateSwitch
    {
        private string[,] dictMsg = null;

        public string Get(string strMsg) {
            string strReturn = strMsg;
            if (Common.currentCulture == "km") {
                if (!String.IsNullOrEmpty(strMsg))
                {
                    for (int i = 0; i < dictMsg.Length / 2; i++)
                    {
                        string key = dictMsg[i, 0].ToString();
                        string value = dictMsg[i, 1].ToString();
                        if (strMsg.Contains(key))
                        {
                            strReturn = strMsg.Replace(key, value);
                            break;
                        }
                    }
                }
                else
                {
                    strReturn = "";
                }
            }
            return strReturn;
        }

        public string Get(MvcHtmlString mvcHmtlString)
        {
            string strReturn = "";
            if (mvcHmtlString != null)
            {

                var strMsg = mvcHmtlString.ToString().Replace("&#39;", "'"); 
                strReturn = strMsg;
                if (Common.currentCulture == "km")
                {
                    if (!String.IsNullOrEmpty(strMsg))
                    {
                        for (int i = 0; i < dictMsg.Length / 2; i++)
                        {
                            string key = dictMsg[i, 0].ToString();
                            string value = dictMsg[i, 1].ToString();
                            if (strMsg.Contains(key))
                            {
                                strReturn = strMsg.Replace(key, value);
                                break;
                            }
                        }
                    }
                    else
                    {
                        strReturn = "";
                    }
                }
            }
            return strReturn;
        }

        public TranslateSwitch()
        {
            dictMsg = new string[,] { 
                { "The SymptomType field is required.", "ប្រភេទនៃរោគសញ្ញាត្រូវបានទាមទារ។" }, 
                { "The Item Category field is required.", "ប្រភេទនៃវត្ថុគឺត្រូវការមិនអាចទទេបានទេ។" }, 
                { "The Interest Rate(%) field is required.", "អត្រាការប្រាក់គឺត្រូវការមិនអាចទទេបានទេ។"},
                { "The Name field is required.", "ឈ្មោះគឺត្រូវការមិនអាចទទេបានទេ។" },
                { "The Interest Rate is not valid.", "អត្រាការប្រាក់មិនមានភាពត្រឹមត្រូវ។" },
                { "The Age must be between 1 and 100.", "អាយុត្រូវឋិតនៅចន្លោះពី1ទៅ100តែប៉ុណ្ណោះ។" },
                { "The Email is not valid.", "អ៊ីម៉ែលមិនត្រឹមត្រូវ។" },
                { "The Email can't be empty.", "អ៊ីម៉ែលមិនអាចទទេ។" },
                { "The Email is not valid (ex : example@gmail.com).", "អ៊ីម៉ែលមិនត្រឹមត្រូវ។" },
                { "The Phone can't be empty.", "លេខទូរស័ព្ទមិនអាចទទេ។" },
                { "The IdNumber can't be empty.", "លេខអត្តសញ្ញាណមិនអាចទទេ។" },
                { "The Pawner Name can't be empty.", "ឈ្មោះអ្នកបញ្ចាំមិនអាចទទេ។" },
                { "The Name can't be empty.", "ឈ្មោះមិនអាចទទេ។" },
                { "The Pawn Amount is not valid.", "ចំនួនទឹកប្រាក់បញ្ចាំមិនអាចទទេ។" },
                { "The Pawn Date is not valid.", "ការបរិច្ឆេទថ្ងៃបញ្ចាំមិនត្រឹមត្រូវ។" },
                { "The Expire Date is not valid.", "ការបរិច្ឆេទថ្ងៃហួសកំណត់មិនត្រឹមត្រូវ។" },
                { "The Contract Date is not valid.", "ការបរិច្ឆេទថ្ងៃ​ចុះកិច្ចសន្យាមិនត្រឹមត្រូវ។" },
                { "The User​​ Name can't be empty.", "ឈ្មោះសំរាប់ចូលក្នុងប្រព័ន្ធមិនអាចទទេ។" },
                { "The Password can't be empty.", "លេខសម្ងាត់មិនអាចទទេ។" },
                { "The user name or password provided is incorrect.", "ឈ្មោះសំរាប់ចូលក្នុងប្រព័ន្ធដែលអ្នកផ្តល់ឲ្យមិនត្រឹមត្រូវទេ។" },
                { "Unexpected Error", "មានបញ្ហាមិនចៃដន្យ" },
                { "UserName Exist", "ឈ្មោះសំរាប់ចូលក្នុងប្រព័ន្ធមានរួចហើយ" },
                { "The Confirm Password is not match.", "លេខសម្ងាត់ដែលបញ្ជាក់មិនផ្ទៀងផ្ទាត់គ្នាទេ។" },
                { "The current password is incorrect or the new password is invalid.", "លេខសម្ងាត់បច្ចុន្បន្នមិនត្រឹមត្រូវ រឺក៏លេខសម្ងាត់ថ្មីមិនត្រឹមត្រូវ។" },
                { "User name already exists. Please enter a different user name.", "ឈ្មោះសំរាប់ចូលក្នុងប្រព័ន្ធមានរួចរាល់ហើយ សូមបញ្ជូលមួយផ្សេងទៀត។" },
                { "A user name for that e-mail address already exists. Please enter a different e-mail address.", "ឈ្មោះសំរាប់ចូលក្នុងប្រព័ន្ធនិងអ៊ីម៉ែលមានរួចរាល់ហើយ សូមបញ្ជូលអ៊ីម៉ែលមួយផ្សេងទៀត។" },
                { "The password provided is invalid. Please enter a valid password value.", "លេខសម្ងាត់ដែលផ្តល់ឲ្យមិនត្រឹមត្រូវ សូមបញ្ជូលឡើងវិញ។" },
                { "The e-mail address provided is invalid. Please check the value and try again.", "អ៊ីម៉ែលដែលផ្តល់ឲ្យមិនត្រឹមត្រូវ សូមត្រួតពិនិត្យឡើងវិញ។" },
                { "The password retrieval answer provided is invalid. Please check the value and try again.", "ចំលើយសំរាប់យកលេខសម្ងាត់ឡើងវិញមិនត្រឹមត្រូវ សូមត្រួតពិនិត្យ និងព្យាយាមឡើងវិញ។" },
                { "The password retrieval question provided is invalid. Please check the value and try again.", "សំនួរសំរាប់យកលេខសម្ងាត់ឡើងវិញមិនត្រឹមត្រូវ សូមត្រួតពិនិត្យ និងព្យាយាមឡើងវិញ។" },
                { "The user name provided is invalid. Please check the value and try again.", "ឈ្មោះសំរាប់ចូលក្នុងប្រព័ន្ធដែលផ្តល់ឲ្យមិនត្រឹមត្រូវ​ សូមត្រួតពិនិត្យ និងព្យាយាមម្តងទៀត។" },
                { "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.", "ការផ្តល់ឲ្យនូវភាពស្របច្បាប់មានបញ្ហា សូមធ្វើការត្រួតពិនិត្យនូវការបញ្ជូលរបស់អ្នកម្តងទៀត។ ប្រសិនបើបញ្ហានៅតែកើតមានដដែល សូមធ្វើការទាក់ទងជាមួយនិងអ្នកគ្រប់គ្រងប្រព័ន្ធ។" },
                { "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.", "សំនើរការបង្កើតអ្នកប្រើត្រូវបានលុបចោល សូមធ្វើការត្រួតពិនិត្យនូវការបញ្ជូលរបស់អ្នកម្តងទៀត។ ប្រសិនបើបញ្ហានៅតែកើតមានដដែល សូមធ្វើការទាក់ទងជាមួយនិងអ្នកគ្រប់គ្រងប្រព័ន្ធ។" },
                { "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.", "បញ្ហាដែលមិនធ្លាប់ស្គាល់មួយបានកើតឡើង សូមធ្វើការត្រួតពិនិត្យនូវការបញ្ជូលរបស់អ្នកម្តងទៀត។ ប្រសិនបើបញ្ហានៅតែកើតមានដដែល សូមធ្វើការទាក់ទងជាមួយនិងអ្នកគ្រប់គ្រងប្រព័ន្ធ។" },
                { "Invalid username or password.", "ឈ្មោះសំរាប់ចូលក្នុងប្រព័ន្ធ រឺលេខសម្ងាត់មិនត្រឹមត្រូវ។" },
                { "You don't have authorized to access this system.​ Please contact to administrator.", "អ្នកមិនសិទ្ធិក្នុងការចូលប្រើប្រព័ន្ធនេះទេ សូមធ្វើការទាក់ទងជាមួយនិងអ្នកគ្រប់គ្រងប្រព័ន្ធ។" },
                { "Please enter username & password", "សូមបញ្ជូលឈ្មោះសំរាប់ចូលក្នុងប្រព័ន្ធ និងលេខសម្ងាត់" },
                { "Login was unsuccessful. Please correct the errors and try again.", "ការចូលក្នុងប្រព័ន្ធមិនទទួលបានជោគជ័យទេ។ សូមកែតម្រូវបញ្ហា បន្ទាប់មកព្យាយាមម្តងទៀត។" },
                { "Cannot connect to the Database, Please contact software provider for help!", "មិនអាចភ្ជាប់ទំនាក់ទំនងជាមួយកន្លែងផ្ទុកទិន្នន័យបានទេ​ សូមទំនាក់ទំនងមកកាន់អ្នកគ្រប់គ្រងប្រព័ន្ធសំរាប់ជំនួយ។" },
                { "You don't have an authorize to overwrite the interest rate.", "អ្នកមិនមានសិទ្ធិធ្វើការផ្លាស់ប្តូរលើអត្រាការប្រាក់នេះទេ​។" },
                { "The Patient Name Invalid, please select again. ", "ឈ្មោះអ្នកជម្ងឺមិនត្រឹមត្រូវទេ សូមជ្រើសរើសម្តងទៀត។" },
                { "The Diagnose Name Invalid, please select again. ", "ឈ្មោះអ្នកធ្វើរោគវិនិច្ឆ័យមិនត្រឹមត្រូវទេ សូមជ្រើសរើសម្តងទៀត។" },
                { "The Confirm Password is not match.", "ការបញ្ជាក់លេខសម្ងាត់មិនផ្ទៀងផ្ទាត់គ្នា។" },
                { "The Confirm Password can't be empty.", "ការបញ្ជាក់លេខសម្ងាត់មិនអាចទទេ។" },
                { "UserName Not Exist", "ឈ្មោះសំរាប់ចូលក្នុងប្រព័ន្ធ មិនមាន" },
                { "The DiagnoseCycle field is required.", "ចំនួនដងគឺចាំបាច់ត្រូវតែមានតម្លៃជាដាច់ខាត។" },
                { "The Item Number field is required.", "លេខវត្ថុគឺចាំបាច់ត្រូវតែមានតម្លៃជាដាច់ខាត។" },
                { "The Pawn Amount ($) field is required.", "ចំនួនទឹកប្រាក់បញ្ចាំគឺចាំបាច់ត្រូវតែមានតម្លៃជាដាច់ខាត។" },
                { "The Interest Rate (%) field is required.", "អត្រាការប្រាក់គឺចាំបាច់ត្រូវតែមានតម្លៃជាដាច់ខាត។" },
                { "The Item Detail field is required.", "ភាពលម្អិតនៃវត្ថុគឺចាំបាច់ត្រូវតែមានតម្លៃជាដាច់ខាត។" },
                { "The Valuer Name field is required.", "ឈ្មោះអ្នកវាយតម្លៃគឺចាំបាច់ត្រូវតែមានតម្លៃជាដាច់ខាត។" },
                { "The Guarantor field is required.", "អ្នកធានាគឺចាំបាច់ត្រូវតែមានតម្លៃជាដាច់ខាត។" },
                { "The Guar. Address field is required.", "អស័យដ្ផានអ្នកធានាគឺចាំបាច់ត្រូវតែមានតម្លៃជាដាច់ខាត។" },
                { "The Item Name field is required.", "ឈ្មោះវត្ថុគឺចាំបាច់ត្រូវតែមានតម្លៃជាដាច់ខាត។" },
                { "The Id Number field is required.", "​លេខអត្តសញ្ញាណគឺចាំបាច់ត្រូវតែមានតម្លៃជាដាច់ខាត។" },
                { "The Tel field is required.", "លេខទូរស័ព្ទគឺចាំបាច់ត្រូវតែមានតម្លៃជាដាច់ខាត។" },
                { "The Position field is required.", "មុខងារគឺចាំបាច់ត្រូវតែមានតម្លៃជាដាច់ខាត។" },
                { "The Age field is required.", "អាយុគឺចាំបាច់ត្រូវតែមានតម្លៃជាដាច់ខាត។" },

            };
        }
    }
}
