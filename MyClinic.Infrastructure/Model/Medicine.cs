using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClinic.Infrastructure
{
    [Table("Medicine")]
    public class Medicine
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(150)]
        [Required]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [Required]
        public int MedicineTypeId { get; set; }

        [Required]
        public int UnitId { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public int CreatedBy { get; set; }

        public int Status { get; set; }
    }


    [Table("vMedicine")]
    public class DTOMedicine
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(150)]
        [Required]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public int Status { get; set; }

        public string U_Name { get; set; }

        public string MedicineType { get; set; }

        public string Unit { get; set; }
    }

 
    public class AutoMedicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MedicineType { get; set; }
    }
}
