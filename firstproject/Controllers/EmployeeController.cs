using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using firstproject.Models;
using firstproject.Models.Repositories;

namespace firstproject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeeController(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public ActionResult Index()
        {
            var employees = _employeeRepository.GetAll();
            ViewData["EmployeesCount"] = employees.Count();
            ViewData["SalaryAverage"] = _employeeRepository.SalaryAverage();
            ViewData["MaxSalary"] = _employeeRepository.MaxSalary();
            ViewData["HREmployeesCount"] = _employeeRepository.HrEmployeesCount();
            return View(employees);
        }
        public ActionResult Search(string term)
        {
            var result = _employeeRepository.Search(term);
            return View("Index", result);
        }
        public ActionResult Details(int id)
        {
            var employee = _employeeRepository.FindByID(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.Add(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public ActionResult Edit(int id)
        {
            var employee = _employeeRepository.FindByID(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.Update(id, employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public ActionResult Delete(int id)
        {
            var employee = _employeeRepository.FindByID(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _employeeRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
