using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MyClinic.Infrastructure;
using System.Data.SqlClient;
using System.Data.Entity;

namespace MyClinic.Core
{
    public class SettingRepository :ISettingRepository
    {
        MyDbContext db;
        public SettingRepository()
        {
            this.db = new MyDbContext();
        }
        
        public void Add(Setting obj)
        {
            try
            {
                db.Setting.Add(obj);
                db.SaveChanges();
            }
            catch (Exception exp)
            {
                
                throw exp;
            }
        }

        public void Update(Setting obj)
        {
            try
            {
                db.Setting.Attach(obj);
                var entry = db.Entry(obj);
                entry.State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public string GetValue(int Id)
        {
            string strReturn = "";
            try
            {
                var records = from o in db.Setting where o.Id == Id select o;
                foreach (var record in records)
                {
                    strReturn = record.KeyValue;
                    break;
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return strReturn;
        }

        public IEnumerable<Setting> Get()
        {
            IEnumerable<Setting> pawners = null;
            try
            {
                pawners = db.Setting.OrderByDescending(x => x.Id).ToList(); 
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return pawners;
        }

        public Setting GetById(int Id)
        {
            Setting strReturn = null;
            try
            {
                var records = from o in db.Setting where o.Id == Id select o;
                foreach (var record in records)
                {
                    strReturn = record;
                    break;
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return strReturn;
        }


        public IEnumerable<Setting> Search(string searchBy, string keyWord, string orderBy, string order, int pageNo, int pageSize, out int totalRecords)
        {
            IEnumerable<Setting> settings = null;
            try
            {
                settings = (from u in db.Setting select u).AsEnumerable();

                if (keyWord.Trim().Length > 0)
                {
                    switch (searchBy)
                    {
                        case "KeyValue":
                            settings = settings.Where(p => p.KeyValue.ToLower().Contains(keyWord.ToLower()));
                            break;
                        
                        case "Id":
                            int intId = 0;
                            Int32.TryParse(keyWord, out intId);
                            settings = settings.Where(p => p.Id.Equals(intId));
                            break;
                        case "KeyDesc":
                            settings = settings.Where(p => p.KeyDes.ToLower().Contains(keyWord.ToLower()));
                            break;
                    }
                }

                totalRecords = settings.AsEnumerable().Count();

                switch (orderBy)
                {
                    
                    case "KeyValue":
                        if (order == "desc")
                            settings = settings.OrderByDescending(p => p.KeyValue);
                        else
                            settings = settings.OrderBy(p => p.KeyValue);
                        break;
                    case "Id":
                        if (order == "desc")
                            settings = settings.OrderByDescending(p => p.Id);
                        else
                            settings = settings.OrderBy(p => p.Id);
                        break;
                    case "KeyDesc":
                        if (order == "desc")
                            settings = settings.OrderByDescending(p => p.KeyDes);
                        else
                            settings = settings.OrderBy(p => p.KeyDes);
                        break;
                }

                var actualPageNo = pageNo - 1;
                settings = settings.Skip(actualPageNo * pageSize).Take(pageSize);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return settings;
        }

        public string Id { get; set; }
    } 
}
