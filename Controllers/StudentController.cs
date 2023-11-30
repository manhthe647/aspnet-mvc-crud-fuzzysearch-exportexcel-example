using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.WebAppMvc.Models;
using QuanLySinhVien.WebAppMvc.ViewModel;
using System.Collections.Generic;

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

        public IActionResult Index()
        {
            var sessions = HttpContext.Session.GetString("Token");
            // Lấy danh sách giảng viên từ cơ sở dữ liệu
            var professors = _dbContext.Professors.ToList();

            // Gán danh sách giảng viên vào ViewBag
            ViewBag.Professors = professors;

            var query = from s in _dbContext.Students
                        join p in _dbContext.Professors on s.AdvisorId equals p.ProfessorId
                        select new StudentProfessorVm
                        {
                            Students = s,
                            ProfessorName = p.FirstName +" "+ p.LastName,

                        };

            var data = query.ToList();
            return View(data);
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

    

    }
}