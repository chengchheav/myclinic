using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClinic.Infrastructure
{
     [Table("DayendClose")]
    public class DayendClose
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime ClosedDate { get; set; }
        public DateTime RunDate { get; set; }
        public DateTime RollbackDate { get; set; }
        public int Status { get; set; }
    }

     public class DTODayendClose
     {
         public int ItemCategoryId { get; set; }
         public int TotalItem { get; set; }
         public decimal NewGrant { get; set; }
         public decimal Redeem { get; set; }
         public decimal Renew { get; set; }
         public decimal InterestEarned { get; set; }
         public decimal RedeemSome { get; set; }
         public int ExpireItem { get; set; }
         public decimal SoldOut { get; set; }
         public int SaleableItem { get; set; }
         public string CategoryName { get; set; }
     }
}
