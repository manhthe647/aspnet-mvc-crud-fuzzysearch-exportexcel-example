using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuanLySinhVien.WebAppMvc.Models;
using QuanLySinhVien.WebAppMvc.ViewModel;

namespace QuanLySinhVien.WebAppMvc.Controllers
{
    public class ProfessorController: BaseController
    {


        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public ProfessorController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }

        public IActionResult Index()
        {
            var sessions = HttpContext.Session.GetString("Token");

             var result = _mapper.ProjectTo<ProfessorVm>(
                _dbContext.Professors).ToList();

            //var professors = _dbContext.Professors.ToList();

            //var professorVms = professors.Select(p => new ProfessorVm
            //{
            //    ProfessorId = p.ProfessorId,
            //    FirstName = p.FirstName,
            //    LastName = p.LastName,
            //    Department = p.Department,
            //}).ToList();

        
            return View(result);
        }


        [HttpPost]
        public IActionResult Create(ProfessorVm vm)
        {
            if (ModelState.IsValid)
            {
                //var professor = new Professor()
                //{
                //    FirstName = vm.FirstName,
                //    LastName = vm.LastName,
                //    Department = vm.Department,
                //};

                //var addprofessor = _dbContext.Professors.Add(professor);
                var professor = _mapper.Map<ProfessorVm, Professor>(vm);
                _dbContext.Professors.Add(professor);
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

                    //professor.FirstName = vm.FirstName;
                    //professor.LastName = vm.LastName;

                    //professor.Department = vm.Department;
                    _mapper.Map<ProfessorVm, Professor>(vm, professor);


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
