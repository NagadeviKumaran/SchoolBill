namespace SchoolBill.DTO
{
    public class PostDto
    {
        public BillMasterDTO BillMaster { get; set; }
        public List<BillDetailDto> BillDetails { get; set; } = new List<BillDetailDto>();
        public List<ClassEntityDto> Classes { get; set; } = new List<ClassEntityDto>();
        public List<SectionEntityDto> Sections { get; set; } = new List<SectionEntityDto>();
        public List<SchoolEntityDto> Schools { get; set; } = new List<SchoolEntityDto>();
        public List<StudentEntityDto> Students { get; set; } = new List<StudentEntityDto>();

    }
}
