using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyClinic.Infrastructure;
using System.Data;

namespace MyClinic.Core
{
    public class LkpNationRepository : ILkpNationRepository
    {
        MyDbContext db;
        public LkpNationRepository()
        {
            this.db = new MyDbContext();
        }

        public List<LkpNation> Get()
        {
            List<LkpNation> lkpnations = null;
            lkpnations = db.Database.SqlQuery<LkpNation>("select distinct Id,Nation from LkpNation").OrderBy(x => x.Nation).ToList();
            return lkpnations;
        }
    }
}