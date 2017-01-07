using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClinic.Infrastructure
{
    public class vDiseaseOperation
    {
        [Key]
        public int DiseaseId { get; set; }
        public string DiseaseName { get; set; }
        public int Qty { get; set; }
    }
}
