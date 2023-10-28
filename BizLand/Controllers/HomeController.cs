using BizLand.DAL;
using BizLand.Models;
using Microsoft.AspNetCore.Mvc;

namespace BizLand.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController( AppDbContext context)
        {
            _context=context;
        }
        public IActionResult Index()
        {
            List<TeamMember>teamMembers=_context.TeamMembers.Where(t => !t.isDeleted).OrderByDescending(t =>t.Id).Take(4).ToList();
            return View(teamMembers);
        }
    }
}
