using System.ComponentModel.DataAnnotations;

namespace SchoolBill.Models
{
    public class ClassEntity
    {
       
            [Key]
            public int Classid { get; set; } // Primary Key
            public string ClassName { get; set; } = null!;
        public ICollection<SectionEntity> Students { get; set; } = new List<SectionEntity>();


    }
}
