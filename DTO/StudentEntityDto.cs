using SchoolBill.Models;
using static System.Collections.Specialized.BitVector32;

namespace SchoolBill.DTO
{
    public class StudentEntityDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int SchoolId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public string ParentName { get; set; }
        public string StuContactNo { get; set; }
        public int Billid { get; set; }


      
    }
}
