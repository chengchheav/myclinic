using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using MyClinic.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Data.Entity;
using Utilities;

namespace MyClinic.Core
{
    public class BackupService :IBackupService
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        MyDbContext db;
        public BackupService()
        {
            this.db = new MyDbContext();
        }

        public void StartBackup(string backupPath, string mediaName, string name)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@backupPath", backupPath);
                parameters[1] = new SqlParameter("@mediaName", mediaName);
                parameters[2] = new SqlParameter("@name", name);
                db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "exec DbBackup @backupPath,@mediaName,@name;",parameters);
            }
            catch (Exception exp)
            {
                log.Error(exp);
            }
        }        

        public void RemoveOutOfPeriodBackup()
        {
            var startPeriodDate = DateTime.Now.AddDays(-Utilities.Common.dbBackupPeriod);
            DirectoryInfo directoryInfo = new DirectoryInfo(Utilities.Common.dbBackupPath);
            List<string> removeFilePath = new List<string>();
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                if (file.CreationTime < startPeriodDate)
                {
                    removeFilePath.Add(file.FullName);
                }
            }

            foreach (var filePath in removeFilePath)
            {
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Exists)
                    fileInfo.Delete();
            }
        }
    }
}
