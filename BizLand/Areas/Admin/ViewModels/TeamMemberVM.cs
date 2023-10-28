
using System.ComponentModel.DataAnnotations;

namespace BizLand.Areas.Admin.ViewModels
{
    public class TeamMemberVM
    {
        [Required,MaxLength(20)]
        public string Name { get; set; }
        [Required,MaxLength(20)]
        public string Position { get; set; }
        [Required]
        public IFormFile Photo { get; set; }

    }
}
