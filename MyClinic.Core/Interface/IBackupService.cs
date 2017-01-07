using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyClinic.Core
{
    public interface IBackupService
    {
        void StartBackup(string backupPath, string mediaName, string name);
        void RemoveOutOfPeriodBackup();
    }
}
