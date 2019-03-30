using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLTeacher.Models;

namespace SQLTeacher.Controllers
{
    public class StartController : Controller
    {
        private readonly SQLTeacherContext _context;
        // Static var for application
        public static People _currentUser;

        public StartController(SQLTeacherContext context)
        {
            _context = context;
        }

        public IActionResult Index(SQLTeacherContext context)
        {

            setStaticVariables();

            // Check roles and open the right page
            if (_currentUser == null)
            {
                return RedirectToAction("Exam", "Queries");
            }
            else if (_currentUser.Role.Name == "teacher")
            {
                return RedirectToAction("Index", "Exercises");
            }
            else if (_currentUser.Role.Name == "student")
            {
                return RedirectToAction("Exam", "Queries");
            }
            else
            {
                return RedirectToAction("Exam", "Queries");
            }

        }

        public void setStaticVariables()
        {
            // Get current user
            _currentUser = _context.People.Include(people => people.Role).FirstOrDefault(p => p.PinCode == config.pinCode);
        }
    }
}