using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClinic.Infrastructure
{
    [Table("Log")]
    public class Log
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("User Id")]
        public int UserId { get; set; }
        [DisplayName("Process Type")]
        public string ProcessType { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("Log Date")]
        public DateTime LogDate { get; set; }
    }
}
