using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SchoolBill.Models
{
    public class StudentEntity
    {

       
            [Key]
            public int Studentid { get; set; } // Primary Key
            public string Studentname { get; set; } = null!;
            public int Classid { get; set; }
            public int Sectionid { get; set; }
            public string ParentName { get; set; }
            public string StuContactno { get; set; }
            public int Billid { get; set; }

               




    }
}
