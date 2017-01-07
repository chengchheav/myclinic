using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClinic.Infrastructure
{
    [Table("vLog")]
    public class DTOAuditLog
    {
        public int Id { get; set; }

        [DisplayName("User Id")]
        public int UserId { get; set; }

        [DisplayName("Process Type")]
        public string ProcessType { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Log Date")]
        public DateTime LogDate { get; set; }

        [DisplayName("Name")]
        public string U_Name { get; set; }

        [DisplayName("Email")]
        public string U_Email { get; set; }

        [DisplayName("Image")]
        public string U_Image { get; set; }
    }

    [Table("vProcessType")]
    public class DTOProcessType
    {
        [Key]
        [DisplayName("Process Type")]
        public string ProcessType { get; set; }
    }
}
