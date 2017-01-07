using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Utilities;

namespace MyClinic.Infrastructure
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
            : base(Common.GetConnectionString())
        { }

        public DbSet<Setting> Setting { get; set; }
        public DbSet<DTODisease> DTODisease { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<DTOPatient> DTOPatient { get; set; }
        public DbSet<Diagnosis> Diagnosis { get; set; }
        public DbSet<DTODiagnosis> DTODiagnosis { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<DTOEmployee> DTOEmployee { get; set; }
        public DbSet<Medicine> Medicine { get; set; }
        public DbSet<DTOMedicine> DTOMedicine { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<LkpNation> LkpNation { get; set; }
        public DbSet<UserType> UserType { get; set; }
        public DbSet<Sequence> Sequence { get; set; }   
        public DbSet<DTOAuditLog> DTOAuditLog { get; set; }
        public DbSet<DTOProcessType> DTOProcessType { get; set; }
        public DbSet<DayendClose> DayendClose { get; set; }

        public DbSet<MedicineUnit> MedicineUnit { get; set; }
        public DbSet<MedicineType> MedicineType { get; set; }
        public DbSet<Usage> Usage { get; set; }

        public DbSet<SymptomType> SymptomType { get; set; }
        public DbSet<Disease> Disease { get; set; }

        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<vMedicineDiagnosis> vMedicineDiagnosis { get; set; }


        public DbSet<vDailyOperation> vDailyOperation { get; set; }
        public DbSet<vDoctorOperation> vDoctorOperation { get; set; }
        public DbSet<vPatientOperation> vPatientOperation { get; set; }
        public DbSet<vDiseaseOperation> vDiseaseOperation { get; set; }
        public DbSet<vMonthlyOperation> vMonthlyOperation { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
