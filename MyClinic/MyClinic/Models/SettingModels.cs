using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyClinic.Infrastructure;

namespace MyClinic.Models
{
    public class SettingModels
    {

        public IEnumerable<Setting> settings { get; set; }
        public Setting setting { get; set; }
        public Pager Pager { get; set; }
        public PageUtilities pageUtilities { get; set; }
    }

}