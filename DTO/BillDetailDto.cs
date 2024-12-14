namespace SchoolBill.DTO
{
    public class BillDetailDto
    {
        public int BillDetailId { get; set; }
        public int Billid { get; set; }
        public int AppNo { get; set; }
        public string Particulars { get; set; }
        public decimal Amount { get; set; }

    }
}
