
using FastReport;
using FastReport.Export.PdfSimple;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolBill.Data;
using SchoolBill.Models;

namespace SchoolBill.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("GenerateReport/{billid}")]
        public IActionResult GenerateReport(int billid)
        {
            // Define the path to the FastReport template file
            var reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "SchoolBill.frx");

            // Check if the report file exists
            if (!System.IO.File.Exists(reportPath))
            {
                return NotFound("Report file not found.");
            }

            // Load the FastReport template
            using var report = new FastReport.Report();
            report.Load(reportPath);

            // Fetch data based on the provided BillId
            var billMaster = _context.BillMasters
                .Include(b => b.Students)
                .Include(b => b.BillDetails)
                
                .FirstOrDefault(b => b.Billid == billid);

            // Check if BillMaster data was found for the given BillId
            if (billMaster == null)
            {
                return NotFound($"No data found for Bill ID: {billid}");
            }

            // Register data sources for FastReport
            report.RegisterData(new[] { billMaster }, "BillMaster");
            report.RegisterData(billMaster.BillDetails, "BillDetails");
           
            report.RegisterData(billMaster.Students, "Students");

            // Set the BillID parameter for the report (if the report template has a parameter)
            report.SetParameterValue("BillID", billid);

            // Prepare the report for export
            using var pdfStream = new MemoryStream();
            report.Prepare();

            // Export the report to a PDF in the memory stream
            var pdfExport = new PDFSimpleExport();
            report.Export(pdfExport, pdfStream);

            pdfStream.Position = 0; // Ensure the stream is at the beginning

            // Return the generated report as a PDF file
            return File(pdfStream.ToArray(), "application/pdf", $"BillReport_{billid}.pdf");
        }
    }
}




