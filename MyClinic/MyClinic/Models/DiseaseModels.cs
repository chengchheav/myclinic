using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyClinic.Infrastructure;

namespace MyClinic.Models
{
    public class DiseaseModels
    {

        public IEnumerable<Disease> diseases { get; set; }
        public Disease disease { get; set; }
        public DTODisease dtoDisease { get; set; }
        public PageUtilities pageUtilities { get; set; }
        public IEnumerable<Position> positions { get; set; }

        public User user { get; set; }
        public Boolean checkPost { get; set; }
        public Boolean checkError { get; set; }
    }
}