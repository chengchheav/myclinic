using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClinic.Infrastructure
{
    [Table("Patient")]
    public class Patient
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        public string Sex { get; set; }

        public int Age { get; set; }

        [StringLength(250)]
        public string Email { get; set; }
        
        public string Image { get; set; }

        public int Status { get; set; }

        [Required]
        public string Tel { get; set; }

        public DateTime Dob { get; set; }
    }

    [Table("vPatient")]
    public class DTOPatient
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        public string Sex { get; set; }

        public int Age { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        public string Image { get; set; }

        public int Status { get; set; }

        [Required]
        public string Tel { get; set; }

        public DateTime Dob { get; set; }

        public string DisplayValue { get; set; }
    }
}
