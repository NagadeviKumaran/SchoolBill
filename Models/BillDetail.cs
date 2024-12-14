using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolBill.Models
{
    public class BillDetail
    {
       
            [Key]
            public int Detailid { get; set; } // Primary Key
            public int Billid { get; set; } // Reference to BillMaster
            public string Particulars { get; set; }
            public decimal Amount { get; set; }
        



    }
}
