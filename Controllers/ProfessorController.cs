using Microsoft.AspNetCore.Mvc;
using QuanLySinhVien.WebAppMvc.Models;
using QuanLySinhVien.WebAppMvc.ViewModel;

namespace QuanLySinhVien.WebAppMvc.Controllers
{
    public class ProfessorController: BaseController
    {


        private readonly AppDbContext _dbContext;
        public ProfessorController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var sessions = HttpContext.Session.GetString("Token");
            var professors = _dbContext.Professors.ToList();

            var professorVms = professors.Select(p => new ProfessorVm
            {
                ProfessorId = p.ProfessorId,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Department = p.Department,
            }).ToList();

            return View(professorVms);
        }


        [HttpPost]
        public IActionResult Create(ProfessorVm vm)
        {
            if (ModelState.IsValid)
            {
                var professor = new Professor()
                {
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    Department = vm.Department,
                };

                var addprofessor = _dbContext.Professors.Add(professor);
                _dbContext.SaveChanges();

            }
            else
            {
                ModelState.AddModelError("", "Có lỗi xảy ra");
            }
            return RedirectToAction("Index", "professor");
        }


        [HttpPost]
        public IActionResult Edit(ProfessorVm vm)
        {
            if (ModelState.IsValid)
            {

                var professor = _dbContext.Professors.SingleOrDefault(s => s.ProfessorId == vm.ProfessorId);
                if (professor != null)
                {

                    professor.FirstName = vm.FirstName;
                    professor.LastName = vm.LastName;
                    
                    professor.Department = vm.Department;


                }
                _dbContext.Update(professor);
                _dbContext.SaveChanges();


            }
            else
            {
                ModelState.AddModelError("", "Có lỗi xảy ra");
            }
            return RedirectToAction("Index", "professor");
        }

        [HttpPost]
        public IActionResult Delete(int professorId)
        {
            if (ModelState.IsValid)
            {
                var professor = _dbContext.Professors.SingleOrDefault(s => s.ProfessorId == professorId);

                
                if (professor != null)
                {
                    _dbContext.Professors.Remove(professor);
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
            return RedirectToAction("Index", "Professor");
        }


    }
}
