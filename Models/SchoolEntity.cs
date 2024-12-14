using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolBill.Models
{
    public class SchoolEntity
    { 
        [Key]
        public int SchoolId { get; set; } // Primary Key
        public string SchoolName { get; set; } = null!;
        public string SchoolAddress { get; set; }
        public string SchoolEmail { get; set; }
        public string Phoneno { get; set; }
        public int Billid { get; set; }

    }




}


