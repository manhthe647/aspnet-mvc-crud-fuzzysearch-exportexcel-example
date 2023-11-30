using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuanLySinhVien.WebAppMvc.Models;
using QuanLySinhVien.WebAppMvc.ViewModel;

namespace QuanLySinhVien.WebAppMvc.Controllers
{
    public class CourseController: Controller
    {

        private readonly AppDbContext _dbContext;

        private readonly IMapper _mapper;
        public CourseController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
             _mapper = mapper;
        }

        public IActionResult Index()
        {

            var professors = _dbContext.Professors.ToList();

            ViewBag.ProfessorsC = professors;

            var sessions = HttpContext.Session.GetString("Token");
            var query = from c in _dbContext.Courses
                        join p in _dbContext.Professors on c.ProfessorId equals p.ProfessorId
                        select new CourseProfessorVm
                        {
                            Courses = c,
                            ProfessorName = p.FirstName + " " + p.LastName,
                        };

            var data = query.ToList();
            return View(data);
        }



        [HttpPost]
        public IActionResult Create(CourseVm vm)
        {
            if (ModelState.IsValid)
            {
                //var course = new Course()
                //{
                //    CourseName = vm.CourseName,
                //    Credits = vm.Credits,
                //    ProfessorId = vm.ProfessorId,
                   
                //};

               var course= _mapper.Map<CourseVm, Course>(vm);
                _dbContext.Courses.Add(course);
                _dbContext.SaveChanges();

            }
            else
            {
                ModelState.AddModelError("", "Có lỗi xảy ra");
            }
            return RedirectToAction("Index", "course");
        }


        [HttpPost]
        public IActionResult Edit(CourseVm vm)
        {
            if (ModelState.IsValid)
            {

                var course = _dbContext.Courses.SingleOrDefault(s => s.CourseId == vm.CourseId);
                if (course != null)
                {
                    _mapper.Map<CourseVm, Course>(vm, course);

                    //course.CourseName = vm.CourseName;
                    //course.Credits = vm.Credits;
                    //course.ProfessorId = vm.ProfessorId;


                }
                _dbContext.Update(course);
                _dbContext.SaveChanges();


            }
            else
            {
                ModelState.AddModelError("", "Có lỗi xảy ra");
            }
            return RedirectToAction("Index", "course");
        }

        [HttpPost]
        public IActionResult Delete(int courseId)
        {
            if (ModelState.IsValid)
            {
                var course = _dbContext.Courses.SingleOrDefault(s => s.CourseId == courseId);

                if (course != null)
                {
                    _dbContext.Courses.Remove(course);
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
            return RedirectToAction("Index", "course");
        }


    }
}
