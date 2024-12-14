using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolBill.Data;
using SchoolBill.DTO;
using SchoolBill.Models;
using System.Diagnostics;


namespace SchoolBill.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly AppDbContext _context;
       


        public DataController(AppDbContext context)
        {
            _context = context;
           
        }
        // GET: api/Data
        [HttpGet]
        public async Task<ActionResult<PostDto>> GetByBillId(int billId)
        {
            // Fetch the BillMaster with related data by BillId
            var billMaster = await _context.BillMasters
                .Include(b => b.BillDetails)
                .Include(b => b.Students)
                .FirstOrDefaultAsync(b => b.Billid == billId);

            // Check if BillMaster is not found
            if (billMaster == null)
            {
                return NotFound($"No data found for Bill ID: {billId}");
            }

            // Fetch related data like Classes, Sections, and Schools (unchanged from GetAllData method)
            var classes = await _context.Classes.ToListAsync();
            var sections = await _context.Sections.ToListAsync();
            var schools = await _context.Schools.ToListAsync();
            var students = await _context.Students.ToListAsync();

            // Prepare and return the result as PostDto
            var result = new PostDto
            {
                BillMaster = new BillMasterDTO
                {
                    Billid = billMaster.Billid,
                    BillDate = billMaster.BillDate,
                    AppNo = billMaster.AppNo,
                },
                BillDetails = billMaster.BillDetails.Select(d => new BillDetailDto
                {
                    BillDetailId = d.Detailid,
                    Billid = d.Billid,
                    
                    Particulars = d.Particulars,
                    Amount = d.Amount
                }).ToList(),
                Classes = classes.Select(c => new ClassEntityDto
                {
                    ClassId = c.Classid,
                    ClassName = c.ClassName
                }).ToList(),
                Sections = sections.Select(s => new SectionEntityDto
                {
                    SectionId = s.Sectionid,
                    SectionName = s.Sectionname
                }).ToList(),
                Schools = schools.Select(s => new SchoolEntityDto
                {
                    SchoolId = s.SchoolId,
                    SchoolName = s.SchoolName,
                    SchoolAddress = s.SchoolAddress,
                    SchoolEmail = s.SchoolEmail,
                    Phoneno = s.Phoneno
                }).ToList(),
                Students = students.Select(s => new StudentEntityDto
                {
                    StudentId = s.Studentid,
                    StudentName = s.Studentname,
                    ClassId = s.Classid,
                    SectionId = s.Sectionid,
                    ParentName = s.ParentName,
                    StuContactNo = s.StuContactno
                }).ToList()
            };

            return Ok(result);
        }


        // POST: api/Data
        [HttpPost]
        public async Task<ActionResult> PostData(PostDto postDto)
        {
            try
            {
                // Create and save BillMaster
                var billMaster = new BillMaster
                {
                    BillDate = postDto.BillMaster.BillDate
                };
                _context.BillMasters.Add(billMaster);
                await _context.SaveChangesAsync();

                var billId = billMaster.Billid;

                // Create and save BillDetails
                foreach (var detail in postDto.BillDetails)
                {
                    var billDetail = new BillDetail
                    {
                        Billid = billId,
                        Particulars = detail.Particulars,
                        Amount = detail.Amount
                    };
                    _context.BillDetails.Add(billDetail);
                }

                // Create and save Classes
                foreach (var classDto in postDto.Classes)
                {
                    var newClassEntity = new ClassEntity
                    {
                        ClassName = classDto.ClassName
                    };
                    _context.Classes.Add(newClassEntity);
                }
                await _context.SaveChangesAsync();

                // Retrieve the saved ClassEntities to get their ClassId
                var savedClasses = await _context.Classes.ToListAsync();

                // Create and save Sections
                foreach (var sectionDto in postDto.Sections)
                {
                    var newSectionEntity = new SectionEntity
                    {
                        Sectionname = sectionDto.SectionName,
                        Classid = sectionDto.ClassId
                    };
                    _context.Sections.Add(newSectionEntity);
                }

                // Create and save Schools
                foreach (var school in postDto.Schools)
                {
                    var schoolEntity = new SchoolEntity
                    {
                        SchoolName = school.SchoolName,
                        SchoolAddress = school.SchoolAddress,
                        SchoolEmail = school.SchoolEmail,
                        Phoneno = school.Phoneno
                    };
                    _context.Schools.Add(schoolEntity);
                }

                // Create and save Students
                foreach (var student in postDto.Students)
                {
                    var studentEntity = new StudentEntity
                    {
                        Studentname = student.StudentName,
                        Classid = student.ClassId,
                        Sectionid = student.SectionId,
                        ParentName = student.ParentName,
                        StuContactno = student.StuContactNo,
                        Billid = billId
                    };
                    _context.Students.Add(studentEntity);
                }

                // Save all changes to the database
                await _context.SaveChangesAsync();

                return Ok(new { message = "Data saved successfully!" });
            }
            catch (Exception ex)
            {
                // Log detailed error information
                Debug.WriteLine($"Error saving data: {ex.Message}");
                Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    Debug.WriteLine($"Inner Exception Stack Trace: {ex.InnerException.StackTrace}");
                }
                return StatusCode(500, $"An error occurred while saving the data: {ex.Message}");
            }
        }

        // PUT: api/Data/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateData(int id, PostDto postDto)
        {
            var billMaster = await _context.BillMasters.FindAsync(id);
            if (billMaster == null)
            {
                return NotFound();
            }

            billMaster.BillDate = postDto.BillMaster.BillDate;
            // Update other properties as necessary

            _context.Entry(billMaster).State = EntityState.Modified;

            foreach (var detail in postDto.BillDetails)
            {
                var billDetail = await _context.BillDetails.FindAsync(detail.BillDetailId);
                if (billDetail != null)
                {
                    billDetail.Particulars = detail.Particulars;
                    billDetail.Amount = detail.Amount;
                    _context.Entry(billDetail).State = EntityState.Modified;
                }
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }
        // DELETE: api/Data/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteData(int id)
        {
            var billMaster = await _context.BillMasters.FindAsync(id);
            if (billMaster == null)
            {
                return NotFound();
            }

            _context.BillMasters.Remove(billMaster);

            var billDetails = await _context.BillDetails.Where(bd => bd.Billid == id).ToListAsync();
            _context.BillDetails.RemoveRange(billDetails);

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}












