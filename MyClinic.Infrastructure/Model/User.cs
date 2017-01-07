using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClinic.Infrastructure
{
    [Table("User")]
    public class User
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [StringLength(50)]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }

        [StringLength(250)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("User Type")]
        public string UserType { get; set; }

        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Created By")]
        public int CreatedBy { get; set; }
        [DisplayName("Modified Date")]
        public DateTime ModifiedDate { get; set; }
        [DisplayName("Modified By")]
        public int ModifiedBy { get; set; }
        public int IsActived { get; set; }
        public string Image { get; set; }
        //public int Status { get; set; }
        public string Tel { get; set; }
    }

    public class SessUser
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int IsActived { get; set; }
        public string UserType { get; set; }
        public string Image { get; set; }
    }
}
