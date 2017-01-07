using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyClinic.Infrastructure;

namespace MyClinic.Core
{
    public interface IMedicineTypeRepository
    {
        IEnumerable<MedicineType> Get();
        string GetMedicineTypeSelectBox();
        string GetUsageSelectBox();

        IEnumerable<Usage> GetUsage();
    }
}
