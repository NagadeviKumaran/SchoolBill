using System.ComponentModel.DataAnnotations;

namespace SchoolBill.Models
{
    public class BillMaster
    {
        
            [Key]
            public int Billid { get; set; } // Primary Key
            public int Studentid { get; set; }
            public int AppNo { get; set; }


        public DateTimeOffset BillDate { get; set; }

            // Navigation properties
            public ICollection<BillDetail> BillDetails { get; set; } = new List<BillDetail>();
            public ICollection<StudentEntity> Students { get; set; } = new List<StudentEntity>();
         


    }



}

