using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClinic.Infrastructure
{
    [Table("Employee")]
    public class Employee
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        public string Sex { get; set; }

        public DateTime Dob { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public int Status { get; set; }

        [Required]
        public string Tel { get; set; }
        

        public string Skill { get; set; }

        [Required(ErrorMessage="The Position field is required.")]
        public int PositionId { get; set; }

        public string Image { get; set; }

        [StringLength(500)]
        public string Address { get; set; }
    }

    [Table("vEmployee")]
    public class DTOEmployee
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        public string Sex { get; set; }

        public DateTime Dob { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public int Status { get; set; }

        [Required]
        public string Tel { get; set; }


        public string Skill { get; set; }

        public int PositionId { get; set; }

        public string PositionName { get; set; }

        public string Image { get; set; }

        [StringLength(500)]
        public string Address { get; set; }
    }
}
