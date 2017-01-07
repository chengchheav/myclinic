using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace MyClinic.Core
{

    public class BackupDbTimer
    {        
        public int Interval { get; set; }

        Thread thread;
        bool threadRun = true;
        DateTime lastRunDate;

        public BackupDbTimer()
        {
            Interval = 1;
            thread = new Thread(new ThreadStart(Run));            
        }

        public void Start()
        {
            thread.Start();
            lastRunDate = DateTime.Parse(DateTime.Now.ToString("dd-MMM-yyyy") + " 00:00:00");
        }

        public void Stop()
        {
            threadRun = false;
        }

        private void Run()
        {
            while (threadRun)
            {
                var dbName = "MyClinicDB";
                var dbBackupTime = Utilities.Common.arrDbBackupTime;
                foreach (var backupTime in dbBackupTime)
                {
                    DateTime dtBackupTime = DateTime.Parse(DateTime.Now.ToString("dd-MMM-yyyy ") + backupTime);
                    //If now >= backupTime && backupTime > lastRunDate
                    if (DateTime.Now >= dtBackupTime && dtBackupTime > lastRunDate)
                    {
                        lastRunDate = dtBackupTime.AddMinutes(5);
                        var filePath = Utilities.Common.dbBackupPath + @"\" + DateTime.Now.ToString("yyyyMMdd") + "_" + dbName + ".bak";
                        FileInfo fileInfo = new FileInfo(filePath);

                        if (!fileInfo.Directory.Exists)
                            fileInfo.Directory.Create();

                        if (fileInfo.Exists)
                            fileInfo.Delete();

                        IBackupService backupService = new BackupService();
                        backupService.StartBackup(filePath, dbName, "Full backup PawnDB");
                        backupService.RemoveOutOfPeriodBackup();
                    }
                }
                Thread.Sleep(Interval * 1000);
            }
        }
    }
}
