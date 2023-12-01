using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.WebAppMvc.Models;
using QuanLySinhVien.WebAppMvc.ViewModel;
using FuzzySharp;
using System.Collections.Generic;
using System.Data;
using OfficeOpenXml;

namespace QuanLySinhVien.WebAppMvc.Controllers
{
    public class StudentController: BaseController
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;


        public StudentController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IActionResult Index(int? page, int? pageSize = 5, string searchQuery = "")
        {
            var sessions = HttpContext.Session.GetString("Token");

            var data = GetStudents();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                data = Search(searchQuery, data);
            }


            // phân trang
            int totalItem = data.Count();
            int totalPage = (int)Math.Ceiling((double)totalItem / (double)pageSize);
            int currentPage = page ?? 1;
            var pagedData = data.Skip((currentPage - 1) * pageSize.Value).Take(pageSize.Value);

            ViewBag.PageSize = pageSize.Value;
            ViewBag.TotalItems = totalItem;
            ViewBag.TotalPages = totalPage;
            ViewBag.CurrentPage = currentPage;

            // Lấy danh sách giảng viên từ cơ sở dữ liệu
            var professors = _dbContext.Professors.ToList();
            ViewBag.Professors = professors;

            ViewBag.SearchQuery = searchQuery;

            return View(pagedData);
        }

        private List<StudentProfessorVm> GetStudents()
        {
            var query = from s in _dbContext.Students
                        join p in _dbContext.Professors on s.AdvisorId equals p.ProfessorId
                        select new StudentProfessorVm
                        {
                            Students = s,
                            ProfessorName = p.FirstName + " " + p.LastName,
                        };

            var data = query.ToList(); // Thực thi truy vấn và lấy danh sách dữ liệu

         
            return data;
        }

        private List<StudentProfessorVm> Search(string searchQuery, List<StudentProfessorVm> vmList)
        {
                // Thực hiện tìm kiếm chính xác và lọc dữ liệu

                //var filteredData = data.Where(x => $"{x.Students.FirstName} {x.Students.LastName} {x.ProfessorName}".Contains(searchQuery)).ToList();
                //return filteredData;

                // Thực hiện tìm kiếm gần đúng và lọc dữ liệu
                var filteredData = vmList.Where(x =>
             Fuzz.PartialRatio($"{x.Students.FirstName} {x.Students.LastName} {x.ProfessorName}", searchQuery) >= 60)
             .ToList();

                return filteredData;
        }

        [HttpPost]
        public IActionResult Create(StudentVm vm)
        {
            if (ModelState.IsValid)
            {
                var student = _mapper.Map<StudentVm, Student>(vm);
                _dbContext.Students.Add(student);
                _dbContext.SaveChanges();


            }
            else
            {
                ModelState.AddModelError("", "Có lỗi xảy ra");
            }
            return RedirectToAction("Index", "Student");
        }


        [HttpPost]
        public IActionResult Edit(StudentVm vm)
        {
            if (ModelState.IsValid)
            {
         
                var student = _dbContext.Students.SingleOrDefault(s => s.StudentId == vm.StudentId);
                if(student != null) { 

                _mapper.Map<StudentVm, Student>(vm, student);

                }


                _dbContext.Update(student);
                _dbContext.SaveChanges();
           

            }
            else
            {
                ModelState.AddModelError("", "Có lỗi xảy ra");
            }
            return RedirectToAction("Index", "Student");
        }

        [HttpPost]
        public IActionResult Delete(int studentId)
        {
            if(ModelState.IsValid)
            {
                var student = _dbContext.Students.SingleOrDefault(s => s.StudentId == studentId);

                if(student != null)
                {
                    _dbContext.Students.Remove(student);
                    _dbContext.SaveChanges();
                    
                }
                else
                {
                    ModelState.AddModelError("", "Không tìm thấy sinh viên có ID tương ứng");
                }
            }
            else
            {
                ModelState.AddModelError("", "Có lỗi xảy ra");
            }
            return RedirectToAction("Index", "Student"); 
        }

        public IActionResult ExportExcel()
        {
            var data = GetStudents();
            var students = data.Select(sp => new { 
                sp.Students.StudentId,
                sp.Students.FirstName,
                sp.Students.LastName,
                sp.Students.Dob,
                sp.Students.GPA,

                sp.ProfessorName })
                .ToList();
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add("Student");
                FormatDateOfBirthColumn(sheet, 4);
                sheet.Cells.LoadFromCollection(students, true);
                package.Save();
            }
            stream.Position = 0;
            var fileName = $"Student_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        private void FormatDateOfBirthColumn(ExcelWorksheet worksheet, int columnIndex)
        {
            var column = worksheet.Column(columnIndex);
            column.Style.Numberformat.Format = "dd/MM/yyyy"; // Định dạng ngày tháng mong muốn, ví dụ: "yyyy-mm-dd"
            column.Width = 12;
        }



    }
}