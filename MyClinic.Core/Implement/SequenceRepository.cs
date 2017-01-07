using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyClinic.Infrastructure;
using System.Data;
using Utilities;

namespace MyClinic.Core
{
    public class SequenceRepository : ISequenceRepository
    {
        MyDbContext db;
        public SequenceRepository()
        {
            this.db = new MyDbContext();
        }

        public string GetNormalSeqNumber()
        {            
            string result="";
            try
            {
                var sequence = db.Database.SqlQuery<Sequence>("exec GetGeneralTicketNumber").FirstOrDefault();
                result = sequence.Number;
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return result;
        }

        public string GetSpecialSeqNumber()
        {
            string result = "";
            try
            {
                var sequence = db.Database.SqlQuery<Sequence>("exec GetSpecialTicketNumber").FirstOrDefault();
                result = sequence.Number;
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return result;
        }
    }
}