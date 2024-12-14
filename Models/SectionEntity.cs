using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolBill.Models
{
    public class SectionEntity
    {
       
            [Key]
            public int Sectionid { get; set; } // Primary Key
            public string Sectionname { get; set; } = null!;
            public int Classid { get; set; } // Reference to ClassEntity
        }

    }





