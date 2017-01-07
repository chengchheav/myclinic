using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClinic.Infrastructure
{
    [Table("Setting")]
    public class Setting
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Value")]
        public string KeyValue { get; set; }

        [DisplayName("Description")]
        public string KeyDes { get; set; }
    }

    public class SettingEdit
    {
        public int Id { get; set; }

        [DisplayName("Value")]
        public string KeyValue { get; set; }

        [DisplayName("Description")]
        public string KeyDes { get; set; }

        [DisplayName("Key Type")]
        public string KeyType { get; set; }
    }
}
