using System.ComponentModel.DataAnnotations;

namespace BizLand.Areas.Admin.ViewModels
{
    public class UpdateTeamMemberVM
    {
        [Required]
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string Name { get; set; }
        [Required, MaxLength(20)]
        public string Position { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
        public string imgUrl{ get; set; }
    }
}
