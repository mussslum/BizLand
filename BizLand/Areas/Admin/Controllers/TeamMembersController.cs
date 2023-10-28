using BizLand.Areas.Admin.ViewModels;
using BizLand.DAL;
using BizLand.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BizLand.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamMembersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TeamMembersController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context =context;
            _webHostEnvironment=webHostEnvironment;

        }
        public IActionResult Index()
        {
            List<TeamMember>teamMembers=_context.TeamMembers.Where(t => !t.isDeleted).ToList();
            return View(teamMembers);
        }
        public IActionResult Details(int id )
        {
            TeamMember teamMember = _context.TeamMembers.Find(id);
            return Json(teamMember);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TeamMemberVM teamMemberVM)
        {
            if(!ModelState.IsValid)
            {
                return View(teamMemberVM);
            }
            if (!teamMemberVM.Photo.ContentType.ToLower().Contains("image/"))
            {
                ModelState.AddModelError("Photo", "File type is wrong");
                return View(teamMemberVM);
            }
            //if (!(teamMemberVM.Photo.Length/1024<=900))
            //{
            //    ModelState.AddModelError("Photo", "File size must be less than 200");
            //    return View(teamMemberVM);
            //}
            string rootPath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", "team");
            string fileName = Guid.NewGuid().ToString()+teamMemberVM.Photo.FileName;
            using (FileStream fileStream = new FileStream(Path.Combine(rootPath,fileName),FileMode.Create))
            {
                teamMemberVM.Photo.CopyTo(fileStream);
            }
            TeamMember teamMember = new TeamMember()
            {
                Name= teamMemberVM.Name,
                Position=teamMemberVM.Position,
                ImagePath=fileName
            };
            _context.Add(teamMember);
            _context.SaveChanges();
            return RedirectToAction("Index", "TeamMembers");
        }
        public IActionResult Delete(int id)
        {
            TeamMember oldMember = _context.TeamMembers.Find(id);
            if(oldMember == null)
            {
                return Json("");
            }
            string ImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", "team",oldMember.ImagePath);
            if (System.IO.File.Exists(ImagePath))
            {
                System.IO.File.Delete(ImagePath);
            }
            _context.Remove(oldMember);
            _context.SaveChanges();
            return RedirectToAction("index");

        }
        public IActionResult Update(int id )
        {
            
            TeamMember updateAbleMember = _context.TeamMembers.Find(id);
            if (updateAbleMember == null)
            {
                return Json("This member not found in server");
            }
            UpdateTeamMemberVM updateTeamMemberVM = new UpdateTeamMemberVM()
            {
                Id = id,
                Name = updateAbleMember.Name,
                Position = updateAbleMember.Position,
                imgUrl=updateAbleMember.ImagePath

            };
            return  View(updateTeamMemberVM);
        }
        [HttpPost]
        public IActionResult Update(UpdateTeamMemberVM updateTeamMemberVM, int id)
        {

            TeamMember updateAbleMember = _context.TeamMembers.Find(id);
            if (updateAbleMember == null)
            {
                return Json("This member not found in server");
            }
            string rootPath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", "team");
            string oldFileName = updateAbleMember.ImagePath;
            string newFileName =updateTeamMemberVM.Photo.FileName;
            if (System.IO.File.Exists(Path.Combine(rootPath, oldFileName)))
            {
                System.IO.File.Delete(Path.Combine(rootPath, oldFileName));
            }
            using (FileStream fileStream = new FileStream(Path.Combine(rootPath, newFileName), FileMode.Create))
            {
                updateTeamMemberVM.Photo.CopyTo(fileStream);
            }
            updateAbleMember.Name= updateTeamMemberVM.Name;
            updateAbleMember.Position= updateTeamMemberVM.Position;
            updateAbleMember.ImagePath= newFileName;
            _context.Update(updateAbleMember);
            _context.SaveChanges();
            return RedirectToAction("index", "TeamMembers");

        }

    }
}
