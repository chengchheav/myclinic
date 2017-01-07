using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyClinic.Infrastructure;

namespace MyClinic.Core
{
    public interface ISettingRepository
    {
        void Add(Setting pawner);
        void Update(Setting pawner);
        IEnumerable<Setting> Get();
        string GetValue(int Id);
        Setting GetById(int Id);
        IEnumerable<Setting> Search(string searchBy, string keyWord, string orderBy, string order, int _pageNo, int _pageSize, out int totalRecords);
    }
}
