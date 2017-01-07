using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyClinic.Infrastructure;
using System.Data.Common;

namespace MyClinic.Core
{
    public class EntityFrameworkHelper
    {
        public bool TestConnection()
        {
            using (var db = new MyDbContext())
            {
                DbConnection conn = db.Database.Connection;
                try
                {
                    conn.Open();   // check the database connection
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
