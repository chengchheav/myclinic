using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClinic.Infrastructure
{
    public class EmployeeAdd
    {
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

        [Required(ErrorMessage = "The Position field is required.")]
        public int PositionId { get; set; }

        //public DateTime DateOfBirth { get; set; }

        public string Image { get; set; }

        public string ImageStream { get; set; }

        [StringLength(500)]
        public string Address { get; set; }
    }

    public class EmployeeEdit
    {
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

        [Required(ErrorMessage = "The Position field is required.")]
        public int PositionId { get; set; }

       //public DateTime DateOfBirth { get; set; }

        public string Image { get; set; }

        public string ImageStream { get; set; }

        [StringLength(500)]
        public string Address { get; set; }
    }
}
